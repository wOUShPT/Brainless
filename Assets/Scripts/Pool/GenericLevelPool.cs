using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericLevelPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T[] prefab;

    public static GenericLevelPool<T> instance { get; private set; } // private set for not changing
    private List<T> levels = new List<T>();
    private int index;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public T Get(int wantedPrefab)
    {
        bool hasFound = false;
        T removed = null;
        if(levels.Count == 0)
        {
            AddObjects(wantedPrefab);
        }
        for(int i = 0; i < levels.Count; i++)
        {
            Debug.Log(levels[i].GetComponent<ThisLevel>().level + "," + prefab[wantedPrefab].GetComponent<ThisLevel>().level);
            if(levels[i].GetComponent<ThisLevel>().level == prefab[wantedPrefab].GetComponent<ThisLevel>().level)
            {
                Debug.Log("É este!!");
                removed = levels[i];
                levels.RemoveAt(i);
                hasFound = true;
                return removed;
                

            }
        }
        if (!hasFound)
        {
            AddObjects(wantedPrefab);
            removed = levels[levels.Count - 1];
            levels.RemoveAt(levels.Count - 1);
            return removed;

        }
        return removed;
        
        
    }
    public void ReturnToPool(T objectTransform)
    {
        objectTransform.gameObject.SetActive(false);
        levels.Add(objectTransform); // Add to the list
    }
    private void AddObjects(int wantedprefab)
    {
        T newObject = GameObject.Instantiate(prefab[wantedprefab]);
        newObject.gameObject.SetActive(false);
        levels.Add(newObject);
    }
}
