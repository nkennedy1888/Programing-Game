using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleUIGeneral : MonoBehaviour
{
    public GameObject exitWindow;
    //public Button exit, cont;
    // Start is called before the first frame update
    void Start()
    {
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
        string accType = GetAccTypeFromFile();

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
            Debug.Log("Error has occurred, redirecting to Login");
            SceneManager.LoadScene("Log In");
        }
    }
    public void Cont()
    {
        exitWindow.SetActive(false);
    }

    string GetAccTypeFromFile()
    {
        string type = "";
        string line;
        string path = "Assets/SaveData/users.txt";
        StreamReader reader = new StreamReader(path);

        while ((line = reader.ReadLine()) != null && line != "")
        {
            if (line.StartsWith("name") && line.Substring(line.IndexOf(":") + 2).Equals(PlayerPrefs.GetString("username")))
            {
                reader.ReadLine();
                line = reader.ReadLine();
                type = line.Substring(line.IndexOf(":") + 2);
            }
        }
        reader.Close();
        return type;
    }
}