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

    public List<GameObject> enemyList;

    [SerializeField]private int enemiesToSpawn = 0;
    public int EnemiesToSpawn
    {
        set
        {
            enemiesToSpawn = value;
            if(enemiesToSpawn >= enemyList.Count) // in case number is superial
            {
                enemiesToSpawn = enemyList.Count - 1;
            }
        }
    }
    public enum Levels
    {
        Level1, Level2,
    }
    public Levels level;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++ )
        {
            Debug.Log(transform.GetChild(i).name);
            
            if(transform.GetChild(i).TryGetComponent<Enemy>(out Enemy enemy))
            {

                enemyList.Add(enemy.gameObject);
            }
            
        }
        DisablingEnemies();
    }
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
    private void OnEnable()
    {
        if(enemiesToSpawn != 0)
        {
            int index = 0;
            foreach (GameObject enemy in enemyList)
            {
                if (index >= enemiesToSpawn)
                {
                    break; // exit the foreach loop
                }
                enemy.gameObject.SetActive(true);
                index++;
                
            }
        }
        
    }
    private void OnDisable()
    {
        DisablingEnemies();
    }
    private void DisablingEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
