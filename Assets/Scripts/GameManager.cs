using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private WaitForSeconds leafSpawnDelay;
    [SerializeField] private GameObject leafPrefab;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            SpawnLeaf();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        leafSpawnDelay = new WaitForSeconds(10f);
        StartCoroutine(LeafSpawnTimer());
        
    }

    public IEnumerator LeafSpawnTimer()
    {
        
        yield return leafSpawnDelay;
        
        SpawnLeaf();
        StartCoroutine(LeafSpawnTimer());
    }

    public void SpawnLeaf()
    {
        Instantiate(leafPrefab, spawnPoint.position, Quaternion.identity);
    }
}
