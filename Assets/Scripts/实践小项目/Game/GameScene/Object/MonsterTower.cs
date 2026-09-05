using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : TankBaseObj
{


    //自动旋转 不用我们自己实现了 因为已经挂载了一个 专门管旋转的脚本

    //间隔开火
    //  应该有一个 间隔时间
    public float fireOffsetTime = 1;
    //记录累加时间 用于 间隔开火判断
    private float nowTime = 0;
    //发射位置
    public Transform[] shootPos;
    //子弹预设体 关联
    public GameObject bulletObj;
   
    // Update is called once per frame
    void Update()
    {
        //不停地累加时间 并记录下来
        nowTime += Time.deltaTime;
        //当时间超过间隔时间时 就开火
        if (nowTime >= fireOffsetTime)
        {
            Fire();
            nowTime = 0;
        }
    }

    public override void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            //实例化几个子弹
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            //这里是设置子弹的拥有者 方便之后 进行属性计算
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }

    public override void Wound(TankBaseObj other)
    {
        //什么内容都不写 让固定坦克无敌
    }
}
