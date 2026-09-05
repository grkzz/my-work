using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBaseObj : MonoBehaviour
{
    //攻击力 防御力 最大当前血量相关
    public int atk;
    public int def;
    public int maxHp;
    public int hp;

    //所有坦克都有炮台相关
    public Transform tankHead;

    //移动旋转速度相关
    public float moveSpeed = 5;
    public float roundSpeed = 50;
    public float headRoundSpeed = 50;

    //死亡特效 关联对应预设体 死亡的时候 动态创建出来 设置位置即可
    public GameObject deadEff;

    /// <summary>
    /// 开火抽象方法 子类重写开火行为即可
    /// </summary>
    public abstract void Fire();
    /// <summary>
    /// 我被别人攻击 造成我自己受伤
    /// </summary>
    /// <param name="other"></param>
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - this.def;
        if (dmg <= 0)
            return;
        //如果伤害大于0 就应该减血
        this.hp -= dmg;
        //判断 如果血量 <=0了 就应该死亡
        if(this.hp <= 0)
        {
            this.hp = 0;
            this.Dead();
        }
    }
    /// <summary>
    /// 死亡行为 当自己血量小于等于0时 就应该死亡
    /// </summary>
    public virtual void Dead()
    {
        //对象死亡 无非就是在场景上移除该对象
        Destroy(this.gameObject);
        //死亡的时候 可能所有坦克 对象 都应该播放一个对应的特效
        if(deadEff != null)
        {
            //就是实例化对象时 顺便 把位置和角度 都一起设置了
            GameObject effobj = Instantiate(deadEff, this.transform.position, this.transform.rotation);
            //由于该特效对象身上 直接关联了音效 所以我们可以在此处 把音效播放相关也控制了
            AudioSource audioSource = effobj.GetComponent<AudioSource>();
            //根据音乐数据 设置 音效 大小 和 是否 播放
            //设置音效大小
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            //音效是否播放(开启的时候让静音关闭，关闭的时候让静音开启)
            audioSource.mute =!GameDataMgr.Instance.musicData.isOpenSound;
            //避免外部取消 Play on Awake
            audioSource.Play();
        }
    }
}
