using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lesson1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
   

      
    }

    // Update is called once per frame
    void Update()
    {
        #region 知识点一 场景切换
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //切换到场景二
            //直接 写代码 切换场景 可能会报错
            //原因是没有把该场景加载到场景列表当中
            SceneManager.LoadScene("Test2");

            //用它不会报错 只会有警告 一样可以切换场景
            //SceneManger
            //Application.LoadLevel("Test2");
        }
        #endregion

        #region 知识点二 退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //执行这句代码 就会退出游戏
            //但是 在编辑模式（Unity）下没有作用
            //发布游戏过后 才能在游戏里面使用
            Application.Quit();
        }
        #endregion
    }
}
