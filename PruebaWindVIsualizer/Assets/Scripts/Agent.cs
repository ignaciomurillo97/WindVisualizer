using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

   public GameObject globe;
   public Vector3 velocity;

   public float globeDistance = 10;

   void Start () {
   }

   void Update () {
      displace(velocity);
      Vector3 coordenates = getCoordenates();
      velocity = FlowField.velocityAtGeoLocation(coordenates.x, coordenates.y);
      velocity = velocity * 0.01f;
   }

   void displace (Vector3 v) {
      float elevation = Mathf.Atan(v.y / globeDistance) * Mathf.Rad2Deg;
      float azimuth = Mathf.Atan(v.x / globeDistance) * Mathf.Rad2Deg;
      transform.RotateAround(globe.transform.position, transform.right, elevation);
      transform.RotateAround(globe.transform.position, Vector3.up, elevation);
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
