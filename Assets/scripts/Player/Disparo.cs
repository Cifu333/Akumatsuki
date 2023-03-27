using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour {

    public GameObject bulletPrefab;
    private Camera cam;
    PlayerStatus player;
    public float bulletLifeTime;
    public bool bullet;
    private Vector2 mouseWorldPoint;


    private float bulletCounter;
    private float bulletCoolCounter;
    public float bulletDuration = 0.25f;
    public float bulletTime = 0.25f;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerStatus>();
        cam = Camera.main;
        bullet = false;
    }
   

    // Update is called once per frame
    void Update() {
        mouseWorldPoint = cam.ScreenToViewportPoint(Input.mousePosition);
        if (GetComponent<HorizontalMovement>().dash == false)
        {
            if (Input.GetMouseButtonDown(0) && player.free == true && Time.timeScale != 0)
            {
                if (bulletCoolCounter <= 0 && bulletCounter <= 0)
                {
                    bullet = true;
                    bulletCounter = bulletDuration;
                }
                Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPosition.z = 0;
                GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation);
                b.transform.right = mouseWorldPosition - transform.position;
                if (GetComponent<GroundDetector>().grounded == false)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * (GetComponent<Jump>().force / 4));
                }
                Destroy(b, bulletLifeTime);
            }
        }

        if (bulletCounter > 0)
        {
            bulletCounter -= Time.deltaTime;
            if (bulletCounter <= 0)
            {
                bulletCoolCounter = bulletTime;
            }
        }

        if (bulletCoolCounter > 0f)
        {
            bulletCoolCounter -= Time.deltaTime;
            if (bulletCoolCounter <= 0)
            {
                bullet = false;
            }
        }
    }
}