using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

   public GameObject globe;
   public Vector3 velocity;

   public float speed = 10;
   public float globeDistance = 10;

   void Start () {
      FlowField.loadFlowField();
   }

   void Update () {
      faceGlobe();
      transform.Translate(velocity * Time.deltaTime, Space.Self);
      Vector3 coordenates = getCoordenates();
      velocity = FlowField.velocityAtGeoLocation(coordenates.x, coordenates.y);
      velocity = velocity * 0.1f;
   }


   void faceGlobe(){
      Vector3 targetDir = globe.transform.position - transform.position;
      transform.rotation = Quaternion.identity;
      transform.rotation = Quaternion.LookRotation(targetDir);
   }

   void mantainDistance() {
      Vector3 offset = transform.position - globe.transform.position;
      offset.Normalize();
      offset *= globeDistance;
      transform.position += globe.transform.position + offset;
   }

   Vector3 getCoordenates(){
      Vector3 offset = transform.position - globe.transform.position;
      return Coordenadas.XYZtoGEO(offset);
   }
}
