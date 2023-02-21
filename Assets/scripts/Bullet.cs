using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 500.0f;
    public float damage = 5;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(rb.transform.right * speed);
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            collision.GetComponent<EnemyLife>().hp -= damage;
            Destroy(gameObject);
        }
    }
}

