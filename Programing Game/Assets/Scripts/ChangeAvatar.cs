using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAvatar : MonoBehaviour
{
    private UserData user;
    private int index;
    private int avatars;

    private void Update()
    {
        avatars = GameObject.FindGameObjectWithTag("Player").GetComponent<AvatarData>().avatars.Length - 1;
    }

    public void ChangePlayer() 
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

        return;
    }
}
