using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Battle : MonoBehaviour
{
    private bool isBattle;
    private GameObject enemy;
    private GameObject player;
    public GameObject battleUI;
    public GameObject battleCam;

    // Start is called before the first frame update
    void Start()
    {
        isBattle = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Fight() 
    {
        isBattle = true;

        player = GameObject.FindGameObjectWithTag("Respawn").transform.GetChild(0).gameObject;
        enemy = GameObject.FindGameObjectWithTag("EnemySpawn").transform.GetChild(0).gameObject;
        battleUI.SetActive(true);
        battleCam.GetComponent<CinemachineVirtualCamera>().Priority = 15;


    }
}
