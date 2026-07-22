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
        
        //Character appointedCharacter = RosterManager.Instance.
        //string appointedName = 
        Get<TextMeshProUGUI>((int)Texts.AssignedCharacterNameText).text = "공석";
    }

    public void OnAppointmentButtonClicked()
    {
        if (UIManager.Instance == null) return;
        CharacterListPopupUI popup = UIManager.Instance.ShowPopupUI<CharacterListPopupUI>();
        
        //popup.SetTargetRole(_myRole);
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
