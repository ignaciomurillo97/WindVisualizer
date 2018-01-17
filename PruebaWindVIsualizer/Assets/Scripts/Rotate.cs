using System.Collections;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public float rotSpeed = 20;

   void Start(){
      
   }

   void Update(){
      if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0)){
         float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
         float rotY = Input.GetAxis("Mouse Y") * -rotSpeed * Mathf.Deg2Rad;

         transform.Rotate(new Vector3(0, rotX, 0), Space.World);
         transform.Rotate(new Vector3(rotY, 0, 0), Space.Self);
      }
   }
}
