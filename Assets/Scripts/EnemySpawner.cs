using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("生成敌人的间隔")] public float EnemySpawnerInterval = 5;
    [Header("敌人列表")] public List<GameObject> EnemyList;
    [Header("下一轮等待计时器")] public NextTurnTimer nextTurnTimer;
    [Header("下一轮音效")] public AudioClip NextTurnAudioClip;

    [Header("结算面板")] public GameObject SettlePanel;
    [Header("结算文字")] public Text SettleText;

    public Dictionary<EnemyType, Queue<GameObject>> EnemyInstances = new Dictionary<EnemyType, Queue<GameObject>>();
    public CustomTimer currentTimerObj;
    public bool CanInstance = true;
    public int CurrentTurn = 1;
    public int AnimalNumInThisTurn = 0;
    public int CueAnimalNum = 0;
    
    private GameTurnData turnData;
    private Turn CurrentTurnData;


    private void Start()
    {
        /*
        currentTimerObj = TimerManager.StartCaculate((int)EnemySpawnerInterval);
        currentTimerObj.FinishCaculate += RandomEnemySpawn;
        */
        turnData = StaticConfigs.gameTurnData_Static;
        CurrentTurnData = turnData.TurnData[0];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject go=Instantiate(EnemyList[0]);
            go.transform.position = EnemyList[0].transform.position;
        }
    }

    private void RandomEnemySpawn(UseTimerTag tag)
    {
        if (AnimalNumInThisTurn >= CurrentTurnData.AnimalNum)
        {
            return;
        }



        int index = (int)Random.Range(0.0f, 3.0f);
        GameObject temp = null;
        if (index < EnemyList.Count)
            temp = EnemyList[index];
        //判断在对象池中是否存在可用的对象
        if(temp!=null)
        {
            if(temp.GetComponent<Enemy>()!=null)
            {
                EnemyType type = temp.GetComponent<Enemy>().enemyType;
                if (EnemyInstances.ContainsKey(type))
                {
                    if(EnemyInstances[type].Count>0)
                    {
                        GameObject newObj = EnemyInstances[type].Dequeue();
                        newObj.SetActive(true);
                        if (newObj.GetComponent<EnemyMoving>() != null)
                            newObj.GetComponent<EnemyMoving>().movingSpeed = CurrentTurnData.AnimalSpeed;
                        ResetSpawnTimer();
                        AnimalNumInThisTurn++;
                        return;
                    }
                }
                else
                {
                    //初始化容器
                    Queue<GameObject> queue = new Queue<GameObject>();
                    EnemyInstances.Add(type, queue);
                }
                  
            }

            GameObject go = Instantiate(temp);
            go.transform.position = temp.transform.position;
            go.SetActive(true);
            if (go.GetComponent<EnemyMoving>() != null)
                go.GetComponent<EnemyMoving>().movingSpeed = CurrentTurnData.AnimalSpeed;
            AnimalNumInThisTurn++;
        }

        ResetSpawnTimer();
    }

    public void ResetSpawnTimer()
    {
        GameObject temp = null;
        if (currentTimerObj != null)
            temp = currentTimerObj.gameObject;
        currentTimerObj = TimerManager.StartCaculate((int)EnemySpawnerInterval);
        currentTimerObj.FinishCaculate += RandomEnemySpawn;
        if (temp != null)
            Destroy(temp);
    }

    public void NextTurnDataUpdate()
    {
        if (CurrentTurn < turnData.TurnData.Count)
        {
            PlayerLevelData.Instance.Consume(ConsumptionEvent.下一轮);
            CurrentTurn++;
            float oldSpeed = CurrentTurnData.AnimalSpeed;
            CurrentTurnData = turnData.TurnData[CurrentTurn - 1];
            AnimalNumInThisTurn = 0;
            CueAnimalNum = 0;
            nextTurnTimer.NextTurnTimerStart();
            if (GetComponent<AudioSource>() != null && NextTurnAudioClip != null)
                GetComponent<AudioSource>().PlayOneShot(NextTurnAudioClip);
            PlayerLevelData.Instance.Consume(ConsumptionEvent.动物增加速度,new PlayerLevelData(0, 0, 0, 0, oldSpeed - CurrentTurnData.AnimalSpeed, 0));

        }
        else
        {
            Debug.Log("游戏胜利");
            //UI启动
            SettlePanel.SetActive(true);
            SettleText.text = "成功";
        }
    }

    public void AddAnimalCueNum()
    {
        CueAnimalNum++;
        if (CueAnimalNum >= CurrentTurnData.AnimalNum)
            NextTurnDataUpdate();
    }
}