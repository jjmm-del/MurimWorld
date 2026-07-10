using UnityEngine;
using UnityEngine.UI;
public abstract class BuildingPanelBase : MonoBehaviour
{
    [Header("Base UI Components")]
    [SerializeField] protected Button closeButton;
    
    [Header("Sub Panels (공통 관리)")] 
    [SerializeField] protected GameObject[] subPanels;

    protected virtual void Awake()
    {
        if (closeButton == null)
        {
            Transform findBtn = transform.Find("CloseButton");
            if (findBtn != null)
            {
                closeButton = findBtn.GetComponent<Button>();
            }
            else
            {
                closeButton = GetComponentInChildren<Button>();
            }
        }
    }

    protected virtual void OnEnable()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePanel);
        }
    }

    protected virtual void OnDisable()
    {
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(ClosePanel);
        }
    }

    public virtual void OpenPanel()
    {
        gameObject.SetActive(true);
        CloseAllSubPanels();
        
        // 넣어도 되고 안넣어도 되는 기능 : 창이 처음 열릴 때 무조건 0번탭 (첫번째 메뉴 켜게 해주기)
        // SwitchSubMenu(0);
    }

    public virtual void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    protected void CloseAllSubPanels()
    {
        if (subPanels == null)
        {
            return;
        }

        foreach (GameObject panel in subPanels)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }

    protected virtual void SwitchSubMenu(int index)
    {
        //켜져 있는 subPanel 정리
        CloseAllSubPanels();
        
        //안정성 검사
        if (subPanels != null && index >= 0 && index < subPanels.Length)
        {
            if (subPanels[index] != null)
            {
                subPanels[index].SetActive(true);
            }
        }
    }
}
