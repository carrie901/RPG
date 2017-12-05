using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public abstract class FSMState
    {
        public FSMControl m_parent;
        public int m_type;

        public abstract void Enter();       //状态进入时执行动作  
        public abstract void Exit();        //状态退出时执行动作  
        public abstract void Update();      //游戏主循环中状态的内部执行机制  
        public abstract void Init();        //状态的初始化  

        public FSMState CheckTransition() //状态转移判断  
        {
            return null;
        } 
    }
}

