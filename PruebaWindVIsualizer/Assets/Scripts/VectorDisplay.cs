using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorDisplay : MonoBehaviour {

   public LineRenderer lineRenderer;

	void Start () {
		
	}

	void Update () {

	}

	public void SetVector(Vector3 vector){
		Vector3 otherPos = transform.TransformPoint (vector);
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, vector);
	}

}
