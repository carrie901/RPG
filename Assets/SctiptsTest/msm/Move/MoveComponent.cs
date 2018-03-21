using UnityEngine;
using System.Collections.Generic;

public class MoveComponent : MonoBehaviour
{

    #region 字段

    public float max_speed = 10;                                                                // 最大速度
    public float max_force = 100;                                                               // 最大推力
    public float mass = 1;                                                                      // 对象质量
    public Vector3 velocity;                                                                    // 速度向量
    public float damping = 0.9f;                                                                // 旋转速率
    public float compute_inteval = 0.2f;                                                        // 计算间隔
    public float complete_disatnce = 3f;                                                        // 到达距离
    private Vector3 acceleration;                                                               // 加速度
    private float timer = 0;                                                                    // 计时器
    protected Vector3 steering_force = Vector3.zero;                                            // 推力
    protected readonly Dictionary<MoveType, MoveBase> move_dic                                  // 移动字典
        = new Dictionary<MoveType, MoveBase>();
    public List<MoveType> arrive_remove = new List<MoveType>();                                 // 移动完成需要删除的移动

    #endregion

    #region MONO

    void Awake()
    {
        move_dic[MoveType.Seek] = null;
        move_dic[MoveType.Arrive] = null;
        move_dic[MoveType.Flee] = null;
        move_dic[MoveType.FollowPath] = null;
        move_dic[MoveType.Pursuit] = null;
        move_dic[MoveType.CollisionAvoidance] = null;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > compute_inteval)
        {
            //得到当前帧的推力
            steering_force = Vector3.zero;
            //遍历当前激活的移动并添加推力*权重
            foreach (var v in move_dic)
            {
                MoveBase move_base = v.Value;
                if (move_base != null)
                {
                    steering_force += move_base.Force() * move_base.weight;
                }
            }

            //移除完成的移动
            foreach (var v in arrive_remove)
            {
                move_dic[v] = null;
            }
            arrive_remove.Clear();

            //限制最大力
            steering_force = Vector3.ClampMagnitude(steering_force, max_force);
            //加速度=推力/质量
            acceleration = steering_force / mass;

            timer = 0;
        }
    }

    void FixedUpdate()
    {
        //增加加速度
        velocity += acceleration * Time.fixedDeltaTime;

        //控制最大速度
        if (velocity.sqrMagnitude > max_speed * max_speed)
            velocity = velocity.normalized * max_speed;

        //移动
        transform.position += velocity * Time.fixedDeltaTime;

        //插值旋转
        if (velocity.sqrMagnitude > 0.01f)
        {
            //计算当前前方到目标方向的插值
            Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.fixedDeltaTime);
            transform.forward = newForward;
        }

    }

    #endregion

    #region 接口


    public void SeekTo(Transform target)
    {
        if (move_dic == null) return;
        if (move_dic[MoveType.Seek] != null)
        {
            if ((move_dic[MoveType.Seek] as MoveSeek).target == target)
                return;
        }
        move_dic[MoveType.Seek] = new MoveSeek(this, target);
    }

    public void ArriveTo(Transform target, float slowDistance)
    {
        if (move_dic[MoveType.Arrive] != null)
        {
            if ((move_dic[MoveType.Arrive] as MoveArrive).target == target)
                return;
        }
        move_dic[MoveType.Arrive] = new MoveArrive(this, target, slowDistance);
    }

    public void FleeTo(Transform target, float fearDistance)
    {
        if (move_dic[MoveType.Flee] != null)
        {
            if ((move_dic[MoveType.Flee] as MoveFlee).target == target)
                return;
        }
        move_dic[MoveType.Flee] = new MoveFlee(this, target, fearDistance);
    }

    public void PursuitTo(Transform target)
    {
        if (move_dic[MoveType.Pursuit] != null)
        {
            if ((move_dic[MoveType.Pursuit] as MovePursuit).target == target)
                return;
        }
        move_dic[MoveType.Pursuit] = new MovePursuit(this, target);
    }

    public void FollowPath(List<Transform> path)
    {
        if (move_dic[MoveType.FollowPath] != null)
        {
            if ((move_dic[MoveType.FollowPath] as MoveFllowPath).path == path)
                return;
        }
        move_dic[MoveType.FollowPath] = new MoveFllowPath(this, path);
    }

    public void OpenCollisionAvoidance(float seeAheadDistance)
    {
        move_dic[MoveType.CollisionAvoidance] = new MoveCollisionAvoidance(this, seeAheadDistance);
    }

    #endregion

}

public enum MoveType
{
    Seek,
    Arrive,
    Flee,
    Pursuit,
    FollowPath,
    CollisionAvoidance
}

#region 移动基础类

public class MoveBase
{

    public float weight = 1;                    //移动优先级
    protected Vector3 disired_velocity;         //目标速度
    protected MoveComponent move;               //移动脚本

    public MoveBase(MoveComponent move)
    {
        this.move = move;
    }

    public virtual Vector3 Force()
    {
        return Vector3.zero;
    }


}

#endregion

#region 靠近

public class MoveSeek : MoveBase
{

    public Transform target;

    public MoveSeek(MoveComponent move, Transform target) : base(move) { this.target = target; }

    public override Vector3 Force()
    {
        //完成目标移除
        if (Vector3.Distance(target.transform.position, move.transform.position) < move.complete_disatnce)
        {
            move.arrive_remove.Add(MoveType.Seek);
            move.velocity = Vector3.zero;
            return Vector3.zero;
        }
        //要到达的速度
        disired_velocity = (target.transform.position - move.transform.position).normalized * move.max_speed;
        //加速度：当速度=要到达的速度，加速度为0
        return disired_velocity - move.velocity;
    }

}

#endregion

#region 缓速到达


public class MoveArrive : MoveBase
{

    public Transform target;
    //减速范围
    float slowDistance;

    public MoveArrive(MoveComponent move, Transform target, float slowDistance) : base(move) { this.target = target; this.slowDistance = slowDistance; }

    public override Vector3 Force()
    {
        if (Vector3.Distance(target.transform.position, move.transform.position) > move.complete_disatnce)
        {
            Vector3 to_target = target.transform.position - move.transform.position;
            Vector3 return_force;

            float distance = to_target.magnitude;

            if (distance > slowDistance)
            {
                //目标速度
                disired_velocity = to_target * move.max_speed;
                //加速度：当速度=要到达的速度，加速度为0
                return_force = disired_velocity - move.velocity;
            }
            else
            {
                //当进入减速范围时 目标速度 = 长度向量-速度向量 逐渐缩小
                disired_velocity = to_target - move.velocity;
                return_force = disired_velocity - move.velocity;
            }
            return return_force;
        }
        else
        {
            //完成目标移除
            move.arrive_remove.Add(MoveType.Arrive);
            move.velocity = Vector3.zero;
            return Vector3.zero;
        }

    }

}

#endregion

#region 逃离


public class MoveFlee : MoveBase
{

    public Transform target;
    float fearDistance;

    public MoveFlee(MoveComponent move, Transform target, float fearDistance) : base(move) { this.target = target; this.fearDistance = fearDistance; }

    public override Vector3 Force()
    {
        if (Vector3.Distance(move.transform.position, target.position) > fearDistance)
        {
            //完成目标移除
            move.arrive_remove.Add(MoveType.Flee);
            move.velocity = Vector3.zero;
            return Vector3.zero;
        }
        //反方向加速度向量
        disired_velocity = (move.transform.position - target.position).normalized * move.max_speed;
        return disired_velocity - move.velocity;
    }

}


#endregion

#region 路径移动

public class MoveFllowPath : MoveBase
{

    public List<Transform> path = new List<Transform>();
    private float slowDistance = 5f;
    private Transform target;
    private int index = 0;

    public MoveFllowPath(MoveComponent move, List<Transform> path) : base(move) { this.path = path; target = path[index]; }

    public override Vector3 Force()
    {
        Vector3 returnForce = Vector3.zero;
        Vector3 distance = target.position - move.transform.position;
        //最后一点
        if (index == path.Count - 1)
        {
            if (Vector3.Distance(target.position, move.transform.position) < move.complete_disatnce)
            {
                //完成目标移除
                move.arrive_remove.Add(MoveType.FollowPath);
                move.velocity = Vector3.zero;
                return Vector3.zero;
            }

            if (distance.magnitude > slowDistance)
            {
                disired_velocity = distance.normalized * move.max_speed;
            }
            else
            {
                disired_velocity = distance - move.velocity;
            }
            returnForce = disired_velocity - move.velocity;
        }
        else
        {
            if (Vector3.Distance(target.position, move.transform.position) < move.complete_disatnce)
            {
                index++;
                target = path[index].transform;
            }
            disired_velocity = distance.normalized * move.max_speed;
            returnForce = disired_velocity - move.velocity;
        }
        return returnForce;
    }

}

#endregion

#region 拦截

public class MovePursuit : MoveBase
{
    public Transform target;

    public MovePursuit(MoveComponent move, Transform target) : base(move) { this.target = target; }

    public override Vector3 Force()
    {
        if (target.GetComponent<MoveComponent>() == null)
        {
            move.arrive_remove.Add(MoveType.Pursuit);
            return Vector3.zero;
        }

        if (Vector3.Distance(target.transform.position, move.transform.position) > move.complete_disatnce)
        {
            Vector3 toTarget = target.transform.position - move.transform.position;
            //两个对象的前方向量夹角
            float relativeDirection = Vector3.Dot(move.transform.forward, target.transform.forward);
            //追踪向量和对象前方向量的夹角>0并且两个对象前方夹角<18
            if (Vector3.Dot(toTarget, move.transform.forward) > 0 && relativeDirection < -0.95f)
            {
                //差不多在一直线上
                disired_velocity = (target.transform.position - move.transform.position).normalized * move.max_speed;
                return disired_velocity - move.velocity;
            }

            //预期到达目标的前方位置的时间
            float lookheadTime = toTarget.magnitude / (move.max_speed + target.GetComponent<MoveComponent>().velocity.magnitude);

            //预期目标位置 = 目标位置 + 目标速度*预期到达时间
            disired_velocity = (target.transform.position + target.GetComponent<MoveComponent>().velocity * lookheadTime - move.transform.position).normalized * move.max_speed;

            return disired_velocity - move.velocity;
        }
        else
        {
            //完成目标移除
            move.arrive_remove.Add(MoveType.Pursuit);
            move.velocity = Vector3.zero;
            return Vector3.zero;
        }

    }

}

#endregion

#region 躲避障碍

public class MoveCollisionAvoidance : MoveBase
{

    private float maxSeeAhead = 2.0f;

    public MoveCollisionAvoidance(MoveComponent move, float maxSeeAhead) : base(move) { this.maxSeeAhead = maxSeeAhead; }


    public override Vector3 Force()
    {

        RaycastHit hit;
        Vector3 returnForce = Vector3.zero;

        //方向：速度向量 距离：视线 * 时间
        if (Physics.Raycast(move.transform.position, move.velocity, out hit, maxSeeAhead * move.velocity.magnitude / move.max_speed))
        {
            //发生碰撞的视线前方向量
            Vector3 ahead = move.transform.position + move.velocity * maxSeeAhead * (move.velocity.magnitude / move.max_speed);
            //用视线向量-碰撞物体的中心点 得到 碰撞物体中心指向视线向量的向量 用这个向量来偏移
            returnForce = ahead - hit.collider.transform.position;
            returnForce *= move.max_force;
            returnForce.y = 0;

        }

        return returnForce;

    }
}

#endregion