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
    [SerializeField] private Image dialogueBallonImage;
    
    [SerializeField] private Sprite speechBubble;
    //[SerializeField] private Sprite secondSpeechBubble;
    private Queue<string> sentences;
    private Queue<AudioClip> audioClipsFromSentences;

    public static DialogueManager instance;
    private bool isPlayerOnTrigger;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip speakBread;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        dialogueText = FindObjectOfType<TextMeshPro>();
        if(instance == null)
        {
            instance = this;
        }
        sentences = new Queue<string>();
        audioClipsFromSentences = new Queue<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerOnTrigger)
        {
            DisplayNextSentence();
        }
    }
    public void StartDialogue(Dialogue dialogue )
    {
        dialogueText.text = "";
        // Clear queus
        sentences.Clear();
        audioClipsFromSentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            
        }
        foreach(AudioClip audioClip in dialogue.audioClip)
        {
            audioClipsFromSentences.Enqueue(audioClip);
        }
        //DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        dialogueText.text = "";
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        // Queue FIFO - First in, First Out
        string sentence = sentences.Dequeue();
        speakBread = audioClipsFromSentences.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        //if (!string.IsNullOrEmpty(sentence))
        //{
        //    for (int i = 0; i < sentence.Length; i++)
        //    {
        //        if (char.IsUpper(sentence[i]) && char.IsUpper(sentence[i + 1]) && char.IsUpper(sentence[i + 2]))
        //        {
        //            dialogueBallonImage.sprite = secondSpeechBubble;
        //            break;
        //        }
        //        dialogueBallonImage.sprite = speechBubble;
        //    }


        //}
        //dialogueText.text = "";
        string[] phrases = sentence.Split(' ');
        for(int i = 0; i < sentence.Length; i++)
        {
            if (char.IsWhiteSpace(sentence[i]) && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else if (!audioSource.isPlaying)
            {
                // Play music when is not a char with empty space
                if(audioSource != null)
                {
                    audioSource.PlayOneShot(speakBread);
                }
                
            }
            if((char.IsUpper(sentence[i]) && sentence[i] != sentence.Length -1))
            {
                if (char.IsUpper(sentence[i + 1]))
                {
                    animator.SetBool("Toshowoff", true);
                }
                
            }
            else
            {
                animator.SetBool("Toshowoff", false);
            }
            dialogueText.text += sentence[i];
            yield return new WaitForSeconds(0.1f);
        }
        //foreach(char letter in sentence.ToCharArray())
        //{
        //    if (char.IsWhiteSpace(letter) && audioSource.isPlaying)
        //    {
        //        audioSource.Stop();
        //    }
        //    else if (!audioSource.isPlaying)
        //    {
        //        // Play music when is not a char with empty space
        //        audioSource.PlayOneShot(speakBread);
        //    }
        //    dialogueText.text += letter;
        //    yield return new WaitForSeconds(0.1f);
        //}
    }

    private void EndDialogue()
    {
        Debug.Log("EndOfConversation");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisplayNextSentence();
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
