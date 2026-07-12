using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TopUI : MonoBehaviour
{
    [Header("UI컴포넌트 연결")]
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI actionPointText;
    [SerializeField] private Button endTurnButton;

    private void OnEnable()
    {
        TurnManager.OnTurnStateChanged += UpdateTopUI;
        endTurnButton.onClick.AddListener(OnEndTurnButtonClicked);
    }

    private void OnDisable()
    {
        TurnManager.OnTurnStateChanged -= UpdateTopUI;
        endTurnButton.onClick.RemoveListener(OnEndTurnButtonClicked);
    }

    private void Start()
    {
        UpdateTopUI();
    }

    private void UpdateTopUI()
    {
        if (TurnManager.Instance != null)
        {
            dateText.text = $"{TurnManager.Instance.CurrentYear}년 {TurnManager.Instance.CurrentMonth}월";
            actionPointText.text = $"남은 행동력: {TurnManager.Instance.CurrentActionPoints}/{TurnManager.Instance.MaxActionPoints}";
            
        }
    }

    private void OnEndTurnButtonClicked()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.EndTurn();
        }
    }

}
