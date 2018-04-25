using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;

public class TCameraCir : MonoBehaviour
{

    /// <summary>  
    /// 中心箭头  
    /// </summary>  
    public GameObject centerObj;
    /// <summary>  
    /// 消息图片对象  
    /// </summary>  
    public GameObject roateObj;
    /// <summary>  
    /// 四元数  
    /// </summary>  
    Quaternion qua;


    // Use this for initialization  
    void Start()
    {

    }

    // Update is called once per frame  
    void Update()
    {

        /*if (centerObj != null)
        {
            //roateObj围绕centerObj旋转，x,y不旋转  
            /* roateObj.transform.RotateAround(centerObj.transform.position, new Vector3(0, 0, 1), 10f * Time.deltaTime);
             //这里处理不然roateObj图片的显示位置发生变化  
             qua = roateObj.transform.rotation;
             qua.z = 0;
             roateObj.transform.rotation = qua;#1#
            transform.RotateAround(centerObj.transform.position, Vector3.up, 20 * Time.deltaTime);
        }*/
        CameraRotate();
    }


    public float x;
    public float y;
    public float RspeedX;
    public float RspeedY;
    public float camHeight = 4;
    public float distance = 1;
    void CameraRotate()
    {
        if (Input.GetMouseButton(0))
        {

            x += Input.GetAxis("Mouse X") * RspeedX;
            y -= Input.GetAxis("Mouse Y") * RspeedX;

            y = MathHelper.WrapAngle(y);

            Quaternion rotateAngle = Quaternion.Euler(y, x, 0);//摄像机偏转角度

            Vector3 direction = new Vector3(0, camHeight, -distance);//摄像机距离物品的距离
            transform.rotation = rotateAngle;//让摄像机始终转向物品
            transform.position = centerObj.transform.position + rotateAngle * direction;//计算旋转多少角度摄像机需要偏移多少
            //transform.LookAt(target);

            y = MathHelper.WrapAngle(y);

        }
    }
}
