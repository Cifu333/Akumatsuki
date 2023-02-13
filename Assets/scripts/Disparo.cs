using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour {

    public GameObject bulletPrefab;
    private Camera cam;
    private Vector2 mouseWorldPoint;

    // Start is called before the first frame update
    void Start() {

        cam = Camera.main;
    }
   

    // Update is called once per frame
    void Update() {
        mouseWorldPoint = cam.ScreenToViewportPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void Shoot() {
        GameObject bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        bulletComponent.Project((mouseWorldPoint - playerPos).normalized);
    }
}