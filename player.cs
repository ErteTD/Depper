using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class player : MonoBehaviour {

    public GameObject MousePing;
    public GameObject animChild;
    public GameObject CastSpell;
    //  [HideInInspector] public GameObject curSpellProjectile;
    [HideInInspector] public List<GameObject> curSpellProjectile = new List<GameObject>();
    [HideInInspector] public List<GameObject> curSpellProjectile_ = new List<GameObject>();
    public float health;
    [HideInInspector] public float fullhealth;
    [HideInInspector]
    public Vector3 targetPosition;
    [HideInInspector]
    //GameObject trackTarget;
    private bool move;
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent agent;
    private bool rightclick;
    // Hide both
    [HideInInspector]
    public float spellrange;
    
    public bool channelingNow;
    [Header("Health&Mana")]
    public Text HealthText;
    public Image HealthBar;
    [HideInInspector] public bool CantBeSlowed;
    private GameObject activeDoor;
    private GameObject activeToken;
    private bool DieOnce;

    [HideInInspector] public bool attackingRightNow;
    private float attackingDuration = 0.25f;

    public float MovementSpeed, MovementSpeed_, slowedDur;


    public GameObject[] trees;
    public GameObject[] Monsters;
    public Text CritText;
    public GameObject CritObj;
    public GameObject HastenVisual;
    public GameObject MultiCastVisual;
    public GameObject BigBoyGlow;
    public GameObject ChangeColor;

    [Header("EnemySpellEffects")]
    [HideInInspector]
    public float BurnDamage, TotalBurnDamage, BurnDur;
    public GameObject FireBurn;
    public GameObject FrostSlow;
    private int ChanCount;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    void Start () {
        CritObj.SetActive(false);
        targetPosition = transform.position; 
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fullhealth = health;
        HealthText.text = health.ToString("F0");
        MovementSpeed_ = MovementSpeed;

        trees = GameObject.FindGameObjectsWithTag("Tree");

        //  InvokeRepeating("UnderATree", 0.1f, 0.5f);
        InvokeRepeating("IlluArmor", 1, 0.5f);

    }
	void Update()
    {
        AmIBurning(); // checks if burning currently
        AmISlowed();
        agent.speed = MovementSpeed;

        // Teeeest
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
       // Vector3 targetMoveAmount = moveDir * MovementSpeed;
        //moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        //Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;

        if (inputX != 0 || inputY != 0)
        {
            targetPosition = transform.position + moveDir;
            move = true;
            rightclick = false;
            agent.stoppingDistance = 0f;
        }

        if (channelingNow == true)
        {
            animChild.GetComponent<MonsterAnim>().PlayerAttack();
            if (Input.GetMouseButtonDown(1) && curSpellProjectile.Count > 0)
            {
                foreach (var item in curSpellProjectile)
                {
                    if (item != null)
                    {
                        item.GetComponent<SpellProjectile>().Stop();
                        curSpellProjectile_.Add(item);
                    }
                }

                foreach (var item in curSpellProjectile_)
                {
                    curSpellProjectile.Remove(item);
                }
            }
        }

        if ((Input.GetMouseButtonDown(0) || (inputX != 0 || inputY != 0)) && channelingNow == true && curSpellProjectile.Count > 0)
        {
            foreach (var item in curSpellProjectile)
            {
                if (item != null)
                {
                    item.GetComponent<SpellProjectile>().Stop();
                    curSpellProjectile_.Add(item);
                }
            }
            foreach (var item in curSpellProjectile_)
            {
                curSpellProjectile.Remove(item);
            }
        }





        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && channelingNow == false)
        {

            rightclick = false;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast things, checks where mouse clicks
			RaycastHit hit;           
			if (Physics.Raycast(ray, out hit) ){
                float dist = Vector3.Distance(hit.point, transform.position); // distance between click point and PC


                if (Input.GetMouseButtonDown(0) && (hit.collider.tag=="Floor" || hit.collider.tag =="Door"))
                {
                    Vector3 DaPoint = new Vector3(hit.point.x, hit.point.y+0.1f, hit.point.z);
                    Instantiate(MousePing, DaPoint, Quaternion.Euler(0,0,0));
                }


            if (dist > 0.1f)
                {
                    targetPosition = hit.point;
                    move = true; // when move true, character moves unless rightclick is true.
                    agent.stoppingDistance = 0f;
                }

                //if (hit.collider.gameObject.tag == "Monster") // if mouse clicks on a object with the tag Monster, PC will start tracking it and following it.
                //{
                //   Monster enemy = hit.collider.GetComponent<Monster>();
                //   trackTarget = GameObject.Find(enemy.name);
                //    agent.stoppingDistance = 1.5f; // stops a bit before reaching the Monster.
                //}

                if (hit.collider.gameObject.tag == "Token") // if mouse clicks on a object with the tag Monster, PC will start tracking it and following it.
                {
                    activeToken = hit.collider.gameObject;
                    //TokenScript token = hit.collider.GetComponent<TokenScript>();
                    //trackTarget = GameObject.Find(token.name);
                    //agent.stoppingDistance = 1f; // stops a bit before reaching the Token.
                }
                else if (activeToken != null)
                {
                    activeToken.GetComponent<TokenScript>().ClickedElsewhere();
                    activeToken = null;
                }

                if (hit.collider.gameObject.tag == "Door") // if mouse clicks on a object with the tag Monster, PC will start tracking it and following it.
                {
                    activeDoor = hit.collider.gameObject;
                }
                else if (activeDoor != null)
                {
                    activeDoor.GetComponent<OneWayDoor>().ClickedElsewhere();
                    activeDoor = null;
                }
            }
		}
        // right click spell cast input
        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                CastSpell.GetComponent<CastSpell>().spellCastLocation = hit.point; // On right click, send the CastSpell script the location of the click, so it can use it for Meteor-like attacks.
               // float dist = Vector3.Distance(hit.point, transform.position);
                targetPosition = hit.point;
                
                //trackTarget = null;
                agent.stoppingDistance = 0f;

                
                    move = true;
                    rightclick = true;
                

                //if (hit.collider.gameObject.tag == "Monster") // same function as earlier, PC still moves toward clicked area/monster if out of range.
                //{
                //    Monster enemy = hit.collider.GetComponent<Monster>();
                //    trackTarget = GameObject.Find(enemy.name);
                //    agent.stoppingDistance = 1.5f;
                //}
            }
        }
      
        if (move)
        {
            if (rightclick == true)
            {
                float dist = Vector3.Distance(targetPosition, transform.position); // distance between clicked area and PC.
                if (dist < spellrange) // If in range, set speed to 0, enable rotation without Navmesh things.
                {
                    //  agent.speed = 0;
                    agent.destination = this.transform.position;
                    Vector3 direction = (targetPosition - transform.position).normalized;

                        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3             
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 999f);

                    // float dot = Vector3.Dot(transform.forward, (targetPosition - transform.position).normalized); 
                    // if (dot > 0.9f && channelingNow == false) // Checks that PC is facing the clicked spot. When facing, call function that casts the spell.
                    // { // WAS MESSING UP WHEN UNDER A enemy, as you were not facing it properly. Not sure if script needed anymore with instant rotations.
                    if (channelingNow == false)
                    {
                        SendSpellCast();

                    }
          // }                                                    
                }
                else if (channelingNow == false)
                {
                    agent.destination = targetPosition;
                }
            }


            //if (trackTarget != null && !rightclick && !attackingRightNow)
            //{
            //    agent.destination = trackTarget.transform.position;
            //    animChild.GetComponent<MonsterAnim>().PlayerMove();
            //}
            if (!rightclick && !attackingRightNow)
            {
                agent.destination = targetPosition;
                animChild.GetComponent<MonsterAnim>().PlayerMove();
            }

        }
        else if (!attackingRightNow)
        {
            animChild.GetComponent<MonsterAnim>().PlayerIdle();
        }


        if (attackingRightNow)
        {

            if (attackingDuration <= 0f)
            {
                if (!channelingNow)
                {
                    attackingRightNow = false;
                }
                attackingDuration = 0.25f;
            }
            attackingDuration -= Time.deltaTime;
        }

            CheckDestinationReached();

      
    }

    void CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, agent.destination);
        if (distanceToTarget < 0.75f)
        {
            move = false;
        }
    }

    //void UnderATree()
    //{
    //    foreach (GameObject tree in trees)
    //    {
    //        if (Vector3.Distance(transform.position, tree.transform.position) < 5f)
    //        {
    //            tree.GetComponent<Renderer>().material.SetFloat("_Cutoff", 1f);
    //        } else if (tree.GetComponent<Renderer>().material.GetFloat("_Cutoff") == 1f)
    //            {
    //            tree.GetComponent<Renderer>().material.SetFloat("_Cutoff", 0.797f);
    //        }
    //    }
    //}


    void IlluArmor()
    {
        if (CastSpell.GetComponent<CastWeapon>().spellSlot2rdy == true && CastSpell.GetComponent<CastWeapon>().CurrentArmor == 2)
        {

            Monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject monster in Monsters)
            {
                if (Vector3.Distance(transform.position, monster.transform.position) < 8f)
                {
                    CastSpell.GetComponent<CastWeapon>().ArmorTrigger();
                }
            }
        }
    }





    public void Channeling()
    {
        channelingNow = true;
    }
    public void StopChanneling()
    {
        channelingNow = false;
    }

    public void TakeDamage(float damage)
    {

        if (CastSpell.GetComponent<CastWeapon>().spellSlot2rdy == true && CastSpell.GetComponent<CastWeapon>().CurrentArmor == 1)
        {
            CastSpell.GetComponent<CastWeapon>().ArmorTrigger(); //currentarmor instead of 0.. TODO
            BurnDur = 0f;
        }
        else
        {

            health -= damage;
            if (health <= 0 && DieOnce == false)
            {
                DieOnce = true;
                Die();
            }

            if (CastSpell.GetComponent<CastWeapon>().spellSlot2rdy == true && CastSpell.GetComponent<CastWeapon>().CurrentArmor == 4)
            {
                CastSpell.GetComponent<CastWeapon>().ArmorTrigger();
            }

            HealthText.text = health.ToString("F1");
            HealthBar.fillAmount = health / fullhealth;
        }
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > fullhealth)
        {
            health = fullhealth;
        }
        HealthText.text = health.ToString("F0");
        HealthBar.fillAmount = health / fullhealth;
    }

    public void Crit(float damage)
    {
        CritObj.SetActive(true);
        CritText.text = damage.ToString("F1");
        Invoke("CritHide", 1f);
    }

    public void CritHide()
    {
        CritObj.SetActive(false);
    }

    public void HastenVis()
    {
       GameObject Haste = Instantiate(HastenVisual, transform);
        //Haste.name = "haste";
        //Invoke("HastenDestory", 2.5f);
        Destroy(Haste, 2.5f);
    }
    public void MultiVis(float Position)
    {
        GameObject Multi = Instantiate(MultiCastVisual, transform);
        Multi.transform.localPosition = new Vector3(Position, 4, 0);
        Destroy(Multi, 0.8f);
    }


    //public void HastenDestory()
    //{
    //    Transform result = gameObject.transform.Find("haste");
    //    if (result)
    //    {
    //        Destroy(transform.Find("haste").gameObject);
    //    }
    //}

    public void SendSpellCast()
    {
        move = false;  // can't move when casting spells.
        CastSpell.GetComponent<CastSpell>().CastCurrentSpell();        
    }
    public void Die()
    {
        animChild.GetComponent<MonsterAnim>().PlayerDie();
        animChild.transform.parent = null;
        this.GetComponent<player>().enabled = false;
    }

    public void AttackAnim()
    {
        animChild.GetComponent<MonsterAnim>().PlayerAttack();
        attackingRightNow = true;
    }







    public void Burn(bool burn, float dur, float str, float dmg)
    {
        if (burn)
        {
            BurnDamage += dmg;
            TotalBurnDamage = BurnDamage * str;
            BurnDur = dur;

            Transform result = gameObject.transform.Find("burn");
            if (!result)
            {
                GameObject burneffect = Instantiate(FireBurn, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation, transform);
                burneffect.name = "burn";
            }
        }
    }

    public void AmIBurning()
    {
        if (BurnDur <= 0)
        {
            BurnDamage = 0;
            Transform result = gameObject.transform.Find("burn");
            if (result)
            {
                Destroy(transform.Find("burn").gameObject);
            }
        }
        else
        {
            BurnDur -= Time.deltaTime;
            TakeDamage(TotalBurnDamage * Time.deltaTime);
        }
    }



    public void Slow(bool slow, float dur, float str) //Could slow attackspeed aswell, though might take a bit of work to slow down animations aswell.
    {
        if (slow && MovementSpeed >= MovementSpeed_ && !CantBeSlowed) //slow is true and currently not slowed. How to work if enemy has a sprint effect? to be seen.
        {
            MovementSpeed /= str;
            agent.speed = MovementSpeed;
           

            GameObject sloweffect = Instantiate(FrostSlow, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation, transform);
            sloweffect.name = "slow";
        }
        if (slow)
        {
            slowedDur = dur;
        }
    }

    public void AmISlowed()
    {
        if (slowedDur <= 0)
        {
            MovementSpeed = MovementSpeed_;
            agent.speed = MovementSpeed;
            Transform result = gameObject.transform.Find("slow");
            if (result)
            {
                Destroy(transform.Find("slow").gameObject);
            }
        }
        else
        {
            slowedDur -= Time.deltaTime;
        }
    }

}
