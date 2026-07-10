using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCastlePanelUI : BuildingPanelBase
{
    [Header("버튼 컴포넌트 연결")]
    [SerializeField] private Button appointmentTabButton; //인재 등록
    [SerializeField] private Button councilTabButton; // 회의
    [SerializeField] private Button diplomacyTabButton;   // 외교
    
    private enum BuildingSubMenu{Appointment, Council, Diplomacy}
    
    protected override void Awake()
    {
        base.Awake();
        
        
    }

    private void Start()
    {
        appointmentTabButton.GetComponentInChildren<TextMeshProUGUI>().text = "인재 등록";
        councilTabButton.GetComponentInChildren<TextMeshProUGUI>().text = "회의";
        diplomacyTabButton.GetComponentInChildren<TextMeshProUGUI>().text = "외교";
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        appointmentTabButton.onClick.AddListener(()=>SwitchSubMenu(BuildingSubMenu.Appointment));
        councilTabButton.onClick.AddListener(()=>SwitchSubMenu(BuildingSubMenu.Council));
        diplomacyTabButton.onClick.AddListener((() => SwitchSubMenu(BuildingSubMenu.Diplomacy)));
        
        
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        appointmentTabButton.onClick.RemoveAllListeners();
        councilTabButton.onClick.RemoveAllListeners();
        diplomacyTabButton.onClick.RemoveAllListeners();
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
