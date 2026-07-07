using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance {get ;private set;}
    
    [Header("시간 설정(1턴 = 1개월")]
    [SerializeField] private int currentYear = 1;
    [SerializeField] private int currentMonth = 1;
    
    [Header("행동력(명령서) 설정")]
    [SerializeField] private int maxActionPoints = 3;
    [SerializeField] private int currentActionPoints = 0;

    [Header("현재 상태")]
    [SerializeField] private GamePhase currentPhase = GamePhase.Domestic;
    
    public static event Action OnTurnStateChanged;

    public int CurrentYear => currentYear;
    public int CurrentMonth => currentMonth;
    public int MaxActionPoints => maxActionPoints;
    public int CurrentActionPoints => currentActionPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentActionPoints = maxActionPoints;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        OnTurnStateChanged?.Invoke();
    }

    public bool UseActionPoint(int cost = 1)
    {
        if (currentActionPoints >= cost)
        {
            currentActionPoints -= cost;
            Debug.Log($"명령을 하달했습니다. 남은 행동력: {currentActionPoints}");

            OnTurnStateChanged?.Invoke();
            return true;
        }
        else
        {
            Debug.LogWarning($"행동력(명령서)이 부족하여 명령을 내릴 수 없습니다.");
            return false;
        }
    }

    //턴 종료 버튼을 눌렀을 때 실행 될 함수
    public void EndTurn()
    {
        currentMonth++;
        if (currentMonth > 12)
        {
            currentMonth = 1;
            currentYear++;
        }

        currentActionPoints = maxActionPoints;
        currentPhase = GamePhase.Domestic; //다음 달은 다시 내정부터 시작
        
        Debug.Log($"---{currentYear}년 {currentMonth}월이 밝았습니다 ---");
        
        //추후 다른 로직 추가
        
        //턴이 넘어갔으니 UI에게 시간과 행동력 업데이트 하라고 방송
        OnTurnStateChanged?.Invoke();
    }
}
