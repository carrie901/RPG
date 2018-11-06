using UnityEngine;


namespace Summer
{
    [ExecuteInEditMode]
    public class TCameraFollow : MonoBehaviour,I_Update
    {

        public Transform target;
        public float smoothing = 5f;
        public CameraSourceData _source_data;
        // Use this for initialization

        public bool flag;


        public void OnUpdate(float dt)
        {
            logic();
        }

        public float _last_time;
        void Update()
        // Update is called once per frame
        //void LateUpdate()
        {
            _last_time = TimeModule.RealtimeSinceStartup;
        }

        public void logic()
        {
            // Create a position the camera is aiming for based on the offset of from the target
            Vector3 target_cam_pos = target.position + _source_data._offset;

            if (flag)
            {
                // Smoothly move
                transform.position = Vector3.Lerp(transform.position, target_cam_pos, smoothing * Time.deltaTime);
            }
            else
            {
                // Smoothly move
                transform.position = target_cam_pos;
            }

            //transform.eulerAngles = _source_data._rotaion;
        }
    }
}

