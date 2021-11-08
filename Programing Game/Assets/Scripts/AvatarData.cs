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

            index = Random.Range(0, 4);
        }
        // isBackdrop = GameObject.FindGameObjectWithTag("Backdrop").activeInHierarchy;

        //Debug.Log(GameObject.FindGameObjectWithTag("Backdrop"));
    }

    // Update is called once per frame
    void Update()
    {

        index = player.GetComponent<Database>().currUser.avatarID;

        

        player.GetComponent<Animator>().runtimeAnimatorController = avatars[index];
    }
}
