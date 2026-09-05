using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    //确定和取消那妞
    public Button btnSure;
    public Button btnCancel;

    //账号密码输入框
    public InputField inputUN;
    public InputField inputPW;

    public override void Init()
    {
        btnCancel.onClick.AddListener(()=> {
            //隐藏自己
            UIManager.Instance.HidePanel<RegisterPanel>();
            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();
        });

        btnSure.onClick.AddListener(()=> { 
        
            //判断输入的账号密码 是否合理
            if( inputPW.text.Length <= 6 ||
                inputUN.text.Length <= 6 )
            {
                //提示不合法 
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示面板上提示的内容
                panel.ChangeInfo("账号和密码都必须大于6位");
                return;
            }
                
            //去注册账号密码
            if( LoginMgr.Instance.RegisterUser(inputUN.text, inputPW.text) )
            {
                //清理登录数据 用于 新注册账号的 数据重置 不然会残留上一个账号的相关数据
                LoginMgr.Instance.ClearLoginData();

                //注册成功
                //显示 登录面板
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                //更新登录面板上的 用户名和密码
                loginPanel.SetInfo(inputUN.text, inputPW.text);

                //隐藏自己
                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                //提示别人 用户名已经存在 
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示内容
                tipPanel.ChangeInfo("用户名已存在");

                //方便别人重新输入
                inputUN.text = "";
                inputPW.text = "";
            }

        });
    }
}
