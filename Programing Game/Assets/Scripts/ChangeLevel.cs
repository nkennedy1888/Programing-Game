using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    public void LevelSelect(int l)
    {
        PlayerPrefs.SetInt("level", l);
        Debug.Log("level is " + l);
    }
}
