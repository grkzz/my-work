using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    //1声明成员变量 关联控件
    public CustomGUISlider sliderMusic;
    public CustomGUISlider sliderSound;

    public CustomGUIToggle togMusic;
    public CustomGUIToggle togSound;

    public CustomGUIButton btnClose;

    // Start is called before the first frame update
    void Start()
    {
        //2监听对应的事件 处理逻辑(修改数据的时候才会调用）
        //处理音乐的变化
        sliderMusic.changeValue += (value) => GameDataMgr.Instance.ChangeBKValue(value);
        //处理音效的变化
        sliderSound.changeValue += (value) => GameDataMgr.Instance.ChangeSoundValue(value);
        //处理音乐开关
        togMusic.changeValue += (value) => GameDataMgr.Instance.OpenOrCloseBKMusic(value);
        //处理音效开关
        togSound.changeValue += (value) => GameDataMgr.Instance.OpenOrCloseSound(value);
        
        btnClose.clickEvent += () =>
        {
            //隐藏自己
            HideMe();

            //判断当前所在场景 应该如何判断
            if(SceneManager.GetActiveScene().name =="BeginScene")
            {
                //让开始面板重新显示出来
                BeginPanel.Instance.ShowMe();
            }
            else
            {
                GamePanel.Instance.ShowMe();
            }
            
        };

        HideMe();
    }

    //根据数据 更新面板
    public void UpdatePaneInfo()
    {
        //我们面板上的信息都是根据 音效数据更新的
        MusicData data = GameDataMgr.Instance.musicData;

        //设置面板内容
        sliderMusic.nowValue = data.bkValue;
        sliderSound.nowValue = data.soundValue;
        togMusic.isSel = data.isOpenBK;
        togSound.isSel = data.isOpenSound;
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //每次显示 面板时 顺便 把面板上的内容更新了
        UpdatePaneInfo();
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
