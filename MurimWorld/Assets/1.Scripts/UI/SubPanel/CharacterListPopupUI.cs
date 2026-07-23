using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListPopupUI : UIPopup
{
    enum GameObjects {CharacterGridContainer}
    enum Buttons {CloseButton}
    enum Texts {TitleText}

    [SerializeField] private GameObject _characterItemPrefab;
    private Action<Character> _onCharacterSelected;
    
    // [Header("Scroll View Infrastructure")]
    // [SerializeField] private Transform _contentContainer;
    // [SerializeField] private GameObject _itemPrefab;
    //
    // [Header("Confirmation Popup UI")]
    // [SerializeField] private GameObject _confirmationPopup;
    // [SerializeField] private TextMeshProUGUI _popupMessageText;
    // [SerializeField] private Button _confirmButton;
    // [SerializeField] private Button _cancelButton;
    //
    // private RoleType _currentSelectingRoleSlot = RoleType.None;
    // private Character _pendingCharacter = null;
    //
    // private Action _onAssignmentCompleteCallback;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        
        Get<Button>((int)Buttons.CloseButton).onClick.AddListener(ClosePopupUI);
    }

    public void SetInfo(string title, Action<Character> onHeroSelected)
    {
        Get<TextMeshProUGUI>((int)Texts.TitleText).text = title;
        _onCharacterSelected = onHeroSelected;
        
        RefreshCharacterList();
    }

    

    private void RefreshCharacterList()
    {
        Transform container = Get<GameObject>((int)GameObjects.CharacterGridContainer).transform;
        foreach(Transform child in container) Destroy(child.gameObject);

        var roster = RosterManager.Instance.AllCharacters;

        foreach (Character character in roster)
        {
            GameObject go = Instantiate(_characterItemPrefab, container);
            CharacterItemUI itemUI = go.GetComponent<CharacterItemUI>();
            itemUI.Init();
            
            itemUI.SetInfo(character, OnCharacterClicked);
        }
    }

    private void OnCharacterClicked(Character selectedCharacter)
    {
        ConfirmationPopupUI confirmUI = UIManager.Instance.ShowPopupUI<ConfirmationPopupUI>();
        
        confirmUI.SetInfo($"[{selectedCharacter.BaseData.CharacterName}]을 정말 선택하시겠습니까?", () =>
        {
            _onCharacterSelected?.Invoke(selectedCharacter);
            
            ClosePopupUI();
        });
    }
}

