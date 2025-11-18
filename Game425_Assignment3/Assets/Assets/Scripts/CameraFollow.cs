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
 public float springConstant = 50f;
 public float dampConstant = 10f;
 public float zoomSpeed = 5f;
 public float minZoom = 5f;
 public float maxZoom = 20f;
 public float defaultZoom = 10f;

 private Vector3 velocity = Vector3.zero;
 private float currentZoom;

 void Start()
 {
  currentZoom = defaultZoom;
 }

 void Update()
 {
  HandleZoom();
 }

 void LateUpdate()
 {
  if (target == null) return;

  Vector3 desiredPosition = target.position + offset.normalized * currentZoom;
  Vector3 displacement = desiredPosition - transform.position;

  // Spring equation: F= kx - dv
  velocity += displacement * springConstant * Time.deltaTime;
  velocity *= 1f - dampConstant * Time.deltaTime;

  transform.position += velocity * Time.deltaTime;
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
