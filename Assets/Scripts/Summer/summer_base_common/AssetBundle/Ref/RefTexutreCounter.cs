using UnityEngine.UI;

namespace Summer
{
    public class RefTexutreCounter : RefCounter
    {
        public RawImage target;
        public override void AddExcute()
        {
            target = gameObject.GetComponent<RawImage>();
        }

        public override void RemoveExcute()
        {
            if (target == null || target.texture == null) return;
            target.texture = null;
        }
    }
}

