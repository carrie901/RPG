using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Summer
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        public string key;
        public E_ViewId view;
        public string des;

        private int sound_id;

        void Awake()
        {
            sound_id = ButtonSoundManager.Instance.FindSoundIdByKey(view, key);
            if (sound_id == -1)
            {
                sound_id = SoundConst.COMMON;
                //LogManager.Error("[{0}]找不到对应的声音的信息.UI:[{1}],Key[{2}]", gameObject.name, view, key);
            }
        }

        public void OnPointerClick(PointerEventData event_data)
        {
            SoundManager.instance.Play(sound_id);
        }
    }
}

