using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam_move : MonoBehaviour
{
    GameObject target;
    GameObject player;
    GameObject child;
    GameObject[] targets;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        target = GameObject.Find("target");
        child = transform.GetChild(0).gameObject;

        this.transform.position = player.transform.position + (target.transform.position - player.transform.position) / 2;
        child.transform.localScale = new Vector3(0.2f, (target.transform.position - this.transform.position).magnitude, 0.2f);
        this.transform.rotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("enemy_missile");
        float least_dist = 0;
        foreach (GameObject t in targets)
        {
            if (least_dist == 0 || least_dist > (player.transform.position - t.transform.position).magnitude || t == null)
            {
                least_dist = (player.transform.position - t.transform.position).magnitude;
                target = t;
            }
        }
        if(target == null)
        {
            target = GameObject.Find("target");
        }
        this.transform.position = player.transform.position + (target.transform.position - player.transform.position) / 2;
        child.transform.localScale = new Vector3(0.2f, (target.transform.position - this.transform.position).magnitude, 0.2f);
        this.transform.rotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
        if(target.CompareTag("enemy_missile"))
        {
            target.GetComponent<enemy_missile_move>().beamed += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
        }
    }
}
