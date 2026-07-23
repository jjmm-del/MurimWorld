using UnityEngine;

public class RepairEquipmentCommand : ICommand
{
    public RepairEquipmentCommand(Equipment equipment)
    {
        
    }

    public string CommandName => "대장간 장비 수리";
    public string Execute()
    {
        return $"대장간에서 장비를 수리했습니다.";
    }
}
