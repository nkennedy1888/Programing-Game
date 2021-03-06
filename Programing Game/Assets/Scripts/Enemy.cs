using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject  playerSpot;
    public GameObject player;
    private Collider2D coll;
    private GameObject enemy;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerSpot = GameObject.FindGameObjectWithTag("Respawn");
        enemy = GameObject.FindGameObjectWithTag("EnemySpawn");
        coll = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.collider.GetType() == typeof(CapsuleCollider2D)) 
        {            
            GameObject playerClone = Instantiate(player, new Vector3(0.5f , 0.5f , 0), Quaternion.identity, playerSpot.transform);
            playerClone.transform.position = playerSpot.transform.position;
            this.gameObject.transform.position = enemy.transform.position;
            this.gameObject.transform.SetParent(enemy.transform, true);
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Battle spot").gameObject.GetComponent<Battle>().Fight();            
        }
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
