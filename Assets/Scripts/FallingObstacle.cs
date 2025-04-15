using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{

    [SerializeField] private Transform obstacle;
    private WaitForSeconds removeLeafObjectTime;

    // Start is called before the first frame update
    void Start()
    {
        removeLeafObjectTime = new WaitForSeconds(10f);
        StartCoroutine(RemoveLeafObjectTimer());
    }

    // Update is called once per frame
    void Update()
    {
        obstacle.position -= new Vector3 (0f, .01f, 0f);
    }

    public IEnumerator RemoveLeafObjectTimer()
    {
        yield return removeLeafObjectTime;
        Destroy(this);
        Destroy(gameObject);
    }
}
