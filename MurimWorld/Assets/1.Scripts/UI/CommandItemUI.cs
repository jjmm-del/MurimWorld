using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommandItemUI : UI_SubItem
{
    enum Texts {NameText};
    enum Buttons {CancelButton}
    private ICommand _myCommand;

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        
        Get<Button>((int)Buttons.CancelButton).onClick.AddListener(OnCancelClicked);
    }

    public void SetInfo(ICommand cmd)
    {
        _myCommand = cmd;
        Get<TextMeshProUGUI>((int)Texts.NameText).text = cmd.CommandName;
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
