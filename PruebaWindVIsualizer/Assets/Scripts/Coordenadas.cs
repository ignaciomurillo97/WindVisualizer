using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordenadas {

   static float latOffset;
   static float lonOffset;
   
   static public Vector3 GEOtoXYZ(float latitud, float longitud, float radio) {
      latitud = (latitud + latOffset) * Mathf.Deg2Rad;
      longitud = (longitud + lonOffset) * Mathf.Deg2Rad;

      float x = radio * Mathf.Sin(longitud) * Mathf.Cos(latitud);
      float z = radio * Mathf.Sin(longitud) * Mathf.Sin(latitud);
      float y = radio * Mathf.Cos(longitud);

      Vector3 cordCartesianas = new Vector3(x, y, z);
      return cordCartesianas;
   }

   static public Vector3 XYZtoGEO(Vector3 coord) {
      float lat = Mathf.Atan(coord.y / coord.x) * Mathf.Rad2Deg;
      float lon = Mathf.Acos(coord.z / coord.magnitude) * Mathf.Rad2Deg;
      return new Vector3(lat, lon);
   }

}
