using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class CommandQueueUI : UIScene
{
    enum GameObjects {ContentContainer}

    [Header("프리팹 연결")]
    [SerializeField] private GameObject _commandItemPrefab;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));

        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnQueueChanged += RefreshQueueUI;
        }
        RefreshQueueUI();
    }

    private void OnDestroy()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnQueueChanged -= RefreshQueueUI;
        }
    }

    private void RefreshQueueUI()
    {
        if (TurnManager.Instance == null) return;

        Transform container = Get<GameObject>((int)GameObjects.ContentContainer).transform;
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        
        IReadOnlyList<ICommand> commands = TurnManager.Instance.PendingCommands;
        foreach (ICommand cmd in commands)
        {
            GameObject go = Instantiate(_commandItemPrefab, container);
            CommandItemUI itemUI = go.GetComponent<CommandItemUI>();
            if (itemUI != null)
            {
                itemUI.Init();
                itemUI.SetInfo(cmd);
            }
        }
    }
}
