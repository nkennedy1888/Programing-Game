using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//All methods in AccountManagement deal strictly with savedata/users.txt data manipulation and insertion
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

    //Retrieve current users password, current user given by GetUsername() 
    string GetPassword()
    {
        string pass = "";
        return pass;
    }

    //Write current users password input to users.txt
    void SetPassword(string temp)
    {

    }

    //Write current users password input to users.txt and PlayerPrefs for persistence across scenes
    public void SetUsername(string temp)
    {
        PlayerPrefs.SetString("username", temp);
    }
    //Read current username from PlayerPrefs; accessible from all scenes
    public string GetUsername()
    {
        return PlayerPrefs.GetString("username");
    }

    //Writes defined account type to users.txt; action done during acc creation
    void SetAccType()
    {

    }

    //Reads account type from users.txt, uses GetUsername() to find appropriate account
    string GetAccType()
    {
        string accType = "";
        return accType;
    }

    ////Unnecessary?
    //bool IsStudent()
    //{
    //    return true;
    //}
    
    //bool IsTeacher()
    //{
    //    return true;
    //}


    //Self-evident; creates an account with the given input parameters and formats the input to the given comment below
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

    //Removes current user entry from playerprefs; redirects to login scene
    void LogOut()
    {

    }

    //Delete all account details from users.txt; call LogOut() to finalize
    void DeleteAccount(string name, string pass)
    {

    }
}
