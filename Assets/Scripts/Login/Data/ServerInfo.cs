using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单个服务器数据
/// </summary>
public class ServerInfo
{
    //区号 ID
    public int id;
    //服务器名
    public string name;
    //服务器状态 0~4就是5种状态
    public int state;
    //是否是新服
    public bool isNew;
}
