using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    //注册按钮
    public Button btnRegister;
    //确定登录按钮
    public Button btnSure;

    //账号和密码控件
    public InputField inputUN;
    public InputField inputPW;

    //记住密码和自动登录 多选框
    public Toggle togPW;
    public Toggle togAuto;

    public override void Init()
    {
        //点击注册 做什么
        btnRegister.onClick.AddListener(()=> {
            //显示注册面板
            UIManager.Instance.ShowPanel<RegisterPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<LoginPanel>();
        });

        //点击登录 做什么
        btnSure.onClick.AddListener(()=> {
            //点击登录后 要验证用户民密码 是否正确 

            //判断是否合法
            if (inputPW.text.Length <= 6 ||
                inputUN.text.Length <= 6)
            {
                //提示不合法 
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示面板上提示的内容
                panel.ChangeInfo("账号和密码都必须大于6位");
                return;
            }

            //验证 用户名和密码 是否 通过
            if( LoginMgr.Instance.CheckInfo(inputUN.text, inputPW.text) )
            {
                //登录成功

                //记录数据
                LoginMgr.Instance.LoginData.userName = inputUN.text;
                LoginMgr.Instance.LoginData.passWord = inputPW.text;
                LoginMgr.Instance.LoginData.rememberPw = togPW.isOn;
                LoginMgr.Instance.LoginData.autoLogin = togAuto.isOn;
                LoginMgr.Instance.SaveLoginData();

                //根据服务器信息 来进行判断 显示哪个面板
                if( LoginMgr.Instance.LoginData.frontServerID <= 0 )
                {
                    //如果从来没有选择过服务器 id为-1时  就应该直接打开 选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //打开我们的服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>();
            }
            else
            {
                //登录失败
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或密码错误");
            }

        });

        //点击记住密码 逻辑
        togPW.onValueChanged.AddListener((isOn) =>
        {
            //当记住密码取消选中状态时 自动登录 也应该取消选中
            if( !isOn )
            {
                togAuto.isOn = false;
            }
        });

        //点击自动登录 逻辑
        togAuto.onValueChanged.AddListener((isOn) =>
        {
            //当我们选中 自动登录时  如果记住密码 没有被选中 应该让它选中
            if(isOn)
            {
                togPW.isOn = true;
            }
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己时  根据数据 更新面板上的内容

        //得到数据
        LoginData loginData = LoginMgr.Instance.LoginData;

        //初始化面板显示
        //更新 两个多选框
        togPW.isOn = loginData.rememberPw;
        togAuto.isOn = loginData.autoLogin;

        //更新账号密码
        inputUN.text = loginData.userName;
        //根据你是否上一次勾选了记住密码 来决定是否 更新密码
        if (togPW.isOn)
            inputPW.text = loginData.passWord;

        //如果是自动登录 做什么
        if( togAuto.isOn )
        {
            //自动去验证账号密码相关
            //验证用户名密码
            if( LoginMgr.Instance.CheckInfo(inputUN.text, inputPW.text) )
            {
                //根据服务器信息 来进行判断 显示哪个面板
                if (LoginMgr.Instance.LoginData.frontServerID <= 0)
                {
                    //如果从来没有选择过服务器 id为-1时  就应该直接打开 选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //打开我们的服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>(false);
            }
            else
            {
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                panel.ChangeInfo("账号密码错误");
            }
        }
    }


    /// <summary>
    /// 提供给外部 快捷设置用户名和密码的方法
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    public void SetInfo(string userName, string passWord)
    {
        inputUN.text = userName;
        inputPW.text = passWord;
    }
}
