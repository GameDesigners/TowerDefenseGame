using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeTowerEvent : MonoBehaviour
{
    public static RectTransform UpgradeTowerBtn;
    [Header("升级粒子系统")] public GameObject UpgradeFX;

    [Header("塔预制体")] public List<GameObject> towers;
    public static GameObject SelectedTower=null;

    private void Start()
    {
        UpgradeTowerBtn = GetComponent<RectTransform>();

        GetComponent<Button>().onClick.AddListener(delegate ()
        {
            if(SelectedTower!=null)
            {
                
                TowerOp op = SelectedTower.GetComponent<TowerOp>();
                if (op.trigger.shoters.Count > 0) return;
                if (op != null)
                {
                    ConsumptionEvent e = ConsumptionEvent.默认;
                    switch(op.level)
                    {
                        case TowerLevel.Level_One:e = ConsumptionEvent.升级至等级二的塔防;break;
                        case TowerLevel.Level_Two:e = ConsumptionEvent.升级至等级三的塔防;break;
                    }

                    if(op.level<TowerLevel.Level_Three&&PlayerLevelData.Instance.Consume(e))
                    {
                        op.level++;
                        int index = (int)op.level;
                        op.UngradeTower(towers[index]);
                    }
                }
            }
        });
    }
    public void HideSelfFunc()
    {
        gameObject.SetActive(false);
        if (SelectedTower != null)
        {
            if (SelectedTower.GetComponent<HighlightableObject>() != null)
                SelectedTower.GetComponent<HighlightableObject>().Off();
        }
    }
}
