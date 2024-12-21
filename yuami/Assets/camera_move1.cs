using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move1 : MonoBehaviour
{
    Vector3 cam_to_pl;
    GameObject player;
    GameObject manager;
    [SerializeField]float distance = 10;
    [SerializeField]float sensitivity = 0.001f;
    GameObject[] enemies;
    List<GameObject> selected_enemies = new List<GameObject>();
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        manager = GameObject.Find("camera_manager");
    }

    // Update is called once per frame
    void Update()
    {
        cam_to_pl = player.transform.position - this.transform.position;
        cam_to_pl = cam_to_pl.normalized;
        Vector3 axis_x = this.transform.right * Input.GetAxis("Mouse X");
        Vector3 axis_y = this.transform.up * Input.GetAxis("Mouse Y");
        Vector3 cam_moved = cam_to_pl + (axis_x + axis_y) * sensitivity;
        Vector3 cam_vec = cam_moved.normalized * distance;
        this.transform.position -= (this.transform.position - new Vector3(manager.transform.position.x, Mathf.Max(manager.transform.position.y, 0), manager.transform.position.z)) / 15;
        this.transform.rotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        target = null;
        selected_enemies = new List<GameObject>();
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject e in enemies)
        {
            Debug.Log(Vector3.Angle(cam_to_pl,e.transform.position - this.transform.position));
            if(Vector3.Angle(cam_to_pl,e.transform.position - this.transform.position) < 17)
            {
                Debug.Log("add sinasaiyo!");
                selected_enemies.Add(e);
                Debug.Log(selected_enemies);
            }
        }
        foreach(GameObject e in selected_enemies)
        {
            float min_dist = 60;
            if ((this.transform.position - e.transform.position).magnitude < min_dist)
            {
                min_dist = (this.transform.position - e.transform.position).magnitude;
                target = e;
            }
        }
    }
}
