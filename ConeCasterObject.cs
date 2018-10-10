using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeCasterObject : MonoBehaviour {
    public GameObject currentspellObject;
    public float StartDelay;
    public bool Rotating;
    public float RotSpeed;
    private float RotAngel;
    public bool FireType;
    public bool FrostType;
    public float AttackTimer;
    private float AttackTimer_;
	void Start () {
        AttackTimer_ = StartDelay;
	}
	
	void Update () {

        //AttackTimer_ -= Time.deltaTime;
        //if (AttackTimer_ <= 0)
        //{
        //    Shoot();
        //    AttackTimer_ = AttackTimer;
        //    if (Rotating)
        //    {
        //        transform.rotation = Quaternion.Euler(0, RotAngel, 0);
        //    }
        //}

        //if (Rotating)
        //{
        //    RotAngel += (RotSpeed * Time.deltaTime);
        //}

        AttackTimer_ -= Time.deltaTime;
        if (AttackTimer_ <= 0)
        {
            Shoot();
            AttackTimer_ = AttackTimer;
        }

        if (Rotating)
        {
            RotAngel += (RotSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, RotAngel, 0);
        }

    }

    void Shoot()
    {
        GameObject test123 = Instantiate(currentspellObject, transform.position, transform.rotation, transform);
        SpellProjectile spell = test123.GetComponent<SpellProjectile>();
        //test123.transform.parent = null;
        if (!Rotating)
        {
            spell.damage = 1;
        }
        else
        {
            spell.damage = 0.3f;
        }
        spell.SlowDuration = 2;
        spell.SlowPercent = 1.25f;
        spell.BurnDuration = 2;
        spell.BurnPercent = 0.3f;
        spell.FireBallBurn = FireType;
        spell.FrostBoltSlow = FrostType;
        spell.cone = true;
        spell.enemyCastingspell = true;
        spell.FireTrailCone = true;
        ShapeCone(test123);
    }
    void ShapeCone(GameObject cone)
    {
        cone.GetComponent<BoxCollider>().center = new Vector3(0,0,-2);
        cone.GetComponent<BoxCollider>().size = new Vector3(3.5f, 3.5f, 0);
        StartCoroutine(ShapeConeNext(0.02f, 10, cone, -2, 0));
    }
    IEnumerator ShapeConeNext(float delay,int iterations, GameObject cone, float x1, float x2)
    {
        yield return new WaitForSeconds(delay);
        if (cone != null)
        {
            Next(cone, iterations, x1, x2);
        }
    }
    void Next(GameObject cone,int iterations, float x1, float x2)
    {
        x1 += 0.4f;
        if (iterations > 5)
        {
            x2 += 0.8f;
        }
        else
        {
            x2 -= 0.8f;
        }
        cone.GetComponent<BoxCollider>().center = new Vector3(0, 0, x1);
        cone.GetComponent<BoxCollider>().size = new Vector3(3.5f, 3.5f,x2);
        if (iterations > 1)
        {
            StartCoroutine(ShapeConeNext(0.11f, iterations - 1, cone, x1, x2));
        }
    }
}
