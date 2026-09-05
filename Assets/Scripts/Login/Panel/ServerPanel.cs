using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    public Button btnStart;
    public Button btnChange;
    public Button btnBack;

    public Text txtName;

    public override void Init()
    {
        btnBack.onClick.AddListener(()=> {
            //避免 自动登录时 返回登录界面 出现问题
            if (LoginMgr.Instance.LoginData.autoLogin)
                LoginMgr.Instance.LoginData.autoLogin = false;

            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
        });

        btnStart.onClick.AddListener(()=> {

            //进入游戏
            //由于过场景 Canvas对象不会被移除 所以 下面的面板应该也要隐藏了
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
            //隐藏我们的 登录背景图面板
            UIManager.Instance.HidePanel<LoginBKPanel>();

            //存储数据的目的 是为了 存储到它当前选择的服务器ID
            LoginMgr.Instance.SaveLoginData();

            //切场景
            SceneManager.LoadScene("GameScene");
        });

        btnChange.onClick.AddListener(()=> {
            //显示服务器列表面板
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            //隐藏自己
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();

        //显示自己的时候 更新 当前选择的服务器名字
        //之后 我们会通过 记录的上一次登录的服务器ID 来更新内容
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if(id <= 0)
        {
            txtName.text = "无选择";
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "区 " + info.name;
        }
       
    }
}
