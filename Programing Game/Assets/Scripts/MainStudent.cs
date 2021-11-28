using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainStudent : MonoBehaviour
{

    public GameObject settings_Parent, account_Parent, changePassParent;

    string newPass, confPass;
    private string path = "Assets/SaveData/users.txt";
    // Start is called before the first frame update
    void Start()
    {
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

    void changePassword()
    {
        if (!newPass.Equals(confPass))
        {
            //"Passwords must match" dialogue
            return;
        }
        

    }

    public string GetUsername()
    {
        return PlayerPrefs.GetString("username");
    }

    string GetPasswordFromFile()
    {
        
        string pass = "";
        string line;

        StreamReader reader = new StreamReader(path);

        while ((line = reader.ReadLine()) != null && line != "")
        {
            if (line.StartsWith("name") && line.Substring(line.IndexOf(":") + 2).Equals(GetUsername()))
            {
                line = reader.ReadLine();
                pass = line.Substring(line.IndexOf(":") + 2);
                break;
            }
        }
        reader.Close();
        return pass;
    }

    //Implement method to change password.

}
