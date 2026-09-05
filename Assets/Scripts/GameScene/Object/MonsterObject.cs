using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    //动画相关
    private Animator animator;
    //位移相关 寻路组件
    private NavMeshAgent agent;
    //一些不变的基础数据
    private MonsterInfo monsterInfo;

    //当前血量
    public int hp;
    //怪物是否死亡
    public bool isDead = false;

    //攻击时间
    private float frontTime;


    // Start is called before the first frame update
    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    //初始化怪物信息
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        //状态机加载
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        //要变的血量
        hp = info.hp;
        //速度、加速度和旋转速度 初始化 之所以速度加速度设置成一样 是因为不想要明显的加速效果
        agent.speed = agent.acceleration =info.moveSpeed;
        agent.angularSpeed = info.roundSpeed;

    }

    //攻击——伤害检测
    //受伤
    public void Wound(int dmg)
    {
        if (isDead)
            return;
        //减少血量
        hp -= dmg;
        //播放受伤动画
        animator.SetTrigger("Wound");

        if(hp<=0)
        {
            //死亡
            Dead();
        }
        else
        {
            //播放音效
            GameDataMgr.Instance.PlaySound("Wound");
        }
    }
    //死亡
    public void Dead()
    {
        isDead = true;
        //停止移动
        agent.isStopped = true;
        //播放死亡动画
        animator.SetBool("Dead", true);
        //播放音效
        GameDataMgr.Instance.PlaySound("dead");
        //加钱——我们之后通过关卡管理类 来管理游戏中的对象 通过它来让玩家加钱
        GameLevelMgr.Instance.player.AddMoney(20);
    }

    //死亡动画播放完毕后会调用的事件方法
    public void DeadEvent()
    {
        //死亡动画播放完毕后移除对象
        //有了关卡管理器再来处理
        //GameLevelMgr.Instance.ChangeMonsterNum(-1);

        //从列表中移除怪物
        GameLevelMgr.Instance.RemoveMonster(this);
        //在场景中移除已经死亡的对象
        Destroy(gameObject);

        //怪物死亡时 检测 游戏是否胜利
        if(GameLevelMgr.Instance.CheckOver())
        {
            //显示结束界面
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            //更新结束界面上的内容并存储奖励数据
            panel.InitInfo((int)(GameLevelMgr.Instance.player.money * 0.5f), true);
        }
    }

    //出生过后移动
    //移动——寻路组件 SetDestination 该方法设置目标点后就会自动移动
    public void BornOver()
    {
        //出生结束后 再让怪物朝目标点移动
        agent.SetDestination(MainTowerObject.Instance.transform.position);

        //播放移动动画
        animator.SetBool("Walk", true);
    }


    // Update is called once per frame
    void Update()
    {
        //检测什么时候停下来攻击
        if (isDead)
            return;
        //根据速度来决定动画播放
        animator.SetBool("Walk", agent.velocity != Vector3.zero);
        //检测和目标点达到一定条件时 就攻击
        if(Vector3.Distance(transform.position,MainTowerObject.Instance.transform.position)
            <5&&Time.time - frontTime >= monsterInfo.atkOffset)
        {
            //记录这次攻击时的时间
            frontTime = Time.time;
            animator.SetTrigger("Atk");
        }
    }

    //伤害检测
    public void AtkEvent()
    {
        GameDataMgr.Instance.PlaySound("Eat");
        //范围检测 进行伤害判断
        Collider[] colliders =  Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1, 1 << LayerMask.NameToLayer("MainTower"));
        for (int i = 0; i < colliders.Length; i++)
        {
            if (MainTowerObject.Instance.gameObject == colliders[i].gameObject)
                MainTowerObject.Instance.Wound(monsterInfo.atk);
            //让保护区域受到伤害
        }
    }
}
