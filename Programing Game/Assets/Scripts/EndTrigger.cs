using UnityEngine;

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