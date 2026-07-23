using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoleSlotItemUI : UI_SubItem
{
    enum Texts {RoleNameText, AssignedCharacterNameText}
    enum Buttons{AppointmentButton};
    
    private RoleType _myRole;

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        
        Get<Button>((int)Buttons.AppointmentButton).onClick.AddListener(OnAppointmentButtonClicked);
    }
    public void SetInfo(RoleType role)
    {
        _myRole = role;
        Get<TextMeshProUGUI>((int)Texts.RoleNameText).text = GetRoleNameKorean(_myRole);
        if (RosterManager.Instance != null)
        {
            Character appointedCharacter = RosterManager.Instance.GetCharacterByRole(_myRole);
            if (appointedCharacter != null)
            {
                Get<TextMeshProUGUI>((int)Texts.AssignedCharacterNameText).text =
                    appointedCharacter.BaseData.CharacterName;
            }
            else
            {
                Get<TextMeshProUGUI>((int)Texts.AssignedCharacterNameText).text = "공석";
            }
        }
        
        
    }

    public void OnAppointmentButtonClicked()
    {
        if (UIManager.Instance == null) return;
        CharacterListPopupUI popup = UIManager.Instance.ShowPopupUI<CharacterListPopupUI>();
        string koreanName = GetRoleNameKorean(_myRole);
        popup.SetInfo($"[{koreanName}] 임명할 인재 선택", (selectedCharacter) =>
        {
            Debug.Log($"임명성공");
            
            //실제 임명 로직
            if (RosterManager.Instance != null)
            {
                RosterManager.Instance.AppointRole(_myRole, selectedCharacter);
            }
            AppointmentSubPanelUI parentPanel = GetComponentInParent<AppointmentSubPanelUI>();
            if (parentPanel != null)
            {
                parentPanel.RefreshRoleSlots();
            }
            //SetInfo(_myRole);
        });
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
