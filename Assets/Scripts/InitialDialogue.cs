using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDialogue : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
