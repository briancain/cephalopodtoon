using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

  private Rigidbody rb;
  // Use this for initialization
  void Start () {
    rb = GetComponent<Rigidbody>();
  }
  // Update is called once per frame
  void Update () {
  }

  void OnCollisionEnter(Collision collider) {
    if (collider.gameObject.CompareTag("Background")) {
      rb.velocity = new Vector3(0,0,0);
    }
  }
}
