using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using System.Numerics;
using UnityEngine;

public class enemy_missile_move : MonoBehaviour
{
    GameObject target;
    [SerializeField] GameObject explode_obj;
    Vector3 target_pos;
    Vector3 initial_velocity_vector;
    Vector3 missile_vector;
    Vector3 missile_past_vector;
    Vector3 missile_pos;
    Vector3 missile_past_pos;
    Vector3 homing_vector;
    public float beamed = 0;
    [SerializeField] float initial_speed = 5;
    [SerializeField] float max_speed = 20;
    [SerializeField] float acceleration_power = 12;
    [SerializeField] float detection_range = 0.8f;
    [SerializeField] float homing_index = 4;
    [SerializeField] float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player");

        transform.Rotate(0, 0, Random.Range(0f, 360f));
        speed = initial_speed;
        initial_velocity_vector = transform.right * initial_speed;
        missile_vector = initial_velocity_vector;
        missile_past_pos = this.transform.position;
        this.transform.position = missile_past_pos + initial_velocity_vector * Time.deltaTime;
        missile_pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        target_pos = target.transform.position;

        //memo: ab = ob - oa -> goal - start

        //The force that moves towards a target
        homing_vector = target.transform.position - this.transform.position;

        //The forces already at work
        missile_past_vector = missile_pos - missile_past_pos;
        missile_past_pos = this.transform.position;

        //Sum of forces
        //The variable missile_vector means the vector that the missile will move in this frame.
        missile_vector = (missile_past_vector / Time.deltaTime) * (1 - homing_index * Time.deltaTime) + homing_vector.normalized * acceleration_power * homing_index * Time.deltaTime;

        //Define speed
        speed = missile_past_vector.magnitude / Time.deltaTime;
        //speed limit
        if (speed > max_speed)
        {
            speed = max_speed;
        }
        else
        {
            speed += acceleration_power * Time.deltaTime;
        }

        //Confirm missile movement
        this.transform.position += missile_vector.normalized * speed * Time.deltaTime;
        missile_pos = this.transform.position;

        //the missile looks at the target.
        this.transform.rotation = Quaternion.LookRotation(missile_vector);

        if (Vector3.Distance(this.transform.position, target_pos) <= detection_range || beamed >= 0.2f)
        {
            GameObject obj = Instantiate(explode_obj, this.transform.position, Quaternion.identity);
            obj.GetComponent<boom_move>().Boom(3, 0.1f);
            Destroy(this.gameObject);
        }
    }
}
