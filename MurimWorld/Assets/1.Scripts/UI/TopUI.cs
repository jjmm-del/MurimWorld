using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TopUI : UIScene
{
    //Hierarchy 창에 있는 오브젝트 이름과 스펠링이 같아야함
    enum Texts {DateText, ActionPointText}
    enum Buttons {EndTurnButton}

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        
        Get<Button>((int)Buttons.EndTurnButton).onClick.AddListener(OnEndTurnButtonClicked);

        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnActionPointChanged += UpdateTopUI;
            TurnManager.Instance.OnMonthEnded += HandleMonthEnded;
            
        }

        UpdateTopUI();
    }

    private void OnDestroy()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnActionPointChanged -= UpdateTopUI;
            TurnManager.Instance.OnMonthEnded -= HandleMonthEnded;
        }
    }
    
    private void UpdateTopUI()
    {
        if (TurnManager.Instance == null) return;

        Get<TextMeshProUGUI>((int)Texts.DateText).text = $"{TurnManager.Instance.CurrentYear}년 {TurnManager.Instance.CurrentMonth}월";
        Get<TextMeshProUGUI>((int)Texts.ActionPointText).text = $"남은 행동력 : {TurnManager.Instance.CurrentAP}/{TurnManager.Instance.MaxAP}";
    }

    private void OnEndTurnButtonClicked()
    {
        if (TurnManager.Instance == null) return;

        Get<Button>((int)Buttons.EndTurnButton).interactable = false;
        TurnManager.Instance.ExecuteTurn();

    }

    private void HandleMonthEnded(int year, int month)
    {
        UpdateTopUI();
        Get<Button>((int)Buttons.EndTurnButton).interactable = true;
        
        //[핵심] 여기서 UIManager를 통해 '월말 결산 팝업'을 띄우기
        //UIManager.Instance.ShowPopupUI<MonthlyReportPopupUI>();
    }
}
