using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItemUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image _portraitImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _statsText;
    [SerializeField] private TextMeshProUGUI _roleText;
    [SerializeField] private Button _selectButton;

    private Character _targetCharacter;

    private Action<Character> _onClickedCallback;

    private void Awake()
    {
        _roleText.text = "";
    }
    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnItemClicked);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnItemClicked);
    }

    public void Initialize(Character character, Action<Character> onClickCallback)
    {
        _targetCharacter = character;
        _onClickedCallback = onClickCallback;
        
        //데이터 반영
        _nameText.text = character.BaseData.CharacterName;
        _statsText.text = $"무력:{character.BaseData.Strength}";

        if (character.CurrentRoleType != RoleType.None)
        {
            _roleText.text = $"<color=yellow>[{GetRoleNameKorean(character.CurrentRoleType)}]</color>";
        }

        if (character.BaseData.Portrait != null && _portraitImage != null)
        {
            _portraitImage.sprite = character.BaseData.Portrait;
        }
    }

    private void OnItemClicked()
    {
        //알림
        _onClickedCallback?.Invoke(_targetCharacter);
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
