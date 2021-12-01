using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class UserAuth : MonoBehaviour
{
    private Database localDB;
    public GameObject b_signUp, b_Submit, parent_AccType, parent_TeacherAcc, parent_StudentAcc, uName, uPass;
    public GameObject err_Username, err_Password, err_ConfPass_Teacher, err_ConfPass_Student, err_ClassCode;
    private string currUser, currPassword, confirmPass, accType;
    private int classCode;
    private bool isNum;

    // Start is called before the first frame update
    void Start()
    {
        localDB = GameObject.FindGameObjectWithTag("Player").GetComponent<Database>();

        if ((SceneManager.GetActiveScene().name == "Log in"))
        {
            parent_AccType.SetActive(false);
            parent_StudentAcc.SetActive(false);
            parent_TeacherAcc.SetActive(false);
            err_Username.SetActive(false);
            err_Password.SetActive(false);
            err_ConfPass_Teacher.SetActive(false);
            err_ConfPass_Student.SetActive(false);
            err_ClassCode.SetActive(false);
            
            //Clears any potential lingering user auth data
            if (PlayerPrefs.HasKey("username"))
            {
                PlayerPrefs.DeleteKey("username");
            }
        }       
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
        isNum = Int32.TryParse(temp, out classCode);
    }

    //Activates account type selection fields
    public void AccountType()
    {
        uName.SetActive(false);
        uPass.SetActive(false);
        b_Submit.SetActive(false);
        b_signUp.SetActive(false);
        parent_AccType.SetActive(true);   
    }

    //IsStudent and IsTeacher used for account creation; defines account type based on user selection
    public void IsStudent()
    {
        accType = "student";
        parent_AccType.SetActive(false);
        parent_StudentAcc.SetActive(true);
        uName.SetActive(true);
        uPass.SetActive(true);
    }
    
    public void IsTeacher()
    {
        accType = "teacher";
        parent_AccType.SetActive(false);
        parent_TeacherAcc.SetActive(true);
        uName.SetActive(true);
        uPass.SetActive(true);
    }

    public void CreateAccount()
    {
        //Invalid input checking
        if (currUser == "" || currPassword == "")
        {
            err_Username.GetComponent<Text>().text = "Please enter a unique user-name and password";
            err_Username.SetActive(true);
            StartCoroutine(messageTimer(err_Username));
            return;
        }

        if (localDB.HasUser(currUser))
        {
            err_Username.GetComponent<Text>().text = "User-name already in use";
            err_Username.SetActive(true);
            StartCoroutine(messageTimer(err_Username));
            return;
        }

        //Invalid input checking
        if (!currPassword.Equals(confirmPass))
        {
            err_Password.GetComponent<Text>().text = "Passwords do not match";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
            return;
        }

        if ((!isNum || classCode < 1000 || classCode > 100000) && accType == "student")
        {
            err_Password.GetComponent<Text>().text = "Not a valid Class Code";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
            return;
        }

        //Calls database function to add new user to the database with key username and value UserData
        localDB.AddNewUser(currUser, currPassword, accType, classCode);

        //Scene redirection on succesful acc creation
        if (accType.ToLower().Equals("teacher"))
        {         
            SetUsername(currUser);
            SceneManager.LoadScene("Main - Teacher");
        }
        else
        {            
            SetUsername(currUser);
            SceneManager.LoadScene("Main - Student");
        }
    }

    public void Login()
    {
        if (currUser == "" || currPassword == "")
        {
            err_Password.GetComponent<Text>().text = "Please enter your user-name and password";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
            return;
        }

        if (!localDB.HasUser(currUser))
        {
            err_Username.GetComponent<Text>().text = "User-name could not be found";
            err_Username.SetActive(true);
            StartCoroutine(messageTimer(err_Username));
            return;
        }

        if (localDB.HasUser(currUser) && !localDB.HasUserandPassword(currUser, currPassword))
        {
            err_Password.GetComponent<Text>().text = "Password is incorrect, please try again";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
            return;
        }

       if(localDB.HasUserandPassword(currUser, currPassword))
       {
            SetUsername(currUser);
            if (localDB.users[currUser].accountType.ToLower().Equals("teacher"))
            {                
                SceneManager.LoadScene("Main - Teacher");
            }
            else if (localDB.users[currUser].accountType.ToLower().Equals("student"))
            {                
                SceneManager.LoadScene("Main - Student");
            }
            else
            {
                err_Password.GetComponent<Text>().text = "An issue has occurred in identifying accType";
                err_Password.SetActive(true);
                StartCoroutine(messageTimer(err_Password));
                PlayerPrefs.DeleteKey("username");
                return;
            }
       } 
    }

    public void SetUsername(string temp)
    {
        PlayerPrefs.SetString("username", temp);        
    }
    //Read current username from PlayerPrefs; 
    public string GetUsername()
    {
        return PlayerPrefs.GetString("username");
    }

    public IEnumerator messageTimer(GameObject message)
    {
        yield return new WaitForSeconds(2f);
        message.gameObject.SetActive(false);
    }
}
