using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour {

   public GameObject globe;
   public GameObject vehiclePrefab;
   public int rate; // particles per sec
   float nextParticleTime;
   float deltaParticleTime;

   public List<Transform> vehicleTransforms;

   public float flowFieldMultiplier;
   public float desiredAlignmentRange; 
   public float desiredSeparationRange;
   public float desiredCohesionRange;

   public float flowFieldPercent; 
   public float separationPercent;
   public float cohesionPercent;
   public float alignmentPercent;

	void Start () {
		deltaParticleTime = 1 / rate;
      nextParticleTime = Time.time + deltaParticleTime;
	}
	
	void Update () {
		if (Time.time > deltaParticleTime){
         nextParticleTime = Time.time + deltaParticleTime;
         addVehicle ();
      }
	}

   void addVehicle (){
      GameObject currVehicle = Instantiate(vehiclePrefab, transform.position, Quaternion.identity);
      Vehicle vehicleComponent = currVehicle.GetComponent<Vehicle>();
      vehicleComponent.globe = globe;
      vehicleComponent.particleGenerator = this;
      vehicleComponent.applyForce(randomVelocity());
      currVehicle.transform.parent = globe.transform;
      vehicleTransforms.Add(currVehicle.transform);
      setVehiclesParams(vehicleComponent);
   }

   void setVehiclesParams(Vehicle v){
      v.flowFieldMultiplier = flowFieldMultiplier;
      v.desiredAlignmentRange = desiredAlignmentRange; 
      v.desiredSeparationRange = desiredSeparationRange;
      v.desiredCohesionRange = desiredCohesionRange;

      v.flowFieldPercent = flowFieldPercent; 
      v.separationPercent = separationPercent;
      v.cohesionPercent = cohesionPercent;
      v.alignmentPercent = alignmentPercent;
   }

   Vector3 randomVelocity(){
      return Random.insideUnitCircle * 0.01f;
   }

}
