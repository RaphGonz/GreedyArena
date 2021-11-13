using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text dialogueText;
    
    public Queue<string> sentences;
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        print("début dialogue");

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count <= 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        //print(sentence);


    }

    void EndDialogue()
    {
        print("fin du dialogue");
    }

    
}
