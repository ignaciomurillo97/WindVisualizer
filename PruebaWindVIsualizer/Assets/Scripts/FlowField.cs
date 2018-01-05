using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField {

   static float latStep;
   static float lonStep;

   static Vector3[][] flowField;

   public static void loadFlowField(){
      float noiseSize = 5;
      flowField = new Vector3[10][];
      
      latStep = 180 / 10;
      lonStep = 360 / 10;

      for (int i = 0; i < flowField.Length; i++){
         Vector3[] row = new Vector3[10];
         for (int j = 0; j < flowField.Length; j++){
            float noise = Mathf.PerlinNoise(i / noiseSize, j / noiseSize) * Mathf.PI * 2;
            row[j] = new Vector3(Mathf.Cos(noise), Mathf.Sin(noise));
         }
         flowField[i] = row;
      }
   }

   public static Vector3 velocityAtGeoLocation(float lat, float lon){
      float i = Mathf.Abs(latToIndex(lat));
      float j = Mathf.Abs(lonToIndex(lon));

      Debug.Log("indices: " + i + ", " + j);

      int i1 = (int)i % flowField.Length; int j1 = (int)j % flowField.Length;
      int i2 = i1 + 1; int j2 = j1;
      int i3 = i1    ; int j3 = j1 + 1;
      int i4 = i1 + 1; int j4 = j1 + 1;

      float percentI = i - i1;
      float percentJ = j - j1;

      Vector3 p1 = flowField[i1][j1];
      Vector3 p2 = flowField[i2][j2];
      Vector3 p3 = flowField[i3][j3];
      Vector3 p4 = flowField[i4][j4];

      Vector3 p12 = Vector3.Lerp(p1, p2, percentI);
      Vector3 p34 = Vector3.Lerp(p3, p4, percentI);

      return Vector3.Lerp(p12, p34, percentJ);
   }

   static float latToIndex(float lat){
      return (int)((lat + 90) / latStep);   
   }

   static float lonToIndex(float lon){
      return (int)((lon + 180) / lonStep);
   }

   static float indexToLat(float index){
      return index * latStep - 90;
   }

   static float indexToLon(float index){
      return index * lonStep - 180;
   }

}
