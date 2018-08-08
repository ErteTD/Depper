using UnityEngine;
using System.Collections;

public class OneTimeThing : MonoBehaviour
{

    public int numObjects = 100;
    public bool Circle;
    public bool Line;
    public GameObject prefab;
    private float LineDist;

    void Start()
    {
        if (Circle)
        {
            Vector3 center = transform.position;
            for (int i = 0; i < numObjects; i++)
            {
                Vector3 pos = RandomCircle(center, 20.0f);
                Quaternion rot = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                var Rock = Instantiate(prefab, pos, rot);
                Rock.transform.parent = gameObject.transform;
            }
        }

        if (Line)
        {
            Vector3 center = transform.position;
            for (int i = 0; i < numObjects; i++)
            {
                Quaternion rot = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                center.x = LineDist;
                Instantiate(prefab, center, rot);
              //  Rock.transform.parent = gameObject.transform;

                LineDist += 1f;
            }
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}