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
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        localDB = GameObject.FindGameObjectWithTag("Player").GetComponent<Database>();

        beg.text = "Beginner \n" + localDB.currUser.progressBeginner.ToString() + "%";
        inte.text = "Intermediate \n" + localDB.currUser.progressIntermediate.ToString() + "%";
        adv.text = "Advanced \n" + localDB.currUser.progressAdvanced.ToString() + "%";
    }
    public void LevelSelect(int l)
    {
        PlayerPrefs.SetInt("level", l);
        Debug.Log("level is " + l);

    }
}
