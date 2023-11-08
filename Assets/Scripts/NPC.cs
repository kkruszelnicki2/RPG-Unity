using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isCollision = false;

    public string NPCname; 

    [TextArea(3, 10)] public string[] quest1;
    [TextArea(3, 10)] public string[] quest1End;
    [TextArea(3, 10)] public string[] quest2End;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isCollision && !GameObject.Find("Game").GetComponent<Game>().dialogue) //Start Dialogue after clicking E
        {
            if(!GameObject.Find("QuestLog").GetComponent<QuestLog>().IsDuringQuest())
            {
                if (GameObject.Find("Game").GetComponent<Game>().GetQuest(0) == 0)
                {
                    GameObject.Find("DialogueBox").GetComponent<Dialogue>().StartDialogue(quest1, NPCname);

                    GameObject.Find("Game").GetComponent<Game>().ManageQuest(0, 1);
                }
            }
            else
            {
                if(GameObject.Find("Game").GetComponent<Game>().GetQuest(0) == 1 && GameObject.Find("QuestLog").GetComponent<QuestLog>().IsFinished())
                {
                    GameObject.Find("QuestLog").GetComponent<QuestLog>().FinishQuest();

                    GameObject.Find("DialogueBox").GetComponent<Dialogue>().StartDialogue(quest1End, NPCname);

                    GameObject.Find("Game").GetComponent<Game>().ManageQuest(0, 2);

                    GameObject.Find("Game").GetComponent<Game>().ManageQuest(1, 1);
                }
                else if(GameObject.Find("Game").GetComponent<Game>().GetQuest(1) == 1 && GameObject.Find("QuestLog").GetComponent<QuestLog>().IsFinished())
                {
                    GameObject.Find("QuestLog").GetComponent<QuestLog>().FinishQuest();

                    GameObject.Find("DialogueBox").GetComponent<Dialogue>().StartDialogue(quest2End, NPCname);

                    GameObject.Find("Game").GetComponent<Game>().ManageQuest(1, 2);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCollision = false;
        }
    }
}
