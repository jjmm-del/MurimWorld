using UnityEngine;
using UnityEngine.UI;
public class BuildingPanelBase : UIPopup
{
    protected GameObject[] _subPanels;
    protected Button[] _tabButtons;

    protected void InitTabs(GameObject[] subPanels, Button[] tabButtons)
    {
        _subPanels = subPanels;
        _tabButtons = tabButtons;

        for (int i = 0; i < _subPanels.Length; i++)
        {
            if (_subPanels[i] != null)
            {
                UIBase subUI = _subPanels[i].GetComponent<UIBase>();
                if (subUI != null)
                {
                    subUI.Init();
                }
            }
        }
        for (int i = 0; i < _tabButtons.Length; i++)
        {
            int index = i;
            _tabButtons[i].onClick.AddListener(() => SwitchTab(index));
        }
    }
    protected virtual void SwitchTab(int index)
    {
        //안정성 검사
        if (_subPanels == null ||_tabButtons == null) return;
        
        for(int i = 0; i<_subPanels.Length; i++)
        {
            bool isActive = (i == index);
            _subPanels[i].SetActive(isActive);
            
            //선택 사항 탭 색깔 바꾸기, 이미지 변경 등
            _tabButtons[i].image.color = isActive ? Color.gray : Color.whiteSmoke;
        }
        
        
    }
}
