
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
using UnityEngine;

/// <summary>
/// 财富分配原则
/// </summary>
public class TestPeople : MonoBehaviour
{

    #region 属性



    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {

    }


    public bool flag;
    // Update is called once per frame
    void Update()
    {
        if (!flag) return;
        flag = false;
        OnExcute1();
    }


    public List<People> _peoples = new List<People>();
    private void OnExcute1()
    {
        _peoples.Clear();
        int count = 100;
        int money = 100;
        for (int i = 0; i < count; i++)
        {
            _peoples.Add(new People(money));
        }

        int getMoneyCount = 17000;
        for (int i = 0; i < getMoneyCount; i++)
        {
            Cal();
        }


        Print();
    }

    private void Cal()
    {
        int length = _peoples.Count;
        for (int i = 0; i < length; i++)
        {
            if (!_peoples[i].CanTake()) continue;
            _peoples[i].Remove();

            int index = Random.Range(0, length);
            _peoples[index].Add();
        }
    }

    private void Print()
    {
        _peoples.Sort(SortPeople);
        for (int i = 0; i < _peoples.Count; i++)
        {
            Debug.Log(_peoples[i].money);
        }
    }

    private int SortPeople(People a, People b)
    {
        if (a.money > b.money) return 1;
        if (a.money < b.money) return -1;
        return 0;
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



    #endregion
}


public class People
{
    public static int iid;

    public int id;
    public int money;
    public People(int m)
    {
        money = m;
        id = iid++;
    }



    public bool CanTake()
    {
        return money > 0;
    }

    public void Add()
    {
        money++;
    }

    public void Remove()
    {
        if (!CanTake()) return;
        money--;
    }


}