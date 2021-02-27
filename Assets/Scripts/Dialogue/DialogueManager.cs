using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //[SerializeField] private Text dialogueText;
    [SerializeField]private TextMeshPro dialogueText;
    private Queue<string> sentences;

    public static DialogueManager instance;
    private bool isPlayerOnTrigger;
    // Start is called before the first frame update
    void Awake()
    {
        dialogueText = FindObjectOfType<TextMeshPro>();
        if(instance == null)
        {
            instance = this;
        }
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerOnTrigger)
        {
            DisplayNextSentence();
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueText.text = "";
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            
        }
        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        dialogueText.text = "";
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        //dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void EndDialogue()
    {
        Debug.Log("EndOfConversation");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTrigger = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTrigger = false;
        }
    }
}
