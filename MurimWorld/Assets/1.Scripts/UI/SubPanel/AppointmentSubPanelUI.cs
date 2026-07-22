using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AppointmentSubPanelUI : UI_SubItem
{
    enum GameObjects {RoleSlotContainer}
    [Header("프리팹 연결")] //동적슬롯으로 변경
    [SerializeField] private GameObject _roleSlotPrefab;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        RefreshRoleSlots();
    }

    public void RefreshRoleSlots()
    {
        Transform container = Get<GameObject>((int)GameObjects.RoleSlotContainer).transform;
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        
        foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
        {
            if (role == RoleType.None) continue;
            
            GameObject go = Instantiate(_roleSlotPrefab, container);
            RoleSlotItemUI slotUI = go.GetComponent<RoleSlotItemUI>();

            if (slotUI != null)
            {
                slotUI.Init();
                slotUI.SetInfo(role);
            }
        }
    }
}
