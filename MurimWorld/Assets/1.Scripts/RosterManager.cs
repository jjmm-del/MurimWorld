using System.Collections.Generic;
using UnityEngine;

public class RosterManager : Singleton<RosterManager>
{
    [Header("런타임 인재 명부")]
    [SerializeField] private List<Character> _allCharacters = new List<Character>();
    
    
    // [무작위 데이터 생성 풀] 나중에 뺄 가능성
    private readonly string[] _familyNames = { "남궁", "제갈", "당", "모용", "황보", "이", "장", "진" };
    private readonly string[] _givenNames = { "검", "호", "룡", "연", "백", "무", "현", "천", "산" };
    private readonly string[] _titles = { "비연", "혈도", "철벽", "풍신", "광마", "신검", "독수" };
    
    public List<Character> AllCharacters => _allCharacters;
    protected override void Awake()
    {
        base.Awake();
        LoadCharactersFromResources();
        
    }

    private void LoadCharactersFromResources()
    {
        CharacterData[] loadedDatas = Resources.LoadAll<CharacterData>("Characters");
        foreach (CharacterData loadedData in loadedDatas)
        {
            _allCharacters.Add(new Character(loadedData));
        }
        Debug.Log($"총 {_allCharacters.Count}명의 캐릭터 데이터를 성공적으로 불러왔습니다.");
    }

    public void AssignRole(Character targetCharacter, RoleType newRole)
    {
        foreach (Character character in _allCharacters)
        {
            if (character.CurrentRoleType == newRole)
            {
                character.CurrentRoleType = RoleType.None;
            }
        }
        targetCharacter.CurrentRoleType = newRole;
        Debug.Log($"{targetCharacter.BaseData.CharacterName}이(가) {newRole}로 임명되었습니다.");
    }
    
    
    // [핵심] 런타임 무작위 인재 생성 함수 - 필요할 때 생성
    public Character GenerateRandomCharacter()
    {
        //메모리 상에 새로운 ScriptableObject 인스턴스를 임시로 생성
        CharacterData randomData = ScriptableObject.CreateInstance<CharacterData>();
        
        //2. 무작위 이름 조합
        string randomTitle = _titles[Random.Range(0, _titles.Length)];
        string randomFamilyName = _familyNames[Random.Range(0, _familyNames.Length)];
        string randomGivenName = _givenNames[Random.Range(0, _givenNames.Length)];

        // 무작위 이름
        randomData.CharacterId = "Random_" + System.Guid.NewGuid().ToString().Substring(0, 5);
        randomData.CharacterName = $"{randomTitle}{randomFamilyName}{randomGivenName}";
        // 무작위 스탯
        randomData.Strength = Random.Range(20,96);
        randomData.Health = Random.Range(20,96);
        randomData.Dexterity = Random.Range(20,96);
        randomData.Intelligence = Random.Range(20,96);
        randomData.Charm = Random.Range(20,96);
        randomData.InnerStrength = Random.Range(20,96);
        randomData.Leadership = Random.Range(20,96);
            
        //초상화는 임시로 비워두거나 이미지 돌려막기 가능
        //randomData.Portrait = _randomPortraits
        
        //객체 생성
        Character newRandomCharacter = new Character(randomData);
        
        //명부 등록
        _allCharacters.Add(newRandomCharacter);
        
        return newRandomCharacter;
    }
    
    
}
