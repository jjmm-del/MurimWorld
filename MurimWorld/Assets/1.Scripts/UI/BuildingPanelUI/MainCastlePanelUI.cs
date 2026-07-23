using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCastlePanelUI : BuildingPanelBase
{
    enum Buttons {CloseButton, AppointmentTabButton, CouncilTabButton, DiplomacyTabButton}
    enum GameObjects { AppointmentSubPanel,CouncilSubPanel, DiplomacySubPanel }
    enum Texts{MainCastleText}

    private AppointmentSubPanelUI _appointSubPanel;
    //private CouncilSubPanelUI _councilSubPanel;
    //private DiplomacyPanelUI _diplomacyPanel;
    
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));

        Get<Button>((int)Buttons.CloseButton).onClick.AddListener(ClosePopupUI);
        Get<TextMeshProUGUI>((int)Texts.MainCastleText).text = "무림맹 본성";
        GameObject[] panels = new GameObject[]
        {
            Get<GameObject>((int)GameObjects.AppointmentSubPanel),
            Get<GameObject>((int)GameObjects.CouncilSubPanel),
            Get<GameObject>((int)GameObjects.DiplomacySubPanel)
        };
        Button[] tabButtons = new Button[]
        {
            Get<Button>((int)Buttons.AppointmentTabButton),
            Get<Button>((int)Buttons.CouncilTabButton),
            Get<Button>((int)Buttons.DiplomacyTabButton)
        };
        //_councilSubPanel - Get<GameObject>((int)GameObjects.CouncilPanel).GetComponent<CouncilSubPanelUI>();
        //_diplomacyPanel - Get<GameObject>((int)GameObjects.DiplomacyPanel).GetComponent<DiplomacySubPanelUI>();
        
        InitTabs(panels, tabButtons);
        SwitchTab(0);
    }
}
