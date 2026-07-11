using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AppointmentSubPanelUI : MonoBehaviour
{
    [Header("2단계: 직책 슬롯 버튼들")] //동적슬롯으로 변경
    [SerializeField] private Button _leaderSlotButton;  //무림맹주
    [SerializeField] private Button _strategistLotButton;   //책사
    [SerializeField] private Button _medicineSlotButton;    //의약당주
    [SerializeField] private Button _windSwordSlotButton;   //풍검대주
    
    [Header("2단계: 현재 임명된 자들의 이름 표시Text")]
    [SerializeField] private TextMeshProUGUI _leaderNameText;
    [SerializeField] private TextMeshProUGUI _strategistNameText;
    [SerializeField] private TextMeshProUGUI _medicineNameText;
    [SerializeField] private TextMeshProUGUI _windSwordNameText;
    
    [Header("3단계: 최종 인재 명단 팝업 스크립트 참조")]
    [SerializeField] private CharacterListPopupUI _characterListPopupUI;

    private void OnEnable()
    {
        _leaderSlotButton.onClick.AddListener(() => OpenCharacterListPopup(RoleType.AllianceLeader));
        _strategistLotButton.onClick.AddListener(() => OpenCharacterListPopup(RoleType.Strategist));
        _medicineSlotButton.onClick.AddListener(() => OpenCharacterListPopup(RoleType.MedicineHead));
        _windSwordSlotButton.onClick.AddListener(() => OpenCharacterListPopup(RoleType.WindSword));

        UpdateCurrentRosterDisplay();
    }

    private void OnDisable()
    {
        _leaderSlotButton.onClick.RemoveAllListeners();
        _strategistLotButton.onClick.RemoveAllListeners();
        _medicineSlotButton.onClick.RemoveAllListeners();
        _windSwordSlotButton.onClick.RemoveAllListeners();
    }

    private void OpenCharacterListPopup(RoleType roleSlot)
    {
        if(_characterListPopupUI!=null)
        {
            _characterListPopupUI.OpenPopup(roleSlot, UpdateCurrentRosterDisplay);
        }
    }

    public void UpdateCurrentRosterDisplay()
    {
        if (RosterManager.Instance == null) return;
        _leaderNameText.text = "공석";
        _strategistNameText.text = "공석";
        _medicineNameText.text = "공석";
        _windSwordNameText.text = "공석";

        foreach (Character c in RosterManager.Instance.AllCharacters)
        {
            switch (c.CurrentRoleType)
            {
                case RoleType.AllianceLeader: _leaderNameText.text = c.BaseData.CharacterName; break;
                case RoleType.Strategist:     _strategistNameText.text = c.BaseData.CharacterName; break;
                case RoleType.MedicineHead:   _medicineNameText.text = c.BaseData.CharacterName; break;
                case RoleType.WindSword:  _windSwordNameText.text = c.BaseData.CharacterName; break;
            }
        }
        
        
    }
    
}
