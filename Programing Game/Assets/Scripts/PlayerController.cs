using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Collider2D objectCollider;
    public Collider2D wallCollider;
    public Collider2D anotherCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anotherCollider = GameObject.FindGameObjectWithTag("Platform").GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(hDirection));
        anim.SetBool("IsJumping", rb.velocity.y > 0.01);
        anim.SetBool("IsFalling", rb.velocity.y < -0.01);

        if (hDirection > 0)
        {
            rb.velocity = new Vector2(hDirection * 6, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        else if (hDirection < 0)
        {
            rb.velocity = new Vector2(hDirection * 6, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);         
        }

       else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);          
        }
       

        if ((objectCollider.IsTouching(anotherCollider) || anim.GetBool("isWalling")) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 8f);
        }

        if (wallCollider.IsTouching(anotherCollider) && !objectCollider.IsTouching(anotherCollider))
        {
            anim.SetBool("isWalling", true);

        }
        else
        {
            anim.SetBool("isWalling", false);
        }
    }
}
