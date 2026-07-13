using UnityEngine;

public interface ICommand
{
    string CommandName { get; } // 명령의 이름 ( UI 표시용 )
    string Execute();
}
