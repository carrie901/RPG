﻿using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 持续时间相关的如：眩晕、定身、临时提高xx属性等、魔法盾等
    /// 类似buff效果 有进入和退出时间 总结出来就是三大项
    /// 1.改变人物的属性
    /// 2.改变人物的数值，包括但不限于血量，魔法，
    /// 3.改变人物的状态
    /// </summary>

    /// <summary>
    /// 技能创生体（buff/弹道EntityObject/法术场AOEObject）管理：
    /// buf挂在unit身上，可能影响unit的一些行为和状态；
    /// 法术场一般由场景管理，影响场景中某范围内的unit；
    /// 导弹类就是技能创建的一个子弹，这个子弹可能以不同的路线移动（直线／抛物线／直接命中等）
    /// </summary>

    /// <summary>
    /// 导弹类：追踪箭、魔兽的远程攻击等
    /// 有一点类型AOEObject这样的东西
    /// 
    /// 地点持续类：火墙一类的技能
    /// </summary>

    /// <summary>
    /// 位置相关的如：瞬移、冲撞、击退、跳跃等
    /// </summary>

    /// <summary>
    /// 对技能系统拆解可分为三类
    /// 1.动作表现
    ///     动作、特效、shader、音效
    /// 2.技能逻辑
    /// 3.伤害判定
    /// 
    /// 
    /// 技能创生体（buf/弹道/法术场）管理：
    /// buf挂在unit身上，可能影响unit的一些行为和状态；
    /// 法术场一般由场景管理，影响场景中某范围内的unit；
    /// 弹道就是技能创建的一个子弹，这个子弹可能以不同的路线移动（直线／抛物线／直接命中等）
    /// 
    /// 流程从开始释放一个技能到效果完全结束大致经历这么几个步骤
    /// 1、发出施放请求
    /// 2、验证是否满足使用技能条件
    /// 3、返回失败结果或者开始执行技能同时开始动作、特效播放
    /// 4、执行该技能需要表现的各项效果
    /// 5、如需伤害判定则进行判断并反馈结果
    /// 
    /// 结构
    /// 首先从玩家角度来说，他们所学习、使用的一定是一个个具体的技能
    /// 而我们要实现一些复杂效果的技能，又要努力避免重写技能代码的情况，就需要组合多种效果
    /// 所以通常一个技能下面需要挂载多种特定的表现效果。多个效果之间可能还会有前后序影响
    /// 于是用序列结构加以管理 链条
    /// 另外一种方法叫技能树,使用树形结构控制技能的执行流程
    /// 技能树的重点并不是根据上下文选择一个合适的节点执行，而是以一定的策略将技能树从头到尾遍历执行一遍。
    /// 操作
    /// 1.改变人物属性
    /// 2.改变人数数值
    /// 3.改变人物状态
    /// 
    /// 我们把技能可能产生的所有具体效果（比如改变属性值、转换仇恨目标等各种特殊操作）都归入到”操作“，
    /// 一个效果就相当于一个特定的程序功能接口。这样，我们可以按照 ”技能 => 效果 => 操作列表“ 的方式组合出各种各样的表现。
    /// 一些操作根据表现需要可能需要设置不同的参数，多的话可以考虑作单独配置（如设置眩晕的持续时间、导弹的追踪距离、跳跃的距离等）。
    /// 这些具体的表现效果，和作为入口的技能一样，最终也会折回到效果流程来完成自己特立独行的操作。
    /// 
    /// 
    /// 数据
    /// 1、技能信息：描述技能名、技能介绍、有效距离、播放动作、技能效果等信息
    /// 2、效果信息：描述一个具名效果的执行操作列表、操作的作用目标等
    /// 3、各个具体类型的效果信息：对导弹、连锁、状态、吟唱、地点之类的效果针对性进行描述其特征以及产生的效果（返回到第2点）
    /// 
    /// 流程
    /// 执行一个技能的过程类似跑一个函数，技能名相当于函数名，技能效果相当于函数体
    /// 论灵活肯定是程序最灵活，一旦存在配死的东西，就产生无法实现的功能。
    /// 而程序的核心在于控制流，即顺序、分支、循环，为了亲爱的灵活性，我们最好把它暴露给外部文件。
    /// 目前先只考虑顺序序列化
    /// 
    /// 
    /// 技能的作用目标
    /// 1.目标是地点/方向性的技能，这类技能通常是根据鼠标提供的位置施放的
    /// 施放这种技能时并不能从施法请求中得到目标对象。这种情况应该从请求中获取位置信息并沿着调用栈传递上去。
    /// 2. 战斗2.0（非锁定目标攻击）即不管是否选择了目标都可以进行攻击，击中了哪些目标是根据实时位置关系计算得到的
    ///     帧判断也行简化版本
    /// 3. 一种则是比较传统的锁定目标攻击，即许多技能需要先选中一个对象才能使用
    /// 通过Pipe思路来查找目标(摄像头的设计思路也是这样,当然还有排他性)
    /// 一组Pipe执行过程中，上一个Pipe产生的结果给到下一个Pipe，最后一个Pipe输出的结果即为最终结果
    /// 我们先实现许多个不同规则的Pipe（如”周围，范围参数“，”队员“，”处于某状态“等），然后通过组合这些Pipe来实现较为复杂的目标筛选。
    /// 
    /// 总体设计思路
    /// 技能表[技能名] => 技能参数信息
    /// 效果表[效果名] => 操作（条件作用目标、操作名、参数）列表
    /// 操作表[操作名] => 操作的具体实现代码
    /// </summary>


    /// <summary>
    /// 状态链
    /// TODO 缺少一个黑箱数据，可以做到从哪里塞数据，也可以拿数据，通过String,Object 这样的形式第一参考目标是行为树的黑箱
    /// 一个技能的整体行为,是否可以由多条线组合而成？
    /// TODO 修改为节点形式,以方便之后更好的扩展，同事也方便做节点之间的连接，以及规划，类似技能行为树
    /// 
    /// TODO
    ///     Bug 播放一个技能，反复播放，在技能释放掉技能控制的时候立马播放重复的一个技能，可能会由于动画文件的缘故导致bug
    /// </summary>
    public class SkillSequence
    {
        #region 属性

        public SkillNode _cur_node;                                                     // 当前节点
        public int _next_index;                                                         // 下标
        public List<SkillNode> _childnodes = new List<SkillNode>(16);                   // 子节点
        public bool _is_complete;                                                       // 是否结束序列节点
        public string des = string.Empty;                                               // 文本说明
        public SkillContainer _skill_container;                                         // 属于哪一个容器

        #endregion

        #region 构造

        public SkillSequence(SkillContainer container)
        {
            _skill_container = container;
            _next_index = 0;
            _is_complete = false;
            _cur_node = null;
        }

        #endregion

        #region public

        public void OnStart()
        {
            SkillLog.Log("Time:{0}-----------------------------序列开始[{1}]-----------------------------", TimeModule.FrameCount, des);
            _reset_sequence();
            DoActionNext();
            ReceiveWithInEvent(E_SkillTransition.START);
        }

        public void OnFinish()
        {
            _cur_node = null;
            _next_index = _childnodes.Count;
            _skill_container.OnFinish();
            SkillLog.Log("Time:{0}-----------------------------序列结束[{1}]-----------------------------", TimeModule.FrameCount, des);
        }

        public void OnUpdate(float dt)
        {
            if (_cur_node == null) return;
            _cur_node.OnUpdate(dt);
        }

        // 接受外部事件
        public void ReceiveWithOutEvent(E_SkillTransition node_event)
        {
            SkillLog.Log("Time: {0} -----------------触发外部:[{1}]事件-----------------", TimeModule.FrameCount, node_event);
            _receive_event(node_event);
        }

        // 接受内部事件
        public void ReceiveWithInEvent(E_SkillTransition node_event)
        {
            SkillLog.Log("Time: {0} -----------------触发内部:[{1}]事件-----------------", TimeModule.FrameCount, node_event);
            _receive_event(node_event);
        }

        // 被内部序列节点调用
        public bool DoActionNext()
        {
            return _do_action_next();
        }

        // 添加节点
        public void AddNode(SkillNode state)
        {
            if (_childnodes.Contains(state))
            {
                LogManager.Error("技能链条已经包含了这个序列节点了");
                return;
            }
            _childnodes.Add(state);
            state.SetParent(this);
        }

        public EntityBlackBoard GetBlackboard()
        {
            return _skill_container.GetBlackboard();
        }

        #endregion

        #region private 

        public void _receive_event(E_SkillTransition node_event)
        {
            if (_cur_node == null) return;
            _cur_node.ReceiveTransitionEvent(node_event);
        }

        // 执行下一个动作
        public bool _do_action_next()
        {
            // 1.没有下一个动作了
            if (_next_index >= _childnodes.Count)
            {
                // 设置完成状态，并且退出
                _is_complete = true;
                OnFinish();
                return false;
            }

            _cur_node = _childnodes[_next_index];

            _next_index++;
            if (_cur_node != null)
                _cur_node.OnStart();
            return true;
        }

        public void _reset_sequence()
        {
            _cur_node = null;
            _next_index = 0;
            _is_complete = false;
            int length = _childnodes.Count;
            for (int i = 0; i < length; i++)
            {
                _childnodes[i].Reset();
            }
        }

        #endregion
    }
}

