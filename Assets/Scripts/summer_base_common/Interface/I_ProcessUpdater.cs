using UnityEngine;
using System.Collections;


public enum E_PROCESS_TYPE
{
    none = 0,
    spell,
    buff,

    max,
}

public interface I_ProcessUpdater
{
    void Destroy();
    bool IsActive();

    /// <summary>
    /// 当前进度
    /// </summary>
    /// <param name="elapse_time"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    bool GetProcess(out float elapse_time, out float duration);
}