using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class InventoryPanelUI : MonoBehaviour
{
    [Header("상단 분류 탭 버튼")]
    [SerializeField] private Button _materialTabButton;
    [SerializeField] private Button _consumableTabButton;
    [SerializeField] private Button _closeButton;
    
    [Header("그리드 컨테니어 및 아이템 칸 프리팹")]
    [SerializeField] private Transform _gridContainer;
    [SerializeField] private GameObject _inventoryItemPrefab;
    
    private enum InventoryTab {Material, Consumable}

    private InventoryTab _currentTab = InventoryTab.Material;

    private void OnEnable()
    {
        _materialTabButton.onClick.AddListener(()=> SwitchTab(InventoryTab.Material));
        _consumableTabButton.onClick.AddListener(()=> SwitchTab(InventoryTab.Consumable));
        _closeButton.onClick.AddListener(()=> gameObject.SetActive(false));

        RefreshInventoryUI();
    }

    private void OnDisable()
    {
        _materialTabButton.onClick.RemoveAllListeners();
        _consumableTabButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    private void SwitchTab(InventoryTab targetTab)
    {
        _currentTab= targetTab;
        RefreshInventoryUI();
    }

    public void RefreshInventoryUI()
    {
        foreach (Transform child in _gridContainer) Destroy(child.gameObject);
        if (InventoryManager.Instance == null) return;

        ItemType targetType = (_currentTab == InventoryTab.Material)?ItemType.Material : ItemType.Consumable;
        foreach (var pair in InventoryManager.Instance.Inventory)
        {
            if (pair.Value <= 0 || pair.Key.ItemType != targetType) continue;
            CreateItemSlot(pair.Key.Icon, pair.Key.ItemName, pair.Value);
        }
    }

    private void CreateItemSlot(Sprite icon, string itemName, int count)
    {
        GameObject go = Instantiate(_inventoryItemPrefab, _gridContainer);
        
        Image iconImage = go.GetComponentInChildren<Image>();
        TextMeshProUGUI countText = go.GetComponentInChildren<TextMeshProUGUI>();

        if (iconImage != null) iconImage.sprite = icon;
        if (countText != null) countText.text = count.ToString();

    }



}
