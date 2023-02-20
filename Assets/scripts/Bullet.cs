using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 500.0f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(rb.transform.right * speed);
    }
}

