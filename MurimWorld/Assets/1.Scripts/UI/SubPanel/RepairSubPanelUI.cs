using UnityEngine;
using System.Collections.Generic;
public class RepairSubPanelUI : MonoBehaviour
{
    [SerializeField] private Transform _contentContainer;
    [SerializeField] private GameObject _repairSlotPrefab;
    private void OnEnable()
    {
        RefreshRepairList();
    }
    private void RefreshRepairList()
    {
        foreach (Transform child in _contentContainer)
        {
            Destroy(child.gameObject);
        }
        
        //List<Equipment> myItems = InventoryManager.Instance.GetEquipments();
        List<Equipment> myItems = GetDummyDamagedItems();
        foreach (Equipment eq in myItems)
        {
            if (eq.CurrentDurability >= eq.BaseData.MaxDurability) continue;
            
            GameObject go = Instantiate(_repairSlotPrefab, _contentContainer);
            go.GetComponent<RepairSlotUI>()?.Initialize(eq,RefreshRepairList);
        }
        
    }

    private List<Equipment> GetDummyDamagedItems()
    {
        EquipmentData dummyData = ScriptableObject.CreateInstance<EquipmentData>();
        dummyData.ItemName = "이가빠진 철검";
        dummyData.MaxDurability = 100;

        Equipment damagedSword = new Equipment(dummyData);
        damagedSword.CurrentDurability = 45; //내구도 닳아있음

        return new List<Equipment> { damagedSword };
    }
    
}
