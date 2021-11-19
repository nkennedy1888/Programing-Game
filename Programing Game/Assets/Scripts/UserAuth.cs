using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UserAuth : MonoBehaviour
{

    public GameObject b_signUp, b_Submit, parent_AccType, parent_TeacherAcc, parent_StudentAcc, uName, uPass;
    public GameObject err_Username, err_Password, err_ConfPass_Teacher, err_ConfPass_Student, err_ClassCode;
    private string currUser, currPassword, confirmPass, classCode, accType;
    private string path = "Assets/SaveData/users.txt";

    

    // Start is called before the first frame update
    void Start()
    {
        
        
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
        classCode = temp;
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
        
        //Debug.Log("User: " +currUser +"Password: " +currPassword);
        if (currUser == "" || currPassword == "")
        {
            err_Username.GetComponent<Text>().text = "Please enter a unique user-name and password";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
            return;
        }

        if (!currPassword.Equals(confirmPass))
        {
            err_Password.GetComponent<Text>().text = "Passwords do not match";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
            return;
        }

        string userEntry;
        //bool recentFileCreate = false;
        //StreamWriter writer = new StreamWriter(path);
        if (!File.Exists(path))
        {
            File.Create(path);
            //recentFileCreate = true;
        }
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


        if (!Compare("name", currUser))
        {

            if (classCode == null)
            {
                classCode = "";
            }
            //if (recentFileCreate) { userEntry = "\n";}

            userEntry =
                "{" +
                "\nname: " + currUser +
                "\npass: " + currPassword +
                "\ntype: " + accType +
                "\ncode: " + classCode +
                "\n},";
            StreamWriter writer = new StreamWriter(path, true);

            writer.WriteLine(userEntry);
            writer.Close();
        }

        if (accType.Equals("teacher"))
        {
            SetUsername(currUser);
            SceneManager.LoadScene("Main - Teacher");
        }
        else
        {
            SetUsername(currUser);
            SceneManager.LoadScene("Main - Student");
        }
        //Check currUser against existing usernames to ensure no duplicate(s)
        //if (File.Exists(path))
        //{

        //    Debug.Log("file check worked");

        //    string line;
        //    StreamReader reader = new StreamReader(path);

        //    while ((line = reader.ReadLine()) != null && line != "")
        //    {
        //        //Important to note: a substring of the format .substring(startIndex, endIndex) is not available in C#, so the condition below finds proper length to get the username substring
        //        //Debug.Log("Username in parsers current line: " +line.Substring(line.IndexOf("] : <") + 5, (line.IndexOf("> :") - (line.IndexOf("] : <") + 5))));
        //        if (line.Substring(line.IndexOf("] : <") + 5, (line.IndexOf("> :") - (line.IndexOf("] : <") + 5))).Equals(currUser))
        //        {
        //            Debug.Log("Duplicate username; enter unique username");
        //            err_Username.SetActive(true);
        //            reader.Close();
        //            return;                 
        //        }
        //    }
        //    reader.Close();
        //}
        //else
        //{
        //    File.Create(path).Close();
        //}

        ////Note: will need to implement a more secure solution
        ////Stores entered user data in users.txt according to the following format:
        ///*
        // * 
        // * [STUDENT] : <username> : <password> : <code>
        // * [TEACHER] : <username> : <password>
        // * 
        // */

        //string userEntry = "";

        //if (student)
        //{
        //    userEntry = "[STUDENT] : <" + currUser + "> : <" + currPassword + "> : <" + classCode + ">";
        //}
        //else if (teacher)
        //{
        //    userEntry = "[TEACHER] : <" + currUser + "> : <" + currPassword + ">";
        //}
        //else { Debug.Log("UserAuth.CreateAccount() has experienced an error in student/teacher determination"); }


        //StreamWriter writer = new StreamWriter(path, true);

        //writer.WriteLine(userEntry);
        //writer.Close();

        //if (student)
        //{
        //    SceneManager.LoadScene("Main - Student");
        //}
        //else if (teacher)
        //{
        //    SceneManager.LoadScene("Main - Teacher");
        //}


    }

    //Need to add functionality to cross-reference whether user is student or teacher
    public void Login()
    {
        
        //Debug.Log("User: " + currUser + "Password: " + currPassword);
        if (currUser == "" || currPassword == "")
        {
            err_Password.GetComponent<Text>().text = "Please enter your user-name and password";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
            return;
        }

        string passInFile = "";
        string line;
        StreamReader reader = new StreamReader(path);

        while ((line = reader.ReadLine()) != null && line != "")
        {
            if (line.StartsWith("name") && line.Substring(line.IndexOf(":") + 2).Equals(currUser))
            {
                line = reader.ReadLine();
                passInFile = line.Substring(line.IndexOf(":") + 2);
                //Debug.Log(passInFile);
                SetUsername(currUser);
                break;
            }
        }
        reader.Close();

        if (File.Exists(path) && !passInFile.Equals("") && currPassword.Equals(passInFile))
        {
            
            if (GetAccTypeFromFile().Equals("teacher"))
            {
                //SetUsername(currUser);
                SceneManager.LoadScene("Main - Teacher");
            }
            else if(GetAccTypeFromFile().Equals("student"))
            {
                //SetUsername(currUser);
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
        else
        {
            err_Password.GetComponent<Text>().text = "Password mismatch";
            err_Password.SetActive(true);
            StartCoroutine(messageTimer(err_Password));
            return;
        }

    }

    string GetPasswordFromFile()
    {
        string pass = "";
        string line;
        
        StreamReader reader = new StreamReader(path);

        while ((line = reader.ReadLine()) != null && line != "")
        {
            if(line.StartsWith("name") && line.Substring(line.IndexOf(":") + 2).Equals(GetUsername()))
            {
                line = reader.ReadLine();
                pass = line.Substring(line.IndexOf(":") + 2);
                break;
            }
        }
        reader.Close();
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
    //Read current username from PlayerPrefs; 
    public string GetUsername()
    {
        return PlayerPrefs.GetString("username");
    }

    ////Writes defined account type to users.txt; action done during acc creation
    //void SetAccType()
    //{

    //}

    //Reads account type from users.txt, uses GetUsername() to find appropriate account
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

    ////Unnecessary?
    //bool IsStudent()
    //{
    //    return true;
    //}

    //bool IsTeacher()
    //{
    //    return true;
    //}

    bool Compare(string key, string value)
    {
        if (File.Exists(path))
        {
            string line;
            StreamReader compReader = new StreamReader(path);

            while ((line = compReader.ReadLine()) != null && line != "")
            {
                if (line.StartsWith(key) && line.Substring(line.IndexOf(":") + 2).Equals(value))
                {
                    compReader.Close();
                    return true;
                }
                else
                {
                    continue;
                }
            }
            compReader.Close();
        }
        else
        {
            //No file exists; return false regardless of input
            return false;
        }

        return false;
    }


    //Self-evident; creates an account with the given input parameters and formats the input to the given comment below
    //void CreateAccount(string type, string name, string pass, string code)
    //{
    //    string userEntry;
    //    bool recentFileCreate = false;
    //    //StreamWriter writer = new StreamWriter(path);
    //    if (!File.Exists(path))
    //    {
    //        File.Create(path);
    //        recentFileCreate = true;
    //    }
    //    /*
    //     * {
    //     * type: //STUDENT or TEACHER
    //     * name: //Defined by user input
    //     * pass: //Defined by user input
    //     * code: //Based on existing teacher codes, still defined by user input
    //     * },//Concludes one account listing
    //     * {
    //     * type: STUDENT
    //     * name: Wawa central
    //     * pass: wawa
    //     * code: 21212 //code for STUDENT indicates which teacher/class they belong to
    //     * },
    //     * {
    //     * type: TEACHER
    //     * name: rock
    //     * pass: johnson
    //     * code: 12122 //code for TEACHER indicates their class
    //     * }//No comma indicates end of file
    //     */

    //    if (!Compare("name", name))
    //    {

    //        if (code == null)
    //        {
    //            code = "";
    //        }
    //        //if (recentFileCreate) { userEntry = "\n";}

    //        userEntry =
    //            "{" +
    //            "type: " + type +
    //            "name: " + name +
    //            "pass: " + pass +
    //            "code: " + code +
    //            "},";

    //        File.WriteAllText(path, userEntry);

    //    }



    //}


    //Removes current user entry from playerprefs; redirects to login scene
    public void LogOut()
    {
        PlayerPrefs.DeleteKey("username");
        SceneManager.LoadScene("Log In");
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
