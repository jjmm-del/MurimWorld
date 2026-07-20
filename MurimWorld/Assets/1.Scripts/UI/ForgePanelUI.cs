using System;
using UnityEngine;
using UnityEngine.UI;

public class ForgePanelUI : BuildingPanelBase
{
    [Header("버튼 컴포넌트 연결")]
    [SerializeField] private Button _craftTabButton; // 제작
    [SerializeField] private Button _repairTabButton; // 수리 등 나중에 추가
    
    private enum ForgeSubMenu{Craft=0, Repair=1}
    
    protected override void OnEnable()
    {
        base.OnEnable();
        _craftTabButton.onClick.AddListener(()=>SwitchSubMenu((int)ForgeSubMenu.Craft));
        _repairTabButton.onClick.AddListener(()=> SwitchSubMenu((int)ForgeSubMenu.Repair));
        
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _craftTabButton.onClick.RemoveAllListeners();
        _repairTabButton.onClick.RemoveAllListeners();
    }
    
    private void SwitchSubMenu(ForgeSubMenu targetMenu)
    {
        base.SwitchSubMenu((int)targetMenu);
        switch (targetMenu)
        {
            case ForgeSubMenu.Craft:
                Debug.Log("대장간 : 무기 및 방어구 제작");
                break;
            case ForgeSubMenu.Repair:
                Debug.Log("대장간: 무기 및 방어구 수리 모드");
                break;
        }
    }
}
