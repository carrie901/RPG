
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

namespace Summer.Sequence
{
    public class SkillFactory
    {
        public static string Id = "Id";
        public static string Track = "Track";
        public static string StartFrame = "StartFrame";
        public static string FrameLength = "FrameLength";
        public static string Leafs = "Leafs";

        public static SequenceLine Create()
        {

            SequenceLine line = new SequenceLine();

            TrackLine track1 = CreateTrack(0, 30 * 3);
            line.AddTrack(track1);

            //1.移动到指定地点
            //1.1 播放移动动画
            //1.2 位移偏移
            SequenceLeafNode node1 = SequenceFactory.CreateNode(SequenceFactory.PLAY_POSITION_TARGET_LEAF_NODE, null);
            (node1 as PlayPositionTargetLeafNode)._bbTarget = SequenceSelfConst.TARGET_POSITION1;
            track1.AddNode(node1);

            TrackLine track2 = CreateTrack(30 * 3, 30 * 3);
            line.AddTrack(track2);

            SequenceLeafNode node2 = SequenceFactory.CreateNode(SequenceFactory.PLAY_ANIMATION_LEAF_NODE, null);
            track2.AddNode(node2);


            TrackLine track3 = CreateTrack(30 * 6, 30 * 3);
            line.AddTrack(track3);

            SequenceLeafNode node3 = SequenceFactory.CreateNode(SequenceFactory.PLAY_POSITION_TARGET_LEAF_NODE, null);
            (node3 as PlayPositionTargetLeafNode)._bbTarget = SequenceSelfConst.TARGET_POSITION2;
            track3.AddNode(node3);
            line.CalEnd();
            return line;
        }

        public static SequenceLine Create(EdNode root)
        {
            SequenceLine sequenceLine = new SequenceLine();
            sequenceLine.Id = root.GetAttribute(Id).ToInt();
            List<EdNode> nodes = root.GetNodes(Track);
            int length = nodes.Count;
            for (int i = 0; i < length; i++)
            {
                EdNode node = nodes[i];
                bool result = AddLeafNodes(sequenceLine, node);
                if (!result)
                    return null;
            }
            return sequenceLine;
        }

        private static bool AddLeafNodes(SequenceLine sequenceLine, EdNode trackNode)
        {
            int startFarme = trackNode.GetAttribute(StartFrame).ToInt();
            int frameLength = trackNode.GetAttribute(FrameLength).ToInt();
            TrackLine trackLine = CreateTrack(startFarme, frameLength);
            sequenceLine.AddTrack(trackLine);

            List<EdNode> nodes = trackNode.Nodes;
            int length = nodes.Count;
            for (int i = 0; i < length; i++)
            {
                EdNode node = nodes[i];
                SequenceLeafNode leafNode = CreateLeftNode(node);
                if (leafNode == null) return false;
                trackLine.AddNode(leafNode);
            }
            return true;
        }

        public static TrackLine CreateTrack(int startFrame, int frameLength)
        {
            TrackLine line = new TrackLine(startFrame, frameLength);
            return line;
        }

        public static SequenceLeafNode CreateLeftNode(EdNode node)
        {
            Type type = Type.GetType("Summer.Sequence." + node.Name);

            SkillLog.Assert(type != null, "SkillFactory CreateLeftNode 找不到对应的技能节点类型:[{0}]", node.Name);
            if (type == null) return null;

            SequenceLeafNode leaf = Activator.CreateInstance(type) as SequenceLeafNode;
            SkillLog.Assert(leaf != null, "SkillFactory CreateLeftNode 实例化失败:[{0}]", node.Name);
            if (leaf != null) leaf.SetConfigInfo(node);
            return leaf;
        }
    }
}
