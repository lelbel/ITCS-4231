using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private Transform obstacleTransform;
    private float movementValue = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        obstacleTransform.position -= new Vector3(0f, movementValue, 0f);
    }
}
