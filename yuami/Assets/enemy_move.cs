using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{
    [SerializeField] GameObject obj;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if(t >= 1)
        {
            t = 0;
            Instantiate(obj, transform.position, Quaternion.identity);
        }
    }
}
