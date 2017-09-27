namespace SummerEditor
{
    public static class EditorConst
    {
        public static string[] texture_exts = { ".tga", ".png", ".jpg", ".tif", ".psd", ".exr" };
        public static string[] material_exts = { ".mat" };
        public static string[] model_exts = { ".fbx", ".asset", ".obj" };
        public static string[] animation_exts = { ".anim" };
        public static string[] meta_exts = { ".meta" };
        public static string[] shader_exts = { ".shader" };
        public static string[] script_exts = { ".cs" };
        public static string[] prefab_exts = { ".prefab" };

        public static string platform_android = "Android";
        public static string platform_ios = "iPhone";
        public static string platform_standalones = "Standalones";

        public static string editor_aniclip_name = "__preview__Take 001";
        public static string[] editor_control_names = {"AnimatorStateMachine",
            "AnimatorStateTransition", "AnimatorState", "AnimatorTransition", "BlendTree" };
    }
}