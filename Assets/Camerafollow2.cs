using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow2 : MonoBehaviour {

	public GameObject cameraObj;
	public Vector3 specificVector;
	public float smoothSpeed;

	void Start () { 
		cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update () {
		specificVector = new Vector3(transform.position.x, transform.position.y, cameraObj.transform.position.z);
		cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, specificVector, smoothSpeed * Time.deltaTime);
	}
}
