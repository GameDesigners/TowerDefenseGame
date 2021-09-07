using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[RequireComponent(typeof(EventTrigger),typeof(HighlightableObject))]
public class TreeOp : MonoBehaviour
{
    public RectTransform CutTreeIconBtn;
    private EventTrigger eventTrigger;
    [Header("选中的外边框颜色")] public Color RimColor = Color.red;
    [Header("砍伐树木的粒子系统")] public GameObject CutWoodParticleSystem;
    private void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        eventTrigger.triggers = new List<EventTrigger.Entry>();
        UnityAction<BaseEventData> action;

        EventTrigger.Entry enter_PointerClick = new EventTrigger.Entry();
        enter_PointerClick.eventID = EventTriggerType.PointerClick;
        enter_PointerClick.callback = new EventTrigger.TriggerEvent();
        action = new UnityAction<BaseEventData>(PointerClickTree);
        enter_PointerClick.callback.AddListener(action);

        EventTrigger.Entry enter_PointerEnter = new EventTrigger.Entry();
        enter_PointerEnter.eventID = EventTriggerType.PointerEnter;
        enter_PointerEnter.callback = new EventTrigger.TriggerEvent();
        action = new UnityAction<BaseEventData>(PointerCoverTree);
        enter_PointerEnter.callback.AddListener(action);

        EventTrigger.Entry enter_PointerExit = new EventTrigger.Entry();
        enter_PointerExit.eventID = EventTriggerType.PointerExit;
        enter_PointerExit.callback = new EventTrigger.TriggerEvent();
        action = new UnityAction<BaseEventData>(PointerExitTree);
        enter_PointerExit.callback.AddListener(action);

        eventTrigger.triggers.Add(enter_PointerClick);
        eventTrigger.triggers.Add(enter_PointerEnter);
        eventTrigger.triggers.Add(enter_PointerExit);
    }

    private void PointerCoverTree(BaseEventData pd)
    {
        if (GetComponent<HighlightableObject>() != null)
            GetComponent<HighlightableObject>().ConstantOn(RimColor);
    }

    private void PointerExitTree(BaseEventData pd)
    {
        if(CutWoodEvent.SelectedWood!=gameObject)
        {
            if (GetComponent<HighlightableObject>() != null)
                GetComponent<HighlightableObject>().Off();
        }
        
    }

    private void PointerClickTree(BaseEventData pd)
    {

        if(CutWoodEvent.SelectedWood!=null)
        {
            if (CutWoodEvent.SelectedWood.GetComponent<HighlightableObject>() != null)
                CutWoodEvent.SelectedWood.GetComponent<HighlightableObject>().Off();
        }

        if (!CutTreeIconBtn.gameObject.activeSelf)
            CutTreeIconBtn.gameObject.SetActive(true);

        Vector3 screenSpacePos = Camera.main.WorldToScreenPoint(transform.position);
        CutTreeIconBtn.anchoredPosition = new Vector2(screenSpacePos.x, screenSpacePos.y);

        CutWoodEvent.SelectedWood = gameObject;
        GetComponent<HighlightableObject>().ConstantOn(RimColor);

        //设置砍伐粒子系统到本树子层级
        CutWoodParticleSystem.transform.parent = gameObject.transform;
        CutWoodParticleSystem.transform.localPosition = new Vector3(0, 1, 0);
        CutWoodParticleSystem.SetActive(false);  //先隐藏
    }

    /*
    private void SetMaterialColor(Color col)
    {
        foreach (var m in GetComponent<MeshRenderer>().materials)
        {
            if (m.shader.name == LEAVE_SHADER_NAME)
            {
                m.SetColor(COLOR, col);
            }
        }
    }
    */
}