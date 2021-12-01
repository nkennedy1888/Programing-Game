using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/*
 * Code from https://forum.unity.com/threads/tab-between-input-fields.263779/ 
 * by user "GeorgeRigato"
 * 
 * Allows users to utilize Tab key press to move between input fields and buttons
 * Does not loop in current state, once the bottom element is reached it will not return to the top element on Tab press
 */
public class TabSwitch : MonoBehaviour
{
    EventSystem system;

    void Start()
    {
        system = EventSystem.current;// EventSystemManager.currentSystem;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {
                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null)
                    inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }
}
