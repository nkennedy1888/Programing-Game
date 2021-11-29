using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndTrigger : MonoBehaviour
{
    private BattleUIGeneral bui;
    void Start()
    {
        bui = GameObject.FindGameObjectWithTag("UI").GetComponent<BattleUIGeneral>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.collider.GetType() == typeof(CapsuleCollider2D))
        {
            bui.Exit();
        }
    }
}