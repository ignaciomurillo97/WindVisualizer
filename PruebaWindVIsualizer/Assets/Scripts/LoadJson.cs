using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadJson : MonoBehaviour {

   public string fileName = "data.json";
   public WindData loadedData;

	// Use this for initialization
	void Start () {
      LoadData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void LoadData() {
       string filePath = Path.Combine (Application.streamingAssetsPath, fileName);

       if (File.Exists (filePath)){
          string dataAsJson = File.ReadAllText(filePath);
          Debug.Log("datos leidos");
          loadedData = JsonUtility.FromJson<WindData>(dataAsJson);
          Debug.Log(loadedData.lonStep);
       } else {
          Debug.LogError("No se puede cargar el archivo");
       }
    }
}
