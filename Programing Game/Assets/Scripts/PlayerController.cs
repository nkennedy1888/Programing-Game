using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    //private Animator anim;
    public Collider2D objectCollider;
    public Collider2D anotherCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        anotherCollider = GameObject.FindGameObjectWithTag("Platform").GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxisRaw("Horizontal");

        if (hDirection > 0)
        {
            rb.velocity = new Vector2(hDirection * 6, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            //anim.SetBool("running", true);
        }

        else if (hDirection < 0)
        {
            rb.velocity = new Vector2(hDirection * 6, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            //anim.SetBool("running", true);
        }

       else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
           // anim.SetBool("running", false);
        }
       

        if (objectCollider.IsTouching(anotherCollider) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 8f);
        }
    }
}
