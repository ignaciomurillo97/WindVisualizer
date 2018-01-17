using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

   public GameObject globe;
   public ParticleGenerator particleGenerator;
   public Vector3 velocity;
   public Vector3 acceleration;
   public float maxSpeed;
   public float maxForce;
   public float mass;

   public float flowFieldMultiplier;
   public float desiredAlignmentRange; 
   public float desiredSeparationRange;
   public float desiredCohesionRange;

   public float flowFieldPercent; 
   public float separationPercent;
   public float cohesionPercent;
   public float alignmentPercent;

   public float globeDistance = 10;

   public float lifeTime;
   float timeOfDeath;

	void Start () {
      if (mass == 0) {
         Debug.LogError("la masa no puede ser 0");
      }
		timeOfDeath = Time.time + lifeTime;
	}
	
	void Update () {
      if (Time.time > timeOfDeath){
         Destroy(gameObject);
      }
      applyBehaviours();
      velocity += acceleration;
      velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
      displace(velocity);
	}

   void applyBehaviours() {
      List<Transform> vehicles = particleGenerator.vehicleTransforms;
      Vector3 flowFieldForce = applyFlowField();
      Vector3 separationForce = separate(vehicles);
      Vector3 cohesionForce = cohere(vehicles);
      Vector3 alignmentForce = align(vehicles);

      flowFieldForce *= flowFieldPercent;
      separationForce *= separationPercent;
      cohesionForce *= cohesionPercent;
      alignmentForce *= alignmentPercent;

      applyForce(flowFieldForce);
      applyForce(separationForce);
      applyForce(cohesionForce);
      applyForce(alignmentForce);
   }

   Vector3 align(List<Transform> vehicles) {
      Vector3 sum = new Vector3();
      int count = 0;
      foreach (Transform other in vehicles){
         float d = Vector3.Distance(transform.position, other.position);
         Vehicle vehicleComponent = other.GetComponent<Vehicle>();
         if (d > 0 && d < desiredAlignmentRange){
            sum += vehicleComponent.velocity;
            count ++;
         }
      }
      if (count > 0){
         sum /= count;
         sum.Normalize();
         sum *= maxSpeed;
         Vector3 steer = sum - velocity;
         steer = Vector3.ClampMagnitude(steer, maxForce);
         return steer;
      } else {
         return new Vector3();
      }
   }

   Vector3 separate(List<Transform> vehicles){
      Vector3 sum = new Vector3();
      int count = 0; 
      foreach (Transform other in vehicles){
         if (other != transform){
            float d = Vector3.Distance(transform.position, other.position);
            if (d > 0 && d < desiredSeparationRange){
               Vector3 diff = transform.position - other.position;
               diff.Normalize();
               diff /= d;
               sum += diff;
               count ++;
            }
         }
      }
      if (count > 0){
         sum /= count;
         sum.Normalize();
         Vector3 steer = sum - velocity;
         steer = Vector3.ClampMagnitude(steer, maxForce);
         return steer;
      } else {
         return new Vector3();
      }
   }
   
   Vector3 cohere(List<Transform> vehicles) {
      Vector3 sum = new Vector3();
      int count = 0;
      foreach (Transform other in vehicles) {
         if (other != transform){
            float d = Vector3.Distance(transform.position, other.position);
            if (d > 0 && d < desiredSeparationRange){
               sum += other.position;
               count ++;
            }
         }
      }
      if (count > 0){
         sum /= count;
         Vector3 steer = seek(sum);
         return steer;
      } else {
         return new Vector3();
      }
   }

   Vector3 seek (Vector3 target){
      Vector3 desired = target - transform.position;
      desired.Normalize();
      desired *= maxSpeed;

      Vector3 steer = desired - velocity;
      return steer;
   }

   Vector3 applyFlowField() {
      Vector3 coordenates = getCoordenates();
      Vector3 force = FlowField.velocityAtGeoLocation(coordenates.x, coordenates.y);
      force *= flowFieldMultiplier;
      force = Vector3.ClampMagnitude(force, maxForce);
      return force;
   }

   public void applyForce(Vector3 force){
      acceleration += force / mass;
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

   void OnDestroy(){
      particleGenerator.vehicleTransforms.Remove(transform);
   }

}
