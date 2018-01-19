using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

   public Globe globe;
   public GameObject globeObject;
   public GameObject particleGeneratorPrefab;

   public ParticleGenerator activeParticleGenerator; 
   public GameObject activeParticleGeneratorObject; 

   public Button ShowFlowField;
   public Button ApplyButton;

   public Slider flowFieldMultiplier;
   public Slider desiredAlignmentRange; 
   public Slider desiredSeparationRange;
   public Slider desiredCohesionRange;

   public Slider flowFieldPercent; 
   public Slider separationPercent;
   public Slider cohesionPercent;
   public Slider alignmentPercent;

   public Slider lifetime;
   public Slider rate;

   public Material mat1;
   public Material mat2;

	void Start () {
		ShowFlowField.onClick.AddListener(ShowFlowFieldClicked);
		ApplyButton.onClick.AddListener(ApplyChanges);
	}
	
	void Update () {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit)) {
         if (hit.transform.tag == "Globe" && Input.GetMouseButton(1)) {
            Vector3 latlon = Coordenadas.XYZtoGEO(hit.point);
            GameObject particleGen = Instantiate(particleGeneratorPrefab, hit.point, Quaternion.identity);
            particleGen.transform.LookAt(Vector3.zero);
            activeParticleGenerator = particleGen.GetComponent<ParticleGenerator>();
            activeParticleGenerator.globe = globeObject;
            ApplyChanges();

            Renderer renderer; 
            if (activeParticleGeneratorObject != null){
               renderer = activeParticleGeneratorObject.GetComponent<Renderer>();
               renderer.sharedMaterial = mat1;
            }
            activeParticleGeneratorObject = particleGen;
            renderer = activeParticleGeneratorObject.GetComponent<Renderer>(); 
            renderer.sharedMaterial = mat2;
         } else if (hit.transform.tag == "ParticleGenerator" && Input.GetMouseButton(0)){
            Renderer renderer; 
            if (activeParticleGeneratorObject != null){
               renderer = activeParticleGeneratorObject.GetComponent<Renderer>();
               renderer.sharedMaterial = mat1;
            }
            activeParticleGeneratorObject = hit.transform.gameObject;
            activeParticleGenerator = hit.transform.GetComponent<ParticleGenerator>();
            renderer = activeParticleGeneratorObject.transform.GetComponent<Renderer>();
            renderer.sharedMaterial = mat2;
            ReadValues();
         }
      }
   }

   void ShowFlowFieldClicked (){
      globe.displayFlowField();
   }

   void ApplyChanges(){
      if (activeParticleGenerator != null){
         activeParticleGenerator.flowFieldMultiplier = flowFieldMultiplier.value;
         activeParticleGenerator.desiredAlignmentRange = desiredAlignmentRange.value; 
         activeParticleGenerator.desiredSeparationRange = desiredSeparationRange.value;
         activeParticleGenerator.desiredCohesionRange = desiredCohesionRange.value;

         activeParticleGenerator.flowFieldPercent = flowFieldPercent.value; 
         activeParticleGenerator.separationPercent = separationPercent.value;
         activeParticleGenerator.cohesionPercent = cohesionPercent.value;
         activeParticleGenerator.alignmentPercent = alignmentPercent.value;
         activeParticleGenerator.lifetime = lifetime.value;
         activeParticleGenerator.rate = (int)rate.value;
      }
   }

   void ReadValues(){
      if (activeParticleGenerator != null){
         flowFieldMultiplier.value = activeParticleGenerator.flowFieldMultiplier;
         desiredAlignmentRange.value = activeParticleGenerator.desiredAlignmentRange;
         desiredSeparationRange.value = activeParticleGenerator.desiredSeparationRange;
         desiredCohesionRange.value = activeParticleGenerator.desiredCohesionRange;

         flowFieldPercent.value = activeParticleGenerator.flowFieldPercent;
         separationPercent.value = activeParticleGenerator.separationPercent;
         cohesionPercent.value = activeParticleGenerator.cohesionPercent;
         alignmentPercent.value = activeParticleGenerator.alignmentPercent;
         lifetime.value = activeParticleGenerator.lifetime;
         rate.value = activeParticleGenerator.rate;
      }
   }

}
