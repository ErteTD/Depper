using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldKingAttack3 : MonoBehaviour
{

    public float speed;
    public float FloatUpTimer;
    public float damage;
    public GameObject Target;
    public GameObject Explode;
    public bool PlayerCast;

    void Update()
    {
        if (FloatUpTimer > 0) // Don't attack.
        {
            transform.position += transform.forward * (1f * Time.deltaTime);
        }
        else // attack
        {
            if (PlayerCast)
            {
                ChainTarget123();
            }
            if (Target != null)
            {
                transform.parent = null;
                Vector3 dir = new Vector3(Target.transform.position.x, 2, Target.transform.position.z) - this.transform.position;
                float distThisFrame = speed * Time.deltaTime;

                transform.Translate(dir.normalized * distThisFrame, Space.World);
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * speed);
                speed += (2 * Time.deltaTime);
            }
            else
            {
                Explode.SetActive(true);
                Destroy(Explode, 1f);
                Explode.transform.parent = null;
                Destroy(gameObject);
            }
        }

        FloatUpTimer -= Time.deltaTime;
    }


    public void ChainTarget123() // Blessed aim ghostcast testing.
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= 50)
        {
            Target = nearestEnemy;
        }
        else
        {
            Target = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Explode.SetActive(true);
            Destroy(Explode, 1f);
            Explode.transform.parent = null;
            Destroy(gameObject);
        }
        if (other.tag == "Player" && !PlayerCast)
        {
            Target.GetComponent<Player>().TakeDamage(damage);
            Explode.SetActive(true);
            Destroy(Explode, 1f);
            Explode.transform.parent = null;
            Destroy(gameObject);
        }
        if (other.tag == "Monster" && PlayerCast && Target != null)
        {
            Target.GetComponent<Monster>().TakeDamage(damage);
            // Make Monster attack friends for 5 sec.
            if (Target.GetComponent<Monster>().Boss == false && Target.GetComponent<Monster>().AttackFriend == false)
            {
                Target.GetComponent<Monster>().AttackFriend = true;
                Target.GetComponent<Monster>().StopFriendAttack();
                GameObject MindControlled = Instantiate(Explode, new Vector3(Target.transform.position.x, Target.transform.position.y + 3, Target.transform.position.z), Target.transform.rotation, Target.transform);

                ParticleSystem ps = MindControlled.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.startSize = 2;
                main.simulationSpeed = 50;

                MindControlled.SetActive(true);
                Destroy(MindControlled, 5);
            }
            Explode.SetActive(true);
            Destroy(Explode, 1f);
            Explode.transform.parent = null;
            Destroy(gameObject);
        }

    }
}
