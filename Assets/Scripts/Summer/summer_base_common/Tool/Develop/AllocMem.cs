using UnityEngine;
using System.Collections;
using System.Text;
using Summer;

/// <summary>
/// AllocMem是一个简单的辅助工具，用于显示您的应用程序分配多少内存。它采用GC.GetTotalMemory来跟踪内存使用
/// Currently allocated(当前分配):显示GC分配的总内存
/// Peak allocated(峰值)：显示了内存分配，（）内的值是GC最后一次应用程序运行期间分配的最大内存
/// Allocation rate(分配率)：显示了应用程序分配内存(以mb为单位)，比如 0.3秒MB内存分配，在这个时候我应该修复。
/// Allocation rate（收集次数/频率）：显示相距多远GC的集合间隔（秒）
/// Last collect delta（最后收集）：显示帧率有多高，当GC上次调用，调用GC通常使帧率下降。
/// </summary>
//[ExecuteInEditMode()]////使这个脚本在编辑模式下运行
public class AllocMem : MonoBehaviour
{

    public bool show = true;
    public bool show_fps = false;
    public bool show_in_editor = false;
    public MemoryDetector memory_detector = new MemoryDetector();
    public void Start()
    {
        useGUILayout = false;
    }
    StringBuilder text = new StringBuilder();
    public TimeInterval interval = new TimeInterval(0.1f);
    // Use this for initialization
    public void OnGUI()
    {
        if (!show || (!Application.isPlaying && !show_in_editor))
        {
            return;
        }
        if (!interval.OnUpdate())
        {
            text.Remove(0, text.Length);
            text.AppendLine(memory_detector.OnExcute());
        }
        /*int coll_count = System.GC.CollectionCount(0);

        if (last_collect_num != coll_count)
        {
            last_collect_num = coll_count;
            delta = Time.realtimeSinceStartup - last_collect;
            last_collect = Time.realtimeSinceStartup;
            last_delta_time = Time.deltaTime;
            collect_alloc = alloc_mem;
            last_gc_frame = Time.frameCount;
        }

        alloc_mem = (int)System.GC.GetTotalMemory(false);

        peak_alloc = alloc_mem > peak_alloc ? alloc_mem : peak_alloc;

        if (Time.realtimeSinceStartup - last_alloc_set > 0.3F)
        {
            int diff = alloc_mem - last_alloc_memory;
            last_alloc_memory = alloc_mem;
            last_alloc_set = Time.realtimeSinceStartup;

            if (diff >= 0)
            {
                alloc_rate = diff;
            }
        }

        */

        /*text.Append("Currently allocated            ");
        text.Append((alloc_mem / 1000000F).ToString("0"));
        text.Append("mb\n");

        text.Append("Peak allocated                ");
        text.Append((peak_alloc / 1000000F).ToString("0"));
        text.Append("mb (last    collect ");
        text.Append((collect_alloc / 1000000F).ToString("0"));
        text.Append(" mb)\n");


        text.Append("Allocation rate                ");
        text.Append((alloc_rate / 1000000F).ToString("0.0"));
        text.Append("mb\n");

        text.Append("Collection frequency        ");
        text.Append(delta.ToString("0.00"));
        text.Append("s\n");

        text.Append("Last collect delta            ");
        text.Append(last_delta_time.ToString("0.000"));
        text.Append("s (");
        text.Append((1F / last_delta_time).ToString("0.0"));

        text.Append("最近收集的一帧:            ");
        text.Append(last_gc_frame.ToString("0.000"));*/

       

        /*if (show_fps)
        {
            text.Append("\n" + (1F / Time.deltaTime).ToString("0.0") + " fps");
        }*/

        //GUI.Box(new Rect(5, 5, 310, 80 + (show_fps ? 16 : 0)), "");
        GUI.Label(new Rect(10, 5, 1000, 200), text.ToString());
        /*GUI.Label (new Rect (5,5,1000,200),
            "Currently allocated            "+(allocMem/1000000F).ToString ("0")+"mb\n"+
            "Peak allocated                "+(peakAlloc/1000000F).ToString ("0")+"mb "+
            ("(last    collect"+(collectAlloc/1000000F).ToString ("0")+" mb)" : "")+"\n"+
            "Allocation rate                "+(allocRate/1000000F).ToString ("0.0")+"mb\n"+
            "Collection space            "+delta.ToString ("0.00")+"s\n"+
            "Last collect delta            "+lastDeltaTime.ToString ("0.000") + " ("+(1F/lastDeltaTime).ToString ("0.0")+")");*/
    }

    private float last_collect = 0;
    private float last_collect_num = 0;
    private float delta = 0;
    private float last_delta_time = 0;
    private int alloc_rate = 0;
    private int last_alloc_memory = 0;
    private float last_alloc_set = -9999;
    private int alloc_mem = 0;
    private int collect_alloc = 0;
    private int peak_alloc = 0;


    private float last_gc_frame = 0;

}