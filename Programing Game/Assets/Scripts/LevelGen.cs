using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    //Set platform bounds
    public int maxX;
    public int maxY;

    //Set players jump hight
    public int maxJump;

    public GameObject player;
    private GameObject platform;
    private PlatformTemplates templates;
    [HideInInspector] public int color;
    private int randY;
    private GameObject prev;   
    private int x = 0;      
    private int y = 0;

    //public GameObject Brick;

    // Start is called before the first frame update
    void Start()
    {
        //Get parent object
        platform = GameObject.FindGameObjectWithTag("Platform");
        //Get platform templates
        templates = GameObject.FindGameObjectWithTag("Platform").GetComponent<PlatformTemplates>();
        //Chose platform color
        color = Random.Range(0, 4);

        //Set spawn location and name Start
        GameObject spawn = Instantiate(templates.left[color], new Vector3(0.5f,0.5f,0), Quaternion.identity, platform.transform);
        spawn.name = "Start";
        Instantiate(templates.leftBot[color], new Vector3(0.5f, -.5f, 0), Quaternion.identity, platform.transform);
        //Set for last tile spawned
        prev = spawn;

        SpawnBoarder();
       


    }

    // Update is called once per frame
    void Update()
    {
        //Get next tile spawn location
        while (x < maxX && y < maxY)
        {       
            if (prev.tag == "Right")       
            {
                int ytemp = y;       
                randY = Random.Range(-maxJump, maxJump);
                
                if (randY == 0) 
                {
                    randY = 2;
                }

                y+= randY; 
                
                if (y < 0) 
                {
                    y = ytemp + 1;
                }
            }

            x++;
            //spawn new tile and set as last spawn
            prev = spawnTile(prev, x, y);

        }

        //Makes sure the last tile is a right tile
        if (x >= maxX && x < maxX + 1 && prev.tag != "Right") 
        {
            x++;
            Instantiate(templates.right[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
            for (float i = -.5f; i <= y; i++)
            {
                Instantiate(templates.rightBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
            }
        }
       
    }

    //Select the spawn point and type for next tile
    GameObject spawnTile(GameObject prev, int x, int y)
    {
        if (prev.tag == "Left") 
        {
            prev =  Instantiate(templates.middle[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
            for (float i = -.5f; i<= y; i++)
            {
                Instantiate(templates.middleBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
            }
        }
        else 
        {
            if (prev.tag == "Middle") 
            {
                int r = Random.Range(0, 4);
                if (r != 0) 
                {
                    prev = Instantiate(templates.middle[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
                    for (float i = -.5f; i <= y; i++)
                    {
                        Instantiate(templates.middleBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
                    }
                }
                else 
                {
                    prev = Instantiate(templates.right[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
                    for (float i = -.5f; i <= y; i++)
                    {
                        Instantiate(templates.rightBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
                    }
                }
            }
            else 
            {
                if (prev.tag == "Right") 
                {                   
                    prev = Instantiate(templates.left[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
                    for (float i = -.5f; i <= y; i++)
                    {
                        Instantiate(templates.leftBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
                    }
                }
            }
        }
        GameObject prevtag = prev;
        return prevtag;
    }

    public void SpawnBoarder() 
    {
        Instantiate(templates.sideBot[color], new Vector3(-0.5f, -1.5f, 0), Quaternion.identity, platform.transform);
        for (int i = 1; i <= maxY + 1; i++)
        {
            Instantiate(templates.sideMid[color], new Vector3(-0.5f, -1.5f + i, 0), Quaternion.identity, platform.transform);
        }
        Instantiate(templates.sideTop[color], new Vector3(-0.5f, -1.5f + maxY + 2, 0), Quaternion.identity, platform.transform);

        Instantiate(templates.sideBot[color], new Vector3(-0.5f + maxX + 3, -1.5f, 0), Quaternion.identity, platform.transform);
        for (int i = 1; i <= maxY + 1; i++)
        {
            Instantiate(templates.sideMid[color], new Vector3(-0.5f + maxX + 3, -1.5f + i, 0), Quaternion.identity, platform.transform);
        }
        Instantiate(templates.sideTop[color], new Vector3(-0.5f + maxX + 3, -1.5f + maxY + 2, 0), Quaternion.identity, platform.transform);

        Instantiate(templates.floatLeft[color], new Vector3(0.5f, -1.5f, 0), Quaternion.identity, platform.transform);
        for (int i = 1; i <= maxX; i++)
        {
            Instantiate(templates.floatMid[color], new Vector3(0.5f + i, -1.5f, 0), Quaternion.identity, platform.transform);
        }
        Instantiate(templates.floatRight[color], new Vector3(0.5f + maxX + 1, -1.5f, 0), Quaternion.identity, platform.transform);

        Instantiate(templates.floatLeft[color], new Vector3(0.5f, -1.5f + maxY + 2, 0), Quaternion.identity, platform.transform);
        for (int i = 1; i <= maxX; i++)
        {
            Instantiate(templates.floatMid[color], new Vector3(0.5f + i, -1.5f + maxY + 2, 0), Quaternion.identity, platform.transform);
        }
        Instantiate(templates.floatRight[color], new Vector3(0.5f + maxX + 1, -1.5f + maxY + 2, 0), Quaternion.identity, platform.transform);
    }

    /*Creates Underground Dirt
    if(GameObject.FindWithTag("Left") != null)
    {
        Instantiate(Brick, new Vector3(1, 1, 0), Quaternion.identity);
    }
    if(GameObject.FindWithTag("Middle") != null)
    {

    }
    if(GameObject.FindWithTag("Left") != null)
    {

    }
    */
}
