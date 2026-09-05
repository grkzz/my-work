using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这个是游戏数据管理类 是一个单例模式对象
/// </summary>
public class GameDataMgr 
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance { get => instance; }

    //音效数据对象
    public MusicData musicData;
    //排行榜数据对象
    public RankList rankData;
    private GameDataMgr()
    {
        //可以去初始化 游戏数据
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "Music") as MusicData;
        //如果第一次进入游戏 没有音效数据 那么所有的数据要不是false 要不是0
        if(!musicData.notFirst)
        {
            musicData.notFirst = true;
            musicData.isOpenBK = true;
            musicData.isOpenSound = true;
            musicData.soundValue = 0.4f;
            musicData.bkValue = 0.4f;
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
        }

        //初始化读取排行榜数据
        rankData = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList), "Rank") as RankList;

    }

    //提供一些API给外部 方便数据的改变存储

    //提供一个 在排行榜中添加数据的方法
    public void AddRankInfo(string name,int score,float time)
    {
        rankData.list.Add(new RankInfo(name,score,time));
        //排序分排名
        rankData.list.Sort((a, b) =>
        {
            if (a.score > b.score)
            {
                return -1;
            }
            else if(a.score == b.score && a.time<b.time)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        });
        //排序过后 移除10条以外的数据
        //从尾部往前遍历 移除每一条
        for (int i = rankData.list.Count-1; i >=10; i--)
        {
            rankData.list.RemoveAt(i);
        }
        //存储
        PlayerPrefsDataMgr.Instance.SaveData(rankData, "Rank");
    }

    //开启或者关闭背景音乐
    public void OpenOrCloseBKMusic(bool isOpen)
    {
        musicData.isOpenBK = isOpen;
        //在这里控制场景上的背景音乐开关
        BKMusic.Instance.ChangeOpen(isOpen);
        //存储改变后的数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }

    //开启或者关闭音效
    public void OpenOrCloseSound(bool isOpen)
    {
        musicData.isOpenSound = isOpen;

       
        //存储改变后的数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }

    //改变背景音乐大小
    public void ChangeBKValue(float value)
    {
        musicData.bkValue = value;

        //在这里控制场景上的背景音乐大小
        BKMusic.Instance.ChangeValue(value);
        //存储改变后的数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }

    //改变音效大小
    public void ChangeSoundValue(float value)
    {
        musicData.soundValue = value;
        //存储改变后的数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }
}
