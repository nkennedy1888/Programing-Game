using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    //public List<UserData> users = new List<UserData>();
    private new string  name;
    [HideInInspector]public UserData currUser;
    //private bool isfound = false;

    Dictionary<string, UserData> users2= new Dictionary<string, UserData>();


    public void Start()
    {
        name = PlayerPrefs.GetString("name");

        currUser = new UserData(name, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        if (users2.ContainsKey(name)) 
        {
            currUser = users2[name];

        }
        else 
        {
            users2.Add(name, currUser);
        
        }

       // for (int i = 0; i < users.Count; i++)
       // {
       //    if (users[i].username.Equals(name)) 
       //     {
       //         currUser = users[i];
       //         isfound = true;
       //         break;
       //     }
       // }
       //
       // if (!isfound)
       // {
       //     currUser = new UserData(name, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
       //     users.Add(currUser);
       //
       // }
        
    }
    public void Update()
    {
        
    }
}
