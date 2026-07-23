using UnityEngine;
using System.Collections.Generic;


public class CraftSubPanelUI : MonoBehaviour
{
    [SerializeField] private Transform _contentContainer;
    [SerializeField] private GameObject _craftSlotPrefab;

    [SerializeField] private EquipmentData[] _craftableBlueprints;
    private void OnEnable()
    {
        RefreshCraftList();
    }

    private void RefreshCraftList()
    {
        foreach (Transform child in _contentContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (EquipmentData blueprint in _craftableBlueprints)
        {
            GameObject go = Instantiate(_craftSlotPrefab, _contentContainer);
            go.GetComponent<CraftSlotUI>()?.Initialize(blueprint);
            
        }
    }
    
    
}
