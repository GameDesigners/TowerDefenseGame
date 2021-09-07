using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum TowerLevel
{
    Level_One,Level_Two,Level_Three
}
[RequireComponent(typeof(HighlightableObject),typeof(EventTrigger),typeof(AudioSource))]
public class TowerOp : MonoBehaviour
{
    private EventTrigger eventTrigger;
    [Header("选中的外边框颜色")] public Color RimColor = Color.red;
    [Header("升级的粒子系统")] public GameObject UpgradeParticleSystem;
    [Header("当前防御塔的等级")] public TowerLevel level;
    [Header("防御塔触发器")] public DefensiveTowerTrigger trigger;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();


        eventTrigger = GetComponent<EventTrigger>();
        eventTrigger.triggers = new List<EventTrigger.Entry>();
        UnityAction<BaseEventData> action;

        EventTrigger.Entry enter_PointerClick = new EventTrigger.Entry();
        enter_PointerClick.eventID = EventTriggerType.PointerClick;
        enter_PointerClick.callback = new EventTrigger.TriggerEvent();
        action = new UnityAction<BaseEventData>(PointerClickTower);
        enter_PointerClick.callback.AddListener(action);

        EventTrigger.Entry enter_PointerEnter = new EventTrigger.Entry();
        enter_PointerEnter.eventID = EventTriggerType.PointerEnter;
        enter_PointerEnter.callback = new EventTrigger.TriggerEvent();
        action = new UnityAction<BaseEventData>(PointerCoverTower);
        enter_PointerEnter.callback.AddListener(action);

        EventTrigger.Entry enter_PointerExit = new EventTrigger.Entry();
        enter_PointerExit.eventID = EventTriggerType.PointerExit;
        enter_PointerExit.callback = new EventTrigger.TriggerEvent();
        action = new UnityAction<BaseEventData>(PointerExitTower);
        enter_PointerExit.callback.AddListener(action);

        eventTrigger.triggers.Add(enter_PointerClick);
        eventTrigger.triggers.Add(enter_PointerEnter);
        eventTrigger.triggers.Add(enter_PointerExit);
    }

    private void OnEnable()
    {
        if(UpgradeTowerEvent.UpgradeTowerBtn!=null)
        {
            UpgradeParticleSystem = UpgradeTowerEvent.UpgradeTowerBtn.GetComponent<UpgradeTowerEvent>().UpgradeFX;
        }
    }

    private void PointerCoverTower(BaseEventData pd)
    {
        if (GetComponent<HighlightableObject>() != null)
            GetComponent<HighlightableObject>().ConstantOn(RimColor);
    }

    private void PointerExitTower(BaseEventData pd)
    {
        if (CutWoodEvent.SelectedWood != gameObject)
        {
            if (GetComponent<HighlightableObject>() != null)
                GetComponent<HighlightableObject>().Off();
        }

    }

    private void PointerClickTower(BaseEventData pd)
    {
        Debug.Log("点击防御塔");
        if (UpgradeTowerEvent.SelectedTower != null)
        {
            if (UpgradeTowerEvent.SelectedTower.GetComponent<HighlightableObject>() != null)
                UpgradeTowerEvent.SelectedTower.GetComponent<HighlightableObject>().Off();
        }

        if (!UpgradeTowerEvent.UpgradeTowerBtn.gameObject.activeSelf)
            UpgradeTowerEvent.UpgradeTowerBtn.gameObject.SetActive(true);

        Vector3 screenSpacePos = Camera.main.WorldToScreenPoint(transform.position);
        UpgradeTowerEvent.UpgradeTowerBtn.anchoredPosition = new Vector2(screenSpacePos.x, screenSpacePos.y);
        
        UpgradeTowerEvent.SelectedTower = gameObject;
        GetComponent<HighlightableObject>().ConstantOn(RimColor);

        
    }

    //升级防御塔
    public void UngradeTower(GameObject towerObj)
    {
        GameObject go = Instantiate(towerObj, transform.parent);
        go.transform.localPosition = new Vector3(0, 0, 0);
        go.SetActive(true);

        if (UpgradeParticleSystem != null)
        {
            //设置升级粒子系统到本树子层级
            UpgradeParticleSystem.transform.parent = go.transform;
            UpgradeParticleSystem.transform.localPosition = new Vector3(0, 1, 0);
            UpgradeParticleSystem.GetComponent<ParticleSystem>().Play();
            Debug.Log("升级粒子系统为空");
        }

        //if (audioSource.clip != null)
        //    audioSource.Play();

        //隐藏升级按钮
        UpgradeTowerEvent.UpgradeTowerBtn.gameObject.SetActive(false);

        Destroy(gameObject);


    }
}