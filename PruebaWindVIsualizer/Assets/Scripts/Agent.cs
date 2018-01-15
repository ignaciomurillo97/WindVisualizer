using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

   public GameObject globe;
   public Vector3 velocity;
   public float speedMult;

   public float globeDistance = 10;

   public TrailRenderer tr;
   public float minLifeTime;
   public float maxLifeTime;
   float lifeTime;
   float timeOfDeath;
   Vector3 startingPos;

   void Start () {
      startingPos = transform.position;
      Reset();
   }

   void Update () {
      if (Time.time > timeOfDeath){
         Reset();
      }

      Vector3 coordenates = getCoordenates();
      velocity = FlowField.velocityAtGeoLocation(coordenates.x, coordenates.y);
      displace(velocity * speedMult);
   }

   void Reset(){
      lifeTime = Random.Range(minLifeTime, maxLifeTime);
      timeOfDeath = lifeTime + Time.time;
      transform.position = startingPos;
      tr.Clear();
   }

   void displace (Vector3 v) {
      float elevation = Mathf.Atan2(v.y, globeDistance) * Mathf.Rad2Deg;
      float azimuth = Mathf.Atan2(v.x, globeDistance) * Mathf.Rad2Deg;
      transform.RotateAround(globe.transform.position, transform.right, elevation);
      transform.RotateAround(globe.transform.position, Vector3.up, azimuth);
   }

   public Vector3 getCoordenates(){
      Vector3 offset = transform.position - globe.transform.position;
      return Coordenadas.XYZtoGEO(offset);
   }

}
