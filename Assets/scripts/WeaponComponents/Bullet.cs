using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 500.0f;
    Rigidbody2D rb;
    WeaponDamage wp;
    private bool impact;

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
            {
                rb.AddForce(rb.transform.right * speed);
            }
            else
            {
                rb.AddForce(rb.transform.right * -speed);
            }
            transform.parent = null;
        }
        impact = false;
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("Impact", impact); 
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy" && wp.hazardus == false) 
        {
            impact = true;
        }
        if (collision.tag == "Player" && wp.hazardus == true)
        {
            impact = true;
        }
        if (collision.gameObject.layer == 3)
        {
            impact = true;
        }
        if (collision.gameObject.layer == 7)
        {
            impact = true;
        }
    }

    void Deactivate()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}

