using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_manager_move : MonoBehaviour
{
    Vector3 cam_to_pl;
    GameObject player;
    GameObject manager;
    [SerializeField] float distance = 10;
    [SerializeField] float sensitivity = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        manager = GameObject.Find("camera_manager");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += player.GetComponent<player_move>().pos_difference;
        cam_to_pl = player.transform.position - this.transform.position;
        cam_to_pl = cam_to_pl.normalized;
        Vector3 axis_x = this.transform.right * Input.GetAxis("Mouse X");
        Vector3 axis_y = this.transform.up * Input.GetAxis("Mouse Y");
        Vector3 cam_moved = cam_to_pl + (axis_x + axis_y) * sensitivity;
        Vector3 cam_vec = cam_moved.normalized * distance;
        this.transform.position = player.transform.position + (cam_vec * -1);
        this.transform.rotation = Quaternion.LookRotation(cam_vec);
    }
}
