﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{

    // Use this for initialization

    public float BoostBurnDur;
    public float BoostBurnPer;
    public float BurnDamage;
    public float BoostTotalBurn;

    public bool FireTrueFrostFalse;
    public bool ArmorProc;
    public float Area;
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
        Destroy(gameObject, 2f);
    }

    public void ExplodeNow()
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
                else
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
                        e.Slow(true, 3, 9999);
                        e.StopAgent();
                    }
                }
            }
        }
    }
}