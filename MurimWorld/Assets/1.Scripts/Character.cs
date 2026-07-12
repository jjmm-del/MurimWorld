using UnityEngine;
using System;

[Serializable]
public class Character
{
    public CharacterData BaseData; //변하지 않는 원본데이터(SO참조)
    public RoleType CurrentRoleType = RoleType.None; //게임 중 변하는 데이터 (직책)
    
    //생성자
    public Character(CharacterData data)
    {
        this.BaseData = data;
        this.CurrentRoleType = RoleType.None;
    }
}
