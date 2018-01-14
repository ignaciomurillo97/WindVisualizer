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
      Vector3 coordenates = getCoordenates();
      velocity = FlowField.velocityAtGeoLocation(coordenates.x, coordenates.y);
      velocity = velocity * 0.01f;
      displace(velocity);
   }

   void displace (Vector3 v) {
      float elevation = Mathf.Atan(v.y / globeDistance) * Mathf.Rad2Deg;
      float azimuth = Mathf.Atan(v.x / globeDistance) * Mathf.Rad2Deg;
      transform.RotateAround(globe.transform.position, transform.right, elevation);
      transform.RotateAround(globe.transform.position, Vector3.up, elevation);
   }

   public Vector3 getCoordenates(){
      Vector3 offset = transform.position - globe.transform.position;
      return Coordenadas.XYZtoGEO(offset);
   }

}
