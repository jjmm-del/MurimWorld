using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCastlePanelUI : UIPopup
{
    enum Buttons {CloseButton, AppointmentTabButton, CouncilTabButton, DiplomacyTabButton}
    enum GameObjects { AppointmentSubPanel,CouncilSubPanel, DiplomacySubPanel }

    private AppointmentSubPanelUI _appointSubPanel;
    //private CouncilSubPanelUI _councilSubPanel;
    //private DiplomacyPanelUI _diplomacyPanel;
    
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.CloseButton).onClick.AddListener(ClosePopupUI);
        Get<Button>((int)Buttons.AppointmentTabButton).onClick.AddListener(() => SwitchTab(0));
        Get<Button>((int)Buttons.CouncilTabButton).onClick.AddListener(() => SwitchTab(1));
        Get<Button>((int)Buttons.DiplomacyTabButton).onClick.AddListener(() => SwitchTab(2));
        
        _appointSubPanel = Get<GameObject>((int)GameObjects.AppointmentSubPanel).GetComponent<AppointmentSubPanelUI>();
        //_councilSubPanel - Get<GameObject>((int)GameObjects.CouncilPanel).GetComponent<CouncilSubPanelUI>();
        //_diplomacyPanel - Get<GameObject>((int)GameObjects.DiplomacyPanel).GetComponent<DiplomacySubPanelUI>();

        _appointSubPanel.Init();

        SwitchTab(0);
    }

    private void SwitchTab(int index)
    {
        Get<GameObject>((int)GameObjects.AppointmentSubPanel).SetActive(index ==0);
        Get<GameObject>((int)GameObjects.CouncilSubPanel).SetActive(index ==1);
        Get<GameObject>((int)GameObjects.DiplomacySubPanel).SetActive(index ==2);

        switch (index)
        {
            case 0: _appointSubPanel.RefreshRoleSlots();
                break;
                //case 1: _councilSubPanel
                //case 2: __diplomacySubPanel
        }
    }
}
