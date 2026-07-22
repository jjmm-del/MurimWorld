using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListPopupUI : UIPopup
{
    [Header("Scroll View Infrastructure")]
    [SerializeField] private Transform _contentContainer;
    [SerializeField] private GameObject _itemPrefab;
    
    [Header("Confirmation Popup UI")]
    [SerializeField] private GameObject _confirmationPopup;
    [SerializeField] private TextMeshProUGUI _popupMessageText;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    private RoleType _currentSelectingRoleSlot = RoleType.None;
    private Character _pendingCharacter = null;

    private Action _onAssignmentCompleteCallback;
    private void OnEnable()
    {
        _confirmButton.onClick.AddListener(OnConfirmAssignment);
        _cancelButton.onClick.AddListener(OnCancelAssignment);
    }

    private void OnDisable()
    {
        _confirmButton.onClick.RemoveListener(OnConfirmAssignment);
        _cancelButton.onClick.RemoveListener(OnCancelAssignment);
    }

    public void OpenPopup(RoleType targetRoleSlot, Action onCompleteCallback)
    {
        _currentSelectingRoleSlot = targetRoleSlot;
        _onAssignmentCompleteCallback = onCompleteCallback;
        
        _confirmationPopup.SetActive(false);
        _pendingCharacter = null;
        
        gameObject.SetActive(true);
        RefreshCharacterList();
    }

    private void RefreshCharacterList()
    {
        foreach (Transform child in _contentContainer)
        {
            Destroy(child.gameObject);
        }

        if (RosterManager.Instance == null)
        {
            return;
        }

        List<Character> roster = RosterManager.Instance.AllCharacters;

        foreach (Character character in roster)
        {
            GameObject go = Instantiate(_itemPrefab, _contentContainer);
            CharacterItemUI itemUI = go.GetComponent<CharacterItemUI>();

            if (itemUI != null)
            {
                itemUI.Initialize(character, OnCharacterSelected);
            }
        }
    }

    private void OnCharacterSelected(Character selectedCharacter)
    {
        if (selectedCharacter.CurrentRoleType == _currentSelectingRoleSlot)
        {
            Debug.Log($"{selectedCharacter.BaseData.CharacterName}은(는) 이미 해당 직책입니다.");
            return;
        }

        if (selectedCharacter.CurrentRoleType != RoleType.None)
        {
            _pendingCharacter = selectedCharacter;

            _popupMessageText.text =
                $"<color=yellow>{selectedCharacter.BaseData.CharacterName}</color>님은 이미 다른 중책을 맡고 계십니다. \n직책을 변경하시겠습니까?";
            _confirmationPopup.SetActive(true);
        }
        else
        {
            ExecuteAssignment(selectedCharacter);
        }
    }

    private void OnConfirmAssignment()
    {
        if (_pendingCharacter != null)
        {
            ExecuteAssignment(_pendingCharacter);
            
        }
    }

    private void OnCancelAssignment()
    {
        _pendingCharacter = null;
        _confirmationPopup.SetActive(false);
    }

    private void ExecuteAssignment(Character selectedCharacter)
    {
        if (RosterManager.Instance != null)
        {
            RosterManager.Instance.AssignRole(selectedCharacter, _currentSelectingRoleSlot);
        }
        _pendingCharacter = null;
        _confirmationPopup.SetActive(false);
        this.gameObject.SetActive(false);
        RefreshCharacterList();
        _onAssignmentCompleteCallback?.Invoke();
    }
    
}

