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

        if (isBattle && !qPars.hit && qPars.attack)
        {
            //add attack anim and miss message
            StartCoroutine(EnemyAttack());
            qPars.hit = false;
            qPars.attack = false;
        }






    }

    public void Fight()
    {
        ScreenFade sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFade>();
        isBattle = true;
        StartCoroutine(sf.FadeToBlack());
        player = GameObject.FindGameObjectWithTag("Respawn").transform.GetChild(0).gameObject;
        enemy = GameObject.FindGameObjectWithTag("EnemySpawn").transform.GetChild(0).gameObject;
        battleUI.SetActive(true);
        battleCam.GetComponent<CinemachineVirtualCamera>().Priority = 15;
        StartCoroutine(sf.FadeToClear());
        enemy.GetComponent<Enemy>().isColl = false;

    }
    float CalculateStat(int currstat, int maxstat)
    {
        float num = (float)currstat / (float)maxstat;

        return num;

    }

    public IEnumerator PlayerAttack()
    {
       
        yield return new WaitForSecondsRealtime(1.7f);

        pAttk = Random.Range(18, 22);
        eCurr -= pAttk;
    }
    public IEnumerator EnemyAttack()
    {
        
        yield return new WaitForSecondsRealtime(2.4f);
        eAttk = Random.Range(12, 19);
        pCurr -= eAttk;
    }
}