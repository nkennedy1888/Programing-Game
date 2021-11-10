using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainStudent : MonoBehaviour
{

    public GameObject settings_Parent, account_Parent;

    // Start is called before the first frame update
    void Start()
    {
        account_Parent.SetActive(false);
        settings_Parent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSettingsVis()
    {
        settings_Parent.SetActive(!settings_Parent.activeSelf);
    }

    public void ToggleAccountVis()
    {

        account_Parent.SetActive(!account_Parent.activeSelf);

    }

    public void NavigateToBattle()
    {
        SceneManager.LoadScene("Battle");
    }
}
