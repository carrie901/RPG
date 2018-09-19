
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Summer
{
    [RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
    public class MeshDrawCall : MonoBehaviour
    {

        #region 属性

        public const int IMAGE_MAX = 50;
        public Vector3[] _vertices = new Vector3[4 * IMAGE_MAX];
        public Vector2[] _uvs = new Vector2[4 * IMAGE_MAX];
        public Color[] _colors = new Color[4 * IMAGE_MAX];
        public int[] _triangles = new int[6 * IMAGE_MAX];
        public bool[] _indexs = new bool[IMAGE_MAX];

        public int DefaultWh = 1024;
        public Texture2D _combineTex;
        public Dictionary<string, Texture2D> _texMap
            = new Dictionary<string, Texture2D>();                                          // 原始的纹理，在Combine之后会清空
        public Dictionary<string, CombineTextureInfo> _texInfoMap
            = new Dictionary<string, CombineTextureInfo>();                                 // 在纹理Combine之后，记录纹理的相关数据

        public List<MeshRectInfo> _widgets = new List<MeshRectInfo>();

        public int SortingOrder;                                                            // 深度 目前还用不到
        public Material _materila;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private Mesh _mesh;
        #endregion

        #region MONO Override

        void Awake()
        {
            BaseMesh();
        }

        private static int _updateFrame = -1;
        // Update is called once per frame
        void LateUpdate()
        {
            UpdateSelf();
        }

        void UpdateSelf()
        {
            // ReSharper disable once RedundantCheckBeforeAssignment
            if (_updateFrame == Time.frameCount) return;

            _updateFrame = Time.frameCount;
            int length = _widgets.Count;

            for (int i = 0; i < length; i++)
            {
                MeshRectInfo info = _widgets[i];
                if (info.PosReBuild)
                {
                    info.PosReBuild = false;
                    UpdateGameObject(info.Id);
                }
            }
        }

        #endregion

        #region Public

        public void AddTex(Texture2D tex)
        {
            if (_texMap.ContainsKey(tex.name)) return;
            _texMap.Add(tex.name, tex);
        }

        public void CombineTex()
        {
            // 合并子纹理
            _combineTex = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
            _combineTex.alphaIsTransparency = true;
            _combineTex.filterMode = FilterMode.Bilinear;
            //_combineTex.dimension = TextureDimension.Tex2D;

            Texture2D[] megreTexs = _texMap.Values.ToArray();
            _texMap.Clear();

            Rect[] uvs = _combineTex.PackTextures(megreTexs, 50);
            // 生成CombineTextureInfo信息
            _texInfoMap.Clear();
            int length = megreTexs.Length;
            for (int i = 0; i < length; i++)
            {
                CombineTextureInfo info = new CombineTextureInfo { TexName = megreTexs[i].name };
                info.Reset(uvs[i]);
                _texInfoMap.Add(info.TexName, info);
            }
            // 销毁老的纹理,并且清空_texMap
            for (int i = length - 1; i >= 0; i--)
            {
                Texture2D tex = megreTexs[i];
                megreTexs[i] = null;
                Resources.UnloadAsset(tex);
            }
            megreTexs = null;
            _materila.SetTexture("_MainTex", _combineTex);
        }

        public void AddGameObject(MeshImage img)
        {
            if (!img.gameObject.activeSelf || !img.enabled) return;
            int index = GetUnUseIndex();
            // 安全检测，是否已经添加
            // 从数据列表中拿出一个安全的Index，Index=true,并且拿到Vertices/Uv/Tri/Color设置值
            img._info.Index = index;
            UpdateIndex(img._info.Index, img._info.TexName, img._info.Pos, img._info.Size);
            _widgets.Add(img._info);
        }

        public void RemoveGameObject(string texName)
        {
            // 安全监测，数据更新等等
            //RemoveIndex
        }

        public void UpdateGameObject(int insId)
        {
            int length = _widgets.Count;
            bool reBuild = false;
            for (int i = 0; i < length; i++)
            {
                if (_widgets[i].Id == insId)
                {
                    MeshRectInfo info = _widgets[i];
                    UpdateIndex(info.Index, info.TexName, info.Pos, info.Size);
                    reBuild = true;
                }
            }
            if (reBuild)
            {
                RebuildDrawCall();
            }
        }


        public void RebuildDrawCall()
        {
            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.uv = _uvs;
            _mesh.triangles = _triangles;
            _mesh.MarkDynamic();
        }

        #endregion

        #region Private Methods

        private int GetUnUseIndex()
        {
            int length = _indexs.Length;
            for (int i = 0; i < length; i++)
            {
                if (!_indexs[i])
                    return i;
            }
            Debug.LogError("已经没有可以用的Index了");
            return -1;
        }

        private void UpdateIndex(int index, string texName, Vector3 pos, Vector2 size)
        {
            // 对应的安全检测
            if (!_texInfoMap.ContainsKey(texName)) return;
            CombineTextureInfo comInfo = _texInfoMap[texName];
            float halfX = size.x / 2;
            float halfY = size.y / 2;

            int startIndex = index;
            _indexs[startIndex] = true;

            // 顶点坐标
            startIndex = index * 4;
            _vertices[startIndex] = new Vector3(pos.x - halfX, pos.y + halfY);
            _vertices[startIndex + 1] = new Vector3(pos.x - halfX, pos.y - halfY);
            _vertices[startIndex + 2] = new Vector3(pos.x + halfX, pos.y - halfY);
            _vertices[startIndex + 3] = new Vector3(pos.x + halfX, pos.y + halfY);

            // uv坐标
            startIndex = index * 4;
            _uvs[startIndex] = comInfo.Uv0;
            _uvs[startIndex + 1] = comInfo.Uv1;
            _uvs[startIndex + 2] = comInfo.Uv2;
            _uvs[startIndex + 3] = comInfo.Uv3;

            int uvIndex = startIndex;
            // 三角形1
            startIndex = index * 6;
            _triangles[startIndex] = uvIndex + 2;
            _triangles[startIndex + 1] = uvIndex + 1;
            _triangles[startIndex + 2] = uvIndex + 0;
            _triangles[startIndex + 3] = uvIndex + 0;
            _triangles[startIndex + 4] = uvIndex + 3;
            _triangles[startIndex + 5] = uvIndex + 2;

            // 顶点颜色
            startIndex = index * 4;
            _colors[startIndex] = Color.white;
            _colors[startIndex + 1] = Color.white;
            _colors[startIndex + 2] = Color.white;
            _colors[startIndex + 3] = Color.white;
        }

        private void RemoveIndex(int index)
        {
            int startIndex = index;
            _indexs[startIndex] = false;
            _vertices[startIndex] = new Vector3(-1000f, -1000f, -1000f);
            _vertices[startIndex + 1] = new Vector3(-1000f, -1000f, -1000f);
            _vertices[startIndex + 2] = new Vector3(-1000f, -1000f, -1000f);
            _vertices[startIndex + 3] = new Vector3(-1000f, -1000f, -1000f);
        }

        private void BaseMesh()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _mesh = new Mesh();
            _meshFilter.mesh = _mesh;
            _meshRenderer.material = _materila;
            _meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            _meshRenderer.lightProbeUsage = LightProbeUsage.Off;
            _meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
            _meshRenderer.receiveShadows = false;
        }

        #endregion
    }


    public struct CombineTextureInfo
    {
        public string TexName;
        public Vector2 Uv0;
        public Vector2 Uv1;
        public Vector2 Uv2;
        public Vector2 Uv3;

        public void Reset(Rect rect)
        {
            //Debug.Log("TexName:" + TexName + "_" + rect.ToString());

            Uv0 = new Vector2(rect.x, (rect.y + rect.height));
            Uv1 = new Vector2(rect.x, rect.y);
            Uv2 = new Vector2(rect.x + rect.width, rect.y);
            Uv3 = new Vector2(rect.x + rect.width, (rect.y + rect.height));
        }
    }

    public class MeshRectInfo
    {
        public Vector2 Pos { get; set; }
        public Vector2 Size { get; set; }
        public int Index { get; set; }
        public string TexName { get; set; }
        public int Id { get; set; }

        public bool Rebuild { get; set; }
        public bool PosReBuild { get; set; }
    }
}

