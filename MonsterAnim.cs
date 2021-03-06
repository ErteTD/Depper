﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnim : MonoBehaviour
{
    private bool alreadyAttacking;
    public Animator anim;
    public new Animation animation;

    // Use this for initialization



    void Awake()
    {
        anim = GetComponent<Animator>();
        animation = GetComponent<Animation>();
    }


    public void IdleAnim()
    {
        anim.Play("Idle", -1);
    }
    public void IdleAnimation()
    {
        animation.Play("idle");
    }
    public void IdleAnimation2()
    {
        animation.Play("Idle");
    }
    public void IdleAnimation3()
    {
        anim.Play("Idle");
    }
    public void IdleAnimation4()
    {
        anim.Play("boss_golem_idle01");
    }
    public void IdleAnimation5()
    {
        anim.speed = 1f;
        anim.Play("fallen_guardian_idle01_04");
    }

    public void Spawn()//for bigboyboss.
    {
        anim.Play("creature1Spawn", -1);
        anim.speed = 0.5f;
    }
    public void WalkAnim()
    {
        anim.Play("Walk02");
    }
    public void CastSpell()
    {
        anim.Play("SwingQuick");
    }
    public void WaitBetweenAttacks()
    {
        anim.Play("Idle2");
    }

    public void RunAnim()
    {
        anim.Play("creature1run", -1);
    }
    public void RunAnimation()
    {
        animation.Play("run");
    }
    public void RunAnimation2()
    {
        animation.Play("Run");
    }
    public void RunAnimation3(int type)
    {
        if (type == 0)
        {
            anim.Play("Run");
        }
        if (type == 1)
        {
            anim.Play("Walk");
        }
    }
    public void RunAnim4()
    {
        anim.Play("boss_golem_walk01");
        anim.speed = 2.5f;
    }
    public void RunAnim5()
    {
        anim.speed = 1f;
        anim.Play("fallen_guardian_run01");
    }

    public void AttackAnim()
    {
        anim.Play("creature1Attack1", -1, 0f);
    }
    public void RoarAnim()
    {
        anim.Play("creature1roar", -1, 0f);
    }
    public void RoarAnim2()
    {
        anim.Play("creature1roar", -1, 0f);
        anim.speed = 1f;
    }

    public void AttackAnimation()
    {
        animation.Play("hpunch");
        animation["hpunch"].speed = 0.75f;
    }
    public void AttackAnimation2()
    {
        var RandomAnim = Random.Range(0, 3);

        switch (RandomAnim)
        {
            case 0:
                animation.Play("Attack");
                break;
            case 1:
                animation.Play("Attack_Left");
                break;
            case 2:
                animation.Play("Attack_Right");
                break;
            default:
                break;
        }

    }
    public void AttackAnimation3()
    {
        anim.Play("Attack");
    }
    public void AttackAnimation4()
    {
        anim.Play("boss_golem_skill01", -1, 0);
        anim.speed = 2f;
    }
    public void RockTossAnim()
    {
        anim.Play("boss_golem_skill01", -1, 0);
        anim.speed = 0.75f;
    }
    public void AttackAnimation5()
    {
        anim.speed = 1f;
        anim.Play("fallen_guardian_boss_skill_attack03_1", -1, 0f);
    }
    public void MeteorAttackAnim()
    {
        anim.speed = 0.5f;
        anim.Play("fallen_guardian_boss_skill_attack04", -1, 0f);
    }
    public void DieAnim()
    {
        anim.Play("creature1Die", -1, 0f);
        Invoke("Die", 1.3f);
    }
    public void DieAnimation()
    {
        animation.Play("death");
        Invoke("Die", 1.4f);
    }
    public void DieAnimation2()
    {
        animation.Play("Death");
        Invoke("Die", 2f);
    }
    public void DieAnimation3()
    {
        anim.Play("Death");
        Invoke("SkeletonRise", 2.5f);
    }
    public void DieAnimation4()
    {
        anim.Play("Death");
        Invoke("Die", 2.1f);
    }
    public void DieAnimation5()
    {
        anim.Play("Death");
    }
    public void StartDeadAnim()
    {
        anim.Play("StartDead");
    }
    public void DieAnimation6()
    {
     //   anim.speed = -1;
        anim.Play("StandUp02");
        gameObject.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        Invoke("Die", 2.1f);
    }
    public void DieAnimation7()
    {
        anim.speed = 0.5f;
        anim.Play("boss_golem_skill04");
        Invoke("Die", 2.1f);
    }

    public void StoneGolemTurnToStone()
    {

        anim.speed = 0.5f;
        anim.Play("boss_golem_skill04"); // stop at frame 80.
    }

    public void StoneGolemStartDead()
    {

        anim.speed = 0f;
        anim.Play("boss_golem_born01", 0, 0); // stop at frame 80.
    }

    public void StoneGolemCrumble()
    {
        anim.speed = 1f;
        anim.Play("boss_golem_death01");
    }

    public void DieAnimation8()
    {
        anim.speed = 1f;
        anim.Play("fallen_guardian_death01_06");
        Invoke("Die", 2f);
    }
    public void SpawnGolem()
    {
        anim.speed = 1.5f;
        anim.Play("boss_golem_born01", -1);
    }

    public void OldKingSpecialAttack1()
    {
        anim.Play("Skill");
    }
    public void SkeletonRise()
    {
        // transform.parent.GetComponent<Monster>()
        anim.Play("Revive");
    }
    void Die()
    {
        Destroy(gameObject);
    }





    ////////// Player animations.
    public void PlayerMove()
    {
        animation.Play("move_forward_fast");
    }
    public void PlayerIdle()
    {
        animation.Play("idle_combat");
    }

    public void SpawnTK(float speed)
    {
        animation["dead"].speed = speed;
        animation["dead"].time = animation["dead"].length;
        animation.Play("dead");
    }

    public void DieTK()
    {
        animation["dead"].speed = 1;
        animation["dead"].time = 0;
        animation.Play("dead");
        Invoke("Die", 5f);
    }

    public void PlayerDie()
    {
        animation.Play("dead");
    }
    public void PlayerAttack()
    {
        //if (!animation.IsPlaying("attack_short_001")){

        if (Player.FindObjectOfType<Player>().channelingNow == false)
        {
            animation["attack_short_001"].time = 0.6f;
            animation["attack_short_001"].speed = 1.2f;
            animation.Play("attack_short_001");
        }
        else
        {

            animation["attack_short_001"].time = 0.6f;
            animation["attack_short_001"].speed = 1.2f;
            //  animation.wrapMode = WrapMode.Loop;
            animation.Play("attack_short_001");


        }
        //}
    }
    public void TimeKeeperAttack()
    {
        animation["attack_short_001"].time = 0.6f;
        animation["attack_short_001"].speed = 1.2f;
        animation.Play("attack_short_001");

    }
}
