using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeperWaveDamage : MonoBehaviour
{
    public bool DamageType;
    public float damage;
    public bool FrostBoltSlow;
    public float SlowDuration;
    public float SlowPercent;
    public bool FireBallBurn;
    public float BurnDuration;
    public float BurnPercent;
    private bool Exited;
    Collider other2;

    // Update is called once per frame
    void Update()
    {

        if (Exited)
        {

            other2.GetComponent<Player>().TakeDamage(damage * Time.deltaTime);
            if (DamageType)
            {
                other2.GetComponent<Player>().Slow(FrostBoltSlow, SlowDuration, SlowPercent);
            }
            else
            {
                other2.GetComponent<Player>().Burn(FireBallBurn, BurnDuration, BurnPercent, damage * Time.deltaTime);
            }
            
        }
    }



    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other2 = other;
            Exited = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Exited = false;
        }
    }

}
