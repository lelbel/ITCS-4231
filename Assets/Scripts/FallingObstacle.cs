using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{

    [SerializeField] private Transform obstacle;
    private WaitForSeconds removeLeafObjectTime;
    public float yoffset;

    // Start is called before the first frame update
    void Start()
    {
        removeLeafObjectTime = new WaitForSeconds(10f);
        StartCoroutine(RemoveLeafObjectTimer());
    }

    // Update is called once per frame
    void Update()
    {
        yoffset = (1f * Time.deltaTime);
        obstacle.position -= new Vector3 (0f, yoffset, 0f);
    }

    public IEnumerator RemoveLeafObjectTimer()
    {
        yield return removeLeafObjectTime;
        Destroy(this);
        Destroy(gameObject);
    }
}
