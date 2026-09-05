using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    //当前装备的武器
    public WeaponObj nowWeapon;
    //武器父对象位置
    public Transform weaponPos;

    // Update is called once per frame
    void Update()
    {
        //1.ws键 控制 前进后退
        //知识点一 Transform位移
        //知识点二 Input 轴向输入检测
        transform.Translate(Input.GetAxis("Vertical")*Vector3.forward * moveSpeed * Time.deltaTime);

        //2.ad键 控制 旋转
        //知识点一 Transform旋转
        //知识点二 Input 轴向输入检测
        transform.Rotate(Input.GetAxis("Horizontal")*Vector3.up*roundSpeed*Time.deltaTime);
        //3.鼠标左右移动 控制 炮台旋转
        //知识点一 Transform旋转
        //知识点二 Input 鼠标轴向输入检测
        tankHead.transform.Rotate(Input.GetAxis("Mouse X") * Vector3.up * headRoundSpeed * Time.deltaTime);
        //4.开火
        //知识点一 Input
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    //重写 父类中的 行为 玩家 可能会存在特殊处理
    public override void Fire()
    {
        if (nowWeapon != null)
            nowWeapon.Fire();
    }

    public override void Dead()
    {
        //这里 不执行 父类的死亡 因为 主摄像机是玩家的子对象 不能直接移除
        //base.Dead();
        //应该处理 失败逻辑 显示失败面板 即可
        Time.timeScale = 0;
        LosePanel.Instance.ShowMe();
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //更新主面板血条
        GamePanel.Instance.UpdateHP(this.maxHp, this.hp);
    }

    /// <summary>
    /// 切换武器
    /// </summary>
    /// <param name="obj"></param>
    public void ChangeWeapon(GameObject weapon)
    {
        if (nowWeapon != null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }

        //切换武器
        //第一个参数 克隆对象    第二个参数 父对象位置(同时设置父对象)    第三个参数 是否保留原缩放比例
        GameObject weaponObj = Instantiate(weapon,weaponPos,false);
        nowWeapon = weaponObj.GetComponent<WeaponObj>();
        nowWeapon.SetFather(this);
    }
}
