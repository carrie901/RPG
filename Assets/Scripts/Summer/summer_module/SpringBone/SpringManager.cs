
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

//
//SpingManager.cs for unity-chan!
//
//Original Script is here:
//ricopin / SpingManager.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
//Revised by N.Kobayashi 2014/06/24
//           Y.Ebata
//
using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    public class SpringManager : MonoBehaviour
    {
        //Kobayashi
        // DynamicRatio is paramater for activated level of dynamic animation 
        public float dynamicRatio = 1.0f;

        public List<SpringBone> springBones = new List<SpringBone>(32);


        void Start()
        {
            SpringBone[] bones = gameObject.GetComponentsInChildren<SpringBone>();
            springBones.Clear();
            int length = bones.Length;
            for (int i = 0; i < length; i++)
            {
                bones[i]._managerRef = this;
                springBones.Add(bones[i]);
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < springBones.Count; i++)
            {
                //					if (dynamicRatio > springBones [i].threshold) {
                springBones[i].UpdateSpring();
            }
        }
    }
}
