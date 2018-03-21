using UnityEngine;
using UnityEngine.UI;
namespace Summer
{
    [RequireComponent(typeof(Text))]
    public class LanguageLoc : MonoBehaviour
    {
        public string key;
        public string pfb_name;
        public string des;

        public Text _text;
        private void Awake()
        {
            /*if (_text == null)
                _text = gameObject.GetComponent<Text>();
            if (_text != null)
            {
                _text.text = LangLocSet.Instance.FindTextIdByKey(pfb_name, key).Loc();
            }
            else
            {
                LogManager.Log("view:[{0}],Key:[{1}]", pfb_name, key);
            }*/
        }

        public bool CheckVaild()
        {
            if (_text == null)
            {
                Debug.LogFormat("本地化文本为空 Name:[0],view:[{ 1}],Key:[{2}]", gameObject.name, pfb_name, key);
                return false;
            }
            string content = LangLocSet.Instance.FindTextIdByKey(pfb_name, key).Loc();
            if (content == LangLocSet.NULL_)
                return false;
            return true;
        }

        #region Editor

        public void SetLanguage(bool value)
        {
            //if (!CheckVaild()) return;
            _text.text = value ? LangLocSet.Instance.FindTextIdByKey(pfb_name, key).Loc() : "本地化";
        }

        public void LoadText()
        {
            if (_text != null)
            {
                _text = gameObject.GetComponent<Text>();
            }

        }

        public void SetKeyDes(string k, string d, string p_name)
        {
            key = k;
            des = d;
            pfb_name = p_name;
        }

        #endregion
    }
}

