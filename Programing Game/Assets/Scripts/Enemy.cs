using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject  playerSpot;
    public GameObject player;
    private Collider2D coll;
    private GameObject enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        playerSpot = GameObject.FindGameObjectWithTag("Respawn");
        enemy = GameObject.FindGameObjectWithTag("EnemySpawn");
        coll = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.collider == coll) 
        {
            GameObject playerClone = Instantiate(player, new Vector3(0.5f , 0.5f , 0), Quaternion.identity, playerSpot.transform);
            playerClone.transform.position = playerSpot.transform.position;

            this.gameObject.transform.position = enemy.transform.position;
            this.gameObject.transform.SetParent(enemy.transform, true);
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Battle spot").gameObject.GetComponent<Battle>().Fight();
            
        }
    }
}
