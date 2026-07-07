using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingController : MonoBehaviour
{
    [Header("건물 설정")]
    public BuildingType myBuildingType;
    
    
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
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        EventManager.OnBuildingClicked?.Invoke(myBuildingType);


    }
}
