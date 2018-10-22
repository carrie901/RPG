
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System.Collections.Generic;
using System.Text;
using Summer;
using UnityEngine;

public class TestMoney1 : MonoBehaviour
{

    #region 属性



    #endregion

    #region MONO Override

    public float currPrice;
    // Use this for initialization
    void Start()
    {

        Check4();
    }

    // Update is called once per frame
    void Update()
    {

        Check1();
        Check2();
        Check3();
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods

    public bool flag = false;
    public void Check1()
    {
        if (!flag) return;
        flag = false;
        TextAsset a = Resources.Load<TextAsset>("11");
        string[] infos = a.text.ToStrs(StringHelper._splitHuanhang);
        List<float> prices = new List<float>();

        for (int i = 0; i < infos.Length; i++)
        {
            string[] lineInfo = infos[i].ToStrs("\t");
            prices.Add(float.Parse(lineInfo[1]));
        }

        float pjMoney = 100;
        float allNum = 0;

        for (int i = 0; i < prices.Count; i++)
        {
            float num = pjMoney / prices[i];
            allNum += num;
        }
        float inMoney = pjMoney * prices.Count;
        float inPirce = inMoney / allNum;
        float outMoney = allNum * currPrice;
        Debug.Log("inMoney:" + inMoney + "inPirce:" + inPirce + "outMoney:" + outMoney);
        Debug.Log("My Money:" + (outMoney - inMoney));
        prices.Sort(SortPrice);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < prices.Count; i++)
        {
            sb.AppendLine(prices[i].ToString());
        }
        FileHelper.WriteTxtByFile(Application.streamingAssetsPath + "/price.csv", sb.ToString());
    }

    public bool flag2 = false;
    public void Check2()
    {
        if (!flag2) return;
        flag2 = false;
        TextAsset a = Resources.Load<TextAsset>("11");
        string[] infos = a.text.ToStrs(StringHelper._splitHuanhang);
        List<float> prices = new List<float>();

        for (int i = 0; i < infos.Length; i++)
        {
            string[] lineInfo = infos[i].ToStrs("\t");
            prices.Add(float.Parse(lineInfo[1]));
        }

        float pjCount = 100;
        float allNum = 0;
        float allMoney = 0;
        for (int i = 0; i < prices.Count; i++)
        {
            allMoney += pjCount * prices[i];
            allNum += pjCount;
            /*float num = pjMoney / prices[i];
            allNum += num;*/
        }

        float outMoney = allNum * currPrice;
        Debug.Log("allMoney:" + allMoney + "_outMoney:" + outMoney + "_" + (outMoney - allMoney));

    }

    public int SortPrice(float a, float b)
    {
        if (a - b > 0) return 1;
        else if (a - b < 0) return -1;
        return 0;
    }

    public bool flag3 = false;
    public void Check3()
    {
        if (!flag3) return;
        flag3 = false;
        float allMoney = 0;
        float startPrice = 1.0f;
        float first = startPrice;
        float m1 = 100;
        float count = 0;
        int day = 400;
        float internalAdd = 0.005f;
        for (int i = 0; i < day; i++)
        {
            first += internalAdd;
            count += (m1 / first);
            allMoney += m1;
        }
        Debug.Log("first:" + first);
        first = (first + 1.0f) / 2;
        float outMoney = count * first;
        Debug.LogFormat("InMoney:[{0}],OutMoney:[{1}], GetMoney:[{2}]", allMoney, outMoney, (outMoney - allMoney));
    }

    public void Check4()
    {
        float startPrice = 1.5f;
        float endPrice = startPrice;
        int day = 200 * 2;
        float inRemove = 0.005f;
        float inAdd = 0.01f;
        float count = 0;
        float m1 = 100;
        int tmpAdd = 0;
        for (int i = 0; i < day; i++)
        {
            bool result = i % 2 == 0;
            if (result)
            {
                endPrice = endPrice * (1 + inAdd);
                 float tmpCount = m1 / endPrice;
                 count += tmpCount;
                 tmpAdd++;
            }
            else
            {
                endPrice = endPrice * (1 - inRemove);
                float tmpCount = m1 / endPrice;
                tmpAdd++;
                count += tmpCount;
            }
        }
        Debug.Log("原始" + startPrice + "结束:" + endPrice + "tmpAdd:" + tmpAdd);
        endPrice = endPrice / 2;
        float outM = count * endPrice;
        float inM = day * m1;
        Debug.LogFormat("inM:[{0}],outM:[{1}], GetMoney:[{2}]", inM , outM, (outM - inM));
    }

    #endregion
}
