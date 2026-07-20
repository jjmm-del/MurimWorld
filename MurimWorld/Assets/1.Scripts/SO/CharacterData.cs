using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "MurimWorld/CharacterData")]

public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public string CharacterId;  //고유 ID
    public string CharacterName;//이름
    public Sprite Portrait;     // 초상화
    public Sprite Splash; //스플래쉬 아트
    public string NickName;
    public MartialRank Rank;
    
    [Header("초기 능력치")]
    public int Strength;
    public int Health;
    public int Dexterity;
    public int Intelligence;
    public int Charm;
    public int InnerStrength;
    public int Leadership;
}
