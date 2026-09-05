using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem : MonoBehaviour
{
    //按钮自己
    public Button btnSelf;
    //显示的区间内容
    public Text txtInfo;

    //区间范围
    private int beginIndex;
    private int endIndex;

    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(()=> {

            //通知选服面板 改变右侧的区间内容
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdatePanel(beginIndex, endIndex);
        });
    }

    public void InitInfo(int beginIndex, int endIndex)
    {
        //记录当前区间按钮的 区间值
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;

        //把区间显示的内容 更新了
        txtInfo.text = beginIndex + " - " + endIndex + "区";
    }

}
