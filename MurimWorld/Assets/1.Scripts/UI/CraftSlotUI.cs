using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class CraftSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _costText; //소모 ap
    [SerializeField] private TextMeshProUGUI _durationText; //제작 기간
    [SerializeField] private Button _craftButton;

    private EquipmentData _myData;

    public void Initialize(EquipmentData data)
    {
        _myData = data;
        _itemNameText.text = data.ItemName;

        StringBuilder costString = new StringBuilder();
        costString.Append($"비용 : AP{data.CraftCostAP}");

        if (data.Recipe != null && data.Recipe.Count > 0)
        {
            foreach (Ingredient ingredient in data.Recipe)
            {
                
                costString.Append($", {ingredient.RequiredItem.ItemName}{ingredient.Amount}");
            }
        }
            
        //_costText.text = $"제작비용: {data.CraftCostAP} AP";
        _costText.text = costString.ToString();
        
        _durationText.text = $"제작 기간 {data.CraftDurationTurns}개월";
        
        _craftButton.onClick.RemoveAllListeners();
        _craftButton.onClick.AddListener(OnCraftClicked);    
        
    }

    private string GetResourceNameKorean(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Silver:   return "은자";
            case ResourceType.Iron:     return "철괴";
            case ResourceType.Wood:     return "목재";
            case ResourceType.Herb:     return "약초";
            default:                    return type.ToString();
        }
    }

    private void OnCraftClicked()
    {
        if (TurnManager.Instance == null || InventoryManager.Instance == null) return;
        
        ICommand craftCmd = new CraftWeaponCommand(_myData);
        bool apSuccess = TurnManager.Instance.TryEnQueueCommand(craftCmd, _myData.CraftCostAP);

        if (apSuccess)
        {
            foreach (Ingredient ingredient in _myData.Recipe)
            {
                InventoryManager.Instance.ConsumeItem(ingredient.RequiredItem, ingredient.Amount);
            }
        }
        Debug.Log($"[대장간]{_myData.ItemName}의 제작을 시작했습니다. 창고에서 자원이 소모됩니다.");
    }
}
