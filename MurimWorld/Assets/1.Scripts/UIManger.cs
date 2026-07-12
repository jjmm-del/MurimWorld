using UnityEngine;

public class UIManger : MonoBehaviour
{
    [Header("UI 패널 연결")]
    [SerializeField]private MainCastlePanelUI mainCastlePanel;
    [SerializeField] private ForgePanelUI forgePanel;
    //필요한 패널 계속 추가

    void OnEnable()
    {
        //EventManager 이벤트 구독
        EventManager.OnBuildingClicked += HandleBuildingClicked;
    }

    void OnDisable()
    {
        EventManager.OnBuildingClicked -= HandleBuildingClicked;
    }

    
    // 건물 클릭시 콜백함수
    private void HandleBuildingClicked(BuildingType clickedBuildingType)
    {
        CloseAllPanel();

        switch (clickedBuildingType)
        {
            case BuildingType.MainCastle:
                if (mainCastlePanel != null)
                {
                    mainCastlePanel.OpenPanel();
                }
                break;
            case BuildingType.Forge:
                if (forgePanel != null)
                {
                    forgePanel.OpenPanel();
                }
                break;
            default:
                Debug.LogWarning("연결된 UI가 없는 건물입니다."+clickedBuildingType);
                break;
        }
    }

    private void CloseAllPanel()
    {
        if (mainCastlePanel != null)
        {
            mainCastlePanel.ClosePanel();
        }

        if (forgePanel != null)
        {
            forgePanel.ClosePanel();
        }
    }
}
