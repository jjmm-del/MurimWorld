using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AppointmentSubPanelUI : MonoBehaviour
{
    [Header("ScrollView Infra(동적슬롯 생성기")] //동적슬롯으로 변경
    [SerializeField] private Transform _roleContentContainer;
    [SerializeField] private GameObject _roleSlotPrefab;
    
    [Header("3단계: 최종 인재 명단 팝업 스크립트 참조")]
    [SerializeField] private CharacterListPopupUI _characterListPopupUI;

    private List<RoleSlotItemUI> _spawnedSlots = new List<RoleSlotItemUI>();
    private void OnEnable()
    {
        //창이 열릴 대 직책 슬롯들을 동적으로 생성
        GenerateRoleSlots();
    }

    private void GenerateRoleSlots()
    {
        foreach (Transform child in _roleContentContainer)
        {
            Destroy(child.gameObject);
        }

        _spawnedSlots.Clear();

        foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
        {
            if (role == RoleType.None) continue;
            
            GameObject go = Instantiate(_roleSlotPrefab, _roleContentContainer);
            RoleSlotItemUI slotUI = go.GetComponent<RoleSlotItemUI>();

            if (slotUI != null)
            {
                slotUI.Initialize(role, OpenCharacterListPopup);
                _spawnedSlots.Add(slotUI);
            }
        }
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
        foreach (RoleSlotItemUI slot in _spawnedSlots)
        {
            slot.RefreshAssignedCharacter();
        }
    }
    
}
