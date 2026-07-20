using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class InventoryManager : Singleton<InventoryManager>
{
    [Header("보유 자원 현황")]
    [SerializeField] private int _silver = 500;
    public int Silver => _silver;
    
    private Dictionary<ItemData, int> _inventory = new Dictionary<ItemData, int>();
    private List<Equipment> _myEquipments = new List<Equipment>();

    public IReadOnlyDictionary<ItemData, int> Inventory => _inventory;
    public List<Equipment> MyEquipments => _myEquipments;
    
    

    //재료 획득
    public void AddItem(ItemData item, int amount)
    {
        if (_inventory.ContainsKey(item))
        {
            _inventory[item] += amount;
        }
        else
        {
            _inventory.Add(item, amount);
        }
        Debug.Log($"[창고] {item.ItemName} {amount}개를 획득했소!");
    }
    //재료가 충분한지 검사
    public int GetItemCount(ItemData item)
    {
        if (item == null) return 0;
        return _inventory.TryGetValue(item, out int count) ? count : 0;
    }
    //재료 차감
    public void ConsumeItem(ItemData item, int amount)
    {
        if (GetItemCount(item)>= amount)
        {
            _inventory[item] -= amount;
        }
    }

    public void RefundResource(ItemData item, int amount)
    {
        _inventory[item] += amount;
    }

    public void AddEquipment(EquipmentData data)
    {
        _myEquipments.Add(new Equipment(data));
        Debug.Log($"[창고]'{data.ItemName}'가 최종 완성되어 맹의 창고에 추가되었소");
    }

}
