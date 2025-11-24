// ISTA 425 / INFO 525 Algorithms for Games
//
// Sample code file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
 // public GameObject player;
 //  public Vector3 offset = new Vector3(-1.73f, 5.48f, -14.86f);

 //  private void Start()
 // {
 //      this.transform.rotation = player.transform.rotation;
 //      this.transform.position = player.transform.position + offset;
 // }
 // private void FixedUpdate()
 // {
 //      this.transform.position = player.transform.position + offset;
 //  }


 // Used Week 9 Camera Slides and ChatGPT as second opinion
 public Transform target;
 public Vector3 offset = new Vector3(0, 5, -10);
 //public Vector3 baseOffset = new Vector3(0, 5, -10); // testing for different version
 public float springConstant = 50f;
 public float dampConstant = 10f;       // 10f is normal; 5f for different version
 public float zoomSpeed = 5f;
 public float minZoom = 5f;
 public float maxZoom = 20f;
 public float defaultZoom = 10f;

 private Vector3 velocity = Vector3.zero;
 private float currentZoom;
 private Vector3 actualPosition; //for different version and another version

 void Start()
 {
  currentZoom = defaultZoom;
  actualPosition = transform.position; //for different version and another version
 }

 void Update()
 {

  //transform.Translate(new Vector3(0, 0, 0.01f));
  HandleZoom();
 }

 void LateUpdate()
  //{
 // if (target == null) return;

  //Vector3 desiredPosition = target.position + offset.normalized * currentZoom;
  //Vector3 displacement = desiredPosition - transform.position;

  // Spring equation: F= kx - dv
 //  velocity += displacement * springConstant * Time.deltaTime;
 //  velocity *= 1f - dampConstant * Time.deltaTime;

 // transform.position += velocity * Time.deltaTime;
 //  transform.LookAt(target);
 // }
 
 //Another version
 {
  if (target == null) return;
  
  //Vector3 idealPosition = target.position + offset.normalized * currentZoom;
  
  // Convert offset into hDist and vDist as per course pseudocode
  float hDist = offset.z; //distance behind the target need to check
  float vDist = offset.y; // vertical distance above the target need to check
 
  //Pseudocode: ideal = pos - forwardhDist + upvDist
  Vector3 idealPosition = target.position - target.forward * hDist * currentZoom + target.up * vDist * currentZoom; // better check
  
  Vector3 displacement = actualPosition - idealPosition; //correct displacement: actual - ideal
  
  Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);
  
  velocity += springAccel * Time.deltaTime;
  
  actualPosition += velocity * Time.deltaTime;
  
  transform.position = actualPosition;
  transform.LookAt(target);
 }

  // different version
 //{
 // if (target == null) return;

 // Vector3 offset = baseOffset.normalized * currentZoom;
  
  //Vector3 idealPosition = target.position + offset;
  
 // Vector3 displacement = actualPosition - idealPosition;

 // Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);
  
 // velocity += springAccel * Time.deltaTime;
 // actualPosition += velocity * Time.deltaTime;

 // transform.position = actualPosition;
  
 // transform.LookAt(target);

 // transform.Translate(new Vector3(0, 0, 0.1f), Space.World); //For testing
 //}
 
 
 
 
 
 

 void HandleZoom()
 {
  if (Input.GetKey(KeyCode.Equals) || Input.GetKey(KeyCode.KeypadPlus)) // Zoom in
  {
   currentZoom -= zoomSpeed * Time.deltaTime;
  }

  if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus)) // Zoom out
  {
   currentZoom += zoomSpeed * Time.deltaTime;
  }

  if (Input.GetKeyDown(KeyCode.Backspace)) // Reset zoom
  {
   currentZoom = defaultZoom;
  }

  currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
 }
 
 
 
 // Different Version
 //if (Input.GetKey(KeyCode.Plus) || 
    // Input.GetKey(KeyCode.KeypadPlus) || // Zoom in
    // Input.GetKey(KeyCode.Equals))
 //{
 // currentZoom -= zoomSpeed * Time.deltaTime;
 //}

//if (Input.GetKey(KeyCode.Minus) || 
   // Input.GetKey(KeyCode.KeypadMinus) || // Zoom out
   // Input.GetKey(KeyCode.Underscore)) //shift-minus keyboards
//{
 //currentZoom += zoomSpeed * Time.deltaTime;
//}

//if (Input.GetKeyDown(KeyCode.Backspace)) // Reset zoom
//{
 //currentZoom = defaultZoom;
//}

//currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
//}
 
 
 
 
 
 
}
