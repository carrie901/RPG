using UnityEngine;
namespace Summer
{

    public interface I_RefCounter
    {
        int RefCount { get; }
        void Retain();
        void Release();
    }

    /// <summary>
    /// TODO 2017.11.10
    ///     纹理，如果有一个图片还没加载成功就已经呗销毁了。这种情况如何处理他的引用计数
    ///     1. 加载资源成功的时候需要判断资源寄存的地方是否需要销毁
    ///     2.资源都是加载成功之后才开始计数
    /// </summary>
    public class RefCounter : MonoBehaviour
    {
        public string ref_name = string.Empty;
        public E_GameResType ref_type;
        void Awake()
        {
            //ResManager.instance.RefIncrease(ref_name, ref_type);
        }

        void OnDestroy()
        {
            RemoveRef();
        }

        public void AddRef(string r_name, E_GameResType type)
        {
            ref_name = r_name;
            ref_type = type;
            ResManager.instance.RefIncrease(ref_name, ref_type);
            AddExcute();
        }

        public void RemoveRef()
        {
            if (string.IsNullOrEmpty(ref_name) || ref_type == E_GameResType.none)
            {
                ResLog.Error("Ref Decrease Error. Name:[{0}],GameResType:[{1}]", ref_name, ref_type);
            }
            RemoveExcute();
            ResManager.instance.RefDecrease(ref_name, ref_type);
        }

        public virtual void AddExcute()
        {

        }

        public virtual void RemoveExcute()
        {

        }

    }
}

