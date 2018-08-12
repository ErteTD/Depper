using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldKingProjectile : MonoBehaviour {

    public float distance;
    public GameObject Skeleton;
    public bool HealKing;
    public float CurHealth;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {

        Vector3 dir = Skeleton.transform.position - this.transform.position;
        float distThisFrame = distance * Time.deltaTime;
        if (dir.magnitude <= distThisFrame)
        {
            if (HealKing)
            {
                Skeleton.GetComponent<Monster>().TakeDamage(-CurHealth);
                //Skeleton.GetComponent<Monster>().Healthbar.fillAmount = Skeleton.GetComponent<Monster>().health / Skeleton.GetComponent<Monster>().health2;

 
            }

            Destroy(gameObject);
        }
        else
        {
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * distance);
        }
    }
}
