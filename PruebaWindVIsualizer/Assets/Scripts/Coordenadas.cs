using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordenadas {

   static float latOffset;
   static float lonOffset;
   
   static public Vector3 GEOtoXYZ(float latitud, float longitud, float radio) {
      latitud = (latitud + latOffset) * Mathf.Deg2Rad;
      longitud = (longitud + lonOffset) * Mathf.Deg2Rad;

      float x = radio * Mathf.Sin(latitud + (Mathf.PI / 2)) * Mathf.Cos(longitud);
      float z = radio * Mathf.Sin(latitud + (Mathf.PI / 2)) * Mathf.Sin(longitud);
      float y = radio * Mathf.Cos(latitud + (Mathf.PI / 2));

      Vector3 cordCartesianas = new Vector3(x, y, z);
      return cordCartesianas;
   }

   static public Vector3 XYZtoGEO(Vector3 coord) {
      float lat = Mathf.Acos(coord.y / coord.magnitude) * Mathf.Rad2Deg - 90;
      float lon = Mathf.Atan(coord.z / coord.x) * Mathf.Rad2Deg;
      return new Vector3(lat, lon);
   }

}
