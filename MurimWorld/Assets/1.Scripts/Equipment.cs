using UnityEngine;
using System;

[Serializable]
public class Equipment
{
    public EquipmentData BaseData;
    public int CurrentDurability;

    public Equipment(EquipmentData data)
    {
        BaseData = data;
        BaseData = data;
        CurrentDurability = BaseData.MaxDurability; //처음 제작 시 새거로 제작
    }
}
