using UnityEngine;

public class CraftWeaponCommand : ICommand
{
    private string _targetWeaponName;

    public CraftWeaponCommand(string weaponName)
    {
        _targetWeaponName = weaponName;
    }

    public string CommandName => "대장간 무기 제작";

    public string Execute()
    {
        //실제 행동로직 -> 무기 생성 후 인벤토리 저장
        // InventoryManager.Instance. AddWeapon(_weaponName)

        return $"대장간에서 뛰어난 솜씨로 [{_targetWeaponName}]를 제작했습니다!";


    }
}
