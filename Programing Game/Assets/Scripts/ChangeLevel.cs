using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour
{
    public Text beg;
    public Text inte;
    public Text adv;
    public Database db;
    
    private void Start()
    {
        beg.text = "Beginner \n" + db.currUser.progressBeginner.ToString() + "%";
        inte.text = "Intermediate \n" + db.currUser.progressIntermediate.ToString() + "%";
        adv.text = "Advanced \n" + db.currUser.progressAdvanced.ToString() + "%";
    }
    public void LevelSelect(int l)
    {
        PlayerPrefs.SetInt("level", l);
        Debug.Log("level is " + l);
    }
}
