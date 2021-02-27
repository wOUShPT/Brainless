using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThisLevel : MonoBehaviour
{
    [Serializable]
    public struct Path
    {
        public GameObject TriggerPath;
        public bool isTheRightDirection;
    }
    public Path[] pathArray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WhichTrigger(GameObject triggerCollided)
    {
        int index = 0;
        // Loop through paths
        foreach(Path path in pathArray)
        {
            
            if(path.TriggerPath == triggerCollided)
            {
                // it's this triggered that playes has triggered
                if (path.isTheRightDirection)
                {
                    LevelGenerator.levelinstance.IncrementIndex();
                }
                LevelGenerator.levelinstance.SpawnAnother(index);
                return;
            }
            index++;
        }
    }
}
