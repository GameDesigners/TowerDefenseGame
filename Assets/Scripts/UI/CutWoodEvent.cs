using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CustomizeUI.Customize_Slider),typeof(AudioSource))]
public class CutWoodEvent : MonoBehaviour
{
    [Header("砍伐树木的粒子系统对象")] public GameObject CutWoodParticleSystem;
    [Header("砍树操作按钮")] public CustomizeUI.Customize_LongPressButton CutWoodBtn;
    [Header("砍伐后生成的树根")] public GameObject StumpObj;

    private CustomizeUI.Customize_Slider CutWoodSlider;
    private AudioSource audioSource;

    /// <summary>
    /// 当前选择的树
    /// </summary>
    public static GameObject SelectedWood = null;
    private const string CUTOFF = "_Cutoff";
    private const string LEAVE_SHADER_NAME = "StandardDoubleSided";

    private void Start()
    {
        CutWoodSlider = GetComponent<CustomizeUI.Customize_Slider>();
        audioSource = GetComponent<AudioSource>();
        CutWoodBtn.LongPressLoadSpeed = 0.6f;
        CutWoodBtn.LongPressEvent.AddListener(FinishCutWoodEvent);
    }

    private void Update()
    {
        CutWoodSlider.Value = CutWoodBtn.PressProgress;
        if (CutWoodBtn.IsPresssing && audioSource.clip != null)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }

        //foreach(var m in SelectedWood.GetComponent<MeshRenderer>().materials)
        //{
        //    if (m.shader.name == LEAVE_SHADER_NAME)
        //    {
        //        float v = RangeMapping(CutWoodSlider.Value);
        //        m.SetFloat(CUTOFF, v);
        //    }
        //}
        
            

        //控制粒子系统的显隐
        if (CutWoodBtn.IsPresssing&&CutWoodBtn.PressProgress!=1)
        {
            if (!CutWoodParticleSystem.activeSelf)
                CutWoodParticleSystem.SetActive(true);
        }
        else
        {
            if (CutWoodParticleSystem.activeSelf)
                CutWoodParticleSystem.SetActive(false);
        }
    }

    public void FinishCutWoodEvent()
    {
        if (!PlayerLevelData.Instance.Consume(ConsumptionEvent.砍树))
            return;

        Debug.Log("树木砍伐完成");
        CutWoodParticleSystem.SetActive(false);
        //gameObject.SetActive(false);
        SelectedWood.GetComponent<MeshRenderer>().enabled = false;
        SelectedWood.GetComponent<CapsuleCollider>().enabled = false;
        GameObject stump =Instantiate(StumpObj, SelectedWood.transform);
        stump.transform.localPosition = new Vector3(0, 3, 0);
            stump.gameObject.SetActive(true);
        if(stump.transform.childCount>0)
        {
            if (stump.transform.GetChild(0).childCount > 0)
                if (stump.transform.GetChild(0).GetChild(0).GetComponent<CreateTreeEvent>() != null)
                    stump.transform.GetChild(0).GetChild(0).GetComponent<CreateTreeEvent>().Tree = SelectedWood;
        }
        //stump.GetComponent<HighlightableObject>().ConstantOn(Color.green);    
        FogWithDepthTexture.currentCount++;
        HideSelfFunc();
        CutWoodSlider.Value = 0;
    }

    public void HideSelfFunc()
    {
        gameObject.SetActive(false);
        if (SelectedWood != null)
        {
            if (SelectedWood.GetComponent<HighlightableObject>() != null)
                SelectedWood.GetComponent<HighlightableObject>().Off();
        }
    }

    //从[0,1] 到 [0.5,1]的映射
    private float RangeMapping(float percent) => 0.5f + 0.5f * percent;
}