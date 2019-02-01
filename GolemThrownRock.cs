using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemThrownRock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RockObstacle;
    public GameObject RockOnGround;
    public GameObject BossCopy;
    public bool firstRock;
    public bool DestroySmallRockAnim;
    public bool realRock;
    public bool BossDeathRocks;
    private bool SmallRock;
    public AudioSource RockImpact;
    public AudioSource RockHitPlayer;
    public int NumberOfBlocks;
    Vector3 asd;

    private void Start()
    {
        if (!firstRock && !DestroySmallRockAnim)
        {
            Invoke("RealRockCanCollide", 0.2f);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" && realRock)
        {
            realRock = false;
            firstRock = false;
            if (!BossDeathRocks)
            {
                for (int i = 0; i < NumberOfBlocks; i++)
                {
                    GameObject Rockk = Instantiate(RockObstacle, transform);
                    Rockk.transform.parent = transform.parent;
                    Rockk.transform.localScale = new Vector3(1, 1, 1);
                    //   Vector3 asd = Random.insideUnitSphere * 10;
                    float xPos = Random.Range(-12, 12);
                    float zPos = Random.Range(-12, 12);
                    asd = new Vector3(Rockk.transform.localPosition.x + xPos, 0, Rockk.transform.localPosition.z + zPos);
                    //    asd = new Vector3(asd.x, 0f, asd.z);
                    Rockk.GetComponent<Rigidbody>().velocity = BallisticVel(asd, 60, Rockk.transform);
                    int xx = Random.Range(-20, 20);
                    int yy = Random.Range(-20, 20);
                    int zz = Random.Range(-20, 20);
                    Rockk.GetComponent<Rigidbody>().angularVelocity = new Vector3(xx, yy, zz);
                }
            }
            else
            {
                GameObject BossR = Instantiate(BossCopy, transform);
                BossR.GetComponent<Monster>().BossCopy = true;
                BossR.GetComponent<Monster>().BossRoom = transform.parent.transform.gameObject.GetComponent<Room>();
                BossR.transform.parent = transform.parent;
                BossR.transform.localScale = new Vector3(1, 1, 1);
            }

            RockImpact.Play();
            Destroy(RockImpact.transform.gameObject, 3f);
            RockImpact.transform.gameObject.transform.parent = null;
            //Destroy(transform.GetChild(0).transform.gameObject, 3f);
            //transform.GetChild(0).transform.parent = null;

            Collider[] cols = Physics.OverlapSphere(transform.position, 5);
            foreach (Collider c in cols)
            {
                Player e = c.GetComponent<Player>();
                if (e != null && e.tag == "Player")
                {
                    e.GetComponent<Player>().TakeDamage(2);
                    RockHitPlayer.Play();
                    Destroy(RockHitPlayer.transform.gameObject, 3f);
                    RockHitPlayer.transform.gameObject.transform.parent = null;
                }
            }
            Destroy(gameObject);
        }

        if (other.tag == "Ground" && SmallRock)
        {

            GameObject Rockkk = Instantiate(RockOnGround, transform);

            Rockkk.transform.parent = transform.parent;
            Rockkk.transform.localScale = new Vector3(1, 1, 1);
            Rockkk.transform.localPosition = new Vector3(transform.localPosition.x, 0.6f, transform.localPosition.z);
            float x = Random.Range(0, 365);
            Rockkk.transform.localRotation = Quaternion.Euler(new Vector3(0, x, 0));
            //  MonsterAnim asdasd2 = Rockkk.gameObject.transform.GetChild(0).gameObject.GetComponent<MonsterAnim>();
            //   asdasd2.StoneGolemStartDead();
            //  Golems.Add(GRock);

            RockImpact.Play();
            Destroy(RockImpact.transform.gameObject, 3f);
            RockImpact.transform.gameObject.transform.parent = null;

            Collider[] cols = Physics.OverlapSphere(transform.position, 2);
            foreach (Collider c in cols)
            {
                Player e = c.GetComponent<Player>();
                if (e != null && e.tag == "Player")
                {
                    e.GetComponent<Player>().TakeDamage(0.5f);
                    RockHitPlayer.Play();
                    Destroy(RockHitPlayer.transform.gameObject, 3f);
                    RockHitPlayer.transform.gameObject.transform.parent = null;

                }
            }
            Destroy(gameObject);
        }
    }

    public void RealRockCanCollide()
    {
        SmallRock = true;
    }

    public void BossDeadDestroyRocks()
    {
        var DieTimer = Random.Range(1f, 3f);
        Invoke("DestoryRock", DieTimer);
        Destroy(gameObject, DieTimer);
    }


    public void DestoryRock()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject Rockk = Instantiate(RockObstacle, transform);
            Rockk.transform.parent = transform.parent;
            Rockk.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            Rockk.GetComponent<GolemThrownRock>().DestroySmallRockAnim = true;
            //   Vector3 asd = Random.insideUnitSphere * 10;
            float xPos = Random.Range(-3, 3);
            float zPos = Random.Range(-3, 3);
            asd = new Vector3(Rockk.transform.localPosition.x + xPos, 0, Rockk.transform.localPosition.z + zPos);
            //    asd = new Vector3(asd.x, 0f, asd.z);
            Rockk.GetComponent<Rigidbody>().velocity = BallisticVel(asd, 30, Rockk.transform);
            int xx = Random.Range(-20, 20);
            int yy = Random.Range(-20, 20);
            int zz = Random.Range(-20, 20);
            Rockk.GetComponent<Rigidbody>().angularVelocity = new Vector3(xx, yy, zz);
            Destroy(Rockk, 2f);
        }
    }


    public Vector3 BallisticVel(Vector3 target, float angle, Transform rock)
    {
        var dir = target - rock.localPosition;  // get target direction
        var h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction
        var dist = dir.magnitude;  // get horizontal distance
        var a = angle * Mathf.Deg2Rad;  // convert angle to radians
        dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
        dist += h / Mathf.Tan(a);  // correct for small height differences
                                   // calculate the velocity magnitude
        var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        if (float.IsNaN(vel))
        {
            vel = 1;
        }

        return vel * dir.normalized;
    }

}

