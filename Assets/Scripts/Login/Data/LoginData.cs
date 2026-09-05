using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录界面可能需要记住的玩家操作相关数据
/// </summary>
public class LoginData
{
    //用户名
    public string userName;
    //密码
    public string passWord;

    //是否记住密码
    public bool rememberPw;
    //是否自动登录
    public bool autoLogin;

    //服务器相关
    //-1代表没有选择过服务器
    public int frontServerID = 0;
}
