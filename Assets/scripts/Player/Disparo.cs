using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour {

    public GameObject bulletPrefab;
    private Camera cam;
    PlayerStatus player;
    public float bulletLifeTime;
    private bool bullet;
    public bool justShot;
    private Vector2 mouseWorldPoint;


    private float bulletCoolCounter;
    public float bulletTime;

    private float time;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<PlayerStatus>();
        cam = Camera.main;
        bullet = false;
        justShot = false;
    }
   

    // Update is called once per frame
    void Update() {
        mouseWorldPoint = cam.ScreenToViewportPoint(Input.mousePosition);
        if (GetComponent<HorizontalMovement>().dash == false)
        {
            if (Input.GetMouseButton(0) && player.free == true && Time.timeScale != 0 && bullet == false)
            {
                if (bulletCoolCounter <= 0)
                {
                    bullet = true;
                    justShot = true;
                    bulletCoolCounter = bulletTime;

                }
                Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPosition.z = 0;
                GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation);
                b.transform.right = mouseWorldPosition - transform.position;
                if (GetComponent<GroundDetector>().grounded == false)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * (GetComponent<Jump>().force / 2));
                }
                Destroy(b, bulletLifeTime);
            }
        }

        if (bullet == true)
        {
            time += Time.deltaTime;
            if (time >= 0.2f)
            {
                time = 0;
                justShot = false;
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