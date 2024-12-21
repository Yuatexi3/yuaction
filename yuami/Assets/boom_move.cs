using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class boom_move : MonoBehaviour
{
    float max_boom_scale = 6;
    float boom_time = 0.5f;
    float boom_scale = 0;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < boom_time)
        {
            timer += Time.deltaTime;
            boom_scale = max_boom_scale * (timer / boom_time);
            this.gameObject.transform.localScale = new Vector3(boom_scale, boom_scale, boom_scale);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Boom(float radius, float time)
    {
        max_boom_scale = radius * 2;
        boom_time = time;
    }
}
