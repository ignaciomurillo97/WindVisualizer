using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

   public Globe globe;
   public GameObject globeObject;
   public GameObject particleGeneratorPrefab;

   public ParticleGenerator activeParticleGenerator; 

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

	void Start () {
		ShowFlowField.onClick.AddListener(ShowFlowFieldClicked);
		ApplyButton.onClick.AddListener(ApplyChanges);
	}
	
	void Update () {
      if (Input.GetMouseButtonUp(1)){
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;

         if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.tag == "Globe") {
               Vector3 latlon = Coordenadas.XYZtoGEO(hit.point);
               GameObject particleGen = Instantiate(particleGeneratorPrefab, hit.point, Quaternion.identity);
               particleGen.transform.LookAt(Vector3.zero);
               activeParticleGenerator = particleGen.GetComponent<ParticleGenerator>();
               activeParticleGenerator.globe = globeObject;
               ApplyChanges();
            } else if (hit.transform.tag == "ParticleGenerator"){
               activeParticleGenerator = hit.transform.GetComponent<ParticleGenerator>();
               ReadValues();
            }
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
      }
   }

}
