using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpot : MonoBehaviour
{
    public int maxX;
    public int maxY;
    private GameObject background;
    private GameObject[] tilesBG;
    private GameObject platform;
    private PlatformTemplates tilesPF; 
    private GameObject battlespotBG;
    private GameObject battlespotPF;
    private int colorBG;
    private int colorPF;
    private bool isfull = false;

    // Start is called before the first frame update
    void Start()
    {
        battlespotBG = GameObject.FindGameObjectsWithTag("Battlespot")[0];
        battlespotPF = GameObject.FindGameObjectsWithTag("Battlespot")[1];

    }

    // Update is called once per frame
    void Update()
    {
        background = GameObject.FindGameObjectWithTag("Background");        
        tilesBG = background.GetComponent<BackgroundGen>().backgrounds;
        colorBG = background.GetComponent<BackgroundGen>().color;


        platform = GameObject.FindGameObjectWithTag("Platform");
        tilesPF = platform.GetComponent<PlatformTemplates>();
        colorPF = platform.GetComponent<LevelGen>().color;

        while (!isfull)
        {
            Instantiate(tilesPF.floatLeft[colorPF], battlespotPF.transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity, battlespotPF.transform);
            for (int x = 1; x < maxX - 5; x++)
            {
                Instantiate(tilesPF.floatMid[colorPF], battlespotPF.transform.position + new Vector3(0.5f + x, 0.5f, 0), Quaternion.identity, battlespotPF.transform);
            }

            Instantiate(tilesPF.floatRight[colorPF], battlespotPF.transform.position + new Vector3(0.5f + maxX-5, 0.5f, 0), Quaternion.identity, battlespotPF.transform);

            for (int y = 0; y < maxY; y += 4)
            {
                for (int x = 0; x < maxX; x += 4)
                {                  
                    Instantiate(tilesBG[colorBG], battlespotBG.transform.position + new Vector3(x, y, 0), Quaternion.identity, battlespotBG.transform);
                }
            }

            isfull = true;
        }
    }
}
