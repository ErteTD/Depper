using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoyFire : MonoBehaviour
{

    public float duration;
    public float damage;
    public bool FireBallBurn;
    public float BurnDuration;
    public float BurnPercent;
    public int PoolNumb;
    public bool FrostBoltSlow;
    public float SlowDuration;
    public float SlowPercent;
    public bool PlayerCasting;

    public GameObject ActPool;

    private void Start()
    {
        Destroy(gameObject, duration);
        Invoke("AnotherOne", 0.15f);
        Invoke("ActivatePool", 0.2f);
    }
    void AnotherOne()
    {
        if (PoolNumb > 0)
        {
            Vector3 Pos = new Vector3(transform.position.x, 1.5f, transform.position.z);
            GameObject PoolObj = Instantiate(this.gameObject, Pos + (transform.forward * 4), transform.rotation, transform);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SpellsCastInThisRoom.Add(PoolObj);
            PoolObj.transform.parent = null;
            PoolObj.GetComponent<BigBoyFire>().PoolNumb = PoolNumb - 1;

        }
    }
    void ActivatePool()
    {
        if (ActPool != null)
        {
            ActPool.SetActive(true);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        IDamageable unit = other.GetComponent<IDamageable>();
        if (unit == null || DamagingSelf(unit))
        {
            return;
        }
        unit.Slow(FrostBoltSlow, SlowDuration, SlowPercent);
        unit.Burn(FireBallBurn, BurnDuration, BurnPercent, damage * Time.deltaTime);
        unit.TakeDamage(damage * Time.deltaTime);
    }

    private bool DamagingSelf(IDamageable unit)
    {
        if (unit is Player && PlayerCasting)
        {
            return true;
        }

        if (unit is Monster && !PlayerCasting)
        {
            return true;
        }
        return false;
    }
}
