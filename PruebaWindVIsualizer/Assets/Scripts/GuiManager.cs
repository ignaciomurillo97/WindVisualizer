using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

   public Globe globe;
   public Button ShowFlowField;

	void Start () {
		ShowFlowField.onClick.AddListener(ShowFlowFieldClicked);
	}
	
	void Update () {
		
	}

   void ShowFlowFieldClicked (){
      globe.displayFlowField();
   }
}
