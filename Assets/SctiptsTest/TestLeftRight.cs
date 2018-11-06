
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
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TestLeftRight : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IMoveHandler
{

    #region 属性

    public RectTransform _img;
    public bool _useX;
    public bool _useY;
    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {

    }

    private Vector3 mStart = new Vector3(-1, 1, 0);
    private Vector3 mEnd = new Vector3(1, 1, 0);

    // Update is called once per frame
    void Update()
    {
        //绘制坐标轴
        Debug.DrawLine(new Vector3(-100, 0, 0), new Vector3(100, 0, 0), Color.green);
        Debug.DrawLine(new Vector3(0, -100, 0), new Vector3(0, 100, 0), Color.green);
        Debug.DrawLine(new Vector3(0, 0, -100), new Vector3(0, 0, 100), Color.green);

        Debug.DrawLine(Vector3.zero, mStart, Color.red);
        Debug.DrawLine(Vector3.zero, mEnd, Color.red);
        Debug.DrawLine(mStart, mEnd, Color.red);
        for (int i = 1; i < 10; ++i)
        {
            Vector3 drawVec = Vector3.Slerp(mStart, mEnd, 0.1f * i);
            Debug.DrawLine(Vector3.zero, drawVec, Color.yellow);

        }
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



    #endregion

    public float _move;
    public float _moveScale;
    public Vector2 _minMax;
    public Vector2 _lastPos;
    public void OnDrag(PointerEventData data)
    {
        Vector3 newPos = Vector3.zero;
        float m = (data.position - _lastPos).x;
        _move = -m * _moveScale;
        _move = Mathf.Clamp(_move, _minMax.x, _minMax.y);
        _img.localEulerAngles = new Vector3(0, 0, _move);
        Debug.Log(_move * _moveScale);
        if (_useX)
        {

        }

        if (_useY)
        {

        }
        //_img.localPosition = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
    }

    public void OnPointerUp(PointerEventData data)
    {
        _lastPos = data.position;
        _move = 0;
        Debug.Log("OnPointerUp");
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDown");
        _lastPos = data.position;
    }

    public void OnMove(AxisEventData data)
    {
        Debug.Log("OnMove:" + data.moveVector);
    }
}
