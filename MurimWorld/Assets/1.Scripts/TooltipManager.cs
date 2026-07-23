using UnityEngine;

public class TooltipManager : Singleton<TooltipManager>
{
    private TooltipUI _tooltipUI;
    // [Header("Tooltip UI Components")]
    // [SerializeField] private GameObject _tooltipPanel;
    // [SerializeField] private TextMeshProUGUI _buildingNameText;
    //
    // [Header("Settings")]
    // [SerializeField] private Vector3 _offset = new Vector3(15f, -15f, 0f);

    protected override void Awake()
    {
        base.Awake();
        InitTooltip();
    }

    public void InitTooltip()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/TooltipUI");
        if (prefab == null) return;

        GameObject go = Instantiate(prefab);
        DontDestroyOnLoad(go); // 씬이 바뀌어도 툴팁은 유지

        Canvas canvas = go.GetComponent<Canvas>();
        if(canvas == null) canvas = go.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        canvas.sortingOrder = 9999;
        
        _tooltipUI = go.GetComponent<TooltipUI>();
        _tooltipUI.Init();
    }

    public void ShowTooltip(string content)
    {
        if (Instance != null && Instance._tooltipUI != null)
        {
            Instance._tooltipUI.Show(content);
        }
    }
    public void HideTooltip()
    {
        if (Instance != null && Instance._tooltipUI != null)
        {
            Instance._tooltipUI.Hide();
        }
    }
        
    
}
