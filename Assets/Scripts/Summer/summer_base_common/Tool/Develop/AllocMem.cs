using UnityEngine;
using System.Collections;
using System.Text;

/// <summary>
/// AllocMem是一个简单的辅助工具，用于显示您的应用程序分配多少内存。它采用GC.GetTotalMemory来跟踪内存使用
/// Currently allocated(当前分配):显示GC分配的总内存
/// Peak allocated(峰值)：显示了内存分配，（）内的值是GC最后一次应用程序运行期间分配的最大内存
/// Allocation rate(分配率)：显示了应用程序分配内存(以mb为单位)，比如 0.3秒MB内存分配，在这个时候我应该修复。
/// Allocation rate（收集次数/频率）：显示相距多远GC的集合间隔（秒）
/// Last collect delta（最后收集）：显示帧率有多高，当GC上次调用，调用GC通常使帧率下降。
/// </summary>
[ExecuteInEditMode()]////使这个脚本在编辑模式下运行
public class AllocMem : MonoBehaviour
{

    public bool show = true;
    public bool showFPS = false;
    public bool showInEditor = false;
    public void Start()
    {
        useGUILayout = false;
    }

    // Use this for initialization
    public void OnGUI()
    {
        if (!show || (!Application.isPlaying && !showInEditor))
        {
            return;
        }

        int collCount = System.GC.CollectionCount(0);

        if (lastCollectNum != collCount)
        {
            lastCollectNum = collCount;
            delta = Time.realtimeSinceStartup - lastCollect;
            lastCollect = Time.realtimeSinceStartup;
            lastDeltaTime = Time.deltaTime;
            collectAlloc = allocMem;
            last_gc_frame = Time.frameCount;
        }

        allocMem = (int)System.GC.GetTotalMemory(false);

        peakAlloc = allocMem > peakAlloc ? allocMem : peakAlloc;

        if (Time.realtimeSinceStartup - lastAllocSet > 0.3F)
        {
            int diff = allocMem - lastAllocMemory;
            lastAllocMemory = allocMem;
            lastAllocSet = Time.realtimeSinceStartup;

            if (diff >= 0)
            {
                allocRate = diff;
            }
        }

        StringBuilder text = new StringBuilder();

        text.Append("Currently allocated            ");
        text.Append((allocMem / 1000000F).ToString("0"));
        text.Append("mb\n");

        text.Append("Peak allocated                ");
        text.Append((peakAlloc / 1000000F).ToString("0"));
        text.Append("mb (last    collect ");
        text.Append((collectAlloc / 1000000F).ToString("0"));
        text.Append(" mb)\n");


        text.Append("Allocation rate                ");
        text.Append((allocRate / 1000000F).ToString("0.0"));
        text.Append("mb\n");

        text.Append("Collection frequency        ");
        text.Append(delta.ToString("0.00"));
        text.Append("s\n");

        text.Append("Last collect delta            ");
        text.Append(lastDeltaTime.ToString("0.000"));
        text.Append("s (");
        text.Append((1F / lastDeltaTime).ToString("0.0"));

        text.Append("最近收集的一帧:            ");
        text.Append(last_gc_frame.ToString("0.000"));
       

        if (showFPS)
        {
            text.Append("\n" + (1F / Time.deltaTime).ToString("0.0") + " fps");
        }

        GUI.Box(new Rect(5, 5, 310, 80 + (showFPS ? 16 : 0)), "");
        GUI.Label(new Rect(10, 5, 1000, 200), text.ToString());
        /*GUI.Label (new Rect (5,5,1000,200),
            "Currently allocated            "+(allocMem/1000000F).ToString ("0")+"mb\n"+
            "Peak allocated                "+(peakAlloc/1000000F).ToString ("0")+"mb "+
            ("(last    collect"+(collectAlloc/1000000F).ToString ("0")+" mb)" : "")+"\n"+
            "Allocation rate                "+(allocRate/1000000F).ToString ("0.0")+"mb\n"+
            "Collection space            "+delta.ToString ("0.00")+"s\n"+
            "Last collect delta            "+lastDeltaTime.ToString ("0.000") + " ("+(1F/lastDeltaTime).ToString ("0.0")+")");*/
    }

    private float lastCollect = 0;
    private float lastCollectNum = 0;
    private float delta = 0;
    private float lastDeltaTime = 0;
    private int allocRate = 0;
    private int lastAllocMemory = 0;
    private float lastAllocSet = -9999;
    private int allocMem = 0;
    private int collectAlloc = 0;
    private int peakAlloc = 0;


    private float last_gc_frame = 0;

}