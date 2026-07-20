using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommandItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Button _cancelButton;
    private ICommand _myCommand;

    public void Initialize(ICommand cmd)
    {
        _myCommand = cmd;
        _nameText.text = cmd.CommandName;
        
        _cancelButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.AddListener(OnCancelClicked);
    }

    private void OnCancelClicked()
    {
        if (_myCommand is CraftWeaponCommand craftCmd)
        {
            foreach (Ingredient ingredient in craftCmd.BluePrint.Recipe)
            {
                InventoryManager.Instance.RefundResource(ingredient.RequiredItem, ingredient.Amount);
            }
            TurnManager.Instance.CancelCommand(craftCmd, craftCmd.BluePrint.CraftCostAP);
        }
    }
}
