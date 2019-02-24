using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmazingChestHead : MonoBehaviour
{
    public GameObject Player_;
    public Light EventLight;
    private bool clicked;
    private bool onlyOnce;
    public GameObject CurrentLoot;
    public int GoldAmount;
    public new Animation animation;
    public AudioSource SpawnSound;
    public AudioSource OpenSound;
    public AudioSource OpenSound2;
    public bool BossChest;
    private GameManager manag;
    public GameObject BossChestHealingPotion;

    void Start()
    {
        manag = GameManager.FindObjectOfType<GameManager>();
        Player_ = GameObject.Find("Player");
        animation = GetComponent<Animation>();
        SpawnSound.Play();
    }
    void Update()
    {

        float dist = Vector3.Distance(Player_.transform.position, transform.position);
        if (dist < 5 && clicked == true && !onlyOnce)
        {
            clicked = false;
            onlyOnce = true;
            OpenChest();
        }

    }

    private void OpenChest()
    {
        animation.Play("ChestAnim");
        OpenSound2.Play();
        OpenSound.PlayDelayed(1f);
        StartCoroutine(SpawnLoot(CurrentLoot, 2f, GoldAmount, BossChest));
    }



    IEnumerator SpawnLoot(GameObject loot, float delay, int gold, bool bossloot)
    {
        yield return new WaitForSeconds(delay);
        GameObject Loot_ = Instantiate(CurrentLoot, transform.position, transform.rotation, transform.parent);
        if (gold > 0)
        {
            Loot_.GetComponent<GoldPickUpScript>().GoldAmount = GoldAmount;
        }
        if (bossloot)
        {

            GameObject Loot_2 = Instantiate(BossChestHealingPotion, transform.position, transform.rotation, transform.parent);
            Loot_2.transform.localPosition = new Vector3(Loot_2.transform.localPosition.x + 3, Loot_2.transform.localPosition.y, Loot_2.transform.localPosition.z);
            Loot_.transform.localPosition = new Vector3(Loot_.transform.localPosition.x- 3, Loot_.transform.localPosition.y, Loot_.transform.localPosition.z);
        }

        transform.parent.GetComponent<Room>().KeepDoorsClosedUntillChestIsOpened = false;


    }


        private void OnMouseOver()
    {

        manag.SelectCursor(true);
        EventLight.intensity = 7f;
        EventLight.range = 4f;
        if (Input.GetMouseButtonDown(MenuScript.MouseMovement))
        {
            clicked = true;
        }
    }

    public void ClickedElsewhere()
    {
        clicked = false;
    }

    private void OnMouseExit()
    {
        manag.SelectCursor(false);
        EventLight.intensity = 5f;
        EventLight.range = 3f;
    }
}
