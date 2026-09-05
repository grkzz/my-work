using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObj : TankBaseObj
{
    //1.要让坦克 在两个点之间 来回的移动
    //当前的目标点
    private Transform targetPos;
    //随机用的点 外面去关联
    public Transform[] randomPos;
    //2.坦克要一直盯着自己的目标
    public Transform lookAtTarget;
    //3.当目标到达一定范围内过后 间隔一段时间 攻击一下目标
    //开火距离 当小于这个距离时 就会主动攻击
    public float fireDis = 5;
    //为了避免 太难 加一个 攻击间隔时间
    public float fireOffsetTime = 1;
    private float nowTime = 0;

    //开火点
    public Transform[] shootPos;
    //子弹预设体
    public GameObject bulletObj;
    
    //两张 血条的图 外面关联
    public Texture maxHpBK;
    public Texture hpBK;

    //显示血条计时用时间
    private float showTime;

    //结构体可以不用new直接赋值
    private Rect maxHpRect;
    private Rect hpRect;
    // Start is called before the first frame update
    void Start()
    {
        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        #region 多个点之间的随即移动逻辑
        //看向自己的目标点 同时面朝向指向点
        this.transform.LookAt(targetPos);
        //不停地向自己的面朝向位移
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //知识点 Vector3里面有一个得到两个点之间距离的方法
        //当距离过小时 认为到达了目的地 重新随机一个点
        if (targetPos!=null&&Vector3.Distance(this.transform.position, targetPos.position) < 1f)
        {
            RandomPos();
        }
        #endregion

        #region 看向自己的目标
        if (lookAtTarget != null)
        {
            tankHead.LookAt(lookAtTarget);
            //当自己的距离 小于等于 配置的 开火距离时
            if (Vector3.Distance(this.transform.position,lookAtTarget.position)<=fireDis)
            {
                nowTime += Time.deltaTime;
                if (nowTime >= fireOffsetTime)
                {
                    Fire();
                    nowTime = 0;
                }
            }
        }
        #endregion
    }

    private void RandomPos()
    {
        if (randomPos.Length == 0)
            return;
        targetPos = randomPos[Random.Range(0,randomPos.Length)];
    }

    public override void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            //设置子弹的 拥有者 用于之后 进行伤害计算
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }

    public override void Dead()
    {
        base.Dead();
        //移动怪物死亡时 需要加分
        GamePanel.Instance.AddScore(10);
    }
    //进行血条UI的绘制
    private void OnGUI()
    {
        if (showTime>0)
        {
            showTime -= Time.deltaTime;
            //画图 画血条
            //1.把怪物当前位置 转换成 屏幕位置
            //摄像机里面提供了API 可以将 世界坐标 转为 屏幕坐标
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            //2.屏幕位置 转换成 GUI位置
            //知识点：如何得到当前屏幕的分辨率高
            screenPos.y = Screen.height - screenPos.y;

            //然后再绘制
            //知识点：GUI中的 图片绘制
            //底图
            maxHpRect.x = screenPos.x - 50;
            maxHpRect.y = screenPos.y - 50;
            maxHpRect.width = 100;
            maxHpRect.height = 15;
            GUI.DrawTexture(maxHpRect, maxHpBK);
            //血条图
            hpRect.x = screenPos.x - 50;
            hpRect.y = screenPos.y - 50;
            //根据血量和最大血量的百分比 决定画多宽
            hpRect.width = (float)hp / maxHp * 100;
            hpRect.height = 15;
            GUI.DrawTexture(hpRect, hpBK);
        }
   
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //设置显示血条的时间
        showTime = 5;
    }
}
