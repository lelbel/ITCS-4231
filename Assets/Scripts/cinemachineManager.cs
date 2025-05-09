using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cinemachineManager : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cam;
    public float cameraXSpeed;
    public float cameraYSpeed;
    void Start() {
        
    }

    void Update() {
        cam.m_XAxis.m_MaxSpeed = cameraXSpeed;
        cam.m_YAxis.m_MaxSpeed = cameraYSpeed;
    }
}
