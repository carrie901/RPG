
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
using UnityEngine;

public class VehicleAvoidance : MonoBehaviour
{

    public float speed = 20.0f;
    public float mass = 5.0f;
    public float force = 50.0f;
    public float minimum_dist_to_avoid = 20.0f;

    //Actual speed of the vehicle 
    private float cur_speed;
    private Vector3 target_point;

    // Use this for initialization
    void Start()
    {
        mass = 5.0f;
        target_point = Vector3.zero;
    }

    void OnGUI()
    {
        GUILayout.Label("Click anywhere to move the vehicle to the clicked point");
    }

    [Range(0, 1)]
    public float time_scale = 1;
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = time_scale;
        //Vehicle move by mouse click
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool result = Input.GetMouseButtonDown(0);
        if (result && Physics.Raycast(ray, out hit, 100.0f))
        {
            target_point = hit.point;
        }

        //Directional vector to the target position
        Vector3 dir = (target_point - transform.position);
        dir.y = 0;
        dir.Normalize();

        //Apply obstacle avoidance
        AvoidObstacles(ref dir);

        //Debug.DrawRay(transform.position, dir*10, Color.red);

        //Don't move the vehicle when the target point is reached
        if (Vector3.Distance(target_point, transform.position) < 3.0f)
            return;

        //Assign the speed with delta time
        cur_speed = speed * Time.deltaTime;

        //Rotate the vehicle to its target directional vector
        //var rot = Quaternion.LookRotation(dir);
        //transform.rotation = rot; // Quaternion.Slerp(transform.rotation, rot, 5.0f * Time.deltaTime);
        transform.eulerAngles = dir;
        //Move the vehicle towards
        //transform.position += transform.forward * cur_speed;



        transform.position += speed * Time.deltaTime * dir;
    }

    //Calculate the new directional vector to avoid the obstacle
    public void AvoidObstacles(ref Vector3 dir)
    {
        RaycastHit hit;

        //Only detect layer 8 (Obstacles)
        int layerMask = 1 << 8;


        Debug.DrawRay(transform.position, dir * 5, Color.red);
        Debug.DrawRay(transform.position, transform.forward * minimum_dist_to_avoid, Color.yellow);
        //Check that the vehicle hit with the obstacles within it's minimum distance to avoid
        if (Physics.Raycast(transform.position, transform.forward, out hit, minimum_dist_to_avoid, layerMask))
        {
            //Get the normal of the hit point to calculate the new direction
            Vector3 hitNormal = hit.normal;


            Vector3 left = Vector3.Cross(Vector3.up, hitNormal);

            hitNormal.y = 0.0f; //Don't want to move in Y-Space
            Debug.DrawRay(hit.transform.position, hit.normal * 10, Color.white);

            Debug.DrawRay(hit.transform.position, left * 10, Color.black);

            //Get the new directional vector by adding force to vehicle's current forward vector
            dir = transform.forward + hitNormal * force;
            dir = left;


        }

    }

}
