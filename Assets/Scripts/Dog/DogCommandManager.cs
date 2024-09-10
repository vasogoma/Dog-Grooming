using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class DogCommandManager : MonoBehaviour
{
    public GameObject[] dogs; // Array to hold the dogs
    public Transform[] waypoints; // Array to hold the waypoints

    private int currentWaypointIndex = 0;
    public Transform waitingAreaWaypoint;
    public Transform bathtubWaypoint;
    public Transform tableWaypoint;

    private DogInputActions dogInputActions;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator animator;

    public GameObject currentDog;

    private int[] currentRoute=new int[]{};

    public string currentPosition = "waitingArea";
    private string targetPosition = "waitingArea";
    public bool canChangeDog = true;

    public QuestManager questManager;

    void Start()
    {
        dogInputActions = new DogInputActions();

        // Enable input actions
        dogInputActions.Enable();

    }

    public void changeDog(GameObject dog)
    {
        canChangeDog = false;
        currentDog = dog;
        navMeshAgent = currentDog.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = currentDog.GetComponent<Animator>();
        questManager.StartQuest(currentDog);
    }

    private void OnDisable()
    {
        // Disable input actions
        dogInputActions.Disable();
    }
    private int GetCurrentIndex()
    {
        return currentRoute[currentWaypointIndex]-1;
    }
    private void GoToPosition(Vector3 position)
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            // Trigger the walking animation
            animator.SetBool("isWalking", true);
            // Set the destination for the NavMeshAgent
            navMeshAgent.SetDestination(position);
        }
    }

    public void GoToBathtub()
    {
        UnfreezeDog();
        if (currentPosition == "waitingArea")
        {
            currentRoute = new int[] {1,2,3,4,7};
        }
        else if(currentPosition == "table")
        {
            currentRoute = new int[] {9,8,4,7};
        } else 
        {
            currentRoute = new int[] {4,7};
        }
        currentPosition = "walking";
        targetPosition = "bathtub";
        currentWaypointIndex = 0;
        GoToPosition(waypoints[GetCurrentIndex()].position);
    }

    public void GoToTable()
    {
        UnfreezeDog();
        if (currentPosition == "waitingArea")
        {
            currentRoute = new int[] {1,2,3,4,8,9};
        }
        else if(currentPosition == "bathtub")
        {
            currentRoute = new int[] {7,4,8,9};
        } else {
            currentRoute = new int[] {4,8,9};
        }
        currentPosition = "walking";
        targetPosition = "table";
        currentWaypointIndex = 0;
        GoToPosition(waypoints[GetCurrentIndex()].position);
    }


    public void GoToWaitingArea(){
        UnfreezeDog();
        if (currentPosition == "bathtub")
        {
            currentRoute = new int[] {7,4,3,2,1};
        }
        else if(currentPosition == "table")
        {
            currentRoute = new int[] {9,8,4,3,2,1};
        } else {
            currentRoute = new int[] {4,3,2,1};
        }
        currentPosition = "walking";
        targetPosition = "waitingArea";
        currentWaypointIndex = 0;
        GoToPosition(waypoints[GetCurrentIndex()].position);
    }

    private void FreezeDog()
    {
        // Stop the walking animation (start idle animation)
        animator.SetBool("isWalking", false);

        // Stop the NavMeshAgent from moving
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.angularSpeed = 0;
        navMeshAgent.acceleration = 0;
        navMeshAgent.isStopped = true;
        Rigidbody rb = currentDog.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.zero);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
    }

    private void UnfreezeDog()
    {
        navMeshAgent.isStopped = false;
        Rigidbody rb = currentDog.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        navMeshAgent.angularSpeed = 10;
        navMeshAgent.acceleration = 3;  
    }

    public void rotateLeft(GameObject dog)
    {
        if(dog == null)
        {
            return;
        }
        dog.transform.Rotate(0, -1, 0);
    }

    public void rotateRight(GameObject dog)
    {
        if(dog == null)
        {
            return;
        }
        dog.transform.Rotate(0, 1, 0);
    }



    private void OnWaypointReached()
    {
        currentWaypointIndex++;
        //Debug.Log("Waypoint reached");
        if (currentWaypointIndex >= currentRoute.Length){
            currentPosition=targetPosition;
            //Debug.Log("Reached destination");
            if (targetPosition == "waitingArea")
            {
                canChangeDog = true;
            }
            currentWaypointIndex = 0;

            // Stop the walking animation
            animator.SetBool("isWalking", false);

            FreezeDog();
            return;
        }
        GoToPosition(waypoints[GetCurrentIndex()].position);
    }

    void Update()
    {
        if (currentDog != null)
        {
            questManager.QuestProgress(currentDog);
        }
        if (currentRoute.Length == 0 || currentWaypointIndex >= currentRoute.Length || currentPosition == targetPosition)
            return;

        // Calculate direction to the destination
        Vector3 destinationDirection = (navMeshAgent.destination - currentDog.transform.position).normalized;

        // Ensure there is some direction to look towards
        if (destinationDirection != Vector3.zero)
        {
            // Calculate the rotation that the dog should have to look towards the destination
            Quaternion targetRotation = Quaternion.LookRotation(destinationDirection, Vector3.up);

            // Smoothly rotate towards the target rotation
            currentDog.transform.rotation = Quaternion.Slerp(currentDog.transform.rotation, targetRotation, Time.deltaTime * navMeshAgent.angularSpeed);
        }

        // Check if the dog is close enough to the current waypoint
        if (Vector3.Distance(currentDog.transform.position, waypoints[GetCurrentIndex()].position) < 0.5f)
        {
            OnWaypointReached();
        }

    }
    


}