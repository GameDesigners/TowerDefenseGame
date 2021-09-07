using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class EGA_DemoLasers : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject TargetPoint;

    public float MaxLength;
    public GameObject[] Prefabs;

    private Vector3 direction;
    private Quaternion rotation;

    [SerializeField] private int Prefab;
    private GameObject Instance;
    private EGA_Laser LaserScript;

    //Double-click protection
    private float buttonSaver = 0f;

    private const string LaserStyle = "LASER_STYLE";

    private bool isStartFire = false;

    private void Start()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void Update()
    {
        if (!isStartFire)
        {
            if(FirePoint!=null&& FirePoint!=null)
            {
                Destroy(Instance);
                Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
                Instance.transform.parent = transform;
                LaserScript = Instance.GetComponent<EGA_Laser>();
                isStartFire = true;
            }
        }

        //if(isStartFire)
        //{
        //    //Enable lazer
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Destroy(Instance);
        //        Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
        //        Instance.transform.parent = transform;
        //        LaserScript = Instance.GetComponent<EGA_Laser>();
        //    }

        //    //Disable lazer prefab
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        LaserScript.DisablePrepare();
        //        Destroy(Instance, 1);
        //    }
        //}

        ////To change lazers
        //if ((Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0) && buttonSaver >= 0.4f)// left button
        //{
        //    buttonSaver = 0f;
        //    Counter(-1);
        //}
        //if ((Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0) && buttonSaver >= 0.4f)// right button
        //{
        //    buttonSaver = 0f;
        //    Counter(+1);         
        //}
        //buttonSaver += Time.deltaTime;


        RotateToMouseDirection(gameObject, TargetPoint.transform.position);
    }

    //To change prefabs (count - prefab number)
    //void Counter(int count)
    //{
    //    Prefab += count;
    //    if (Prefab > Prefabs.Length - 1)
    //    {
    //        Prefab = 0;
    //    }
    //    else if (Prefab < 0)
    //    {
    //        Prefab = Prefabs.Length - 1;
    //    }

    //    PlayerPrefs.SetInt(LaserStyle, Prefab);
    //}
  
    //To rotate fire point
    void RotateToMouseDirection (GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);     
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }
}
