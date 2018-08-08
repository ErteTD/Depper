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
        Invoke("KillMe", duration);
        Invoke("AnotherOne", 0.15f);
        Invoke("ActivatePool", 0.2f);
    }

    void AnotherOne()
    {
        if (PoolNumb > 0)
        {
            Vector3 Pos = new Vector3(transform.position.x, 1.5f, transform.position.z);
            GameObject PoolObj = Instantiate(this.gameObject, Pos + (transform.forward * 4), transform.rotation, transform);

            PoolObj.transform.parent = null;
            PoolObj.GetComponent<BigBoyFire>().PoolNumb = PoolNumb - 1;

        }
    }


    void ActivatePool()
    {
        ActPool.SetActive(true);
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !PlayerCasting)
        {
            Player enemy = other.GetComponent<Player>();
            enemy.Slow(FrostBoltSlow, SlowDuration, SlowPercent);
            enemy.Burn(FireBallBurn, BurnDuration, BurnPercent, damage * Time.deltaTime);
            enemy.TakeDamage(damage * Time.deltaTime);
        }

        if (other.tag == "Monster" && PlayerCasting)
        {
            Monster enemy = other.GetComponent<Monster>();
            enemy.Slow(FrostBoltSlow, SlowDuration, SlowPercent);
            enemy.Burn(FireBallBurn, BurnDuration, BurnPercent, damage * Time.deltaTime);
            enemy.TakeDamage(damage * Time.deltaTime);
        }
    }





    public void KillMe()
    {
        Destroy(gameObject);
    }
}
