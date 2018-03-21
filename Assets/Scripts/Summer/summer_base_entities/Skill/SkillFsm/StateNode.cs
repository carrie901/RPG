using System;

namespace Summer
{
    public class StateNode : StateNodeBase
    {

        public Action _on_enter;
        public Action _on_exit;
        public Action<float> _on_update;
        public static readonly Action empty_action = delegate {/*do nothing*/};
        public static readonly Action<float> empty_update = delegate {/*do nothing*/};

        public StateNode() { }
        public StateNode(string name, Action on_enter, Action<float> on_update, Action on_exit)
        {
            _name = name;
            _on_enter = on_enter ?? empty_action;
            _on_exit = on_exit ?? empty_action;
            _on_update = on_update ?? empty_update;
        }
        public override void Destroy()
        {
            _on_enter = null;
            _on_exit = null;
        }

        public override void OnEnter(object obj) { _on_enter(); }
        public override void OnExit() { _on_exit(); }
        public override void OnUpdate(float dt) { _on_update(dt); }

        //永远不会主动end，然后调用下一个
        public override bool AutoTransNext() { return false; }
    }
}

