using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItemUI : UI_SubItem
{
    enum Texts {CharacterNameText,CharacterStatText, CharacterRoleText}
    enum Buttons {SelectButton}
    
    // [Header("UI Components")]
    // [SerializeField] private Image _portraitImage;
    // [SerializeField] private TextMeshProUGUI _nameText;
    // [SerializeField] private TextMeshProUGUI _statsText;
    // [SerializeField] private TextMeshProUGUI _roleText;
    // [SerializeField] private Button _selectButton;

    private Character _myCharacter;
    private Action<Character> _onSelectedCallback;
    
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.SelectButton).onClick.AddListener(OnSelectClicked);

    }

    public void SetInfo(Character character, Action<Character> onSelected)
    {
        _myCharacter = character;
        _onSelectedCallback = onSelected;

        Get<TextMeshProUGUI>((int)Texts.CharacterNameText).text = character.BaseData.CharacterName;
        // Get<TextMeshProUGUI>((int)Texts.CharacterStatText).text = character.CharacterData;
        // Get<TextMeshProUGUI>((int)Texts.CharacterNameText).text = character.CharacterName;
    }

    private void OnSelectClicked()
    {
        //알림
        _onSelectedCallback?.Invoke(_myCharacter);
    }

    private string GetRoleNameKorean(RoleType role)
    {
        switch (role)
        {
            case RoleType.AllianceLeader: return "무림맹주";
            case RoleType.AllianceSubLeader: return "무림 부맹주";
            default:
                return "";
        }
        
    }

}
