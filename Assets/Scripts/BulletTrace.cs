using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletTrace : MonoBehaviour
{
    [Header("瞄准目标")] public Transform target;
    [Header("子弹速度")] public float speed = 8f;
    [Header("伤害值")] public float maxDamage=15f;
    [Header("最小伤害")] public float minDamage = 8f;

    private AudioSource audioSource;
    private static string EnemyTag="Enemy";

    /*
    private Vector3 speed = new Vector3(0, 0, 5);  //子弹本地坐标速度
    private Vector3 lastSpeed;                     //存储转向前判单的本地坐标速度
    private int rotateSpeed=90;                    //旋转的速度，单位 度/秒
    private Vector3 finalForward;                  //目标到自身连线的向量，最终朝向
    private float angleOffset;                     //自己的forward朝向和mFinalForward之间的夹角
    private RaycastHit hit;                        //碰撞检测，用于判断角色是否中弹
    */
    private void Start()
    {
        //将炮弹的本地坐标速度转换为世界坐标
        //speed = transform.TransformDirection(speed);
        //transform.forward = -(target.position - transform.position);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //UpdateRotation();
        //UpdatePosition();

        transform.LookAt(target);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== EnemyTag)
        {
            other.GetComponent<Enemy>().GetDamage(Random.Range(minDamage,maxDamage));
            audioSource.Play();
            if (transform.childCount > 0)
                transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(DestroySelf(1.5f));
        }
    }

    private IEnumerator DestroySelf(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    /*

    //射线检测，如果击中目标点则销毁炮弹
    private void CheckHint()
    {
        if(Physics.Raycast(transform.position,transform.forward,out hit))
        {
            if(hit.transform==target&&hit.distance<1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void UpdatePosition()
    {
        transform.position = transform.position + speed * Time.deltaTime;
    }

    //旋转，使其朝向目标
    private void UpdateRotation()
    {
        //先将速度转换为本地坐标，旋转之后再变为世界坐标
        lastSpeed = transform.InverseTransformDirection(speed);
        ChangeForward(rotateSpeed * Time.deltaTime);

        speed = transform.TransformDirection(lastSpeed);
    }

    private void ChangeForward(float speed)
    {
        //获得目标但到自身的朝向
        finalForward = (target.position - transform.position).normalized;

        //方向与子弹的方向不一致
        if(finalForward!=transform.forward)
        {
            angleOffset = Vector3.Angle(transform.forward, finalForward);
            if (angleOffset > rotateSpeed)
                angleOffset = rotateSpeed;

            //将自身forward朝向慢慢转向最终朝向
            transform.forward = Vector3.Lerp(transform.forward, finalForward, speed / angleOffset);
        }
    }
    */
}
