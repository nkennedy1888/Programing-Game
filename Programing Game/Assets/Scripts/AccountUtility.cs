using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountUtility : MonoBehaviour
{

    public GameObject interactiveDropdown;
    //GameObject changeAvatar, changePassword, signOut;
    
    

    // Start is called before the first frame update
    void Start()
    {
        interactiveDropdown.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropdownState(Dropdown active)
    {
        //Reads displayed text for dropdown by reading the active index value
        //state = active.options[active.value].text;
        //Debug.Log("Current state: " + state);
    }


    public void ToggleVisibility()
    {

        interactiveDropdown.SetActive(!interactiveDropdown.activeSelf);

    }

}
