using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem : MonoBehaviour
{
    //按钮本身
    public Button btnSelf;
    //是否是新服
    public Image imgNew;
    //状态图
    public Image imgState;
    //名字
    public Text txtName;

    //当前按钮 代表哪个服务器 之后 会使用其中的数据
    public ServerInfo nowServerInfo;

    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(()=> {
            //记录当前你选择的服务器
            LoginMgr.Instance.LoginData.frontServerID = nowServerInfo.id;

            //隐藏 选服面板
            UIManager.Instance.HidePanel<ChooseServerPanel>();

            //显示 服务器面板
            UIManager.Instance.ShowPanel<ServerPanel>();
        });
    }

    /// <summary>
    /// 初始化方法 用于 更新按钮显示相关
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(ServerInfo info)
    {
        //记录下数据
        nowServerInfo = info;

        //更新按钮上的信息
        txtName.text = info.id + "区 " + info.name;
        //是否是新服
        imgNew.gameObject.SetActive(info.isNew);
        //一开让状态图 显示
        imgState.gameObject.SetActive(true);
        //状态
        //加载图集
        SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
        switch (info.state)
        {
            case 0:
                imgState.gameObject.SetActive(false);
                break;
            case 1://流畅
                imgState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                break;
            case 2://繁忙
                imgState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                break;
            case 3://火爆
                imgState.sprite = sa.GetSprite("ui_DL_huobao_01");
                break;
            case 4://维护
                imgState.sprite = sa.GetSprite("ui_DL_weihu_01");
                break;
        }
    }
}
