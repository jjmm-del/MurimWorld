using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TrainingGroundPanelUI : BuildingPanelBase
{
    enum Buttons{CloseButton, GroupTrainingTabButton, SparringMatchTabButton }
    enum GameObjects{GroupTrainingSubPanel, SparringMatchSubPanel}
    enum Texts{TrainingGroundText}
    
    //private GroupTrainingSubPanelUI _groupTrainingSubPanel;
    //private SparringMatchSubPanelUI _sparringMatchSubPanel;

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Get<Button>((int)Buttons.CloseButton).onClick.AddListener(ClosePopupUI);
        Get<TextMeshProUGUI>((int)Texts.TrainingGroundText).text = "연무장";
        GameObject[] panels = new GameObject[]
        {
            Get<GameObject>((int)GameObjects.GroupTrainingSubPanel),
            Get<GameObject>((int)GameObjects.SparringMatchSubPanel)
        };

        Button[] tabButtons = new Button[]
        {
            Get<Button>((int)Buttons.GroupTrainingTabButton),
            Get<Button>((int)Buttons.SparringMatchTabButton)
        };
        InitTabs(panels,tabButtons);
        SwitchTab(0);
    }
}
