using UnityEngine;
using UnityEngine.SceneManagement;
public class AvatarData : MonoBehaviour
{
    public bool isBackdrop;
    public PlayerController playerController;
    public RuntimeAnimatorController[] avatars;
    public int index;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        //gets current scene and disables player movement if not the battle scene
        if (SceneManager.GetActiveScene().name == "Battle")
        {
            isBackdrop = false;
        }
        else
        {
            isBackdrop = true;
        }
        
        if (isBackdrop)
        {
            playerController.enabled = false;

            //gets random avatar for login scene
            if (SceneManager.GetActiveScene().name == "Log in")
            {
                index = Random.Range(0, 4);
            }
                       
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //gets current scene and gets player avatar if not log in scene
        if (SceneManager.GetActiveScene().name != "Log in") 
        {
            index = player.GetComponent<Database>().currUser.avatarID;
        }
        
       
        player.GetComponent<Animator>().runtimeAnimatorController = avatars[index];
    }
}
