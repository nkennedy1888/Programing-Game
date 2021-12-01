using UnityEngine;

public class BackgroundGen : MonoBehaviour
{
    //Array of background color tiles
    public GameObject[] backgrounds;
    
    //Set background size
    public int maxX;
    public int maxY;
    private GameObject background;
    [HideInInspector] public int color;

    // Start is called before the first frame update
    void Start()
    {
        //Gets parent object 
        background = GameObject.FindGameObjectWithTag("Background");
        //Gets random background color
        color = Random.Range(0, backgrounds.Length);

        //Spawn background
        for (int y = 0; y < maxY; y+=4)
        {
            for (int x = 0; x < maxX; x+=4)
            {
                Instantiate(backgrounds[color], new Vector3(x, y, 0), Quaternion.identity, background.transform);
            }
        }
    }
}
