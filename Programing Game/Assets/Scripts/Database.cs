using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Database : MonoBehaviour
{
    
    private new string  name;
    [HideInInspector]public UserData currUser;
    
    public Dictionary<string, UserData> users= new Dictionary<string, UserData>();


    public void Start()
    {
        if (!File.Exists("Assets/SaveData/stats.txt"))
        {
            SaveData.CreateFile();
        }

        users = SaveData.LoadData();

        Debug.Log(SceneManager.GetActiveScene().name.ToString() + " is the current scene");

        if (!SceneManager.GetActiveScene().name.Equals("Log in"))
        {
            PlayerPrefs.DeleteAll();


            //gets data from save file when a new scene loads
            
            //gets current users name from system
            name = PlayerPrefs.GetString("username");
            //sets current users data to defualt

            //currUser = new UserData(name, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);



            //checks if user exists in database alread
            if (users.ContainsKey(name))
            {
                //sets current users data to correct data from database
                currUser = users[name];
            }
            else
            {
                currUser = new UserData(name, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                //adds current user as a new database entry
                users.Add(name, currUser);
                //saves update
                SaveData.SaveGame(users);

            }

            Debug.Log("mDB_User: " + currUser.username + " mDB_Pass: " + currUser.password);
        }
    }
    public void Update()
    {
        
    }


    public void OnDestroy()
    {
        if(!SceneManager.GetActiveScene().name.Equals("Log in"))
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
            Debug.Log("User succesfully added via Database class - AddNewUser function");
            Debug.Log("Username stored: " + users[m_name].username + " Password stored: " +users[m_name].password);
            
            SaveData.SaveGame(users);
            Debug.Log("Save succesful");
        }
        else
        {
            Debug.Log("AddNewUser function error");
        }
        
    }

    //public void UpdateUser(string m_name, string m_password, string m_accType, int m_classCode, float progBeg, float progInt, float progAdv, int avatarID, int qCorrectBeg, int qWrongBeg, int qCorrectInt, int qWrongInt, int qCorrectAdv, int qWrongAdv)
    //{
    //    for (int i = 0; i < 14; i++)
    //    {
    //        switch ()
    //    }
    //}

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
