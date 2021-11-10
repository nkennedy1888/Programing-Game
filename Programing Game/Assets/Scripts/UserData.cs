using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string username;
    public float progressBeginner;
    public float progressIntermediate;
    public float progressAdvanced;
    public int avatarID;
    public int qstCrctBeginner;
    public int qstWrgBeginner;
    public int qstCrctIntermediate;
    public int qstWrgIntermediate;
    public int qstCrctAdvanced;
    public int qstWrgAdvanced;

    public UserData(string name, int avatar, float progBeg, float progInt, float progAdv, int qCBeg, int qWBeg, int qCInt, int qWInt, int qCAdv, int qWAdv)
    {
        username = name;
        progressBeginner = progBeg;
        progressIntermediate = progInt;
        progressAdvanced = progAdv;
        avatarID = avatar;
        qstCrctBeginner = qCBeg;
        qstWrgBeginner = qWBeg;
        qstCrctIntermediate = qCInt;
        qstWrgIntermediate = qWInt;
        qstCrctAdvanced = qCAdv;
        qstWrgAdvanced = qWAdv;
    }
}
