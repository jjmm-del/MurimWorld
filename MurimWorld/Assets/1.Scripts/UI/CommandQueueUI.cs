using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class CommandQueueUI : MonoBehaviour
{
    [SerializeField] private Transform _contentContainer; 
    [SerializeField] private GameObject _commandItemPrefab;

    private void OnEnable()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnQueueChanged += RefreshQueueUI;
        }

        RefreshQueueUI();
    }

    private void OnDisable()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnQueueChanged -= RefreshQueueUI;
        }
    }

    private void RefreshQueueUI()
    {
        if (TurnManager.Instance == null) return;

        foreach (Transform child in _contentContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (ICommand cmd in TurnManager.Instance.PendingCommands)
        {
            GameObject go = Instantiate(_commandItemPrefab, _contentContainer);
            CommandItemUI itemUI = go.GetComponent<CommandItemUI>();
            if (itemUI != null)
            {
                itemUI.Initialize(cmd);
            }
        }
    }
}
