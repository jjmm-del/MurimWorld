using UnityEngine;
using System;
using System.Linq.Expressions;
using TMPro;
using UnityEngine.UI;

public class RoleSlotItemUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _roleNameText;
    [SerializeField] private TextMeshProUGUI _assignedNameText;
    [SerializeField] private Button _slotButton;

    private RoleType _myRole;
    private Action<RoleType> _onSlotClickedCallback;

    public void Initialize(RoleType role, Action<RoleType> onClickCallback)
    {
        _myRole = role;
        _onSlotClickedCallback = onClickCallback;

        _roleNameText.text = GetRoleNameKorean(role);
        RefreshAssignedCharacter();
        
        _slotButton.onClick.RemoveAllListeners();
        _slotButton.onClick.AddListener((() => _onSlotClickedCallback?.Invoke(_myRole)));
    }

    public void RefreshAssignedCharacter()
    {
        if (RosterManager.Instance == null) return;
        
        Character assignedCharacter = RosterManager.Instance.AllCharacters.Find(c=>c.CurrentRoleType == _myRole);
        if (assignedCharacter != null)
        {
            _assignedNameText.text = $"<color=#00FF00>{assignedCharacter.BaseData.CharacterName}</color>";
        }
        else
        {
            _assignedNameText.text =$"<color=gray>공석</color>";
        }
    }

    private string GetRoleNameKorean(RoleType role)
    {
        switch (role)
        {
            case RoleType.AllianceLeader: return "무림맹주";
            case RoleType.AllianceSubLeader : return "무림부맹주";
            case RoleType.MedicineHead: return "약왕당주";
            case RoleType.Strategist : return "총괄군사";
            case RoleType.WindSword : return "풍검대주";
            default: return role.ToString();
                
        }
    }
    
    
}
