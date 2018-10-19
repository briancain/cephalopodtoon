using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  [SerializeField]
  float playerSpeed;
  [SerializeField]
  GameObject bulletPrefab;

  [SerializeField]
  Transform bulletSpawn;

  private Rigidbody rb;

  private float fireDelta = 0.1f;
  private float nextFire = 0.5f;
  private float fireTime = 0.0f;

  private float playerHealth = 100f;

  private bool isDown;

  // Use this for initialization
  void Start () {
    rb = GetComponent<Rigidbody>();
    playerSpeed = 5;
    isDown = false;
  }
  // Update is called once per frame
  void Update () {
   if (Input.GetButton("Fire1")) {
     Fire();
   }

  }

  void Fire() {
    fireTime = fireTime + Time.deltaTime;

    if (fireTime > nextFire) {
      nextFire = fireTime + fireDelta;

      Vector3 playerPos = gameObject.transform.position;
      Vector3 playerDirection = gameObject.transform.forward;
      Quaternion playerRotation = gameObject.transform.rotation;
      float spawnDistance = 2;
      float bulletForce = 20;
      Vector3 spawnPos = playerPos + playerDirection*spawnDistance;
      spawnPos.y = spawnPos.y + 0.5f;

      GameObject bullet = (GameObject)Instantiate(bulletPrefab, spawnPos, playerRotation);

      bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletForce;

      Destroy(bullet, 2.0f);

      nextFire = nextFire - fireTime;
      fireTime = 0.0f;
    }
  }

  void FixedUpdate() {
    float moveHorizontal = Input.GetAxis ("Horizontal");
    float moveVertical = Input.GetAxis ("Vertical");

    float cameraHorizontal = Input.GetAxis ("Horizontal2");
    float cameraVertical = Input.GetAxis ("Vertical2");

    Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

    //Swim();

    rb.velocity =  movement * playerSpeed;
  }

  void Swim() {
    // TODO: FIXME: :p
    Vector3 position = transform.position;
     if (Input.GetButton("Fire2") && !isDown) {
       isDown = true;
       position = Vector2.Lerp(position, new Vector2(position.x, position.y - 1.2f), 0.1f);
     }

     if (Input.GetButtonUp("Fire2") && isDown) {
       isDown = false;
       position = Vector2.Lerp(position, new Vector2(position.x, position.y + 1.2f), 0.1f);
     }
     transform.position = position;
  }

  void OnCollisionEnter(Collision col) {
    if (col.gameObject.CompareTag("Ink")) {
      playerHealth = playerHealth - 10f;
      if (playerHealth <= 0f) {
        // DEATH
        Debug.Log("Player dead");
      }
    }
  }
}
