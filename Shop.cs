using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject Player_;
    public GameManager GM;
    public Light EventLight;
    private bool ShopOpen;
    private bool clicked;
    private ToolTipScript tts;
    void Start()
    {
        tts = FindObjectOfType<ToolTipScript>();
        Player_ = GameObject.Find("Player");
        GM = GameManager.FindObjectOfType<GameManager>();
    }
    void Update()
    {
        float dist = Vector3.Distance(Player_.transform.position, transform.position);
        if (dist < 5 && clicked == true)
        {
            clicked = false;

            if (ShopOpen == false)
            {
                ShopOpen = true;
                GM.ShopWindowFunc(true);
                GM.CurrentShopLocation = transform.position;
                GM.ForceHideAllOtherWindows = true;
                GM.ShopRoom = transform;
            }
            else
            {
                ShopOpen = false;
                GM.ShopWindowFunc(false);
                GM.SellTokenWindow.SetActive(false);
                GM.SellItemWindow.SetActive(false);
                GM.ShowSellItemWindowBool = false;
                GM.ShowSellWindowBool = false;
                GM.ForceHideAllOtherWindows = false;
                GM.ShopRoom = null;
            }
        }


        if (ShopOpen && dist > 7)
        {
            ShopOpen = false;
            GM.ShopWindowFunc(false);
            GM.SellTokenWindow.SetActive(false);
            GM.SellItemWindow.SetActive(false);
            GM.ShowSellItemWindowBool = false;
            GM.ShowSellWindowBool = false;
            GM.ForceHideAllOtherWindows = false;
            GM.ShopRoom = null;
            if ((tts.ToolTipHC1|| tts.ToolTipHC2) && (tts.SelectWeapon.activeSelf == false && tts.SelectArmor.activeSelf == false))
            {
                tts.CLOSEALLITEMTIPS();
            }
        }
    }

    private void OnMouseOver()
    {
        FindObjectOfType<GameManager>().SelectCursor(true);
        EventLight.intensity = 7f;
        EventLight.range = 4f;
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), MenuScript.MoveLoc)))
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
        FindObjectOfType<GameManager>().SelectCursor(false);
        EventLight.intensity = 5f;
        EventLight.range = 3f;
    }
}
