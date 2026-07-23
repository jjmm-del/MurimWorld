using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "MurimWorld/Item data")]
public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string ItemId;       //예: Mat_001;
    public string ItemName;     //예: 만년빙정, 철괴
    public ItemType ItemType;   //
    public Sprite Icon;             //아이템 이미지
    [TextArea] public string Description;  //극강의 한기를 품은 광석, 철광석을 재련한 철괴
    
    [Header("소비품 전용 옵션(재료일 땐 무시")]
    public int EffectValue;
}
