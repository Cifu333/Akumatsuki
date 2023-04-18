using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Token : MonoBehaviour {

    public GameObject bulletPrefab;
    private Camera cam;
    PlayerStatus player;
    public float bulletLifeTime;
    private bool bullet;
    public bool justShot;
    private Vector2 mouseWorldPoint;

    public float timeForRespawn;
    private float timeCounter;

    private float bulletCoolCounter;
    public float bulletTime;

    private float time;

    private bool isDown;

    // Start is called before the first frame update
    void Start() {
        player = transform.parent.parent.GetComponent<PlayerStatus>();
        cam = Camera.main;
        bullet = false;
        justShot = false;
    }
   

    // Update is called once per frame
    void Update() {
        Shoot();
        ReSpawn();
    }

    private void Shoot()
    {
        mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        float anguloRadianes = Mathf.Atan2(mouseWorldPoint.x - transform.parent.transform.position.x, mouseWorldPoint.y - transform.parent.transform.position.y);
        float anguloGrados = (180 / Mathf.PI) * anguloRadianes - 90;
        transform.parent.rotation = Quaternion.Euler(0, 0, -anguloGrados);
        if (transform.parent.parent.GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
            transform.parent.rotation = Quaternion.Euler(0, 0, -anguloGrados + 180);

        if (transform.parent.parent.GetComponent<HorizontalMovement>().dash == false)
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
                b.transform.right = mouseWorldPosition - transform.parent.position;
                if (transform.parent.parent.GetComponent<GroundDetector>().grounded == false)
                {
                    transform.parent.parent.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    transform.parent.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (transform.parent.parent.GetComponent<Jump>().force / 2));
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

    private void ReSpawn()
    {
        mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        if (transform.parent.parent.GetComponent<HorizontalMovement>().dash == false)
        {
            if (Input.GetKeyDown(KeyCode.R) && player.free == true && Time.timeScale != 0 && bullet == false)
                isDown = true;

            if (Input.GetKeyUp(KeyCode.R) || bullet == true || player.free == false)
                isDown = false;

            if (isDown == true)
            {
                timeCounter += Time.deltaTime;
                float distanceX = transform.parent.position.x - transform.position.x;
                float distanceY = transform.parent.position.y - transform.position.y;
                float movementX = distanceX / (timeForRespawn / 4f);
                float movementY = distanceY / (timeForRespawn / 4f);
                transform.position += new Vector3(movementX, movementY) * Time.deltaTime;
                if (timeCounter >= timeForRespawn)
                    SceneManager.LoadScene(1);
            }
            if (isDown == false)
            {
                timeCounter = 0;
                float max = Mathf.Sqrt((transform.position.y - transform.parent.position.y) * (transform.position.y - transform.parent.position.y) + (transform.position.x - transform.parent.position.x) * (transform.position.x - transform.parent.position.x));
                if (max < 2)
                {
                    if (transform.parent.parent.GetComponent<HorizontalMovement>().dir == HorizontalMovement.Direction.LEFT)
                        transform.position -= transform.right * Time.deltaTime;
                    else
                        transform.position += transform.right * Time.deltaTime; 
                }
            }


        }
    }
}