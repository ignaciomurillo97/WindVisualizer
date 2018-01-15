using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAtPoint : MonoBehaviour {

   public LineRenderer lineRenderer;
   public float globeDistance;

	void Start () {
	}
	
	void Update () {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit)) {
         Vector3 latlon = Coordenadas.XYZtoGEO(hit.point);
         Vector3 v = FlowField.velocityAtGeoLocation(latlon.x, latlon.y);
         lineRenderer.SetPosition (0, hit.point);
         showVector(latlon.x, latlon.y, v);
      }
	}

   void showVector(float latOrigin, float lonOrigin, Vector3 v){
      float elevation = Mathf.Atan(v.y / globeDistance) * Mathf.Rad2Deg;
      float azimuth = Mathf.Atan(v.x / globeDistance) * Mathf.Rad2Deg;
      Vector3 otherPos = Coordenadas.GEOtoXYZ(elevation + latOrigin, azimuth + lonOrigin, globeDistance);
      lineRenderer.SetPosition (1, otherPos);
   }
}
