using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class TooltipManager : Singleton<TooltipManager>
{
    [Header("Tooltip UI Components")]
    [SerializeField] private GameObject _tooltipPanel;
    [SerializeField] private TextMeshProUGUI _buildingNameText;

    [Header("Settings")]
    [SerializeField] private Vector3 _offset = new Vector3(15f, -15f, 0f);

    protected override void Awake()
    {
        base.Awake();
        HideTooltip();
    }

    private void Update()
    {
        if (_tooltipPanel != null && _tooltipPanel.activeSelf)
        {
            if (Mouse.current != null)
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                _tooltipPanel.transform.position = new Vector3(mousePos.x, mousePos.y,0)+_offset;
            }
        }
    }

    public void ShowTooltip(string buildingName)
    {
        if (_tooltipPanel == null || _buildingNameText == null) return;
        _buildingNameText.text = buildingName;
        _tooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        if (_tooltipPanel != null)
        {
            _tooltipPanel.SetActive(false);
        }
    }
        
    
}
