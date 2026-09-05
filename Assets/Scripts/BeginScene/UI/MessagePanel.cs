using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    public Button cleanAllBtn;
    public Button sureBtn;

    public override void Init()
    {
        cleanAllBtn.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.ResetData();
            Application.Quit();
        });

        sureBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<MessagePanel>();
        });
    }

 
}
