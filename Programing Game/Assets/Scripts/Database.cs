using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    
    private new string  name;
    [HideInInspector]public UserData currUser;
    
    Dictionary<string, UserData> users= new Dictionary<string, UserData>();


    public void Start()
    {
        //gets data from save file when a new scene loads
        users = SaveData.LoadData();
        //gets current users name from system
        name = PlayerPrefs.GetString("username");
        //sets current users data to defualt
        currUser = new UserData(name, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //checks if user exists in database alread
        if (users.ContainsKey(name)) 
        {
            //sets current users data to correct data from database
            currUser = users[name];
        }
        else 
        {
            //adds current user as a new database entry
            users.Add(name, currUser);
            //saves update
            SaveData.SaveGame(users);
        
        }

       
        
    }
    public void Update()
    {
        
    }

    public void OnDestroy()
    {
        //saves user data when scene is changed in game
        users[name] = currUser;
        SaveData.SaveGame(users);
    }
}
