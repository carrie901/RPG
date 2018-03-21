using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 基础的状态界面
    /// </summary>
    public abstract class StateNodeBase
    {

        public string _name;
        public override string ToString() { return _name; }
        public abstract void OnEnter(object obj);
        public abstract void OnExit();
        public abstract void OnUpdate(float dt);

        /// <summary>
        /// 是否要自动transition到下一个节点
        /// </summary>
        /// <returns></returns>
        public abstract bool AutoTransNext();

        public abstract void Destroy();
    }
}

