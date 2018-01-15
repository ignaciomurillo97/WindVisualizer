using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour {

   public int sqrtAgentCount = 10;
   public GameObject agentPrefab;
   public float distance;

   public GameObject vectorDisplayPrefab;

   public bool flowFieldDisplayed;

   void Start () {
      generateAgents(distance);
   }

   void Update () {

   }

   void generateAgents(float distance){
      float latSeparation = 180 / (sqrtAgentCount);
      float lonSeparation = 360 / (sqrtAgentCount);

      for (int i = 0; i < sqrtAgentCount; i++){
         for (int j = 0; j < sqrtAgentCount; j++){
            float lat = latSeparation * (i - sqrtAgentCount/2);
            float lon = lonSeparation * (j - sqrtAgentCount/2);
            addAgent(lat, lon);
         }
      }
   }

   void addAgent(float lat, float lon){
      Vector3 pos = Coordenadas.GEOtoXYZ(lat, lon, distance);
      GameObject currAgent = Instantiate(agentPrefab, pos, Quaternion.identity);
      Agent agentComponent = currAgent.GetComponent<Agent>();
      agentComponent.globe = gameObject;
      currAgent.transform.parent = transform;
   }

   public void displayFlowField() {
      if (!flowFieldDisplayed){
         Vector3[,] flowField = FlowField.flowField;
         for (int i = 0; i < FlowField.flowFieldHeight; i++){
            for (int j = 0; j < FlowField.flowFieldWidth; j++){
               Vector3 currVector = flowField[i, j];

               float lat = FlowField.latStep * (i - FlowField.flowFieldHeight / 2);
               float lon = FlowField.lonStep * j;

               addVector (lat, lon, currVector);
            }
         }
      }
   }

   void addVector (float lat, float lon, Vector3 flowFieldVector) {
      Vector3 pos = Coordenadas.GEOtoXYZ(lat, lon, distance);
      flowFieldVector = Coordenadas.GEOtoXYZ(lat + flowFieldVector.y, flowFieldVector.x + lon, distance);

      GameObject currVector = Instantiate (vectorDisplayPrefab, pos, Quaternion.identity);

      VectorDisplay vd = currVector.GetComponent<VectorDisplay>();
      vd.SetVector (flowFieldVector);
      currVector.transform.parent = transform;
   }

}
