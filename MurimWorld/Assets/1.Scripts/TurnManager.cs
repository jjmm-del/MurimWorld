using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    [Header("시간 설정(1턴 = 1개월")]
    [SerializeField] private int _currentYear = 1;
    [SerializeField] private int _currentMonth = 1;
    
    [Header("행동력(ActionPoint")]
    [SerializeField] private int _maxAP = 3;
    [SerializeField] private int _currentAP = 3;
    
    
    [SerializeField]private List<ICommand> _pendingCommands = new List<ICommand>();
    public int CurrentYear => _currentYear;
    public int CurrentMonth => _currentMonth;
    public int MaxAP => _maxAP;
    public int CurrentAP => _currentAP;
    public IReadOnlyList<ICommand> PendingCommands => _pendingCommands;

    public Action OnQueueChanged;
    public Action OnActionPointChanged;
    public Action<string> OnCommandExecuted; //명령 하나가 끝날 때 발생
    public Action<int, int> OnMonthEnded; //모든 명령이 끝나고 달이 넘어갈 때 발생

    protected override void Awake()
    {
        base.Awake();
        RefreshMaxActionPoint();
    }

    public void RefreshMaxActionPoint()
    {
        int baseAp = 3;
        if (RosterManager.Instance != null)
        {
            int totalMembers = RosterManager.Instance.AllCharacters.Count;

            int memeberBonus = totalMembers / 3; 
            
            //추가 로직
            _maxAP = baseAp + memeberBonus;
        }
        _currentAP = _maxAP;
        OnActionPointChanged?.Invoke();
    }

    public bool TryEnQueueCommand(ICommand command, int apCost)
    {
        if (_currentAP >= apCost)
        {
            _currentAP -= apCost;
            _pendingCommands.Add(command);

            OnActionPointChanged?.Invoke();
            OnQueueChanged?.Invoke();
            Debug.Log($"[명령하달]{command.CommandName} 예약 완료");
            return true;
        }

        return false;
    }

    public void CancelCommand(ICommand commandToCancel, int refundAP)
    {
        if (_pendingCommands.Contains(commandToCancel))
        {
            _pendingCommands.Remove(commandToCancel);
            _currentAP += refundAP; //ap환불
            OnActionPointChanged?.Invoke();
            OnQueueChanged?.Invoke();
            Debug.Log($"[명령 취소]{commandToCancel.CommandName}취소 완료. {refundAP}AP가 환불");

        }
    }
    
    //턴 종료 시 실행 될 함수
    public void ExecuteTurn()
    {
        //코루틴을 돌려 순차적을 실행(시각적 피드백)
        StartCoroutine(ExecuteTurnCoroutine());
    }

    public IEnumerator ExecuteTurnCoroutine()
    {
        int executedYear = _currentYear;
        int executedMonth = _currentMonth;
        
        //명령들 실행
        OnCommandExecuted?.Invoke($"\n==={executedYear}년 {executedMonth}월 실행 보고===");
        
        List<ICommand> commandToExecute = new List<ICommand>(_pendingCommands);
        
        _pendingCommands.Clear();
        
        foreach (ICommand cmd in commandToExecute)
        {
            string resultLog = cmd.Execute();
            OnCommandExecuted?.Invoke($"--{resultLog}");

            if (cmd is CraftWeaponCommand craftCmd)
            {
                if (!craftCmd.IsCompleted)
                {
                    _pendingCommands.Add(craftCmd);
                }
            }
            
            yield return new WaitForSeconds(0.7f); //
        }
        
        
        //시간변경 로직
        _currentMonth++;
        if (_currentMonth > 12)
        {
            _currentMonth = 1;
            _currentYear++;
        }
        
        OnQueueChanged?.Invoke();
        RefreshMaxActionPoint();
        OnMonthEnded?.Invoke(executedYear, executedMonth);
    }
    
}
