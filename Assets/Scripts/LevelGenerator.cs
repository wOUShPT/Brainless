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
    public List<Transform> levelHaven;

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
        Transform chosenLevelPart = levelParts[spawnIndex];
        Transform lastlevelPartTransform = SpawnLevelPart(chosenLevelPart,lastEndPosition);
        ThisLevel thislevel = lastlevelPartTransform.gameObject.GetComponent<ThisLevel>();
        // set the right direction
        thislevel.pathArray[index].isTheRightDirection = true;
        lastEndPosition = lastlevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(Transform levelPart,Vector3 spawnPosition)
    {
        // Instantiate the level prefab
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector2.Distance(enemy.gameObject.transform.position, lastEndPosition) < playerDistanceSpawnLevelPart)
        //{
        //    // Spawn another level part
        //    SpawnLevel();
        //}
    }
    public void IncrementIndex()
    {
        spawnIndex++;
        if(spawnIndex >= levelParts.Count)
        {
            spawnIndex = 0;
        }
    }
    public void SpawnAnother(int index )
    {
        // Spawn another level part
        SpawnLevel(index);
    }
}
