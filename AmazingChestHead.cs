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
    public AudioSource OpenSound;
    public AudioSource OpenSound2;
    void Start()
    {
        Player_ = GameObject.Find("Player");
        animation = GetComponent<Animation>();
        transform.parent.GetComponent<Room>().AddMonster(gameObject);
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
        StartCoroutine(SpawnLoot(CurrentLoot, 2f, GoldAmount));
    }



    IEnumerator SpawnLoot(GameObject loot, float delay, int gold)
    {
        yield return new WaitForSeconds(delay);
        GameObject Loot_ = Instantiate(CurrentLoot, transform.position, transform.rotation, transform.parent);
        if (gold > 0)
        {
            Loot_.GetComponent<GoldPickUpScript>().GoldAmount = GoldAmount;
        }
        transform.parent.GetComponent<Room>().RemoveMonster(gameObject);
    }


        private void OnMouseOver()
    {
        EventLight.intensity = 7f;
        EventLight.range = 4f;
        if (Input.GetMouseButtonDown(0))
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
        EventLight.intensity = 5f;
        EventLight.range = 3f;
    }
}
