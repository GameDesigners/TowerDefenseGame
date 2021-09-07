using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurnTimer : MonoBehaviour
{
    [Header("下一波提示文本")] public Text nextTurnTxt;
    [Header("准备的时间")] public int prepareSeconds = 15;

    private CustomTimer prepareTimer=null;

    private void Start()
    {
        nextTurnTxt.enabled = false;
    }

    public void NextTurnTimerStart()
    {
        prepareTimer = TimerManager.StartCaculate(prepareSeconds);
        prepareTimer.FinishCaculate += PrepareTimer_FinishCaculate;
        if (!nextTurnTxt.enabled)
            nextTurnTxt.enabled = true;
    }

    private void Update()
    {
        if(prepareTimer!=null)
        {
            if (prepareTimer.IsCaculating())
                nextTurnTxt.text = $"{prepareTimer.CurrentTime}秒后来袭";
        }
    }


    private void PrepareTimer_FinishCaculate(UseTimerTag obj)
    {
        GameObject temp = prepareTimer.gameObject;
        GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>().ResetSpawnTimer();
        if (nextTurnTxt.enabled)
            nextTurnTxt.enabled = false;
        if (temp != null)
            Destroy(temp);
    }
}
