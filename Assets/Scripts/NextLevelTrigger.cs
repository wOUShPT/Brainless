using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    private ThisLevel thisLevel;
    private bool alreadyPassed = false;
    // Start is called before the first frame update
    void Start()
    {
        thisLevel = GetComponentInParent<ThisLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyPassed)
        {
            alreadyPassed = true;
            thisLevel.WhichTrigger(this.gameObject);
        }
    }
}
