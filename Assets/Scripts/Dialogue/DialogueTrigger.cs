using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dejaVuhdialogue;
    public Dialogue[] passDialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerDialogue(bool haspassed, int whichDialogueTrigger)
    {

        if (haspassed)
        {
            // Trigger a pass Dialogue
            
            DialogueManager.instance.StartDialogue(passDialogue[0]);
        }
        else
        {
            //Trigger a dejavu Dialogue
            if(whichDialogueTrigger >= passDialogue.Length)
            {
                whichDialogueTrigger = passDialogue.Length - 1;
            }
            DialogueManager.instance.StartDialogue(dejaVuhdialogue[whichDialogueTrigger]);
        }
        
    }
}
