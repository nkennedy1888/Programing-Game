using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class UserAuth : MonoBehaviour
{

    public GameObject b_signUp, b_Submit, parent_AccType, parent_TeacherAcc, parent_StudentAcc;
    private string currUser, currPassword, confirmPass, classCode;
    private bool student = false, teacher = false;
    private string path = "Assets/SaveData/users.txt";
    // Start is called before the first frame update
    void Start()
    {
        parent_AccType.SetActive(false);
        parent_StudentAcc.SetActive(false);
        parent_TeacherAcc.SetActive(false);
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

    public void GetConfirmPass(string temp)
    {
        confirmPass = temp;
    }

    public void GetCodeInput(string temp)
    {
        classCode = temp;
    }

    public void AccountType()
    {
        b_Submit.SetActive(false);
        b_signUp.SetActive(false);
        parent_AccType.SetActive(true);   
    }

    public void IsStudent()
    {
        student = true;
        parent_AccType.SetActive(false);
        parent_StudentAcc.SetActive(true);
    }

    public void IsTeacher()
    {
        teacher = true;
        parent_AccType.SetActive(false);
        parent_TeacherAcc.SetActive(true);
    }


    public void CreateAccount()
    {
        
        Debug.Log("User: " +currUser +"Password: " +currPassword);
        if (currUser == "" || currPassword == "")
        {
            Debug.Log("Please enter a unique user-name and password");
            return;
        }

        if (!currPassword.Equals(confirmPass))
        {
            Debug.Log("Passwords do not match");
            return;
        }

        //Check currUser against existing usernames to ensure no duplicate(s)
        if (File.Exists(path))
        {
            
            Debug.Log("file check worked");

            string line;
            StreamReader reader = new StreamReader(path);

            while ((line = reader.ReadLine()) != null && line != "")
            {
                //Important to note: a substring of the format .substring(startIndex, endIndex) is not available in C#, so the condition below finds proper length to get the username substring
                //Debug.Log("Username in parsers current line: " +line.Substring(line.IndexOf("] : <") + 5, (line.IndexOf("> :") - (line.IndexOf("] : <") + 5))));
                if (line.Substring(line.IndexOf("] : <") + 5, (line.IndexOf("> :") - (line.IndexOf("] : <") + 5))).Equals(currUser))
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
         * [STUDENT] : <username> : <password> : <code>
         * [TEACHER] : <username> : <password>
         * 
         */

        string userEntry = "";
        
        if (student)
        {
            userEntry = "[STUDENT] : <" + currUser + "> : <" + currPassword + "> : <" + classCode + ">";
        }
        else if (teacher)
        {
            userEntry = "[TEACHER] : <" + currUser + "> : <" + currPassword + ">";
        }
        else { Debug.Log("UserAuth.CreateAccount() has experienced an error in student/teacher determination"); }
        
        
        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine(userEntry);
        writer.Close();

        if (student)
        {
            SceneManager.LoadScene("Main - Student");
        }
        else if (teacher)
        {
            SceneManager.LoadScene("Main - Teacher");
        }
        

    }

    public void Login()
    {
        Debug.Log("User: " + currUser + "Password: " + currPassword);
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
                    PlayerPrefs.SetString("name", currUser);
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
