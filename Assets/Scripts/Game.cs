using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int SpawnerID = 0;

    public bool dialogue = false;
    //quest list
    private int[] quests = { 0, 0 }; //0 - quest nieodebrany    1 - quest odebrany    2 - quest uko�czony

    public int GetID()
    {
        this.SpawnerID++;
        return SpawnerID;
    }
    //quest list functions
    public int GetQuest(int questNumber) //Checking if this quest is already claimed
    {
        return quests[questNumber];
    }

    public void ManageQuest(int questNumber,int questState)
    {
        if(quests[questNumber] == 0 && questState == 1)
        {
            ItemClass reward = new ItemClass();
            quests[questNumber] = questState;

            switch(questNumber)
            {
                case 0:
                    GameObject.Find("QuestLog").GetComponent<QuestLog>().AddQuest(
                        new Quest("Pierwsze zadanie!","Zabij","Zabij 3 slime'y","Slime",3,"Guardian",100)
                        );

                    reward.Constructor("health potion",2,0,"HP");
                    GameObject.Find("QuestLog").GetComponent<QuestLog>().SetReward(reward);

                    break;
                case 1:
                    GameObject.Find("QuestLog").GetComponent<QuestLog>().AddQuest(
                        new Quest("Wi�kszy przeciwnik.", "Zabij", "Pokonaj Kr�la Slime", "Kr�l Slime", 1, "Guardian", 300)
                        );

                    reward.Constructor("health potion", 4, 0, "HP");
                    GameObject.Find("QuestLog").GetComponent<QuestLog>().SetReward(reward);
                    break;
            }
        }
        else if(quests[questNumber] == 1 && questState == 2)
        {
            quests[questNumber] = questState;
        }
    }
}
