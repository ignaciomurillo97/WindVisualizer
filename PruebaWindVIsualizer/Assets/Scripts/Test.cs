using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	void Start () {
		WindData wd = new WindData();
      wd.lonStep = 10;
      wd.latStep = 10;
      wd.latSamples = 10;
      wd.lonSamples = 10;

      wd.data = new WindDataPoint[1];
      WindDataPoint dataPoint = new WindDataPoint();
      dataPoint.speed = 10;
      dataPoint.deg = 10;
      dataPoint.lat = 10;
      dataPoint.lon = 10;

      wd.data[0] = dataPoint;

      Debug.Log("Iniciando Json");
      string json = JsonUtility.ToJson(wd); 
      Debug.Log("Json: " + json);
	}
	
	void Update () {
		
	}
}
