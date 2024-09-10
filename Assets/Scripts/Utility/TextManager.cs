using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public QuestManager questManager;
    public DogCommandManager dogCommandManager;
    public bool[] questProgress = new bool[4];
    public Text text;
    public Text dogName;

    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dogCommandManager.currentDog==null || dogCommandManager.currentDog != gameObject)
        {
            return;
        }
        GetQuestProgress();
        Quest quest= questManager.GetQuest(gameObject);
        dogName.text = quest.questName+ " Task Progress:";
        
        if (!questProgress[0] && !questProgress[1] && !questProgress[2] && questProgress[3])
        {
            text.text = "All ready! Choose another dog";
            return;
        }
        // First line: Dirty or clean
        if (questProgress[0] && !questProgress[1]) //Dirty and dry
        {
            text.text = "The dog needs washing. Go to the bathtub and start cleaning";
            return;
        }
        if (questProgress[0] && questProgress[1])// Dirty, wet and not soapy
        {
            text.text = "The dog is dirty, use the soap to clean the dog";
            return;
        }
        if (!questProgress[0] && questProgress[1] && questProgress[2])// Clean, wet and soapy
        {
            text.text = "The dog is soapy, use the water to rinse the dog";
            return;
        }
        if (!questProgress[0] && questProgress[1] && !questProgress[2]) // Clean, wet and not soapy
        {
            text.text = "The dog is wet, use the blowdryer to dry the dog";
            return;
        }
        if (!questProgress[0] && !questProgress[1] && !questProgress[2] && !questProgress[3]) // Clean, dry, not soapy and uncut
        {
            text.text = "Still needs grooming. Use the clippers to cut the fur";
            return;
        }
        
    }

    private void GetQuestProgress()
    {
        questProgress=questManager.QuestProgress(gameObject);
    }
}
