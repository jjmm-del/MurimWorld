using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class TurnResultUI : MonoBehaviour
{
    [Header("하단 시스템 로그 UI")]
    [SerializeField]private GameObject _bottomLogPanel;
    [SerializeField] private TextMeshProUGUI _bottomLogText;
    [SerializeField] private Button _toggleLogButton;
    private bool _isLogOpen = true;
    
    [Header("월말 결산 팝업 UI")]
    [SerializeField] private GameObject _reportPopupPanel;
    [SerializeField] private TextMeshProUGUI _reportTitleText;
    [SerializeField] private TextMeshProUGUI _reportContentText;
    [SerializeField] private Button _closeReportButton;

    private List<string> _logHistory = new List<string>();
    private const int MaxLogLines = 50;
    private string _monthlySummary = "";

    private void Awake()
    {
        if (_toggleLogButton != null)
        {
            _toggleLogButton.onClick.AddListener(ToggleBottomLog);
        }
    }
    private void OnEnable()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnCommandExecuted += HandleCommandLog;
            TurnManager.Instance.OnMonthEnded += ShowMonthlyReport;
        }

        _closeReportButton.onClick.AddListener(CloseReport);
    }

    private void OnDisable()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnCommandExecuted -= HandleCommandLog;
            TurnManager.Instance.OnMonthEnded -= ShowMonthlyReport;
        }

        _closeReportButton.onClick.RemoveListener(CloseReport);
    }

    private void HandleCommandLog(string log)
    {
        _logHistory.Add(log);
        if (_logHistory.Count > MaxLogLines)
        {
            _logHistory.RemoveAt(0);
        }
        
        _bottomLogText.text = string.Join($"\n", _logHistory);

        if (!log.StartsWith("==="))
        {
            _monthlySummary += $"\n{log}";
        }
    }

    private void ShowMonthlyReport(int year, int month)
    {
        _reportTitleText.text = $"무림맹 정세 보고({year}년 {month}월)";

        if (string.IsNullOrEmpty(_monthlySummary))
        {
            _monthlySummary = "\n이번 달은 평화로웠습니다. 특이 동향이 없습니다.";
        }
        _reportContentText.text = _monthlySummary;
        _reportPopupPanel.SetActive(true);
        
        _monthlySummary = "";
    }

    private void CloseReport()
    {
        _reportPopupPanel.SetActive(false);
    }

    private void ToggleBottomLog()
    {
        _isLogOpen = !_isLogOpen;
        _bottomLogPanel.SetActive(_isLogOpen);
    }
}
