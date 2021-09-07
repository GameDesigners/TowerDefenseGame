using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CustomizeUI.Customize_Slider),typeof(AudioSource))]
public class CreateTreeEvent : MonoBehaviour
{
    //[Header("砍伐树木的粒子系统对象")] public GameObject CutWoodParticleSystem;
    [Header("砍树操作按钮")] public CustomizeUI.Customize_LongPressButton CutWoodBtn;
    public GameObject Tree;



    private CustomizeUI.Customize_Slider CutWoodSlider;
    private GameObject root;


    /// <summary>
    /// 当前选择的树
    /// </summary>
    public static GameObject SelectedWood = null;
    private const string CUTOFF = "_Cutoff";
    private const string LEAVE_SHADER_NAME = "StandardDoubleSided";

    private AudioSource audioSource;

    private void Start()
    {
        CutWoodSlider = GetComponent<CustomizeUI.Customize_Slider>();
        CutWoodBtn.LongPressLoadSpeed = 0.6f;
        CutWoodBtn.LongPressEvent.AddListener(FinishCutWoodEvent);
        root = transform.parent.transform.parent.gameObject;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (root != null)
        {
            Vector3 screenSpacePos = Camera.main.WorldToScreenPoint(root.transform.position);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(screenSpacePos.x, screenSpacePos.y + 50);

            //控制粒子系统的显隐
            if (CutWoodBtn.IsPresssing && CutWoodBtn.PressProgress != 1)
            {
               root.GetComponent<HighlightableObject>().ConstantOn(Color.green);
            }
            else
            {
                root.GetComponent<HighlightableObject>().Off();
            }
        }

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
    }

    public void FinishCutWoodEvent()
    {
        if (!PlayerLevelData.Instance.Consume(ConsumptionEvent.种树))
            return;

        Debug.Log("树木种植完成");
        gameObject.SetActive(false);
        Tree.GetComponent<MeshRenderer>().enabled = true;
        Tree.GetComponent<CapsuleCollider>().enabled = true;
        FogWithDepthTexture.currentCount--;
        Destroy(root);
    }
}