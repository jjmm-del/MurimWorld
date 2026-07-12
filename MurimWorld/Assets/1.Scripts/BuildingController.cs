using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingController : MonoBehaviour
{
    [Header("건물 설정")]
    [SerializeField]private BuildingType _myBuildingType;
    [SerializeField] private string _buildingName;
    
    
    [Header("하이라이트 설정")]
    public Color HighlightColor = Color.yellow;
    private Color originColor;
    private Renderer meshRenderer;
    
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        if (meshRenderer != null)
        {
            originColor = meshRenderer.material.color;
        }
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (meshRenderer != null)
        {
            meshRenderer.material.color = HighlightColor;
        }

        if (TooltipManager.Instance != null)
        {
            TooltipManager.Instance.ShowTooltip(_buildingName);
        }
    }

    void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (meshRenderer != null)
        {
            meshRenderer.material.color = originColor;
        }

        if (TooltipManager.Instance != null)
        {
            TooltipManager.Instance.HideTooltip();
        }
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if(TooltipManager.Instance!=null)
        {
            TooltipManager.Instance.HideTooltip();
        }
        EventManager.OnBuildingClicked?.Invoke(_myBuildingType);
        meshRenderer.material.color = originColor;
    }
}
