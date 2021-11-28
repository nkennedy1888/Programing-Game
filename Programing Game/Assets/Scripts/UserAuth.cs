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
    private string path = "Assets/SaveData/users.txt";
    private int classCode;

    

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
        classCode = Int32.Parse(temp);
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
        //Debug.Log("User: " +currUser +"Password: " +currPassword);
        if (currUser == "" || currPassword == "")
        {
            err_Username.GetComponent<Text>().text = "Please enter a unique user-name and password";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
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

        //Calls database function to add new user to the database with key username and value UserData
        localDB.AddNewUser(currUser, currPassword, accType, classCode);

        //Scene redirection on succesful acc creation
        if (accType.ToLower().Equals("teacher"))
        {
            Debug.Log("To Teacher screen");
            SetUsername(currUser);
            SceneManager.LoadScene("Main - Teacher");
        }
        else
        {
            Debug.Log("To Student screen");
            SetUsername(currUser);
            SceneManager.LoadScene("Main - Student");
        }


    }

    //Need to add functionality to cross-reference whether user is student or teacher
    public void Login()
    {
        Debug.Log("User: " + currUser + "Password: " + currPassword);
        if (currUser == "" || currPassword == "")
        {
            err_Password.GetComponent<Text>().text = "Please enter your user-name and password";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
            return;
        }

        Debug.Log(localDB.HasUserandPassword(currUser, currPassword));

       if(localDB.HasUserandPassword(currUser, currPassword))
       {
            SetUsername(currUser);
            if (localDB.users[currUser].accountType.ToLower().Equals("teacher"))
            {
                Debug.Log("is teacher");
                //SetUsername(currUser);
                SceneManager.LoadScene("Main - Teacher");
            }
            else if (localDB.users[currUser].accountType.ToLower().Equals("student"))
            {
                Debug.Log("is student");
                //SetUsername(currUser);
                SceneManager.LoadScene("Main - Student");
            }
            else
            {
                Debug.Log("Help");
                Debug.Log("acctype error");
                err_Password.GetComponent<Text>().text = "An issue has occurred in identifying accType";
                err_Password.SetActive(true);
                StartCoroutine(messageTimer(err_Password));
                PlayerPrefs.DeleteKey("username");
                return;
            }
       }
        //Debug.Log("DB_User: " + localDB.currUser.username + " DB_Pass: " + localDB.currUser.password);

        //if (currPassword.Equals(localDB.currUser.password))
        //{
        //    Debug.Log("correct password");
        //    //Password verified, proceed to account redirection
        //    if (true)
        //    {
        //        Debug.Log("is teacher");
        //        //SetUsername(currUser);
        //        SceneManager.LoadScene("Main - Teacher");
        //    }
        //    else if (true)
        //    {
        //        Debug.Log("is student");
        //        //SetUsername(currUser);
        //        SceneManager.LoadScene("Main - Student");
        //    }
        //    else
        //    {
        //        Debug.Log("acctype error");
        //        err_Password.GetComponent<Text>().text = "An issue has occurred in identifying accType";
        //        err_Password.SetActive(true);
        //        StartCoroutine(messageTimer(err_Password));
        //        PlayerPrefs.DeleteKey("username");
        //        return;
        //    }
        //}
        //else
        //{
        //    Debug.Log("Password invalid");
        //    //Password rejected remove current user from playprefs to allow for future login attempts
        //    if (PlayerPrefs.HasKey("username"))
        //    {
        //        PlayerPrefs.DeleteKey("username");
        //    }
        //    return;
        //}

    }

    //Write current users password input to users.txt and PlayerPrefs for persistence across scenes
    public void SetUsername(string temp)
    {
        PlayerPrefs.SetString("username", temp);
        //localDB.currUser = localDB.users[temp];
    }
    //Read current username from PlayerPrefs; 
    public string GetUsername()
    {
        return PlayerPrefs.GetString("username");
    }

    string GetAccTypeFromFile()
    {
        string type = "";
        string line;

        StreamReader reader = new StreamReader(path);

        while ((line = reader.ReadLine()) != null && line != "")
        {
            if (line.StartsWith("name") && line.Substring(line.IndexOf(":") + 2).Equals(GetUsername()))
            {
                reader.ReadLine();
                line = reader.ReadLine();
                type = line.Substring(line.IndexOf(":") + 2);
            }
        }
        reader.Close();
        return type;
    }



    //Delete all account details from users.txt; call LogOut() to finalize
    void DeleteAccount(string name, string pass)
    {

    }

    public IEnumerator messageTimer(GameObject message)
    {
        yield return new WaitForSeconds(2f);
        message.gameObject.SetActive(false);
    }
}
