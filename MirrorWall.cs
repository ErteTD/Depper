using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MirrorWall : MonoBehaviour
{


    private float ChannelTimer = 0.5f;
    private bool noBounce;
    public float BounceDistance = 1.5f;

    List<GameObject> MonsterList2 = new List<GameObject>();
    public GameObject[] MonsterList;

    public void BoltBounce(bool bounce, bool channel, GameObject bolt, GameObject Current, bool Monster)
    {

        if (channel && ChannelTimer > 0)
        {
            noBounce = true;
        }
        else
        {
            noBounce = false;
        }

        if (bounce && noBounce == false)
        {

            if (!Monster)
            {
                MonsterList = GameObject.FindGameObjectsWithTag("Monster");
            }
            else
            {
                MonsterList = GameObject.FindGameObjectsWithTag("Player");
            }

            foreach (GameObject enemy in MonsterList)
            { // if not null might need
                float dist = Vector3.Distance(enemy.transform.position, transform.position);
                if (dist < 20 && dist > 1)
                {
                    MonsterList2.Add(enemy);
                }
            }
            if (MonsterList2.Count >= 1)
            {
                var randomTarget = Random.Range(0, MonsterList2.Count);
                var Rot = Quaternion.LookRotation((MonsterList[randomTarget].transform.position - transform.position).normalized);
                var Pos2 = Vector3.MoveTowards(this.transform.position, MonsterList[randomTarget].transform.position, BounceDistance);

                GameObject Bounce = Instantiate(bolt, new Vector3(Pos2.x, 2.6f, Pos2.z), Rot, this.transform);
                SpellProjectile spell = Bounce.GetComponent<SpellProjectile>();

                if (Current.GetComponent<SpellProjectile>() != null)
                {
                    if (!spell.enabled)
                    {
                        spell.enabled = true;
                    }


                    SpellProjectile curr = Current.GetComponent<SpellProjectile>();
                    spell.damage = curr.damage;
                    spell.projectilespeed = curr.projectilespeed;
                    spell.ghostCast = false;
                    spell.spellName = curr.spellName;
                    spell.LBBounce = curr.LBBounce;
                    spell.Push = curr.Push;
                    spell.LBBounceAmount = curr.LBBounceAmount - 1;
                    spell.enemyCastingspell = Monster;
                }
                else
                {
                    Poolscript curr = Current.GetComponent<Poolscript>();
                    spell.damage = curr.damage;
                    spell.projectilespeed = curr.projectilespeed;
                    spell.ghostCast = false;
                    spell.spellName = curr.spellName;
                    spell.LBBounce = curr.LBBounce;
                    spell.LBBounceAmount = curr.LBBounceAmount - 1;
                    spell.enemyCastingspell = Monster;
                }



                Bounce.transform.parent = null;
                Bounce.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                spell.spellCastLocation = MonsterList2[randomTarget].transform.position;

                spell.channeling = false;
                spell.cone = false;
                spell.aoeSizeMeteor = 0;


                spell.BHBool = false;

                if (spell.LBBounceAmount <= 0)
                {
                    spell.LBBounce = false; // could make it count --;
                }


                if (channel)
                {
                    Bounce.gameObject.GetComponent<Collider>().enabled = true;
                    spell.lightChild1.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild2.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild3.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild4.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild5.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    spell.lightChild6.GetComponent<ParticleSystemRenderer>().lengthScale = 1;
                    //   Invoke("CantBounce", 0.5f); // can't bounce again for 0.5s
                    //    noBounce = true;
                    ChannelTimer = 0.75f;
                }


            }
            MonsterList2.Clear();
        }

        ChannelTimer -= Time.deltaTime;
    }
}
