using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lesson3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 随机数
        //Unity中的随机数
        //Unity当中 的Random类 和C#中的不一样（命名空间不同）
        //使用是随机数 int重载 规则是 左包含右不包含（和C#中一样）
        //0-99之间的数
        int randomNum = Random.Range(0,100);
        print(randomNum);
        //float重载 规则是 左包含右也包含
        //1.1-9.9之间的数
        float randomNumF = Random.Range(1.1f, 9.9f);

        //C#中的随机数
        //System.Random r = new System.Random();
        //r.Next(0, 100);
        #endregion

        #region 知识点二 委托
        //C#的自带委托
        System.Action ac = () =>
        {
            print("123");
        };

        System.Action<int, float> ac2 = (i, f) =>
        {
            
        };
        System.Func<int> fc = () =>
        {
            return 1;
        };

        System.Func<int,string> fc1 = (a) =>
        {
            return "1";
        };

        //Unity的自带委托
        UnityAction uac = () =>
        {

        };
        UnityAction<string> uac1 = (s) =>
        {

        };
        //Unity自带委托只有无返回值的
        //有返回值的Func没有专属类
        
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
