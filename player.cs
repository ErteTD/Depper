using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable {


    public GameObject MousePing;
    public GameObject animChild;
    private MonsterAnim anim;
    public GameObject CastSpell;

    private CastSpell CS;
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

    private float inputX;
    private float inputY;
    void Start()
    {
        CS = CastSpell.GetComponent<CastSpell>();
        anim = animChild.GetComponent<MonsterAnim>();
        CritObj.SetActive(false);
        targetPosition = transform.position;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fullhealth = health;
        HealthText.text = health.ToString("F0");
        MovementSpeed_ = MovementSpeed;
        InvokeRepeating("IlluArmor", 1, 0.5f);
    }
    void Update()
    {
        AmIBurning();
        AmISlowed();
        agent.speed = MovementSpeed;

        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX != 0 || inputY != 0)
        {
            Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
            targetPosition = transform.position + moveDir;
            move = true;
            rightclick = false;
            agent.stoppingDistance = 0f;
        }

        if (channelingNow == true)
        {
            anim.PlayerAttack();
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
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 HitGroundlevel = new Vector3(hit.point.x, 1, hit.point.z);
                float dist = Vector3.Distance(HitGroundlevel, transform.position); // distance between click point and PC
                if (Input.GetMouseButtonDown(0) && (hit.collider.tag == "Floor" || hit.collider.tag == "Door" || hit.collider.tag == "Wall"))
                {
                    Vector3 DaPoint = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
                    Instantiate(MousePing, DaPoint, Quaternion.Euler(0, 0, 0));
                }

                if (dist > 0.1f)
                {
                    targetPosition = HitGroundlevel;
                    move = true; // when move true, character moves unless rightclick is true.
                    agent.stoppingDistance = 0f;
                }
                if (hit.collider.gameObject.tag == "Token") // if mouse clicks on a object with the tag Monster, PC will start tracking it and following it.
                {
                    activeToken = hit.collider.gameObject;
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
                CS.spellCastLocation = hit.point;
                targetPosition = hit.point;
                agent.stoppingDistance = 0f;
                move = true;
                rightclick = true;
            }
        }

        if (move)
        {
            if (rightclick == true)
            {
                float dist = Vector3.Distance(targetPosition, transform.position); // distance between clicked area and PC.
                if (dist < spellrange) // If in range, set speed to 0, enable rotation without Navmesh things.
                {

                    if (Vector3.Distance(agent.destination, transform.position) > 1)
                    {
                        agent.destination = this.transform.position;
                    }
                    Vector3 direction = (targetPosition - transform.position).normalized;

                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3             
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 999f);
                     if (channelingNow == false)
                    {
                        SendSpellCast();
                    }                                                 
                }
                else if (channelingNow == false)
                {
                    agent.destination = targetPosition;
                }
            }
            if (!rightclick && !attackingRightNow)
            {
                if (Vector3.Distance(agent.destination, targetPosition) > 1 || (inputX != 0 || inputY != 0))
                {
                    agent.destination = targetPosition;
                }
                float distanceToTarget = Vector3.Distance(transform.position, agent.destination);
                float velocity = agent.velocity.magnitude / agent.speed;
                if (distanceToTarget > 0.75f || velocity > 0.9f)
                {
                    anim.PlayerMove();
                }

            }

        }
        else if (!attackingRightNow)
        {
            PlayerIsIdle();
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

    public void PlayerIsIdle()
    {
        anim.PlayerIdle();
    }

    void CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, agent.destination);
        if (distanceToTarget < 0.75f)
        {
            move = false;
        }
    }

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
        Destroy(Haste, 2.5f);
    }
    public void MultiVis(float Position)
    {
        GameObject Multi = Instantiate(MultiCastVisual, transform);
        Multi.transform.localPosition = new Vector3(Position, 4, 0);
        Destroy(Multi, 0.8f);
    }

    public void SendSpellCast()
    {
        move = false;  // can't move when casting spells.
        CS.CastCurrentSpell();
    }
    public void Die()
    {
        anim.PlayerDie();
        animChild.transform.parent = null;
        this.GetComponent<Player>().enabled = false;
    }

    public void AttackAnim()
    {
        anim.PlayerAttack();
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
