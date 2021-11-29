using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour
{
    public Text beg;
    public Text inte;
    public Text adv;
    //public Database db;
    private Database localDB;
    private void Start()
    {
        localDB = GameObject.FindGameObjectWithTag("Player").GetComponent<Database>();

        beg.text = "Beginner \n" + localDB.users[PlayerPrefs.GetString("username")].progressBeginner.ToString() + "%";
        inte.text = "Intermediate \n" + localDB.users[PlayerPrefs.GetString("username")].progressIntermediate.ToString() + "%";
        adv.text = "Advanced \n" + localDB.users[PlayerPrefs.GetString("username")].progressAdvanced.ToString() + "%";
    }
    public void LevelSelect(int l)
    {
        PlayerPrefs.SetInt("level", l);
        Debug.Log("level is " + l);

    }
}
