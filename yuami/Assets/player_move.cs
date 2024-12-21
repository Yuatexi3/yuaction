using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    [SerializeField] GameObject missile;
    [SerializeField] GameObject beam;
    float movement_speed = 5;
    float shootable;
    [SerializeField] int mode = 0;
    GameObject camera_manager;
    public Vector3 pos_difference;
    // Start is called before the first frame update
    void Start()
    {
        camera_manager = GameObject.Find("camera_manager");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camera_to_this = this.transform.position - camera_manager.transform.position;
        Vector3 direction_to_go = new Vector3(camera_to_this.x, 0, camera_to_this.z).normalized;
        Vector3 direction_to_go_right = Quaternion.Euler(0, 90, 0) * direction_to_go;
        Vector3 past_pos = this.transform.position;
        
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += direction_to_go * Time.deltaTime * movement_speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= direction_to_go_right * Time.deltaTime * movement_speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= direction_to_go * Time.deltaTime * movement_speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += direction_to_go_right * Time.deltaTime * movement_speed;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            this.transform.position += Vector3.up * Time.deltaTime * movement_speed;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.position += Vector3.down * Time.deltaTime * movement_speed;
        }

        if(mode == 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (shootable >= 0.16f)
                {
                    shootable = 0;
                    Instantiate(missile, this.transform.position, Quaternion.identity);
                }
            }
        }

        if(mode == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(beam, this.transform.position, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            mode++;
        }

        if(mode == 2)
        {
            mode = 0;
        }

        shootable += Time.deltaTime;

        pos_difference = this.transform.position - past_pos;
    }
}
