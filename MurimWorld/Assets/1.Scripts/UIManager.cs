using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil;

public class UIManager : Singleton<UIManager>
{
    //팝업이 뜰 때마다 캔버스가 위로 겹치도록 올려주는 기준값
    private int _order = 10;
    private Stack<UIPopup> _popupStack = new Stack<UIPopup>();

    private void Start()
    {
        EventManager.OnBuildingClicked += HandleBuildingClicked;
        Instance.ShowSceneUI<TopUI>();
        Instance.ShowSceneUI<BottomLogUI>();
        Instance.ShowSceneUI<CommandQueueUI>(); //GameManager로 이관해도 될거같기도
    }

    private void OnDestroy()
    {
        EventManager.OnBuildingClicked -= HandleBuildingClicked;
    }
    
    
    #region 기존 프레임 워크 코어 인프라
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.FindChild<Canvas>(go, null, true);
        if(canvas == null) canvas = go.AddComponent<Canvas>();

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/{name}");
        if (prefab == null)
        {
            Debug.LogError($"[UIManager]{name}SceneUI 프리팹을 찾을 수 없습니다.");
            return null;
        }
        GameObject go = Instantiate(prefab);
        T sceneUI = go.GetComponent<T>();
        SetCanvas(go, false);
        return sceneUI;
    }
    
    public T ShowPopupUI<T>(string name = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/{name}");
        if (prefab == null)
        {
            Debug.LogError($"[UIManager]{name}PopupUI 프리팹을 Resources에서 찾을 수 없습니다.");
            return null;
        }

        GameObject go = Instantiate(prefab);
        T popup = go.GetComponent<T>();
        
        _popupStack.Push(popup);
        popup.Init();
        
        return popup;
    }
        
    

    public void ClosePopupUI(UIPopup popup)
    {
        if (_popupStack.Count == 0) return;

        if (_popupStack.Peek() != popup)
        {
            Debug.LogWarning("[UIManager] 닫으려는 팝업이 최상단 팝업이 아닙니다.");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0) return;

        UIPopup popup = _popupStack.Pop();
        Destroy(popup.gameObject);

        _order--;
    }
    #endregion
    private void HandleBuildingClicked(BuildingType clickedBuildingType)
    {
        if (Instance == null) return;
        CloseAllPopupUI();

        switch (clickedBuildingType)
        {
            case BuildingType.MainCastle:
                Instance.ShowPopupUI<MainCastlePanelUI>();
                break;
             case BuildingType.Forge:
                 Instance.ShowPopupUI<ForgePanelUI>();
                 break;
             case BuildingType.TrainingGround:
                 Instance.ShowPopupUI<TrainingGroundPanelUI>();
                 break;
            // case BuildingType.Pharmacy:
            //     Instance.ShowPopupUI<PharmacyPanelUI>();
            // case BuildingType.TrainingGround:
            //     Instance.ShowPopupUI<TrainingGroundPanelUI>();
            // case BuildingType.TrainingGround:
            //     Instance.ShowPopupUI<TrainingGroundPanelUI>();
            // case BuildingType.TrainingGround:
            //     Instance.ShowPopupUI<TrainingGroundPanelUI>();
            default:
                Debug.LogWarning("연결된 UI가 없는 건물입니다." + clickedBuildingType);
                break;
        }
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }
}
