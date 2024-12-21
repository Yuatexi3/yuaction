using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    GameObject player;
    float sensitivity = 150;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse_axis;
        mouse_axis.x = Input.GetAxis("Mouse X");
        mouse_axis.y = Input.GetAxis("Mouse Y");
        mouse_axis.z = 0;
        transform.RotateAround(player.transform.position, mouse_axis, sensitivity * Time.deltaTime);
    }
}
