using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "NewEquipment", menuName = "MurimWorld/Equipment Data")]
public class EquipmentData : ScriptableObject
{
    public string ItemId;
    public string ItemName;
    public int MaxDurability = 100;
    public int CraftCostAP = 1;
    
    
    public int CraftDurationTurns = 1;// 제작에 소요되는 턴 수
    public List<Ingredient> Recipe = new List<Ingredient>();
}

