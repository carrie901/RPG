
namespace Summer
{
    public interface I_CameraPipeline
    {
        void OnEnable();
        void OnDisable();
        void Process(CameraPipelineData data, float dt);
    }
}