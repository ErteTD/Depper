using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPoison : MonoBehaviour {


    public float damage;
    private float TickRate;
    public float TickRate_;
    public float PoisonDur;
    public bool NahAH;
    public GameObject Daddy;

    public void Start()
    {
        Invoke("KillMe", PoisonDur);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && TickRate <= 0)
        {
            player player = other.GetComponent<player>();
            player.TakeDamage((damage));
            TickRate = TickRate_;
        }
        TickRate -= Time.deltaTime;
    }
    public void Update()
    {
        if (NahAH)
        {
            if (Daddy == null)
            {
                NahAH = false;
                KillMe();
            }
        }
    }

    public void KillMe()
    {
        if (!NahAH)
        {
            Destroy(gameObject);
        }
    }
}
