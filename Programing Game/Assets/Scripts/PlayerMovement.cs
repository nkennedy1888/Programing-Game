using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    public const string jumpKey = "Space";
    public const string leftKey = "A";
    public const string rightKey = "D";


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(jumpKey)) {
            rb.AddForce(new Vector2(0f, 0f), ForceMode2D.Impulse);
        }
        if (Input.GetButtonDown(leftKey)) {
            rb.AddForce(new Vector2(speed, 0f), ForceMode2D.Impulse);
        }
        if (Input.GetButtonDown(rightKey)) {
            rb.AddForce(new Vector2(-speed, 0f), ForceMode2D.Impulse);
        }
    }
}
