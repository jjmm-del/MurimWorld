using UnityEngine;

public class UIPopup : UIBase
{
    public override void Init()
    {
        UIManager.Instance.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        UIManager.Instance.ClosePopupUI(this);
    }
}
