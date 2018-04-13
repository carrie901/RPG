
using UnityEngine;
namespace Summer
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class MesShanXing : MonoBehaviour
    {
        public float _radius;
        public float _angle;
        public int _segments;
        public MeshFilter mesh_filter;

        private void Start()
        {
            mesh_filter = GetComponent<MeshFilter>();
            mesh_filter.mesh = CreateMesh(_radius, _angle, _segments);
        }

        public Mesh CreateMesh(float radius, float angle, int segments)
        {
            int vertices_count = segments * 2 + 2;
            Vector3[] vertices = new Vector3[vertices_count];
            float angle_rad = Mathf.Deg2Rad * angle;
            float angle_cur = angle_rad;
            float angledelta = angle_rad / segments;

            vertices[0] = new Vector3(0, 0, 0);
            for (int i = 1; i < vertices_count; i++)
            {
                float cos_a = Mathf.Cos(angle_cur);
                float sin_a = Mathf.Sin(angle_cur);

                vertices[i] = new Vector3(radius * cos_a, 0, radius * sin_a);
                angle_cur -= angledelta;
            }

            int triangle_count = segments * 3;
            int[] triangles = new int[triangle_count];
            for (int i = 0, vi = 0; i < triangle_count; i += 3, vi++)
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
