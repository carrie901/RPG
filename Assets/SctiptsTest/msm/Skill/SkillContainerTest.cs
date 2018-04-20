using Summer;
using System.Collections.Generic;
public class SkillContainerTest
{
    public static SkillContainer container;
    public static float _last_time = -1;

    public static List<E_SkillTransition> events = new List<E_SkillTransition>();
    public static SkillSequence Create()
    {
        SkillSequence skill_sequence = new SkillSequence(null);

       




        return skill_sequence;
    }

    public static void OnUpdate(float dt)
    {
        if (_last_time < 0) return;
        _last_time += dt;
        if (_last_time > 3)
        {
            _last_time = 0;
            if (events.Count > 0)
            {
                E_SkillTransition name = events[0];
                container.ReceiveTransitionEvent(name);
                events.RemoveAt(0);
            }
            else
            {
                _last_time = -1;
            }
        }
    }
}