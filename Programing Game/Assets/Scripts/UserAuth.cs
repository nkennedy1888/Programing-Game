using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class UserAuth : MonoBehaviour
{
    private string currUser, currPassword;
    private string path = "Assets/SaveData/users.txt";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Receives string data from inputfield 'User Name' from On End trigger
    public void GetUserInput(string temp)
    {
        currUser = temp;
    }

    //Receives string data from inputfield 'Password' from On End trigger
    public void GetPassInput(string temp)
    {
        currPassword = temp;
    }

    
    public void CreateAccount()
    {
        Debug.Log("User: " +currUser +"Password: " +currPassword);
        if (currUser == "" || currPassword == "")
        {
            Debug.Log("Please enter a unique user-name and password");
            return;
        }

        //Check currUser against existing usernames to ensure no duplicate(s)
        if (File.Exists(path))
        {
            
            Debug.Log("file check worked");

            string line;
            StreamReader reader = new StreamReader(path);

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(currUser))
                {
                    Debug.Log("Duplicate username; enter unique username");
                    reader.Close();
                    return;                 
                }
            }
            reader.Close();
        }
        else
        {
            File.Create(path).Close();
        }

        //Note: will need to implement a more secure solution
        //Stores entered user data in users.txt according to the following format:
        /*
         * 
         * <username> <password>
         * <username> <password>
         * 
         */

        string userEntry = "<" +currUser +"> : <" +currPassword +">";
        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine(userEntry);
        writer.Close();

        SceneManager.LoadScene("Main - Student");

    }

    public void Login()
    {
        if (currUser == "" || currPassword == "")
        {
            Debug.Log("Please enter your user-name and password");
            return;
        }

        if (File.Exists(path))
        {
            string line;
            StreamReader reader = new StreamReader(path);

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(currUser) && line.Contains(currPassword))
                {
                    SceneManager.LoadScene("Main - Student");
                    reader.Close();
                    return;
                }
            }
            reader.Close();

            Debug.Log("Entered account does not exist");
            return;
        }

    }
}
