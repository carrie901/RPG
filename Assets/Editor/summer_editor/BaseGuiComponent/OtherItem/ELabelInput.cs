
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

namespace SummerEditor
{
    public class ELabelInput : EComponent
    {
        public ELabel title_lab;
        public EInput _input;
        public ELabelInput(string title, float width_title, string input_lab, float width_input, float height = DEFAULT_HEIGHT) : base(width_title + width_input, height)
        {
            title_lab = new ELabel(title, width_title);
            _input = new EInput(input_lab, width_input);
            AddComponent(title_lab, 0, 0);
            AddComponentRight(_input, title_lab);
            show_box = false;
        }

        public string Text { get { return _input.text; } }
    }

    public class ELabelIntInput : EComponent
    {
        public ELabel title_lab;
        public EIntInput input;
        public ELabelIntInput(string title, float width_title, int input_lab, float width_input, float height = DEFAULT_HEIGHT) : base(width_title + width_input, height)
        {
            title_lab = new ELabel(title, width_title);
            input = new EIntInput(input_lab, width_input);
            AddComponent(title_lab, 0, 0);
            AddComponentRight(input, title_lab);
            show_box = false;
        }

        public int Value { get { return input.GetValue(); } }
    }


    public class ELabelTextArea : EComponent
    {
        public ELabel title_lab;
        public ETextArea textarea;
        public ELabelTextArea(string title, float width_title, float width_input, float height) : base(width_title + width_input, height)
        {
            title_lab = new ELabel(title, DEFAULT_HEIGHT);
            textarea = new ETextArea(width_input, height);
            AddComponent(title_lab, 0, 0);
            AddComponentRight(textarea, title_lab);
            show_box = false;
        }
    }
}
