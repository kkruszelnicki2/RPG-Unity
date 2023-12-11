using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering;

public class QuestLog : MonoBehaviour
{
    Quest quest;

    [SerializeField] GameObject questName;
    [SerializeField] GameObject questObjective;
    [SerializeField] GameObject questProgress;

    [SerializeField] GameObject itemObject;

    private bool duringQuest = false;

    public void AddQuest(Quest newQuest)
    {
        if(!duringQuest)
        {
            duringQuest = true;

            this.quest = newQuest;

            questName.GetComponent<TextMeshProUGUI>().text = this.quest.GetName();
            questObjective.GetComponent<TextMeshProUGUI>().text = this.quest.GetObjectiveFull();
            questProgress.GetComponent<TextMeshProUGUI>().text = this.quest.GetProgress();
        }
    }
    
    public void Progress(string progress)
    {
        if(duringQuest)
        {
            quest.UpdateProgress(progress);
            questProgress.GetComponent<TextMeshProUGUI>().text = this.quest.GetProgress();
        }
    }

    public void SetReward(ItemClass reward)
    {
        quest.AddReward(reward);
    }

    public void FinishQuest()
    {
        duringQuest = false;

        questName.GetComponent<TextMeshProUGUI>().text = "";
        questObjective.GetComponent<TextMeshProUGUI>().text = "";
        questProgress.GetComponent<TextMeshProUGUI>().text = "";

        foreach (ItemClass item in quest.GetRewards())
        {
            itemObject.GetComponent<ItemClass>().InitialiseItem(item.item,item.GetAmount());
            GameObject.Find("InventoryUI").GetComponent<Inventory>().AddItem(itemObject.GetComponent<ItemClass>());
        }

        GameObject.Find("Player").GetComponent<PlayerController>().Experience(quest.GetExp());

        quest = null;
    }

    public bool IsDuringQuest()
    {
        return duringQuest;
    }

    public bool IsFinished()
    {
        return quest.IsFinished();
    }
}
