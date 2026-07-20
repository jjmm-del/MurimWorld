using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCastlePanelUI : BuildingPanelBase
{
    [Header("버튼 컴포넌트 연결")]
    [SerializeField] private Button _appointmentTabButton; //인재 등록
    [SerializeField] private Button _councilTabButton; // 회의
    [SerializeField] private Button _diplomacyTabButton;   // 외교
    
    private enum BuildingSubMenu{Appointment, Council, Diplomacy}
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _appointmentTabButton.GetComponentInChildren<TextMeshProUGUI>().text = "인재 등록";
        _councilTabButton.GetComponentInChildren<TextMeshProUGUI>().text = "회의";
        _diplomacyTabButton.GetComponentInChildren<TextMeshProUGUI>().text = "외교";
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        _appointmentTabButton.onClick.AddListener(()=>SwitchSubMenu(BuildingSubMenu.Appointment));
        _councilTabButton.onClick.AddListener(()=>SwitchSubMenu(BuildingSubMenu.Council));
        _diplomacyTabButton.onClick.AddListener((() => SwitchSubMenu(BuildingSubMenu.Diplomacy)));
        
        
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _appointmentTabButton.onClick.RemoveAllListeners();
        _councilTabButton.onClick.RemoveAllListeners();
        _diplomacyTabButton.onClick.RemoveAllListeners();
    }
    
    

    private void SwitchSubMenu(BuildingSubMenu targetMenu)
    {
        base.SwitchSubMenu((int)targetMenu);

        switch (targetMenu)
        {
            case BuildingSubMenu.Appointment:
                Debug.Log("인재 관리");
                break;
            case BuildingSubMenu.Council:
                Debug.Log("지도를 보며 정세 파악");
                break;
            case BuildingSubMenu.Diplomacy:
                Debug.Log("외교");
                break;
        }
    }
    
    
    
}
