using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/*
*   重构时间：2021.3.5
*   名称：ThreeDObjectClick.cs
*   描述：3D物品点击事件
*   编写：ZhuSenlin
*/

public class ThreeDObjectClick : MonoBehaviour
{
    public GameObject defensiveTower;
    public Material oldMaterial;
    public Material newMaterial;

    [Header("选中后外发光的颜色")] public Color rimColor=new Color(1,0,0,1);
    private EventTrigger eventTrigger;

    private void Start()
    {
        oldMaterial = GetComponent<MeshRenderer>().material;
        eventTrigger = GetComponent<EventTrigger>();
        eventTrigger.triggers = new List<EventTrigger.Entry>();

        EventTrigger.Entry enter_PointerClick = new EventTrigger.Entry();
        enter_PointerClick.eventID = EventTriggerType.PointerClick;
        enter_PointerClick.callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> action = new UnityAction<BaseEventData>(CreateDefensiveTower);
        enter_PointerClick.callback.AddListener(action);

        EventTrigger.Entry enter_PointerEnter = new EventTrigger.Entry();
        enter_PointerEnter.eventID = EventTriggerType.PointerEnter;
        enter_PointerEnter.callback = new EventTrigger.TriggerEvent();
        action = new UnityAction<BaseEventData>(PointerCoverDefensiveTower);
        enter_PointerEnter.callback.AddListener(action);

        EventTrigger.Entry enter_PointerExit = new EventTrigger.Entry();
        enter_PointerExit.eventID = EventTriggerType.PointerExit;
        enter_PointerExit.callback = new EventTrigger.TriggerEvent();
        action = new UnityAction<BaseEventData>(PointerExitDefensiveTower);
        enter_PointerExit.callback.AddListener(action);

        eventTrigger.triggers.Add(enter_PointerClick);
        eventTrigger.triggers.Add(enter_PointerEnter);
        eventTrigger.triggers.Add(enter_PointerExit);
    }
    /// <summary>
    /// 创建防御塔
    /// </summary>
    public void CreateDefensiveTower(BaseEventData pd)
    {
        Debug.Log("Click Event");
        if(PlayerLevelData.Instance.Consume(ConsumptionEvent.创建等级一的塔防))
        {
            //if (transform.childCount > 0)
            //{
            //    if (transform.GetChild(0) != null && !transform.GetChild(0).gameObject.activeSelf)
            //        transform.GetChild(0).gameObject.SetActive(true);
            //    gameObject.GetComponent<MeshRenderer>().enabled = false;
            //    gameObject.GetComponent<BoxCollider>().enabled = false;
            //}

            GameObject go = Instantiate(defensiveTower, transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.SetActive(true);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void PointerCoverDefensiveTower(BaseEventData pd)
    {
        GetComponent<MeshRenderer>().material = newMaterial;
        if(GetComponent<HighlightableObject>()!=null)
            GetComponent<HighlightableObject>().ConstantOn(rimColor);
    }

    public void PointerExitDefensiveTower(BaseEventData pd)
    {
        GetComponent<MeshRenderer>().material = oldMaterial;
        if (GetComponent<HighlightableObject>() != null)
            GetComponent<HighlightableObject>().Off();

    }
}
