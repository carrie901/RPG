
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

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Sequence
{

    public interface I_SequenceFactory
    {
        SequenceLine Create(SkillContainer container, SpellInfoCnf spell_info);
    }

    public class SequenceNodeActionFactory
    {
        public static T Create<T>() where T : SequenceLeafNode, new()
        {
            T t = new T();
            return t;
        }
    }

    public class SequenceFactory
    {
        public static Dictionary<string, Type> _map = new Dictionary<string, Type>();


        public const string PLAY_ANIMATION_LEAF_NODE = "PlayAnimationLeafNode";
        public const string PLAY_POSITION_OFFSET_LEAF_NODE = "PlayPositionOffsetLeafNode";
        public const string PLAY_POSITION_TARGET_LEAF_NODE = "PlayPositionTargetLeafNode";
        static SequenceFactory()
        {
            _map.Add(PLAY_ANIMATION_LEAF_NODE, typeof(PlayAnimationLeafNode));
            _map.Add(PLAY_POSITION_OFFSET_LEAF_NODE, typeof(PlayPositionOffsetLeafNode));
            _map.Add(PLAY_POSITION_TARGET_LEAF_NODE, typeof (PlayPositionTargetLeafNode));
        }

        public static SequenceLeafNode CreateNode(string node_name, EdNode cnf_node)
        {
            Type t = _map[node_name];
            SequenceLeafNode node = Activator.CreateInstance(t) as SequenceLeafNode;
            if (node != null)
                node.SetConfigInfo(cnf_node);
            return node;
        }

        #region 属性

        public static SequenceLeafNode CreateAnimation(EdNode cnf_node)
        {
            PlayAnimationLeafNode node = SequenceNodeActionFactory.Create<PlayAnimationLeafNode>();
            node.SetConfigInfo(cnf_node);
            return node;
        }

        public static SequenceLeafNode CreateChangeAnimationSpeed(EdNode cnf_node)
        {
            PlayAnimationLeafNode node = SequenceNodeActionFactory.Create<PlayAnimationLeafNode>();
            node.SetConfigInfo(cnf_node);
            return node;
        }

        #endregion

    }


    public class NormalAttackSequence
    {
        public SequenceLine Create()
        {
            SequenceLine line = new SequenceLine();

            return line;
        }
    }

}