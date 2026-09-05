using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel labRank;
    public UILabel labName;
    public UILabel labTIme;
   
    /// <summary>
    /// 根据排行榜单条数据 对组合控件 进行显示初始化
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="name"></param>
    /// <param name="time"></param>
    public void InitInfo(int rank,string name,int time)
    {
        //当前排名
        labRank.text = rank.ToString();
        //名字
        labName.text = name;
        //时间
        string str = "";
        //小时
        if (time / 3600 > 0)
            str += time / 3600 +"h";
        //分
        if (time % 3600 / 60 > 0 || str != "")
            str += time % 3600 / 60 + "m";
        //秒
        str += time % 60 + "s";

        labTIme.text = str;

    }
}
