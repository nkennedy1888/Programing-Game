using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            anim.SetBool("IsHit", true);
            Destroy(collision.gameObject);
        }
    }
}
