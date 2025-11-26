// ISTA 425 / INFO 525 Algorithms for Games
//
// Sample code file

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
 // Used Week 9 Camera Slides, ChatGPT as second opinion, and help from the teacher.
 public Transform target;
 public Vector3 offset = new Vector3(0, 5, -10);
 public float springConstant = 50f;
 public float dampConstant = 10f;       
 public float zoomSpeed = 5f;
 public float minZoom = 5f;
 public float maxZoom = 20f;
 public float defaultZoom = 10f;

 private Vector3 velocity = Vector3.zero;
 private float currentZoom;
 private Vector3 actualPosition; 

 void Start()
 {
  currentZoom = defaultZoom;
  actualPosition = transform.position - target.forward * offset.z * currentZoom + target.up * offset.y * currentZoom; 
 }

 void Update()
 {
  HandleZoom();
 }

 void LateUpdate()
 {
  if (target == null) return;
  
  // Convert offset into hDist and vDist as per course pseudocode
  float hDist = offset.z; //distance behind the target need to check
  float vDist = offset.y; // vertical distance above the target need to check
 
  //Pseudocode: ideal = pos - forwardhDist + upvDist
  Vector3 idealPosition = target.position - target.forward * hDist * currentZoom + target.up * vDist * currentZoom; 
  
  Vector3 displacement = actualPosition - idealPosition; 
  
  Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);
  
  velocity += springAccel * Time.deltaTime;
  
  actualPosition += velocity * Time.deltaTime;
  
  transform.position = actualPosition;
  transform.LookAt(target);
 }
 
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
}
