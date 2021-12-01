using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Database : MonoBehaviour
{   
    public new string  name;
    [HideInInspector]public UserData currUser;    
    public Dictionary<string, UserData> users= new Dictionary<string, UserData>();

    public void Start()
    {    
        if (!File.Exists(Application.streamingAssetsPath + "/data.stuff"))
        {
            SaveData.CreateFile();
        }
        users = SaveData.LoadData();

        if (!SceneManager.GetActiveScene().name.Equals("Log in"))
        {   
            //gets current users name from system
            name = PlayerPrefs.GetString("username");

            //checks if user exists in database alread
            if (users.ContainsKey(name))
            {
                //sets current users data to correct data from database
                currUser = users[name];
            }     
        }
    }

    public void OnDestroy()
    {
        if(!SceneManager.GetActiveScene().name.Equals("Log in") && this.tag == "Player")
        {
            //saves user data when scene is changed in game
            users[name] = currUser;
            SaveData.SaveGame(users);
        }
    }

    //This should ONLY be ran in the log in scene; directly adds a user to the database, no usage of playerprefs is included, though it would be wise to set the playerpref accordingly after calling this function
    public void AddNewUser(string m_name, string m_password, string m_accType, int m_classCode)
    {
        if (!HasUser(m_name))
        {
            currUser = new UserData(m_name, m_password, m_accType, m_classCode, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            users.Add(m_name, currUser);   
            SaveData.SaveGame(users);
        } 
    }

    public bool HasUserandPassword(string m_name, string m_password)
    {
        //Returns true if username and corresponding password match what's in the database; else return false
        if (HasUser(m_name) && users[m_name].password.Equals(m_password))
        {
            return true;
        }
        return false;
    }  

    public bool HasUser(string tempUser)
    {
        if (users.ContainsKey(tempUser))
        {
            return true;
        }
        return false;
    }

    //Removes current user entry from playerprefs; redirects to login scene
    public void LogOut()
    {
        PlayerPrefs.DeleteKey("username");
        SceneManager.LoadScene("Log In");
    }
}
