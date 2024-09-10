using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus
{
    Inactive,
    Active,
    Completed
}

public class Quest
{
    public string questName;

    // Fur types that need to be cut (0 head, 1 ears, 2 body, 3 legs, 4 tail)
    public int[] goalCutFur;
    public QuestStatus status;

    public GameObject star;

}

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    public GameObject[] starts;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the quests
        quests = new Quest[3];

        // Quest 1 - Firulais
        quests[0] = new Quest();
        quests[0].questName = "Firulais";
        // Only clean the body, no fur cutting required
        quests[0].goalCutFur = new int[]{0, 0, 0, 0, 0};
        quests[0].status = QuestStatus.Inactive;
        quests[0].star = starts[0];

        // Quest 2 - Rex
        quests[1] = new Quest();
        quests[1].questName = "Rex";
        // Cut all the fur types
        quests[1].goalCutFur = new int[]{100, 100, 100, 100, 100};
        quests[1].status = QuestStatus.Inactive;
        quests[1].star = starts[1];

        // Quest 3 - Fifi
        quests[2] = new Quest();
        quests[2].questName = "Fifi";
        // Only cut the body fur
        quests[2].goalCutFur = new int[]{0, 0, 100, 0, 0};
        quests[2].status = QuestStatus.Inactive;
        quests[2].star = starts[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool[] QuestProgress(GameObject dog)
    {
        Quest quest = GetQuest(dog);
        bool isDirty = true;
        bool isWet = true;
        bool isSoap = true;
        bool isCut = true;

        FurStateController furStateController = dog.GetComponent<FurStateController>();
        int[] furTypes = furStateController.countFurTypes; // 0 is clean dry, 1 is clean wet, 2 is soapy, 3 is dirty dry, 4 is dirty wet
        int[] totalFurCount = furStateController.totalFurCount;
        int[] cutFurCount = furStateController.cutFurCount; // 0 is head, 1 is ears, 2 is body, 3 is legs, 4 is tail

        // Check if the dog is dirty
        isDirty = (furTypes[3] + furTypes[4]) > 0;
        // Check if the dog is wet
        isWet = (furTypes[1] + furTypes[4]) > 0;
        // Check if the dog is soapy
        isSoap = furTypes[2] > 0;
        
        // Check if the dog is cut
        for (int i = 0; i < cutFurCount.Length; i++)
        {
            int percentage = (cutFurCount[i] * 100) / totalFurCount[i];
            if (percentage < quest.goalCutFur[i])
            {
                isCut = false;
                break;
            }
        }
        if (isCut && !isDirty && !isWet && !isSoap)
        {
            quest.status = QuestStatus.Completed;
            quest.star.SetActive(true);
        }
        return new bool[]{isDirty, isWet, isSoap, isCut};
    }

    public Quest GetQuest(GameObject dog)
    {
        if (dog.name == "Dog 1")
        {
            return quests[0];
        }
        else if (dog.name == "Dog 2")
        {
            return quests[1];
        }
        else if (dog.name == "Dog 3")
        {
            return quests[2];
        }
        return null;
    }
    public void StartQuest(GameObject dog)
    {
        Quest quest = GetQuest(dog);
        quest.status = QuestStatus.Active;
    }

}
