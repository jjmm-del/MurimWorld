using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TopUI : MonoBehaviour
{
    [Header("UI컴포넌트 연결")]
    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private TextMeshProUGUI _actionPointText;
    [SerializeField] private Button _endTurnButton;

    private void OnEnable()
    {
        _endTurnButton.onClick.AddListener(OnEndTurnButtonClicked);
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnActionPointChanged += UpdateTopUI; ;
            TurnManager.Instance.OnMonthEnded += HandleMonthEnded;
        }
    }

    private void OnDisable()
    {
        _endTurnButton.onClick.RemoveListener(OnEndTurnButtonClicked);
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnActionPointChanged -= UpdateTopUI;
            TurnManager.Instance.OnMonthEnded -= HandleMonthEnded;
        }
    }

    private void Start()
    {
        UpdateTopUI();
    }

    private void UpdateTopUI()
    {
        if (TurnManager.Instance == null) return;
        _dateText.text = $"{TurnManager.Instance.CurrentYear}년 {TurnManager.Instance.CurrentMonth}월";
        if (_actionPointText != null)
        { 
            _actionPointText.text = $"남은 행동력: {TurnManager.Instance.CurrentAP}/{TurnManager.Instance.MaxAP}"; 
        }
            
    }

    private void OnEndTurnButtonClicked()
    {
        if (TurnManager.Instance == null) return;
        
        _endTurnButton.interactable = false;
        TurnManager.Instance.ExecuteTurn();
        
    }

    private void HandleMonthEnded(int year, int month)
    {
        UpdateTopUI();
        EnableEndTurnButton();
    }
    private void EnableEndTurnButton()
    {
        if (_endTurnButton != null)
        {
            _endTurnButton.interactable = true;
        }
    }

}
