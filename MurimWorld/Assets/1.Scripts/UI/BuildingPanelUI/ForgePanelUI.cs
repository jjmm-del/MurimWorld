using System;
using UnityEngine;
using UnityEngine.UI;

public class ForgePanelUI : BuildingPanelBase
{
    //[Header("버튼 컴포넌트 연결")]
    //[SerializeField] private Button _craftTabButton; // 제작
    //[SerializeField] private Button _repairTabButton; // 수리 등 나중에 추가
    
    enum Buttons {CloseButton, CraftTabButton,RepairTabButton}
    enum GameObjects {CraftSubPanel,RepairSubPanel}
    
    private CraftSubPanelUI _craftSubPanel;
    //private enum ForgeSubMenu{Craft=0, Repair=1}
    
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.CloseButton).onClick.AddListener(ClosePopupUI);
        //Get<Button>((int)Buttons.CraftTabButton).onClick.AddListener
        //Get<Button>((int)Buttons.RepairTabButton).onClick.AddListener
        //
        GameObject[] panels = new GameObject[]
        {
            Get<GameObject>((int)GameObjects.CraftSubPanel),
            Get<GameObject>((int)GameObjects.RepairSubPanel)
        };
        Button[] tabButtons = new Button[]
        {
            Get<Button>((int)Buttons.CraftTabButton),
            Get<Button>((int)GameObjects.CraftSubPanel),
        };
        
        InitTabs(panels, tabButtons);
        SwitchTab(0);
    }

}
