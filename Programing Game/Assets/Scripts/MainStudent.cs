using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStudent : MonoBehaviour
{

    public GameObject settings_Parent;

    // Start is called before the first frame update
    void Start()
    {
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


}
