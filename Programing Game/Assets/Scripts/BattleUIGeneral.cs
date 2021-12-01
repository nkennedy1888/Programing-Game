using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUIGeneral : MonoBehaviour
{
    private Database localDB;
    public GameObject exitWindow;
    // Start is called before the first frame update
    void Start()
    {
        localDB = GameObject.FindGameObjectWithTag("Player").GetComponent<Database>();
        exitWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleExitWinVis();
        }
    }

    public void ToggleExitWinVis()
    {
        exitWindow.SetActive(!exitWindow.activeSelf);
    }
    public void Exit()
    {
        //Get account type and redir to appropriate "Main - [acctype]" scene
        string accType = localDB.currUser.accountType.ToLower();
        
        if (accType.Equals("teacher"))
        {
            SceneManager.LoadScene("Main - Teacher");
        }
        else if (accType.Equals("student"))
        {
            SceneManager.LoadScene("Main - Student");
        }
        else
        {
            SceneManager.LoadScene("Log In");
        }
    }

    public void Cont()
    {
        exitWindow.SetActive(false);
    }
}