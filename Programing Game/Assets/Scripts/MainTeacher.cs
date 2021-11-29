using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTeacher : MonoBehaviour
{
    Dictionary<string, float> d_Rankings = new Dictionary<string, float>();

    public GameObject settings_Parent, account_Parent, changePassParent;
    private Database localDB;

    string newPass, confPass;

    // Start is called before the first frame update
    void Start()
    {
        localDB = GameObject.FindGameObjectWithTag("Player").GetComponent<Database>();

        account_Parent.SetActive(false);
        settings_Parent.SetActive(false);
        changePassParent.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetNewPass(string temp)
    {
        newPass = temp;
    }

    public void GetConfPass(string temp)
    {
        confPass = temp;
    }

    public void ToggleSettingsVis()
    {
        settings_Parent.SetActive(!settings_Parent.activeSelf);
    }

    public void ToggleAccountVis()
    {

        account_Parent.SetActive(!account_Parent.activeSelf);


    }

    public void TogglePassChangeVis()
    {
        changePassParent.SetActive(!changePassParent.activeSelf);
    }


    public void NavigateToBattle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void ChangePassword()
    {
        Debug.Log("Current username: " + PlayerPrefs.GetString("username"));
        if (!newPass.Equals(confPass))
        {
            //"Passwords must match" dialogue
            return;
        }
        else
        {
            localDB.users[PlayerPrefs.GetString("username")].password = newPass;
        }


    }

    //public void GetRankings()
    //{
    //    //Iterate over each user, collecting and calculating scores based on the following
    //    //(Questions correct/(Questions incorrect+Questions correct)) Note: will need to sum correct and incorrect from the three seperate difficulties
    //    //Beginner category has weight of 25%
    //    //Intermediate has weight of 30%
    //    //Advanced has weight of 45%
    //    //Multiply weighted avg by completion status
    //    float weightedAvg, begAvg, intAvg, advAvg, ranking;

    //    foreach (KeyValuePair<string, UserData> entry in localDB.users)
    //    {
    //        //Reset all values
    //        weightedAvg = 0.0F;
    //        begAvg = 0.0F;
    //        intAvg = 0.0F;
    //        advAvg = 0.0F;
    //        ranking = 0.0F;
    //        //Calculate averages
    //        begAvg = (entry.Value.qstCrctBeginner)/(entry.Value.qstWrgBeginner + entry.Value.qstCrctBeginner);
    //        intAvg = (entry.Value.qstCrctIntermediate) / (entry.Value.qstWrgIntermediate + entry.Value.qstCrctIntermediate);
    //        advAvg = (entry.Value.qstCrctAdvanced) / (entry.Value.qstWrgAdvanced + entry.Value.qstCrctAdvanced);

    //        //Calculate weighted averages
    //        begAvg *= 0.25F;
    //        intAvg *= 0.30F;
    //        advAvg *= 0.45F;

    //        //Gives current 'grade'
    //        weightedAvg = begAvg + intAvg + advAvg;

    //        //Calculate final averages based on completion %
    //        begAvg *= entry.Value.progressBeginner;
    //        intAvg *= entry.Value.progressIntermediate;
    //        advAvg *= entry.Value.progressAdvanced;

    //        //Sum final avgs for ranking
    //        ranking = advAvg + intAvg + begAvg;
    //        d_Rankings.Add(entry.Key, ranking);
    //    }
    //    //l_Ranks = d_Rankings.ToList();
    //    //l_Ranks.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

    //    //Should sort d_Rankings to display users in descending order based on score
    //    d_Rankings = (Dictionary<string, float>)(from entry in d_Rankings orderby entry.Value descending select entry);

    //}



    //Clears playerprefs and redirects to Log in scene
    public void LogOut()
    {
        PlayerPrefs.DeleteKey("username");
        SceneManager.LoadScene("Log in");
    }

    public string GetUsername()
    {
        return PlayerPrefs.GetString("username");
    }

}

