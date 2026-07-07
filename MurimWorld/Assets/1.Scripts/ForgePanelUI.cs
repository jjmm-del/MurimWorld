using System;
using UnityEngine;
using UnityEngine.UI;

public class ForgePanelUI : MonoBehaviour
{
    [Header("버튼 컴포넌트 연결")] [SerializeField]
    private Button closeButton;
    [SerializeField] private Button craftButton; // 제작
    [SerializeField] private Button repairButton; // 수리 등 나중에 추가

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        craftButton.onClick.AddListener(OnCraftButtonClicked);
        repairButton.onClick.AddListener(OnRepairButtonClicked);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        craftButton.onClick.RemoveListener(OnCraftButtonClicked);
        repairButton.onClick.RemoveListener(OnRepairButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }

    // 📌 무기 제작 버튼 클릭 (행동력 1 소모)
    private void OnCraftButtonClicked()
    {
        if (TurnManager.Instance != null)
        {
            if (TurnManager.Instance.UseActionPoint(1))
            {
                Debug.Log("대장간에서 예리한 철검을 한 자루 제작했습니다!");
                // (추후 여기에 인벤토리에 무기 아이템을 추가하는 로직 삽입)
            }
        }
    }

    private void OnRepairButtonClicked()
    {
        if (TurnManager.Instance != null)
        {
            if (TurnManager.Instance.UseActionPoint(1))
            {
                Debug.Log("대장간에서 무기 수리");
            }
        }
    }
}
