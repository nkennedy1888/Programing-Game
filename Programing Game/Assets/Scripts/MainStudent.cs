using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainStudent : MonoBehaviour
{

    public GameObject settings_Parent, account_Parent, changePassParent;
    private Database localDB;

    string newPass, confPass;
    // Start is called before the first frame update
    void Start()
    {
        localDB = GameObject.FindGameObjectWithTag("Player").GetComponent<Database>();

        account_Parent.SetActive(false);
        settings_Parent.SetActive(false);
        changePassParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetNewPass(string temp)
    {
        newPass = temp;
    }

    public void GetConfPass(string temp)
    {
        confPass = temp;
    }

    public void ToggleSettingsVis()
    {
        settings_Parent.SetActive(!settings_Parent.activeSelf);
    }

    public void ToggleAccountVis()
    {

        account_Parent.SetActive(!account_Parent.activeSelf);


    }

    public void TogglePassChangeVis()
    {
        changePassParent.SetActive(!changePassParent.activeSelf);
    }

    public void NavigateToBattle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void ChangePassword()
    {
        Debug.Log("Current username: " + PlayerPrefs.GetString("username"));
        if (!newPass.Equals(confPass))
        {
            //"Passwords must match" dialogue
            return;
        }
        else
        {
            localDB.users[PlayerPrefs.GetString("username")].password = newPass;
        }
        

    }

    //Clears playerprefs and redirects to Log in scene
    public void LogOut()
    {
        PlayerPrefs.DeleteKey("username");
        SceneManager.LoadScene("Log in");
    }

    public string GetUsername()
    {
        return PlayerPrefs.GetString("username");
    }

}
