using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WindData {
   public WindDataPoint[] data;

   public float lonStep;
   public float latStep;

   public int latSamples;
   public int lonSamples;
}
