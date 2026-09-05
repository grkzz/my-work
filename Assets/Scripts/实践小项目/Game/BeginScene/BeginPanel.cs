using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginPanel : BasePanel<BeginPanel>
{
    //首先声明公共的成员变量 来关联各个控件
    public CustomGUIButton btnBegin;
    public CustomGUIButton btnSetting;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnRank;

    // Start is called before the first frame update
    void Start()
    {
        //目的 为了方便控制坦克头部转向 所以锁定鼠标在窗口内
        Cursor.lockState = CursorLockMode.Confined;

        //监听一次按钮点击过后要做什么
        btnBegin.clickEvent += () =>
        {
            //切换场景
            SceneManager.LoadScene("GameScene");
        };
        btnSetting.clickEvent += () =>
        {
            //打开设置面板
            SettingPanel.Instance.ShowMe();
            HideMe();
        };
        btnQuit.clickEvent += () =>
        {
            //退出游戏
            Application.Quit();
        };
        btnRank.clickEvent += () =>
        {
            //打开排行榜面板
            RankPanel.Instance.ShowMe();
            HideMe();
        };
    }
}
