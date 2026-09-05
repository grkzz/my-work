using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : BasePanel<WinPanel>
{
    //关联 控件
    public CustomGUIInput inputInfo;
    public CustomGUIButton btnSure;
    // Start is called before the first frame update
    void Start()
    {
        btnSure.clickEvent += () =>
        {
            //取消游戏暂停
            Time.timeScale = 1;

            //把数据记录到排行榜中 并且 回到主场景中
            GameDataMgr.Instance.AddRankInfo(inputInfo.content.text,
                GamePanel.Instance.nowScore,
                GamePanel.Instance.nowTime);

            //接着 就返回我们的 开始界面即可
            SceneManager.LoadScene("BeginScene");
        };

        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
