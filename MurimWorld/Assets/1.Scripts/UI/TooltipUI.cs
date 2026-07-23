using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TooltipUI : UIBase
{
    enum Texts {TooltipText}
    enum GameObjects {TooltipPanel}

    private Vector2 _offset = new Vector2(75f, 35f);
   // private RectTransform _rectTransform;

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        
        //_rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        if (Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Get<GameObject>((int)GameObjects.TooltipPanel).transform.position = mousePos+_offset;
            //_rectTransform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }

    public void Show(string contents)
    {
      Get<GameObject>((int)GameObjects.TooltipPanel).SetActive(true);
      Get<TextMeshProUGUI>((int)Texts.TooltipText).text = contents;
      
      
    }

    public void Hide()
    {
        Get<GameObject>((int)GameObjects.TooltipPanel).SetActive(false);
    }
}
