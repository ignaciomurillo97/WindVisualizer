using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

   public float speed;

	void Start () {
		
	}
	
	void Update () {
	   if (Input.GetAxis("Mouse ScrollWheel") != 0){
         Camera.main.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * 10;
      }
	}
}
