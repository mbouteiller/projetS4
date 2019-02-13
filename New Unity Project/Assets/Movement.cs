using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    public float speed;
    public float jumpSpeed = 8;
    private Rigidbody rb;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += jumpSpeed * Vector3.up;
        }
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            if(other.name == "Blue flag")
            {

                rend.material.color = Color
            }
        }
    }
}
