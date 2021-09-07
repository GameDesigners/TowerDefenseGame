using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGameTrigger : MonoBehaviour
{
    [Header("结算UI")] public GameObject JieSuanUI;
    [Header("结算文字")] public Text JieSuanText;
    [Header("基地剩余血量")] public CustomizeUI.Customize_Slider  Health;

    private LevelResourceInitialConfig forestConfig;
    [SerializeField] private float HealthValue;
    [SerializeField] private float HoleValue;
    private void Start()
    {
        forestConfig = StaticConfigs.resourceInitializeConfig_Static;
        HealthValue = forestConfig.ForestLevelResource.LifeCount;
        HoleValue = HealthValue;
    }
    public void OnTriggerEnter(Collider other)
    {
        
        Enemy enemy = other.GetComponent<Enemy>();
        Debug.Log($"有动物进入:Health{enemy.Health} HoleHealth:{enemy.HoleHealth}");

        if (enemy.Health < enemy.HoleHealth)
        {
            HealthValue = HealthValue - (enemy.HoleHealth-enemy.Health) > 0 ? (enemy.HoleHealth - enemy.Health) : 0;
            if (HealthValue == 0)
            {
                JieSuanUI.SetActive(true);
                JieSuanText.text = "失败";
            }
            else
            {
                PlayerLevelData.Instance.Consume(ConsumptionEvent.基地自愈动物, new PlayerLevelData((int)((enemy.HoleHealth - enemy.Health)), 0, 0, 0, 0, 0));
                //更新Slider数据
                Health.Value = HealthValue / HoleValue;
                //动物治愈成功
            }
        }
    }
}
