using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadJson : MonoBehaviour {

   public string fileName = "data.json";
   public WindData loadedData;
   public bool usePerlinNoise;

	void Start () {
      LoadData();
	}
	
	void Update () {
		
	}
    private void LoadData() {
       string filePath = Path.Combine (Application.streamingAssetsPath, fileName);

       if (File.Exists (filePath)){
          string dataAsJson = File.ReadAllText(filePath);
          Debug.Log("datos leidos");
          loadedData = JsonUtility.FromJson<WindData>(dataAsJson);
          if (usePerlinNoise){
             FlowField.loadFlowFieldFromPerlin();
          } else {
             FlowField.loadFlowField(loadedData);
          }
       } else {
          Debug.LogError("No se puede cargar el archivo");
       }
    }
}
