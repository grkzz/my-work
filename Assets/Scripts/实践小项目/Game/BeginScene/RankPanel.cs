using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
    
{
    //关联public的 控件对象

    public CustomGUIButton btnClose;

    //因为控件较多 拖的话 工作量太大了 我们直接偷懒 通过代码找
    private List<CustomGUILabel> labName = new List<CustomGUILabel>();
    private List<CustomGUILabel> labScore = new List<CustomGUILabel>();
    private List<CustomGUILabel> labTime = new List<CustomGUILabel>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            //小知识应用 找字对象的子对象 可以通过 斜杠来区分父子关系
            labName.Add(this.transform.Find("Name/labName" + i).GetComponent<CustomGUILabel>());
            labScore.Add(this.transform.Find("Score/labScore" + i).GetComponent<CustomGUILabel>());
            labTime.Add(this.transform.Find("Time/labTime" + i).GetComponent<CustomGUILabel>());
        }
        //处理事件监听逻辑
        btnClose.clickEvent += () =>
        {
            BeginPanel.Instance.ShowMe();
            HideMe();
        };
        //加入的测试代码
        //GameDataMgr.Instance.AddRankInfo("测试数据",100,8432);
        //GameDataMgr.Instance.AddRankInfo("测试数据", 100, 600);
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanelInfo();
    }
    public void UpdatePanelInfo()
    {
        //处理根据排行榜数据 更新面板
        //获取 GameDataMgr中的排行榜列表 用于在这里更新

        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        for (int i = 0; i < list.Count; i++)
        {
            //名字
            labName[i].content.text = list[i].name;
            //分数
            labScore[i].content.text = list[i].score.ToString();
            //时间 存储的时间单位是s
            //把秒数 转换成 时 分 秒
            int time = (int)list[i].time;
            labTime[i].content.text = "";
            //得到几个小时
            if(time/3600>0)
            {
                labTime[i].content.text += time/3600 + "时";
            }
            if (time % 3600 /60 > 0 || labTime[i].content.text != "")
            {
                labTime[i].content.text += time % 3600 / 60 + "分";
            }
            labTime[i].content.text += time%60 + "秒";
        }
    }
}
