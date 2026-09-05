using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound;
    public override void Init()
    {
        //初始化面板显示内容 根据本地存储的设置数据来初始化
        MusicData data = GameDataMgr.Instance.musicData;
        togMusic.isOn = data.musicOpen;
        togSound.isOn = data.soundOpen;
        sliderMusic.value = data.musicValue;
        sliderSound.value = data.soundValue;

        btnClose.onClick.AddListener(() =>
        {
            //为了节约性能 只有当设置完成后关闭面板时才会真正记录保存数据 写到硬盘上
            GameDataMgr.Instance.SaveMusicData();
            //隐藏自己 隐藏设置面板
            UIManager.Instance.HidePanel<SettingPanel>();
        });

        togMusic.onValueChanged.AddListener((isOpen) =>
        {
            //让背景音乐进行开关
            BKMusic.Instance.SetIsOpen(isOpen);
            //记录开关的数据
            GameDataMgr.Instance.musicData.musicOpen = isOpen;
        });

        togSound.onValueChanged.AddListener((isOpen) =>
        {
            //记录音效的开关数据
            GameDataMgr.Instance.musicData.soundOpen = isOpen;
        });

        sliderMusic.onValueChanged.AddListener((value) =>
        {
            //让背景音乐大小改变
            BKMusic.Instance.ChangeValue(value);
            //记录背景音乐大小数据
            GameDataMgr.Instance.musicData.musicValue = value;
        });


        sliderSound.onValueChanged.AddListener((value) =>
        {
            //记录背景音效大小的数据
            GameDataMgr.Instance.musicData.soundValue = value;
        });
    }
}
