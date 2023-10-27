using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();

    [SerializeField] GameObject text;
    [SerializeField] GameObject characterName;

    Vector2 outOfGame = new Vector2(530, -200);
    Vector2 inGame = new Vector2(530, 150);

    public void Update()
    {
        if(GameObject.Find("Game").GetComponent<Game>().dialogue && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(string dialogue, string name)
    {
        GameObject.Find("Player").GetComponent<PlayerController>().DontMove();

        Show();

        GameObject.Find("Game").GetComponent<Game>().dialogue = true;

        text.GetComponent<TextMeshProUGUI>().text = dialogue;
        characterName.GetComponent<TextMeshProUGUI>().text = name;
    }

    public void StartDialogue(string[] dialogues, string name)
    {
        GameObject.Find("Player").GetComponent<PlayerController>().DontMove();

        Show();

        GameObject.Find("Game").GetComponent<Game>().dialogue = true;
        sentences.Clear();

        characterName.GetComponent<TextMeshProUGUI>().text = name;

        foreach (string sentence in dialogues)
        {
            sentences.Enqueue(sentence);
        }

        text.GetComponent<TextMeshProUGUI>().text = sentences.Dequeue();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        text.GetComponent<TextMeshProUGUI>().text = sentences.Dequeue();
    }

    private void EndDialogue()
    {
        GameObject.Find("Game").GetComponent<Game>().dialogue = false;

        Hide();

    }

    private void Show()
    {
        transform.position = inGame;
    }
    private void Hide()
    {
        transform.position = outOfGame;
    }
}
