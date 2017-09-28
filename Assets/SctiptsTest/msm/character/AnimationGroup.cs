using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGroup : MonoBehaviour
{

    public enum ClipType
    {
        clip_attack1 = 0,
        clip_attack2,
        clip_attack_summon,
        clip_damage,
        clip_dash1,
        clip_death1,
        clip_idle1,
        clip_move1,
        clip_stun1,
        
    };

    public AnimationClip[] clips;
    public string GetClipName(ClipType clip_type)
    {
        string anim_name = string.Empty;
        int index = (int)clip_type;
        if (index < clips.Length && index >= 0 && clips[index] != null)
            anim_name = clips[index].name;
        return anim_name;
    }
}
