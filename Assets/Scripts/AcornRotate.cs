using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcornSPIN : MonoBehaviour
{
	public float spd;

	void Update(){
		transform.Rotate(Vector3.up,spd * Time.deltaTime,0);
	}
   
}
