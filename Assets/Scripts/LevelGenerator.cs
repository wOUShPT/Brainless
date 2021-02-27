using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator levelinstance;
    private const float playerDistanceSpawnLevelPart = 50f;
    [SerializeField] private Transform levelPart_Start; 
    [SerializeField] private List<Transform> levelParts;
    [SerializeField] private Enemy enemy;
    [SerializeField] private int spawnIndex = 0;
    public List<ThisLevel> levelSeen;
    private int howManyEnemiesToSpawn = 0;

    private Vector3 lastEndPosition;
    private void Awake()
    {
        levelinstance = this;
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        //int startingSpawnLevelPart = 1;
        //for(int i = 0;i < startingSpawnLevelPart; i++)
        //{
        //    SpawnLevel();
        //}
    }
    private void SpawnLevel(int index)
    {
        //Transform chosenLevelPart = levelParts[spawnIndex];
        Transform lastlevelPartTransform = SpawnLevelPart(spawnIndex,lastEndPosition);
        ThisLevel thislevel = lastlevelPartTransform.gameObject.GetComponent<ThisLevel>();

        // set the right direction
        
        thislevel.pathArray[index].isTheRightDirection = true;
        levelSeen.Add(thislevel);
        lastEndPosition = lastlevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(int levelPartIndex,Vector3 spawnPosition)
    {
        // Instantiate the level prefab
        ThisLevel levelPartScript = LevelPool.instance.Get(levelPartIndex);
        levelPartScript.transform.position = spawnPosition;
        Transform levelPartTransform = levelPartScript.transform;
        howManyEnemiesToSpawn++;
        levelPartScript.EnemiesToSpawn = howManyEnemiesToSpawn;
        levelPartTransform.gameObject.SetActive(true);
        //Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
    private void SendToPool()
    {
        // return to the pool to be available to use it again
        ResetRightPath(levelSeen[0]);
        LevelPool.instance.ReturnToPool(levelSeen[0]);
        
        levelSeen.RemoveAt(0);
    }
    public void ResetRightPath(ThisLevel level)
    {
        for(int i = 0; i < level.pathArray.Length; i++)
        {
            level.pathArray[i].isTheRightDirection = false;
            level.pathArray[i].TriggerPath.GetComponent<NextLevelTrigger>().AlreadyPassed = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncrementIndex()
    {
        spawnIndex++;
        if(spawnIndex >= levelParts.Count)
        {
            
            spawnIndex = 0;
            
        }
        // reset how many enemies gonna spawn
        howManyEnemiesToSpawn = -1;
    }
    public void SpawnAnother(int index )
    {
        // Spawn another level part
        SpawnLevel(index);
        if(levelSeen.Count >= 3)
        {
            SendToPool();
        }
    }
}
