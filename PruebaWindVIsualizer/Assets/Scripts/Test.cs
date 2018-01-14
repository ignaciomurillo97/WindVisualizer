using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

   public GameObject agentPrefab;
   public GameObject agent;
   public GameObject g;

   public float lat; 
   public float lon;
   public float distance;

	void Start () {
      Vector3 agentPos = Coordenadas.GEOtoXYZ(lat, lon, distance);
      agent = Instantiate(agentPrefab, agentPos, Quaternion.identity);

      Agent a = agent.GetComponent<Agent>();
      a.globe = g;
      Vector3 agentCoordenates = a.getCoordenates();
	}
	
	void Update () {
	}
}
