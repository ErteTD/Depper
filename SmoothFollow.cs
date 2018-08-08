using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

    public GameObject target;
    public float distance;
    public float height;
    public float heightDamping;


    // Use this for initialization
    void Start()
    {

        var wantedHeight = target.transform.position.y + height;
        var currentHeight = transform.position.y;

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping + Time.deltaTime);

        transform.position = target.transform.position;
        transform.position -= Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        transform.LookAt(target.transform);

    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            return;
        }

        var wantedHeight = target.transform.position.y + height;
        var currentHeight = transform.position.y;

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping + Time.deltaTime);

        transform.position = target.transform.position;
        transform.position -= Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        transform.LookAt(target.transform);


    }
}
