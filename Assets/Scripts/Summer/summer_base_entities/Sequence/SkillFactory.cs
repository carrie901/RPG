
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

using System.Collections.Generic;
using UnityEngine;

namespace Summer.Sequence
{
    public class SkillFactory
    {
        public static SequenceLine Create()
        {

            SequenceLine line = new SequenceLine();

            TrackLine track1 = CreateTrack(0,30*3);
            line.AddTrack(track1);

            //1.移动到指定地点
            //1.1 播放移动动画
            //1.2 位移偏移
            SequenceLeafNode node1 = SequenceFactory.CreateNode(SequenceFactory.PLAY_POSITION_TARGET_LEAF_NODE, null);
            (node1 as PlayPositionTargetLeafNode)._bb_target = SequenceSelfConst.TARGET_POSITION1;
            track1.AddNode(node1);

            TrackLine track2 = CreateTrack(30 * 3, 30 * 3);
            line.AddTrack(track2);

            SequenceLeafNode node2 = SequenceFactory.CreateNode(SequenceFactory.PLAY_ANIMATION_LEAF_NODE, null);
            track2.AddNode(node2);


            TrackLine track3 = CreateTrack(30 * 6, 30 * 3);
            line.AddTrack(track3);

            SequenceLeafNode node3 = SequenceFactory.CreateNode(SequenceFactory.PLAY_POSITION_TARGET_LEAF_NODE, null);
            (node3 as PlayPositionTargetLeafNode)._bb_target = SequenceSelfConst.TARGET_POSITION2;
            track3.AddNode(node3);
            line.CalEnd();
            return line;
        }

        public static TrackLine CreateTrack(int start_frame, int frame_length)
        {
            TrackLine line = new TrackLine(start_frame, frame_length);
            return line;
        }



    }
}
