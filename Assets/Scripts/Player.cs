using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  [SerializeField]
  float playerSpeed;

  private Rigidbody rb;


  [SerializeField]
  GameObject bulletPrefab;

  [SerializeField]
  Transform bulletSpawn;

  // Use this for initialization
  void Start () {
    rb = GetComponent<Rigidbody>();
    playerSpeed = 5;
  }
  // Update is called once per frame
  void Update () {
   if (Input.GetKeyDown(KeyCode.Space)) {
     Fire();
   }
  }

  void Fire() {

    Vector3 playerPos = gameObject.transform.position;
    Vector3 playerDirection = gameObject.transform.forward;
    Quaternion playerRotation = gameObject.transform.rotation;
    float spawnDistance = 10;
    Vector3 spawnPos = playerPos + playerDirection*spawnDistance;

    GameObject bullet = (GameObject)Instantiate(bulletPrefab, spawnPos, playerRotation);

    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

    Destroy(bullet, 2.0f);
  }

  void FixedUpdate() {
    float moveHorizontal = Input.GetAxis ("Horizontal");
    float moveVertical = Input.GetAxis ("Vertical");

    Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

    rb.velocity =  movement * playerSpeed;
  }
}
