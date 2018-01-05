using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour {

   public int sqrtAgentCount = 10;
   public GameObject agentPrefab;
   public float distance;

	// Use this for initialization
	void Start () {
      generateAgents(distance);
	}

   // Update is called once per frame
   void Update () {

   }

   void generateAgents(float distance){
      float latSeparation = 180 / (sqrtAgentCount);
      float lonSeparation = 360 / (sqrtAgentCount);

      for (int i = 0; i < sqrtAgentCount; i++){
         for (int j = 0; j < sqrtAgentCount; j++){
            float lat = latSeparation * i;
            float lon = lonSeparation * j;
            addAgent(lat, lon, distance);
         }
      }
   }

   void addAgent(float lat, float lon, float distance){
      Vector3 pos = Coordenadas.GEOtoXYZ(lat, lon, distance);
      GameObject currAgent = Instantiate(agentPrefab, pos, Quaternion.identity);
      Agent agentComponent = currAgent.GetComponent<Agent>();
      agentComponent.globe = gameObject;
      currAgent.transform.parent = transform;
   }

}
