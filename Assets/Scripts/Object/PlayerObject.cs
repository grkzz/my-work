using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private static PlayerObject instance;
    public static PlayerObject Instance => instance;
    //血量
    public int nowHp;
    public int maxHp;
    //移动速度
    public int speed;
    //旋转速度
    public int roundSpeed;
    //目标四元数角度
    private Quaternion targetQ;
    //是否死亡
    public bool isDead;
    //当前世界坐标转屏幕上的点
    private Vector3 nowPos;
    //上一次玩家的位置 就是在位移前 玩家的位置
    private Vector3 frontPos;
    private void Awake()
    {
        instance = this;
    }
    public void Dead()
    {
        isDead = true;
        //显示游戏结束面板
        GameOverPanel.Instance.ShowMe();
    }

    public void Wound()
    {
        if (isDead)
            return;
        //减血
        nowHp -= 1;
        //更新游戏面板上的血量显示
        GamePanel.Instance.ChangeHp(nowHp);
        //是否死亡了
        if (nowHp <= 0)
            Dead();
    }

    private float hValue;
    private float vValue;
    void Update()
    {
        //如果死亡了 没必要再移动了
        if (isDead)
            return;

        //移动 旋转逻辑

        //旋转 
        hValue = Input.GetAxisRaw("Horizontal");
        vValue = Input.GetAxisRaw("Vertical");

        //如果没有按照我们的ad键 那么目标角度 就是000度(单位四元数是000不是111)
        if (hValue == 0)
            targetQ = Quaternion.identity;
        //如果按ad键 就是 0020 或者00-20根据你按的次数决定
        else
            targetQ = hValue < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);
        //让飞机朝着 这个目标四元数 去旋转
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQ, Time.deltaTime * roundSpeed);

        //在位移之前 记录 之前的位置
        frontPos = transform.position;

        //移动
        transform.Translate(Vector3.forward * vValue * Time.deltaTime*speed);
        transform.Translate(Vector3.right * hValue * Time.deltaTime * speed,Space.World);

        //进行极限判断
        nowPos = Camera.main.WorldToScreenPoint(transform.position);
        //左右溢出判断
        if (nowPos.x <= 0 || nowPos.x >= Screen.width)
            transform.position = new Vector3(frontPos.x,transform.position.y,transform.position.z);
        //上下溢出判断
        if (nowPos.y <= 0 || nowPos.y >= Screen.height)
            transform.position = new Vector3(transform.position.x, transform.position.y, frontPos.z);

        //射线检测 用于销毁子弹
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            //这里只检测子弹层
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000,
                1 << LayerMask.NameToLayer("Bullet")))
            {
                BulletObject bulletObj = hitInfo.transform.GetComponent<BulletObject>();
                //直接让被点中的子弹销毁
                bulletObj.Dead();
            }
        }
    }
}
