using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKOrbScript : MonoBehaviour
{
    public bool TargetBool;
    public Transform Target1;
    public Transform Target2;
    public float LifeTime;
    public float damage;
    public bool FrostBoltSlow;
    public float SlowDuration;
    public float SlowPercent;
    public bool FireBallBurn;
    public float BurnDuration;
    public float BurnPercent;


    void Start()
    {
        Invoke("DestroyOrb", LifeTime);
    }
    // Update is called once per frame
    void Update()
    {
        float dist1 = Vector3.Distance(Target1.transform.position, transform.position);
        float dist2 = Vector3.Distance(Target2.transform.position, transform.position);
        Vector3 Pos = new Vector3(transform.position.x, 3, transform.position.z);

        if (TargetBool && dist1 > 3)
        {
            transform.position = Vector3.MoveTowards(Pos, Target1.position, 12 * Time.deltaTime);
        }
        else if (TargetBool && dist1 < 3)
        {
            TargetBool = false;
        }

        if (!TargetBool && dist2 > 3)
        {
            transform.position = Vector3.MoveTowards(Pos, Target2.position, 12 * Time.deltaTime);
        }
        else if (!TargetBool && dist2 < 3)
        {
            TargetBool = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage(damage);
            other.GetComponent<Player>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
            other.GetComponent<Player>().Burn(FireBallBurn, BurnDuration, BurnPercent, damage);
            DestroyOrb();
        }
    }


    void DestroyOrb()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        Destroy(transform.GetChild(1).gameObject, 0.5f);
        transform.GetChild(1).gameObject.transform.parent = null;
        Destroy(transform.GetChild(0).gameObject);
        Destroy(gameObject);
    }
}
