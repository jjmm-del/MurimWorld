using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class ConfirmationPopupUI : UIPopup
{
    enum Texts{Message}
    enum Buttons{ConfirmButton, CancelButton}

    private Action _onConfirmAction;
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        
        Get<Button>((int)Buttons.ConfirmButton).onClick.AddListener(OnConfirmClicked);
        Get<Button>((int)Buttons.CancelButton).onClick.AddListener(ClosePopupUI);
    }

    public void SetInfo(string Message, Action onConfirm)
    {
        Get<TextMeshProUGUI>((int)Texts.Message).text = Message;
        _onConfirmAction = onConfirm;
    }

    private void OnConfirmClicked()
    {
        ClosePopupUI();
        _onConfirmAction?.Invoke();
    }
}
