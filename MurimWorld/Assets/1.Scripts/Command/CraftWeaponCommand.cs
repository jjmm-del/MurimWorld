using UnityEngine;

public class CraftWeaponCommand : ICommand
{
    private EquipmentData _bluePrint;
    private int _remainingTurns;
    
    public CraftWeaponCommand(EquipmentData data)
    {
        _bluePrint = data;
        _remainingTurns = data.CraftDurationTurns;
    }

    public string CommandName => $"[{_bluePrint.ItemName}] 제작 예약 (남은 기간(: {_remainingTurns}달)";
    public EquipmentData BluePrint => _bluePrint;

    public string Execute()
    {
        _remainingTurns--;
        if (_remainingTurns <= 0)
        {
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddEquipment(_bluePrint);
            }

            return $"[{_bluePrint.ItemName}] 장인의 혼을 담아 마침내 제작을 완료 했습니다.";
        }
        else
        {
            return $"[{_bluePrint}] 풀무질 중... 제작 완료까지 앞으로{_remainingTurns}";
        }
    }
    public bool IsCompleted => _remainingTurns <= 0;
}
