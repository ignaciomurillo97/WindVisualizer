using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField {

   static float latStep;
   static float lonStep;

   static Vector3[,] flowField;
   static int flowFieldWidth;
   static int flowFieldHeight;

   public static void loadFlowFieldFromPerlin(){
      float noiseSize = 5;
      flowFieldHeight = 100;
      flowFieldWidth = 100;

      flowField = new Vector3[flowFieldHeight, flowFieldWidth]; 
      
      latStep = 180 / flowFieldHeight;
      lonStep = 360 / flowFieldWidth;

      for (int i = 0; i < flowFieldHeight; i++){
         for (int j = 0; j < flowFieldWidth; j++){
            float noise = Mathf.PerlinNoise(i / noiseSize, j / noiseSize) * Mathf.PI * 2;
            flowField[i, j] = new Vector3(Mathf.Cos(noise), Mathf.Sin(noise));
         }
      }
   }

   public static void loadFlowField(WindData wd) {
      latStep = wd.latStep;
      lonStep = wd.lonStep;

      WindDataPoint[] data = wd.data;
      Debug.Log("Wind Data Length" + data.Length);

      flowField = new Vector3[(int)wd.latSamples, (int)wd.lonSamples];
      flowFieldWidth = (int)wd.lonSamples;
      flowFieldHeight = (int)wd.latSamples;
      Debug.Log("width " + flowFieldWidth);
      Debug.Log("height " + flowFieldHeight);

      foreach (WindDataPoint dataPoint in data){
         int i = (int)latToIndex(dataPoint.lat);
         int j = (int)lonToIndex(dataPoint.lon);
         Debug.Log("indices: " + i + ", " + j);
         flowField[i, j] = getWindVector(dataPoint.deg, dataPoint.speed);
      }
   }

   static Vector3 getWindVector(float ang, float speed){
      return new Vector3(
            Mathf.Sin(ang * Mathf.Deg2Rad),
            Mathf.Sin(ang * Mathf.Deg2Rad)
            ) * speed;
   }

   public static Vector3 velocityAtGeoLocation(float lat, float lon){
      if (flowField == null) return Vector3.zero;
      float i = Mathf.Abs(latToIndex(lat));
      float j = Mathf.Abs(lonToIndex(lon));

      int i1 = (int)i % flowFieldHeight; 
      int j1 = (int)j % flowFieldWidth;

      int i2 = (i1 + 1) % flowFieldHeight;
      int j2 = (j1    ) % flowFieldWidth;

      int i3 = (i1    ) % flowFieldHeight;
      int j3 = (j1 + 1) % flowFieldWidth;

      int i4 = (i1 + 1) % flowFieldHeight;
      int j4 = (j1 + 1) % flowFieldWidth;

      float percentI = i - i1;
      float percentJ = j - j1;

      Vector3 p1 = flowField[i1, j1];
      Vector3 p2 = flowField[i2, j2];
      Vector3 p3 = flowField[i3, j3];
      Vector3 p4 = flowField[i4, j4];

      Vector3 p12 = Vector3.Lerp(p1, p2, percentI);
      Vector3 p34 = Vector3.Lerp(p3, p4, percentI);

      Vector3 res = Vector3.Lerp(p12, p34, percentJ);
      return res; 
   }

   static int latToIndex(float lat){
      return (int)((lat + 90) / latStep);   
   }

   static int lonToIndex(float lon){
      return (int)((lon + 180) / lonStep);
   }

   static float indexToLat(float index){
      return index * latStep - 90;
   }

   static float indexToLon(float index){
      return index * lonStep - 180;
   }

}
