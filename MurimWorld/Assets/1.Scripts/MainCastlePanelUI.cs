using UnityEngine;
using UnityEngine.UI;

public class MainCastlePanelUI : MonoBehaviour
{
    [Header("버튼 컴포넌트 연결")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button reliefButton; // 메인 행동들 -> 나중에 수정
    [SerializeField] private Button expandButton;   // 22

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        reliefButton.onClick.AddListener(OnReliefButtonClicked);
        expandButton.onClick.AddListener(OnExpandButtonClicked);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        reliefButton.onClick.RemoveListener(OnReliefButtonClicked);
        expandButton.onClick.RemoveListener(OnExpandButtonClicked);   
    }
    
    // x 버튼 클릭 시 패널 비활성화
    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
    
    // 메인행동 1 
    private void OnReliefButtonClicked()
    {
        if (TurnManager.Instance != null)
        {
            if (TurnManager.Instance.UseActionPoint(1))
            {
                Debug.Log("임시 메인행동 1 실행 완료 행동력 1 감소");
            }
        }
    }

    private void OnExpandButtonClicked()
    {
        if (TurnManager.Instance != null)
        {
            if (TurnManager.Instance.UseActionPoint(2))
            {
                Debug.Log("임시 메인행동 2 실행완료 행동력 2 감소");
            }
        }
    }
}
