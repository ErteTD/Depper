using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderRobe : MonoBehaviour
{
    public float damage;
    private float zapCD_;
    public GameObject ZapVis;
    public AudioSource ZapSound;
    private GameObject CurrentZap;
    private GameObject CurrentTarget;
    // Start is called before the first frame update
    void Start()
    {
        zapCD_ = 1;
    }

    // Update is called once per frame
    void Update()
    {
        zapCD_ -= Time.deltaTime;

        if (zapCD_ <= 0)
        {
            zapCD_ = 1;
            FindZap();
        }
        if (CurrentZap != null)
        {
            CurrentZap.transform.position = transform.position;

            if (CurrentTarget != null)
            {
                float dist2 = Vector3.Distance(CurrentTarget.transform.position, transform.position);
                CurrentZap.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().lengthScale = dist2/2;
                CurrentZap.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().lengthScale = dist2/2;
                CurrentZap.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().lengthScale = dist2/2;
                CurrentZap.transform.LookAt(new Vector3(CurrentTarget.transform.position.x, CurrentTarget.transform.position.y + 3, CurrentTarget.transform.position.z));


            }

        }
    }

    void FindZap()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 25f);
        Monster nearestEnemy = null;
        float shortestDistance = 99;
        foreach (Collider col in cols)
        {
            if (col.tag == "Monster")
            {
                Monster enemy = col.transform.GetComponent<Monster>();
                if (enemy.AggroRange > 100)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy;
                    }
                }
            }
        }
        if (nearestEnemy != null)
        {
            zapzapzap(nearestEnemy);
        }
    }

    void zapzapzap(Monster ZapTarget)
    {
        ZapTarget.TakeDamage(damage);
        float dist = Vector3.Distance(ZapTarget.transform.position, transform.parent.position);
        float dist2 = Vector3.Distance(ZapTarget.transform.position, transform.position);
        float zapzap = Mathf.InverseLerp(5, 25, dist);
        GameObject Zap = Instantiate(ZapVis, transform);
        Zap.transform.localPosition = new Vector3(0, 0, 0);

        Zap.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().lengthScale = dist2/2;
        Zap.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().lengthScale = dist2/2;
        Zap.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().lengthScale = dist2/2;
        CurrentTarget = ZapTarget.gameObject;
        Zap.transform.parent = null;
        CurrentZap = Zap;
        Zap.transform.LookAt(new Vector3(ZapTarget.transform.position.x, ZapTarget.transform.position.y+3, ZapTarget.transform.position.z));
        Destroy(Zap, 0.5f);

        ZapSound.Play();
        zapCD_ = 0.5f + (3 * zapzap);
    }
}
