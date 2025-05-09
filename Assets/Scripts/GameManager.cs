using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private WaitForSeconds leafSpawnDelay;
    [SerializeField] private GameObject leafPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform spawnPoint3;
    [SerializeField] private Transform spawnPoint4;
    [SerializeField] private Transform spawnPoint5;
    [SerializeField] private Transform spawnPoint6;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            SpawnLeaf();
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(spawnPoint);
            DontDestroyOnLoad(spawnPoint1);
            DontDestroyOnLoad(spawnPoint2);
            DontDestroyOnLoad(spawnPoint3);
            DontDestroyOnLoad(spawnPoint4);
            DontDestroyOnLoad(spawnPoint5);
            DontDestroyOnLoad(spawnPoint6);
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
        Instantiate(leafPrefab, spawnPoint1.position, Quaternion.identity);
        Instantiate(leafPrefab, spawnPoint2.position, Quaternion.identity);
        Instantiate(leafPrefab, spawnPoint3.position, Quaternion.identity);
        Instantiate(leafPrefab, spawnPoint4.position, Quaternion.identity);
        Instantiate(leafPrefab, spawnPoint5.position, Quaternion.identity);
        Instantiate(leafPrefab, spawnPoint6.position, Quaternion.identity);
    }
}
