using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class BottomLogUI : UIScene
{
    enum Texts {BottomLogText}
    enum Buttons {ToggleLogButton}
    enum GameObjects {BottomLogPanel}

    private List<string> _logHistory = new List<string>();
    private const int MaxLogLines = 50;
    private bool _isLogOpen = true;

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        
        Get<Button>((int)Buttons.ToggleLogButton).onClick.AddListener(ToggleBottomLog);
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnCommandExecuted += HandleCommandLog;
        }
        
    }

    private void OnDestroy()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnCommandExecuted -= HandleCommandLog;
        }
    }

    private void HandleCommandLog(string log)
    {
        _logHistory.Insert(0, log);
        if(_logHistory.Count>MaxLogLines) _logHistory.RemoveAt(_logHistory.Count -1);

        Get<TextMeshProUGUI>((int)Texts.BottomLogText).text = string.Join("\n", _logHistory);
    }

    private void ToggleBottomLog()
    {
        _isLogOpen =!_isLogOpen;
        Get<GameObject>((int)GameObjects.BottomLogPanel).SetActive(_isLogOpen);
    }
    
}
