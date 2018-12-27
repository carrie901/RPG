
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
//                 佛祖保佑             永无BUG

using System.Collections.Generic;
using UnityEngine;
using Summer;
public class TestAnimation01 : MonoBehaviour
{

    #region 属性



    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        /*Animator animator = EntitesManager.Instance.Manual._animGroup._animator;
        AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo(0); 
        if ((animatorInfo.normalizedTime > 1.0f) && !animatorInfo.IsName(AnimationNameConst.IDLE))
        {
            //EntitesManager.Instance.Manual._anim_group.PlayAnim(AnimationNameConst.IDLE);
        }*/
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), "动作"))
        {
            //EntitesManager.Instance.Manual.AnimGroup.PlayAnimation(AnimationNameConst.ATTACK_01);
        }
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



    #endregion
}
