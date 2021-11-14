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
    public GameObject finish;
    public GameObject player;
    public GameObject[] enemy;
    private GameObject platform;
    private PlatformTemplates templates;
    [HideInInspector] public int color;
    private int randY;
    private GameObject prev;   
    private int x = 0;      
    private int y = 0;
    private int enemies;
    private int numEnemy = 0;
    

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

        enemies = Random.Range(4, 10);

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
            GameObject prev2 = prev;
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
            spawnRight(prev, prev2, x, y);

        }

        //Makes sure the last tile is a right tile
        if (x >= maxX-1 && x < maxX + 1 && prev.tag != "Right") 
        {
            x++;
            Instantiate(templates.right[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
            Instantiate(finish, new Vector3(0.5f + x, 0.5f + y + 1, 0), Quaternion.identity, platform.transform);
            for (float i = -.5f; i <= y; i++)
            {
                Instantiate(templates.rightBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
            }
        }

        if (x >= maxX-1 && x <= maxX +1 && prev.tag == "Right")
        {
            x++;
            Instantiate(templates.middle[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
            for (float i = -.5f; i <= y; i++)
            {
                Instantiate(templates.middleBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
            }

            Instantiate(templates.right[color], new Vector3(0.5f + x + 1, 0.5f + y, 0), Quaternion.identity, platform.transform);
            Instantiate(finish, new Vector3(0.5f + x + 1, 0.5f + y + 1, 0), Quaternion.identity, platform.transform);
            for (float i = -.5f; i <= y; i++)
            {
                Instantiate(templates.rightBot[color], new Vector3(0.5f + x + 1, i, 0), Quaternion.identity, platform.transform);
            }
        }

        //spawns random enemies
        while (numEnemy <= enemies)
        {
            GameObject[] middles = GameObject.FindGameObjectsWithTag("Middle");
            GameObject[] nearestEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            int index = Random.Range(4, middles.Length);

            if (numEnemy != 0)
            {
                for (int i = 0; i < nearestEnemy.Length; i++)
                {
                    if ((nearestEnemy[i].transform.position.x < middles[index].transform.position.x - 2) || (nearestEnemy[i].transform.position.x > middles[index].transform.position.x + 2))
                    {
                        SpawnEnemy(middles[index]);
                        numEnemy++;
                        break;
                    }
                    else
                    {
                        index = Random.Range(4, middles.Length);
                    }
                }
            }
            else
            {
                SpawnEnemy(middles[index]);
                numEnemy++;
            }

        }

    }

    //Select the spawn point and type for next tile
    GameObject spawnTile(GameObject prev, int x, int y)
    {
        GameObject prevtag = prev;
        if (prev.tag == "Left") 
        {
            prevtag =  Instantiate(templates.middle[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
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
                    prevtag = Instantiate(templates.middle[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
                    for (float i = -.5f; i <= y; i++)
                    {
                        Instantiate(templates.middleBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
                    }
                }
                else 
                {
                    prevtag = Instantiate(templates.right[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
                }
            }
            else 
            {
                if (prev.tag == "Right") 
                {                   
                    prevtag = Instantiate(templates.left[color], new Vector3(0.5f + x, 0.5f + y, 0), Quaternion.identity, platform.transform);
                    for (float i = -.5f; i <= y; i++)
                    {
                        if (i >= prev.transform.position.y)
                        {
                            Instantiate(templates.leftBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
                        }
                        else
                        {
                            Instantiate(templates.middleBot[color], new Vector3(0.5f + x, i, 0), Quaternion.identity, platform.transform);
                        }
                        
                    }
                }
            }
        }
        return prevtag;
    }

    void spawnRight(GameObject curr, GameObject prev, int x, int yNext) 
    {
        if (prev.tag == "Right")
        {            
            for (float i = -.5f; i < prev.transform.position.y; i++)
            {
                if (i >= yNext)
                {
                    Instantiate(templates.rightBot[color], new Vector3(0.5f + x - 1, i, 0), Quaternion.identity, platform.transform);
                }
                else
                {
                    Instantiate(templates.middleBot[color], new Vector3(0.5f + x - 1, i, 0), Quaternion.identity, platform.transform);
                }

            }
        }
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

   void SpawnEnemy(GameObject plat) 
    {
        int i = Random.Range(0, enemy.Length);
        Instantiate(enemy[i], plat.transform.position + new Vector3(0, 1, 0), Quaternion.identity, platform.transform);        
    }

   
}
