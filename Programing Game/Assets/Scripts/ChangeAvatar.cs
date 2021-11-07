using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAvatar : MonoBehaviour
{
    private UserData user;
    private int index;
    private int avatars = 3;

    void ChangePlayer() 
    {
        user = GameObject.FindGameObjectWithTag("Player").GetComponent<Database>().currUser;
        index = user.avatarID;

        if( index < avatars) 
        {
            user.avatarID += 1;
        }
        else 
        {
            user.avatarID = 0;
        }
    }
}
