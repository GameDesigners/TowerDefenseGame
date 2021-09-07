using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInteractiveTree : MonoBehaviour
{
    [Header("砍伐树木事件脚本")] public CutWoodEvent cutWoodEvent;
    private List<GameObject> InteractiveTrees;

    private CustomTimer customerTimer;

    private void Start()
    {
        InteractiveTrees = new List<GameObject>();
        for (int index = 0; index < transform.childCount; index++)
            InteractiveTrees.Add(transform.GetChild(index).gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ShowAllInterativeTrees();
    }
    private void ShowAllInterativeTrees()
    {

        //取消选取
        cutWoodEvent.HideSelfFunc();
        foreach(var t in InteractiveTrees)
        {
            if (t.GetComponent<MeshRenderer>().enabled)
                t.GetComponent<HighlightableObject>().ConstantOn(Color.white);
        }

        customerTimer = TimerManager.StartCaculate(3);
        customerTimer.FinishCaculate += CustomerTimer_FinishCaculate;
    }

    private void CustomerTimer_FinishCaculate(UseTimerTag obj)
    {
        foreach (var t in InteractiveTrees)
        {
            if (t == CutWoodEvent.SelectedWood)
                continue;

            if (t.GetComponent<MeshRenderer>().enabled)
                t.GetComponent<HighlightableObject>().Off();
        }
        Destroy(customerTimer.gameObject);
        customerTimer = null;
    }
}