
using UnityEngine;
namespace Summer
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class MesShanXing : MonoBehaviour
    {
        public float _radius;
        public float _angle;
        public int _segments;
        public MeshFilter _meshFilter;

        private void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = CreateMesh(_radius, _angle, _segments);
        }

        public Mesh CreateMesh(float radius, float angle, int segments)
        {
            int verticesCount = segments * 2 + 2;
            Vector3[] vertices = new Vector3[verticesCount];
            float angleRad = Mathf.Deg2Rad * angle;
            float angleCur = angleRad;
            float angledelta = angleRad / segments;

            vertices[0] = new Vector3(0, 0, 0);
            for (int i = 1; i < verticesCount; i++)
            {
                float cosA = Mathf.Cos(angleCur);
                float sinA = Mathf.Sin(angleCur);

                vertices[i] = new Vector3(radius * cosA, 0, radius * sinA);
                angleCur -= angledelta;
            }

            int triangleCount = segments * 3;
            int[] triangles = new int[triangleCount];
            for (int i = 0, vi = 0; i < triangleCount; i += 3, vi++)
            {
                triangles[i] = 0;
                triangles[i + 1] = vi + 1;
                triangles[i + 2] = vi + 2;
            }
            //负载属性与mesh
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            
            //mesh.uv = uvs;
            return mesh;
        }
    }
}
