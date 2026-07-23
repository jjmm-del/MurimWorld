using UnityEngine;
using TMPro;
public class TooltipUI : UIBase
{
    enum Texts {TooltipText}
    enum GameObjects {TooltipPanel}

    private RectTransform _rectTransform;

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        
        _rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    public void Show(string contents, Vector2 position)
    {
      Get<GameObject>((int)GameObjects.TooltipPanel).SetActive(true);
      Get<TextMeshProUGUI>((int)GameObjects.TooltipPanel).text = contents;
      
      transform.position = position;
    }

    public void Hide()
    {
        Get<GameObject>((int)GameObjects.TooltipPanel).SetActive(false);
    }
}
