using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGeneral : MonoBehaviour
{
    public GameObject exitWindow;
    //public Button exit, cont;
    // Start is called before the first frame update
    void Start()
    {
        exitWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleExitWinVis();
        }
    }

    public void ToggleExitWinVis()
    {
        exitWindow.SetActive(!exitWindow.activeSelf);
    }
    public void Exit()
    {
       //Get account type and redir to appropriate "Main - [acctype]" scene
    }
    public void Cont()
    {
        exitWindow.SetActive(false);
    }
}