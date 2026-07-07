using UnityEngine;
using UnityEngine.UI;
public abstract class BuildingPanelBase : MonoBehaviour
{
    [Header("Base UI Components")]
    [SerializeField] protected Button closeButton;

    protected virtual void Awake()
    {
        if (closeButton == null)
        {
            Transform findBtn = transform.Find("closeButton");
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
    }

    public virtual void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
