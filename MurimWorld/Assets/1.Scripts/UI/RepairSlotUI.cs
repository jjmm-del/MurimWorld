using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class RepairSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _durabilityText;
    [SerializeField] private TextMeshProUGUI _repairCostText;
    [SerializeField] private Button _repairButton;

    private Equipment _myEquipment;
    private int _calculatedCost;
    private Action _onRepairReserved;

    public void Initialize(Equipment equipment, Action onRepairReserved)
    {
        _myEquipment = equipment;
        _onRepairReserved = onRepairReserved;

        _itemNameText.text = equipment.BaseData.ItemName;
        _durabilityText.text = $"내구도:{equipment.CurrentDurability} / {equipment.BaseData.MaxDurability}";
        
        int damagedAmount = equipment.BaseData.MaxDurability - equipment.CurrentDurability;
        _calculatedCost = Mathf.Max(1, damagedAmount / 10); //최소 비용 1AP 보장

        _repairCostText.text = $"수리비: {_calculatedCost} AP";
        
        _repairButton.onClick.RemoveAllListeners();
        _repairButton.onClick.AddListener(OnRepairClicked);
    }

    private void OnRepairClicked()
    {
        ICommand repairCmd = new RepairEquipmentCommand(_myEquipment);
        bool success = TurnManager.Instance.TryEnQueueCommand(repairCmd, _calculatedCost);

        if (success)
        {
            _onRepairReserved?.Invoke();
        }
    }
}
