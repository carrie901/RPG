
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
#if UNITY_EDITOR
using System.Collections.Generic;
using Summer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class DemoTexturePacker : MonoBehaviour
{

    #region 属性

    public List<Image> _imgs;
    public TexturePacker _texPacker;

    public bool flagDis = false;

    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        _texPacker = new TexturePacker();
        for (int i = 1; i < 16; i++)
        {
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(string.Format("Assets/res_bundle/icon/Buff/{0:00}.png", i));
            _texPacker.AddTexture2D(tex);
        }
        _texPacker.PackerTexture();

        for (int i = 0; i < _imgs.Count; i++)
        {
            _imgs[i].sprite = _texPacker.GetSprite(string.Format("{0:00}", (i + 1)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisposeTex();
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods

    private void DisposeTex()
    {
        if (flagDis)
        {
            flagDis = false;
            _texPacker.Dispose();
        }
    }


    #endregion
}
#endif