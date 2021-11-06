using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class AccountManagement : MonoBehaviour
{
    
    string path = "Assets/SaveData/users.txt";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //All methods in AccountManagement deal strictly with savedata/users.txt data manipulation and insertion
    string GetPassword()
    {
        string pass;
        return pass;
    }

    void SetPassword(string temp)
    {

    }

    public void SetUsername(string temp)
    {
        PlayerPrefs.SetString("username", temp);
    }
    string GetUsername()
    {
        return PlayerPrefs.GetString("username");
    }

    //void SetUsername(string temp)
    //{

    //}

    void SetAccType()
    {

    }

    string GetAccType()
    {
        string accType;
        return accType;
    }

    bool IsStudent()
    {

    }
    
    bool IsTeacher()
    {
        
    }



    void CreateAccount(string type, string name, string pass, string code)
    {

        
        /*
         * {
         * type: //STUDENT or TEACHER
         * name: //Defined by user input
         * pass: //Defined by user input
         * code: //Based on existing teacher codes, still defined by user input
         * },//Concludes one account listing
         * {
         * type: STUDENT
         * name: Wawa central
         * pass: wawa
         * code: 21212 //code for STUDENT indicates which teacher/class they belong to
         * },
         * {
         * type: TEACHER
         * name: rock
         * pass: johnson
         * code: 12122 //code for TEACHER indicates their class
         * }//No comma indicates end of file
         */

        if (!File.Exists(path))
        {
            
        }
        
    }

    void DeleteAccount(string name, string pass)
    {

    }
}
