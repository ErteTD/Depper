using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{

    // Use this for initialization

    public float BoostBurnDur;
    public float BoostBurnPer;
    public float BurnDamage;
    public float BoostTotalBurn;
    public GameObject Explosion;

    public bool IlluExplosion;
    public bool FireTrueFrostFalse;
    public bool ArmorProc;
    public float Area;
    public bool GolemExplosion;
    public float DestoryDur = 2f;
    public void Start()
    {
        if (!ArmorProc)
        {
            float RandomDur = Random.Range(0.9f, 1);
            Area = 5;
            Invoke("ExplodeNow", RandomDur);
        }
        else
        {
            Area = 8;
            ExplodeNow();
        }
        Destroy(gameObject, DestoryDur);
    }

    public void ExplodeNow()
    {
        Explosion.SetActive(true);

        if (!GolemExplosion)
        {
            Collider[] cols2 = Physics.OverlapSphere(transform.position, Area);
            foreach (Collider c in cols2)
            {
                Monster e = c.GetComponent<Monster>();
                if (e != null && e.gameObject != gameObject)
                {
                    if (FireTrueFrostFalse == true)
                    {
                        e.Burn(true, BoostBurnDur, BoostBurnPer, BurnDamage * BoostBurnPer);
                        e.TakeDamage(BoostTotalBurn * BoostBurnPer);
                    }
                    else if (!IlluExplosion)
                    {
                        if (e.MonsterType != 5)
                        {
                            if (!ArmorProc)
                            {
                                e.Slow(true, BoostBurnDur, BoostBurnPer);
                            }
                            e.StopAgent();
                        }
                        else
                        {
                            e.Slow(true, 2, 9999);
                            e.StopAgent();
                        }
                    }
                }
            }
        }
    }
}
