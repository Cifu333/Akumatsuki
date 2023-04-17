using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 500.0f;
    Rigidbody2D rb;
    WeaponDamage wp;

    // Start is called before the first frame update
    void Start()
    {
        wp = GetComponent<WeaponDamage>();
        rb = GetComponent<Rigidbody2D>();
        if (wp.hazardus == false)
            rb.AddForce(rb.transform.right * speed);
        else
        {
            if (transform.parent.GetComponent<EnemyMovement>().dir == EnemyMovement.Direction.RIGHT)
                rb.AddForce(rb.transform.right * speed);
            else
                rb.AddForce(rb.transform.right * -speed);
        }
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy" && wp.hazardus == false) 
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Player" && wp.hazardus == true)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 3) 
        {
            Destroy(gameObject);
        }
    }
}

