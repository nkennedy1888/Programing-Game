using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Battle : MonoBehaviour
{
    private bool isBattle;
    private GameObject enemy;
    private GameObject player;
    public GameObject battleUI;
    public GameObject battleCam;
    public Question_Parser qPars;
    public BattleUIGeneral bUI;
    public GameObject bullet;
    public float bulletSpeed;
    public int pMax;
    public int eMax;
    private int pAttk;
    private int eAttk;
    private int pCurr;
    private int eCurr;
    public Text pHealth;
    public Text eHealth;
    public Slider pBar;
    public Slider eBar;
    // Start is called before the first frame update
    void Start()
    {
      
        isBattle = false;
        pCurr = pMax;
        eCurr = eMax;


    }

    // Update is called once per frame
    void Update()
    {

        pBar.value = CalculateStat(pCurr, pMax);
        eBar.value = CalculateStat(eCurr, eMax);
        pHealth.text = pCurr + " / " + pMax;
        eHealth.text = eCurr + " / " + eMax;

        if(isBattle && qPars.hit && qPars.attack)
        {
            //add hit anim and attck anim
            StartCoroutine(PlayerAttack());
            StartCoroutine(EnemyAttack());
            qPars.hit = false;
            qPars.attack = false;
            
        }

        if (isBattle)
        {
            
        }
        
        if (isBattle && !qPars.hit && qPars.attack)
        {
            //add attack anim and miss message
            StartCoroutine(EnemyAttack());
            qPars.hit = false;
            qPars.attack = false;
            
        }

        if (isBattle && eCurr == 0)
        {
            ScreenFade sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFade>();
            isBattle = false;
            StartCoroutine(sf.FadeToBlack());
            Destroy(enemy);
            Destroy(player);
            battleUI.SetActive(false);
            battleCam.GetComponent<CinemachineVirtualCamera>().Priority = 5;
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().enabled = true;
            StartCoroutine(sf.FadeToClear());
           
        }

        if(isBattle && pCurr == 0)
        {
            bUI.Exit();
        }



    }


    public void Fight()
    {
        eCurr = eMax;
        pCurr = pMax;
        ScreenFade sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFade>();
        isBattle = true;
        StartCoroutine(sf.FadeToBlack());
        player = GameObject.FindGameObjectWithTag("Respawn").transform.GetChild(0).gameObject;
        enemy = GameObject.FindGameObjectWithTag("EnemySpawn").transform.GetChild(0).gameObject;
        player.tag = "PlayerClone";
        battleUI.SetActive(true);
        battleCam.GetComponent<CinemachineVirtualCamera>().Priority = 15;
        StartCoroutine(sf.FadeToClear());

    }
    float CalculateStat(int currstat, int maxstat)
    {
        float num = (float)currstat / (float)maxstat;

        return num;

    }

    public IEnumerator PlayerAttack()
    {

        GameObject nbullet = Instantiate(bullet, new Vector3(1, 0, 0) + player.transform.position, Quaternion.identity);
        nbullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, nbullet.GetComponent<Rigidbody2D>().velocity.y);
        yield return new WaitForSecondsRealtime(1.7f);

        pAttk = Random.Range(18, 22);
        
        if (eCurr <= pAttk)
        {
            eCurr = 0;
        }
        else
        {
            eCurr -= pAttk;
        }
        enemy.GetComponent<Enemy>().anim.SetBool("IsHit", false);
    }
    public IEnumerator EnemyAttack()
    {
        yield return new WaitForSecondsRealtime(1.7f);

        GameObject nbullet = Instantiate(bullet, new Vector3(-1,0,0) + enemy.transform.position, Quaternion.identity,enemy.transform);
        nbullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, nbullet.GetComponent<Rigidbody2D>().velocity.y);
        yield return new WaitForSecondsRealtime(2.1f);
        if (isBattle)
        {
            eAttk = Random.Range(12, 19);
            if (pCurr<= eAttk)
            {
                pCurr = 0;
            }
            else
            {
                pCurr -= eAttk;
            }
            player.GetComponent<PlayerBattle>().anim.SetBool("IsHit", false);
        }   
    }
}