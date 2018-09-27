using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{


    public bool SlotIsInUse1;
    public bool SlotIsInUse2;
    public bool SlotIsInUse3;
    public Texture2D DefCursor;
    public GameObject EscMenu;
    public static int minRoom_, maxRoom_, CurrentLevel_;
    public static bool StartLevel_, GiveLoot_;
    public GameObject BossHealth;
    [Header("ShopStuff")]
    public int RandomSpellBuyCost;
    public int RandomItemBuyCost;
    public int PotionBuyCost;
    public int SellSpellGold;
    public int SellItemGold;
    private bool SellTokenSuccess;
    public bool ShowSellWindowBool;
    public bool ShowSellItemWindowBool;
    public AudioSource NotEnoughGoldSound;
    public AudioSource CloseShopWindow;
    public AudioSource SellTokenSound;
    public GameObject ShopWindow;
    public GameObject SellTokenWindow;
    public GameObject SellItemWindow;
    public Vector3 CurrentShopLocation;
    public List<GameObject> Items;
    public bool ForceHideAllOtherWindows;
    public Transform ShopRoom;

    [Header("Currency")]
    public int Money;
    public int Lives;
    public Text CurrentGoldText;
    public Text CurrentLivesText;
    public Text DeathScreenLivesText;

    [Header("Inventory Weapon")]
    public GameObject SpiderWeapon;
    public int SpiderWeaponToken;
    public GameObject BlinkWeapon;
    public int BlinkWeaponToken;
    public GameObject FireWeapon;
    public int FireWeaponToken;
    public GameObject IKWeapon;
    public int IKWeaponToken;
    public GameObject BlobWeapon;
    public int BlobWeaponToken;
    [Header("Inventory Armor")]
    public GameObject SpiderArmor;
    public int SpiderArmorToken;
    public GameObject IlluArmor;
    public int IlluArmorToken;
    public GameObject RoidArmor;
    public int RoidArmorToken;
    public GameObject IKArmor;
    public int IKArmorToken;
    public GameObject BlobArmor;
    public int BlobArmorToken;

    [Header("ShopItems")]
    public GameObject SpiderWeapon_;
    public GameObject BlinkWeapon_;
    public GameObject FireWeapon_;
    public GameObject IKWeapon_;
    public GameObject BlobWeapon_;
    public GameObject SpiderArmor_;
    public GameObject IlluArmor_;
    public GameObject RoidArmor_;
    public GameObject IKArmor_;
    public GameObject BlobArmor_;

    [Header("Resources")]
    public int meteorToken;
    public int coneToken;
    public int ghostToken;
    public int doubleToken;
    public int splitToken;
    public int channelingToken;
    public int boostToken;
    public int hastenToken;
    public int empowerToken;
    public int bhToken;
    public int pushToken;
    public int poolToken;
    public int ChaosToken;
    public int CompToken;
    public int AimToken;

    [Header("Resource Images")]
    public GameObject ShowTokensPanel;
    private bool showTP;
    public Text meteorText;
    public Text coneText;
    public Text ghostText;
    public Text doubleText;
    public Text splitText;
    public Text compText;
    public Text boostText;
    public Text hastenText;
    public Text empowerText;
    public Text bhText;
    public Text pushText;
    public Text poolText;
    public Text chaosText;
    public Text channelingText;
    public Text aimText;
    // Forgive me father for I have sinned....
    public Text meteorText_;
    public Text coneText_;
    public Text ghostText_;
    public Text doubleText_;
    public Text splitText_;
    public Text compText_;
    public Text boostText_;
    public Text hastenText_;
    public Text empowerText_;
    public Text bhText_;
    public Text pushText_;
    public Text poolText_;
    public Text chaosText_;
    public Text channelingText_;
    public Text aimText_;

    [Header("Item Count")]
    public Text Item1;
    public Text Item2;
    public Text Item3;
    public Text Item4;
    public Text Item5;
    public Text Item6;
    public Text Item7;
    public Text Item8;
    public Text Item9;
    public Text Item10;


    [Header("Activly used tokens for each slot")]
    public bool meteorToken_;
    public bool coneToken_;
    public bool ghostToken_;
    public bool doubleToken_;
    public bool splitToken_;
    public bool channelingToken_;
    public bool boostToken_;
    public bool hastenToken_;
    public bool empowerToken_;
    public bool bhToken_;
    public bool pushToken_;
    public bool poolToken_;
    public bool ChaosToken_;
    public bool CompToken_;
    public bool AimToken_;

    public bool meteorToken_2;
    public bool coneToken_2;
    public bool ghostToken_2;
    public bool doubleToken_2;
    public bool splitToken_2;
    public bool channelingToken_2;
    public bool boostToken_2;
    public bool hastenToken_2;
    public bool empowerToken_2;
    public bool bhToken_2;
    public bool pushToken_2;
    public bool poolToken_2;
    public bool ChaosToken_2;
    public bool CompToken_2;
    public bool AimToken_2;

    public bool meteorToken_3;
    public bool coneToken_3;
    public bool ghostToken_3;
    public bool doubleToken_3;
    public bool splitToken_3;
    public bool channelingToken_3;
    public bool boostToken_3;
    public bool hastenToken_3;
    public bool empowerToken_3;
    public bool bhToken_3;
    public bool pushToken_3;
    public bool poolToken_3;
    public bool ChaosToken_3;
    public bool CompToken_3;
    public bool AimToken_3;

    public GameObject TokenTT1, TokenTT2, TokenTT3, TokenTT4;
    public GameObject TokenTT1T, TokenTT2T, TokenTT3T, TokenTT4T, TokenTT5T;
    public GameObject ControlMenu;
    private bool ShowHide, ShowHide2;
    public GameObject MiniCamera;
    public float changeInX, changeInZ;
    public List<GameObject> Illus;
    private bool show;
    private float SetTime;
    private GameObject CurrentRoom;

    [Header("MiniMapLarge")]
    public GameObject MiniMapLargeCamera;
    public GameObject MiniMapLargePanel;
    private bool ToggleBoolLargeCamera;

    private CastWeapon cw;
    private ToolTipScript tts;
    private Player Player_;
    private MapGrid MG;
    private GameObject MainCamera;


    void Start()
    {
        MainCamera = GameObject.Find("MainCamera");

        MG = FindObjectOfType<MapGrid>();
        Player_ = FindObjectOfType<Player>();
        cw = FindObjectOfType<CastWeapon>();
        tts = FindObjectOfType<ToolTipScript>();
      Vector2 asd = new Vector2(0, 0);
        MiniCamera = GameObject.Find("MiniMapCamera");
        Cursor.SetCursor(DefCursor, asd, CursorMode.Auto);
        SetTime = Time.timeScale;
        CurrentGoldText.text = "Gold: " + Money.ToString();
        CurrentLivesText.text = "Lives: " + Lives.ToString();
        DeathScreenLivesText.text = "Continue (" + Lives.ToString() + ")";
    }


    public void SetCurrentRoom(GameObject room) // not used currently
    {
        CurrentRoom = room;
    }
    public GameObject GetCurrentRoom() // not used currently
    {
        return CurrentRoom;
    }

    public void GainGold(int amount)
    {
        Money += amount;
        CurrentGoldText.text = "Gold: "+ Money.ToString();
    }

    public void ShopWindowFunc(bool status)
    {
        ShopWindow.SetActive(status);
        if (status == false)
        {
            CloseShopWindow.Play();
        }
    }

    public void BuyRandomToken()
    {
        if (Money >= RandomSpellBuyCost)
        {
            Money -= RandomSpellBuyCost;
            CurrentGoldText.text = "Gold: " + Money.ToString();
            var randomToken = Random.Range(0, MapGrid.FindObjectOfType<MapGrid>().Tokens.Count);

            float random1 = Random.Range(0f,0f);
            float random2 = Random.Range(-4f, -2f);

            var CurLoc = CurrentShopLocation + transform.forward * random1 + transform.right * random2;
            GameObject CurLoot = Instantiate(MapGrid.FindObjectOfType<MapGrid>().Tokens[randomToken], new Vector3(CurLoc.x, 1, CurLoc.z), Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z));
            CurLoot.transform.parent = ShopRoom;
          
        }
        else
        {
            NotEnoughGoldSound.Play();
        }
    }

    public void BuyHealingPotion()
    {
        if (Money >= PotionBuyCost)
        {
            Money -= PotionBuyCost;
            CurrentGoldText.text = "Gold: " + Money.ToString();
            float random1 = Random.Range(0f, 0f);
            float random2 = Random.Range(-4f, -2f);

            var CurLoc = CurrentShopLocation + transform.forward * random1 + transform.right * random2;
            GameObject CurLoot = Instantiate(MapGrid.FindObjectOfType<MapGrid>().Healing[0], new Vector3(CurLoc.x, 1, CurLoc.z), Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z));
            CurLoot.transform.parent = ShopRoom;
        }
        else
        {
            NotEnoughGoldSound.Play();
        }
    }

    public void BuyRandomItem()
    {
        if (Money >= RandomItemBuyCost)
        {
            Money -= RandomItemBuyCost;
            CurrentGoldText.text = "Gold: " + Money.ToString();
            var randomToken = Random.Range(0, Items.Count);

            float random1 = Random.Range(0f, 0f);
            float random2 = Random.Range(-4f, -2f);


            var CurLoc = CurrentShopLocation + transform.forward * random1 + transform.right * random2;
            GameObject CurLoot = Instantiate(Items[randomToken], new Vector3(CurLoc.x, 1, CurLoc.z), Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z));
            CurLoot.transform.parent = ShopRoom;
        }
        else
        {
            NotEnoughGoldSound.Play();
        }
    }

    public void ButtonRooms(int Type)
    {
        switch (Type)
        {
            case 0:
                minRoom_ = 6;
                maxRoom_ = 6;
                CurrentLevel_ = 0;
                GiveLoot_ = false;

                break;
            case 1:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 0;
                GiveLoot_ = true;
                break;
            case 2:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 1;
                GiveLoot_ = true;
                break;
            case 3:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 2;
                GiveLoot_ = true;
                break;
            case 4:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 3;
                GiveLoot_ = true;
                break;
            case 5:
                minRoom_ = 20;
                maxRoom_ = 20;
                CurrentLevel_ = 8;
                GiveLoot_ = true;
                break;
            case 6:
                minRoom_ = 0;
                maxRoom_ = 0;
                CurrentLevel_ = 4;
                GiveLoot_ = true;
                break;
        }
        StartLevel_ = true;
        Time.timeScale = SetTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ControlTime(float time)
    {
        SetTime = time;
    }

    public void ShowAvailableTokens()
    {
        showTP = !showTP;
        ShowTokensPanel.SetActive(showTP);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShowHide2 = ShowHide2 ? false : true;
            ControlMenu.SetActive(ShowHide2);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLargeMiniMap();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowEscMenu();
        }
    }

    public void PickedUpItem()
    {
        CurrentTokens();
        if (SpiderWeaponToken >= 1)
        {
            SpiderWeapon.SetActive(true);
            if (cw.CurrentWeapon != 1 || SpiderWeaponToken > 1)
            {
                SpiderWeapon_.SetActive(true);
            }
            else
            {
                SpiderWeapon_.SetActive(false);
            }
        }
        else
        {
            SpiderWeapon.SetActive(false);
            SpiderWeapon_.SetActive(false);
        }
        if (BlinkWeaponToken >= 1)
        {
            BlinkWeapon.SetActive(true);
            if (cw.CurrentWeapon != 2 || BlinkWeaponToken > 1)
            {
                BlinkWeapon_.SetActive(true);
            }
            else
            {
                BlinkWeapon_.SetActive(false);
            }
        }
        else
        {
            BlinkWeapon.SetActive(false);
            BlinkWeapon_.SetActive(false);
        }
        if (FireWeaponToken >= 1)
        {
            FireWeapon.SetActive(true);
            if (cw.CurrentWeapon != 3|| FireWeaponToken > 1)
            {
                FireWeapon_.SetActive(true);
            }
            else
            {
                FireWeapon_.SetActive(false);
            }
        }
        else
        {
            FireWeapon.SetActive(false);
            FireWeapon_.SetActive(false);
        }
        if (IKWeaponToken >= 1)
        {
            IKWeapon.SetActive(true);
            if (cw.CurrentWeapon != 4|| IKWeaponToken >1)
            {
                IKWeapon_.SetActive(true);
            }
            else
            {
                IKWeapon_.SetActive(false);
            }
        }
        else
        {
            IKWeapon_.SetActive(false);
            IKWeapon.SetActive(false);
        }
        if (BlobWeaponToken >= 1)
        {
            BlobWeapon.SetActive(true);
            if (cw.CurrentWeapon != 5 ||BlobWeaponToken > 1)
            {
                BlobWeapon_.SetActive(true);
            }
            else
            {
                BlobWeapon_.SetActive(false);
            }
        }
        else
        {
            BlobWeapon_.SetActive(false);
            BlobWeapon.SetActive(false);
        }
        if (SpiderArmorToken >= 1)
        {
            SpiderArmor.SetActive(true);
            if (cw.CurrentArmor != 1||SpiderArmorToken>1)
            {
                SpiderArmor_.SetActive(true);
            }
            else
            {
                SpiderArmor_.SetActive(false);
            }
        }
        else
        {
            SpiderArmor_.SetActive(false);
            SpiderArmor.SetActive(false);
        }
        if (IlluArmorToken >= 1)
        {
            IlluArmor.SetActive(true);
            if (cw.CurrentArmor != 2|| IlluArmorToken>1)
            {
                IlluArmor_.SetActive(true);
            }
            else
            {
                IlluArmor_.SetActive(false);
            }
        }
        else
        {
            IlluArmor_.SetActive(false);
            IlluArmor.SetActive(false);
        }
        if (RoidArmorToken >= 1)
        {
            RoidArmor.SetActive(true);
            if (cw.CurrentArmor != 3|| RoidArmorToken>1)
            {
                RoidArmor_.SetActive(true);
            }
            else
            {
                RoidArmor_.SetActive(false);
            }
        }
        else
        {
            RoidArmor_.SetActive(false);
            RoidArmor.SetActive(false);
        }
        if (IKArmorToken >= 1)
        {
            IKArmor.SetActive(true);
            if (cw.CurrentArmor != 4|| IKArmorToken>1)
            {
                IKArmor_.SetActive(true);
            }
            else
            {
                IKArmor_.SetActive(false);
            }
        }
        else
        {
            IKArmor_.SetActive(false);
            IKArmor.SetActive(false);
        }
        if (BlobArmorToken >= 1)
        {
            BlobArmor.SetActive(true);
            if (cw.CurrentArmor != 5|| BlobArmorToken>1)
            {
                BlobArmor_.SetActive(true);
            }
            else
            {
                BlobArmor_.SetActive(false);
            }
        }
        else
        {
            BlobArmor_.SetActive(false);
            BlobArmor.SetActive(false);
        }
        Item1.text = SpiderArmorToken.ToString();
        Item2.text = IlluArmorToken.ToString();
        Item3.text = RoidArmorToken.ToString();
        Item4.text = IKArmorToken.ToString();
        Item5.text = BlobArmorToken.ToString();
        Item6.text = SpiderWeaponToken.ToString();
        Item7.text = BlinkWeaponToken.ToString();
        Item8.text = FireWeaponToken.ToString();
        Item9.text = IKWeaponToken.ToString();
        Item10.text = BlobWeaponToken.ToString();



    }

    public void ShowEscMenu()
    {
        EscMenu.SetActive(show = show ? false : true);
        if (show)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = SetTime;
        }

    }

    public void ToggleLargeMiniMap()
    {
        ToggleBoolLargeCamera = !ToggleBoolLargeCamera;
        MiniMapLargeCamera.SetActive(ToggleBoolLargeCamera);
        MiniMapLargePanel.SetActive(ToggleBoolLargeCamera);
    }


    public void EscButton()
    {
        Application.Quit();
    }
    public void RestartButton()
    {
        Time.timeScale = SetTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ContinueAfterDeath()
    {
        if (Lives > 0)
        {
            MG.CurrentLevel--;
            MG.maxRoom--;
            MG.NextLevel();
            Player_.Continue();
            MainCamera.GetComponent<CamController>().zLevel = -26;
            MainCamera.GetComponent<CamController>().zLevel2 = -26;
            MainCamera.GetComponent<CamController>().xLimit1 = 0;
            MainCamera.GetComponent<CamController>().xLimit2 = 0;
            MainCamera.GetComponent<CamController>().zTest = -22;
            MiniCamera.transform.position = new Vector3(-1000, 100, 0);
            Lives--;
            CurrentLivesText.text = "Lives: " + Lives.ToString();
            DeathScreenLivesText.text = "Continue (" + Lives.ToString() + ")";

            BossHealth.transform.GetChild(0).gameObject.SetActive(false);
            BossHealth.transform.GetChild(1).gameObject.SetActive(false);
            BossHealth.transform.GetChild(2).gameObject.SetActive(false);
            BossHealth.transform.GetChild(3).gameObject.SetActive(false);

            if (Lives == 0)
            {
                DeathScreenLivesText.text = "Game Over";
            }
        }
        else
        {
            NotEnoughGoldSound.Play();
        }

    }


    public void CurrentTokens()
    {
        meteorText.text = meteorToken.ToString();
        coneText.text = coneToken.ToString();
        ghostText.text = ghostToken.ToString();
        doubleText.text = doubleToken.ToString();
        splitText.text = splitToken.ToString();
        channelingText.text = channelingToken.ToString();
        boostText.text = boostToken.ToString();
        hastenText.text = hastenToken.ToString();
        empowerText.text = empowerToken.ToString();
        bhText.text = bhToken.ToString();
        pushText.text = pushToken.ToString();
        poolText.text = poolToken.ToString();
        chaosText.text = ChaosToken.ToString();
        compText.text = CompToken.ToString();
        aimText.text = AimToken.ToString();

        meteorText_.text = meteorToken.ToString();
        coneText_.text = coneToken.ToString();
        ghostText_.text = ghostToken.ToString();
        doubleText_.text = doubleToken.ToString();
        splitText_.text = splitToken.ToString();
        channelingText_.text = channelingToken.ToString();
        boostText_.text = boostToken.ToString();
        hastenText_.text = hastenToken.ToString();
        empowerText_.text = empowerToken.ToString();
        bhText_.text = bhToken.ToString();
        pushText_.text = pushToken.ToString();
        poolText_.text = poolToken.ToString();
        chaosText_.text = ChaosToken.ToString();
        compText_.text = CompToken.ToString();
        aimText_.text = AimToken.ToString();

    }

    public void OpenSellTokenMenu()
    {
        ShowSellWindowBool = !ShowSellWindowBool;
        SellTokenWindow.SetActive(ShowSellWindowBool);
        //CloseShopWindow.Play();

        ShowSellItemWindowBool = false;
        SellItemWindow.SetActive(ShowSellItemWindowBool);
    }

    public void OpenSellItemMenu()
    {
        ShowSellItemWindowBool = !ShowSellItemWindowBool;
        SellItemWindow.SetActive(ShowSellItemWindowBool);
       // CloseShopWindow.Play();
        PickedUpItem();
        ShowSellWindowBool = false;
        SellTokenWindow.SetActive(ShowSellWindowBool);
    }

    public void ForceCloseShopWhenOtherButtonsAreClicked()
    {
        SellTokenWindow.SetActive(false);
        SellItemWindow.SetActive(false);
        ShowSellItemWindowBool = false;
        ShowSellWindowBool = false;
    }

    public void SellSpellToken(int spell)
    {
        switch (spell)
        {
            case 1:
                if (meteorToken > 0)
                {
                    meteorToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 2:
                if (coneToken > 0)
                {
                    coneToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 3:
                if (ghostToken > 0)
                {
                    ghostToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;

            case 21:
                if (doubleToken > 0)
                {
                    doubleToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 22:
                if (splitToken > 0)
                {
                    splitToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 23:
                if (CompToken > 0)
                {
                    CompToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;

            case 31:
                if (boostToken > 0)
                {
                    boostToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 32:
                if (hastenToken > 0)
                {
                    hastenToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 33:
                if (empowerToken > 0)
                {
                    empowerToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 41:
                if (bhToken > 0)
                {
                    bhToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 42:
                if (pushToken > 0)
                {
                    pushToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 43:
                if (poolToken > 0)
                {
                    poolToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;

            case 51:
                if (ChaosToken > 0)
                {
                    ChaosToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 52:
                if (channelingToken > 0)
                {
                    channelingToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
            case 53:
                if (AimToken > 0)
                {
                    AimToken--;
                    GainGold(SellSpellGold);
                    SellTokenSuccess = true;
                }
                else SellTokenSuccess = false;
                break;
        }

        if (SellTokenSuccess)
        {
            SellTokenSound.Play();
        }
        else
        {
            NotEnoughGoldSound.Play();
        }

        PickedUpItem();
    }

    public void SellItem(int Item)
    {
        switch (Item)
        {
            case 1:
                SpiderArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (SpiderArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 2:
                IlluArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (IlluArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 3:
                RoidArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (RoidArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 4:
                IKArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (IKArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 5:
                BlobArmorToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (BlobArmor_.activeSelf == false)
                {
                    tts.CloseArmor();
                }
                break;
            case 6:
                SpiderWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (SpiderWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 7:
                BlinkWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (BlinkWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 8:
                FireWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (FireWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 9:
                IKWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (IKWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
            case 10:
                BlobWeaponToken--;
                GainGold(SellItemGold);
                PickedUpItem();
                if (BlobWeapon_.activeSelf == false)
                {
                    tts.CloseWeapon();
                }
                break;
        }
        SellTokenSound.Play();
    }


    public void CheckIfTokenBought(int slot, int tokenlevel)
    {
        SlotIsInUse1 = false;
        SlotIsInUse2 = false;
        SlotIsInUse3 = false;
        switch (slot)
        {
            case 1:
                switch (tokenlevel)
                {
                    case 1:
                        if (meteorToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (coneToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (ghostToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 2:
                        if (doubleToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (splitToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (CompToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 3:
                        if (boostToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (hastenToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (empowerToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 4:
                        if (bhToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (pushToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (poolToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 5:
                        if (ChaosToken_)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (channelingToken_)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (AimToken_)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                }
                break;
            case 2:
                switch (tokenlevel)
                {
                    case 1:
                        if (meteorToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (coneToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (ghostToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 2:
                        if (doubleToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (splitToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (CompToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 3:
                        if (boostToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (hastenToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (empowerToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 4:
                        if (bhToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (pushToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (poolToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 5:
                        if (ChaosToken_2)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (channelingToken_2)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (AimToken_2)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                }
                break;
            case 3:
                switch (tokenlevel)
                {
                    case 1:
                        if (meteorToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (coneToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (ghostToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 2:
                        if (doubleToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (splitToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (CompToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 3:
                        if (boostToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (hastenToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (empowerToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 4:
                        if (bhToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (pushToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (poolToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                    case 5:
                        if (ChaosToken_3)
                        {
                            SlotIsInUse1 = true;
                        }
                        if (channelingToken_3)
                        {
                            SlotIsInUse2 = true;
                        }
                        if (AimToken_3)
                        {
                            SlotIsInUse3 = true;
                        }
                        break;
                }
                break;
        }
    }


    public void BuyToken(int SpellType)
    {
        Spellbook spellb = Spellbook.FindObjectOfType<Spellbook>();
        switch (spellb.curSlot)
        {
            case 1:

                switch (SpellType)
                {
                    case 1:

                        if (!meteorToken_)
                        {
                            meteorToken--;
                            meteorToken_ = true;

                            if (coneToken_)
                            {
                                coneToken++;
                                coneToken_ = false;

                            }
                            if (ghostToken_)
                            {
                                ghostToken++;
                                ghostToken_ = false;
                            }
                        }
                        else
                        {
                            meteorToken++;
                            meteorToken_ = false;
                            spellb.MeteorBlocked.enabled = false;
                        }


                        break;
                    case 2:

                        if (!coneToken_)
                        {
                            coneToken--;
                            coneToken_ = true;
                            if (meteorToken_)
                            {
                                meteorToken++;
                                meteorToken_ = false;
                            }
                            if (ghostToken_)
                            {
                                ghostToken++;
                                ghostToken_ = false;
                            }
                        }
                        else
                        {
                            coneToken++;
                            coneToken_ = false;
                            spellb.ConeBlocked.enabled = false;
                        }
                        break;

                    case 3:
                        if (!ghostToken_)
                        {
                            ghostToken--;
                            ghostToken_ = true;
                            if (meteorToken_)
                            {
                                meteorToken++;
                                meteorToken_ = false;
                            }
                            if (coneToken_)
                            {
                                coneToken++;
                                coneToken_ = false;
                            }
                        }
                        else
                        {
                            ghostToken++;
                            ghostToken_ = false;
                            spellb.GhostCastBlocked.enabled = false;
                        }
                        break;
                    case 4:
                        if (!doubleToken_)
                        {
                            doubleToken--;
                            doubleToken_ = true;
                            if (splitToken_)
                            {
                                splitToken++;
                                splitToken_ = false;
                            }
                            if (CompToken_)
                            {
                                CompToken++;
                                CompToken_ = false;
                            }
                        }
                        else
                        {
                            doubleToken++;
                            doubleToken_ = false;
                            spellb.DoubleCastBlocked.enabled = false;
                        }
                        break;
                    case 5:
                        if (!splitToken_)
                        {
                            splitToken--;
                            splitToken_ = true;
                            if (doubleToken_)
                            {
                                doubleToken++;
                                doubleToken_ = false;
                            }
                            if (CompToken_)
                            {
                                CompToken++;
                                CompToken_ = false;
                            }
                        }
                        else
                        {
                            splitToken++;
                            splitToken_ = false;
                            spellb.SplitCastBlocked.enabled = false;
                        }
                        break;
                    case 6:
                        if (!CompToken_)
                        {
                            CompToken--;
                            CompToken_ = true;
                            if (splitToken_)
                            {
                                splitToken++;
                                splitToken_ = false;
                            }
                            if (doubleToken_)
                            {
                                doubleToken++;
                                doubleToken_ = false;
                            }
                        }
                        else
                        {
                            CompToken++;
                            CompToken_ = false;
                            spellb.CompOrbBlocked.enabled = false;
                        }
                        break;
                    case 7:
                        if (!boostToken_)
                        {
                            boostToken--;
                            boostToken_ = true;
                            if (hastenToken_)
                            {
                                hastenToken++;
                                hastenToken_ = false;
                            }
                            if (empowerToken_)
                            {
                                empowerToken++;
                                empowerToken_ = false;
                            }
                        }
                        else
                        {
                            boostToken++;
                            boostToken_ = false;
                            spellb.BoostBlocked.enabled = false;
                        }
                        break;
                    case 8:
                        if (!hastenToken_)
                        {
                            hastenToken--;
                            hastenToken_ = true;
                            if (boostToken_)
                            {
                                boostToken++;
                                boostToken_ = false;
                            }
                            if (empowerToken_)
                            {
                                empowerToken++;
                                empowerToken_ = false;
                            }
                        }
                        else
                        {
                            hastenToken++;
                            hastenToken_ = false;
                            spellb.HastenBlocked.enabled = false;
                        }
                        break;
                    case 9:
                        if (!empowerToken_)
                        {
                            empowerToken--;
                            empowerToken_ = true;
                            if (hastenToken_)
                            {
                                hastenToken++;
                                hastenToken_ = false;
                            }
                            if (boostToken_)
                            {
                                boostToken++;
                                boostToken_ = false;
                            }
                        }
                        else
                        {
                            empowerToken++;
                            empowerToken_ = false;
                            spellb.EmpowerBlocked.enabled = false;
                        }
                        break;
                    case 10: //START
                        if (!bhToken_)
                        {
                            bhToken--;
                            bhToken_ = true;
                            if (pushToken_)
                            {
                                pushToken++;
                                pushToken_ = false;
                            }
                            if (poolToken_)
                            {
                                poolToken++;
                                poolToken_ = false;
                            }
                        }
                        else
                        {
                            bhToken++;
                            bhToken_ = false;
                            spellb.BHBlocked.enabled = false;
                        }
                        break;
                    case 11:
                        if (!pushToken_)
                        {
                            pushToken--;
                            pushToken_ = true;
                            if (bhToken_)
                            {
                                bhToken++;
                                bhToken_ = false;
                            }
                            if (poolToken_)
                            {
                                poolToken++;
                                poolToken_ = false;
                            }
                        }
                        else
                        {
                            pushToken++;
                            pushToken_ = false;
                            spellb.PushBlocked.enabled = false;
                        }
                        break;
                    case 12:
                        if (!poolToken_)
                        {
                            poolToken--;
                            poolToken_ = true;
                            if (pushToken_)
                            {
                                pushToken++;
                                pushToken_ = false;
                            }
                            if (bhToken_)
                            {
                                bhToken++;
                                bhToken_ = false;
                            }
                        }
                        else
                        {
                            poolToken++;
                            poolToken_ = false;
                            spellb.PoolBlocked.enabled = false;
                        }
                        break;
                    // START
                    case 13:
                        if (!ChaosToken_)
                        {
                            ChaosToken--;
                            ChaosToken_ = true;
                            if (channelingToken_)
                            {
                                channelingToken++;
                                channelingToken_ = false;
                            }
                            if (AimToken_)
                            {
                                AimToken++;
                                AimToken_ = false;
                            }
                        }
                        else
                        {
                            ChaosToken++;
                            ChaosToken_ = false;
                            spellb.ChaosOrbBlocked.enabled = false;
                        }
                        break;
                    case 14:
                        if (!channelingToken_)
                        {
                            channelingToken--;
                            channelingToken_ = true;
                            if (ChaosToken_)
                            {
                                ChaosToken++;
                                ChaosToken_ = false;
                            }
                            if (AimToken_)
                            {
                                AimToken++;
                                AimToken_ = false;
                            }
                        }
                        else
                        {
                            channelingToken++;
                            channelingToken_ = false;
                            spellb.ChannelingBlocked.enabled = false;
                        }
                        break;
                    case 15:
                        if (!AimToken_)
                        {
                            AimToken--;
                            AimToken_ = true;
                            if (channelingToken_)
                            {
                                channelingToken++;
                                channelingToken_ = false;
                            }
                            if (ChaosToken_)
                            {
                                ChaosToken++;
                                ChaosToken_ = false;
                            }
                        }
                        else
                        {
                            AimToken++;
                            AimToken_ = false;
                            spellb.BlessedAimBlocked.enabled = false;
                        }
                        break;
                }
                break;

            case 2:

                switch (SpellType)
                {
                    case 1:

                        if (!meteorToken_2)
                        {
                            meteorToken--;
                            meteorToken_2 = true;

                            if (coneToken_2)
                            {
                                coneToken++;
                                coneToken_2 = false;

                            }
                            if (ghostToken_2)
                            {
                                ghostToken++;
                                ghostToken_2 = false;
                            }
                        }
                        else
                        {
                            meteorToken++;
                            meteorToken_2 = false;
                            spellb.MeteorBlocked.enabled = false;
                        }


                        break;
                    case 2:

                        if (!coneToken_2)
                        {
                            coneToken--;
                            coneToken_2 = true;
                            if (meteorToken_2)
                            {
                                meteorToken++;
                                meteorToken_2 = false;
                            }
                            if (ghostToken_2)
                            {
                                ghostToken++;
                                ghostToken_2 = false;
                            }
                        }
                        else
                        {
                            coneToken++;
                            coneToken_2 = false;
                            spellb.ConeBlocked.enabled = false;
                        }
                        break;

                    case 3:
                        if (!ghostToken_2)
                        {
                            ghostToken--;
                            ghostToken_2 = true;
                            if (meteorToken_2)
                            {
                                meteorToken++;
                                meteorToken_2 = false;
                            }
                            if (coneToken_2)
                            {
                                coneToken++;
                                coneToken_2 = false;
                            }
                        }
                        else
                        {
                            ghostToken++;
                            ghostToken_2 = false;
                            spellb.GhostCastBlocked.enabled = false;
                        }
                        break;
                    case 4:
                        if (!doubleToken_2)
                        {
                            doubleToken--;
                            doubleToken_2 = true;
                            if (splitToken_2)
                            {
                                splitToken++;
                                splitToken_2 = false;
                            }
                            if (CompToken_2)
                            {
                                CompToken++;
                                CompToken_2 = false;
                            }
                        }
                        else
                        {
                            doubleToken++;
                            doubleToken_2 = false;
                            spellb.DoubleCastBlocked.enabled = false;
                        }
                        break;
                    case 5:
                        if (!splitToken_2)
                        {
                            splitToken--;
                            splitToken_2 = true;
                            if (doubleToken_2)
                            {
                                doubleToken++;
                                doubleToken_2 = false;
                            }
                            if (CompToken_2)
                            {
                                CompToken++;
                                CompToken_2 = false;
                            }
                        }
                        else
                        {
                            splitToken++;
                            splitToken_2 = false;
                            spellb.SplitCastBlocked.enabled = false;
                        }
                        break;
                    case 6:
                        if (!CompToken_2)
                        {
                            CompToken--;
                            CompToken_2 = true;
                            if (splitToken_2)
                            {
                                splitToken++;
                                splitToken_2 = false;
                            }
                            if (doubleToken_2)
                            {
                                doubleToken++;
                                doubleToken_2 = false;
                            }
                        }
                        else
                        {
                            CompToken++;
                            CompToken_2 = false;
                            spellb.CompOrbBlocked.enabled = false;
                        }
                        break;
                    case 7: //START
                        if (!boostToken_2)
                        {
                            boostToken--;
                            boostToken_2 = true;
                            if (hastenToken_2)
                            {
                                hastenToken++;
                                hastenToken_2 = false;
                            }
                            if (empowerToken_2)
                            {
                                empowerToken++;
                                empowerToken_2 = false;
                            }
                        }
                        else
                        {
                            boostToken++;
                            boostToken_2 = false;
                            spellb.BoostBlocked.enabled = false;
                        }
                        break;
                    case 8:
                        if (!hastenToken_2)
                        {
                            hastenToken--;
                            hastenToken_2 = true;
                            if (boostToken_2)
                            {
                                boostToken++;
                                boostToken_2 = false;
                            }
                            if (empowerToken_2)
                            {
                                empowerToken++;
                                empowerToken_2 = false;
                            }
                        }
                        else
                        {
                            hastenToken++;
                            hastenToken_2 = false;
                            spellb.HastenBlocked.enabled = false;
                        }
                        break;
                    case 9:
                        if (!empowerToken_2)
                        {
                            empowerToken--;
                            empowerToken_2 = true;
                            if (hastenToken_2)
                            {
                                hastenToken++;
                                hastenToken_2 = false;
                            }
                            if (boostToken_2)
                            {
                                boostToken++;
                                boostToken_2 = false;
                            }
                        }
                        else
                        {
                            empowerToken++;
                            empowerToken_2 = false;
                            spellb.EmpowerBlocked.enabled = false;
                        }
                        break;
                    case 10: //START
                        if (!bhToken_2)
                        {
                            bhToken--;
                            bhToken_2 = true;
                            if (pushToken_2)
                            {
                                pushToken++;
                                pushToken_2 = false;
                            }
                            if (poolToken_2)
                            {
                                poolToken++;
                                poolToken_2 = false;
                            }
                        }
                        else
                        {
                            bhToken++;
                            bhToken_2 = false;
                            spellb.BHBlocked.enabled = false;
                        }
                        break;
                    case 11:
                        if (!pushToken_2)
                        {
                            pushToken--;
                            pushToken_2 = true;
                            if (bhToken_2)
                            {
                                bhToken++;
                                bhToken_2 = false;
                            }
                            if (poolToken_2)
                            {
                                poolToken++;
                                poolToken_2 = false;
                            }
                        }
                        else
                        {
                            pushToken++;
                            pushToken_2 = false;
                            spellb.PushBlocked.enabled = false;
                        }
                        break;
                    case 12:
                        if (!poolToken_2)
                        {
                            poolToken--;
                            poolToken_2 = true;
                            if (pushToken_2)
                            {
                                pushToken++;
                                pushToken_2 = false;
                            }
                            if (bhToken_2)
                            {
                                bhToken++;
                                bhToken_2 = false;
                            }
                        }
                        else
                        {
                            poolToken++;
                            poolToken_2 = false;
                            spellb.PoolBlocked.enabled = false;
                        }
                        break;
                    // START
                    case 13:
                        if (!ChaosToken_2)
                        {
                            ChaosToken--;
                            ChaosToken_2 = true;
                            if (channelingToken_2)
                            {
                                channelingToken++;
                                channelingToken_2 = false;
                            }
                            if (AimToken_2)
                            {
                                AimToken++;
                                AimToken_2 = false;
                            }
                        }
                        else
                        {
                            ChaosToken++;
                            ChaosToken_2 = false;
                            spellb.ChaosOrbBlocked.enabled = false;
                        }
                        break;
                    case 14:
                        if (!channelingToken_2)
                        {
                            channelingToken--;
                            channelingToken_2 = true;
                            if (ChaosToken_2)
                            {
                                ChaosToken++;
                                ChaosToken_2 = false;
                            }
                            if (AimToken_2)
                            {
                                AimToken++;
                                AimToken_2 = false;
                            }
                        }
                        else
                        {
                            channelingToken++;
                            channelingToken_2 = false;
                            spellb.ChannelingBlocked.enabled = false;
                        }
                        break;
                    case 15:
                        if (!AimToken_2)
                        {
                            AimToken--;
                            AimToken_2 = true;
                            if (channelingToken_2)
                            {
                                channelingToken++;
                                channelingToken_2 = false;
                            }
                            if (ChaosToken_2)
                            {
                                ChaosToken++;
                                ChaosToken_2 = false;
                            }
                        }
                        else
                        {
                            AimToken++;
                            AimToken_2 = false;
                            spellb.BlessedAimBlocked.enabled = false;
                        }
                        break;
                }
                break;
            case 3:

                switch (SpellType)
                {
                    case 1:

                        if (!meteorToken_3)
                        {
                            meteorToken--;
                            meteorToken_3 = true;

                            if (coneToken_3)
                            {
                                coneToken++;
                                coneToken_3 = false;

                            }
                            if (ghostToken_3)
                            {
                                ghostToken++;
                                ghostToken_3 = false;
                            }
                        }
                        else
                        {
                            meteorToken++;
                            meteorToken_3 = false;
                            spellb.MeteorBlocked.enabled = false;
                        }


                        break;
                    case 2:

                        if (!coneToken_3)
                        {
                            coneToken--;
                            coneToken_3 = true;
                            if (meteorToken_3)
                            {
                                meteorToken++;
                                meteorToken_3 = false;
                            }
                            if (ghostToken_3)
                            {
                                ghostToken++;
                                ghostToken_3 = false;
                            }
                        }
                        else
                        {
                            coneToken++;
                            coneToken_3 = false;
                            spellb.ConeBlocked.enabled = false;
                        }
                        break;

                    case 3:
                        if (!ghostToken_3)
                        {
                            ghostToken--;
                            ghostToken_3 = true;
                            if (meteorToken_3)
                            {
                                meteorToken++;
                                meteorToken_3 = false;
                            }
                            if (coneToken_3)
                            {
                                coneToken++;
                                coneToken_3 = false;
                            }
                        }
                        else
                        {
                            ghostToken++;
                            ghostToken_3 = false;
                            spellb.GhostCastBlocked.enabled = false;
                        }
                        break;
                    case 4:
                        if (!doubleToken_3)
                        {
                            doubleToken--;
                            doubleToken_3 = true;
                            if (splitToken_3)
                            {
                                splitToken++;
                                splitToken_3 = false;
                            }
                            if (CompToken_3)
                            {
                                CompToken++;
                                CompToken_3 = false;
                            }
                        }
                        else
                        {
                            doubleToken++;
                            doubleToken_3 = false;
                            spellb.DoubleCastBlocked.enabled = false;
                        }
                        break;
                    case 5:
                        if (!splitToken_3)
                        {
                            splitToken--;
                            splitToken_3 = true;
                            if (doubleToken_3)
                            {
                                doubleToken++;
                                doubleToken_3 = false;
                            }
                            if (CompToken_3)
                            {
                                CompToken++;
                                CompToken_3 = false;
                            }
                        }
                        else
                        {
                            splitToken++;
                            splitToken_3 = false;
                            spellb.SplitCastBlocked.enabled = false;
                        }
                        break;
                    case 6:
                        if (!CompToken_3)
                        {
                            CompToken--;
                            CompToken_3 = true;
                            if (splitToken_3)
                            {
                                splitToken++;
                                splitToken_3 = false;
                            }
                            if (doubleToken_3)
                            {
                                doubleToken++;
                                doubleToken_3 = false;
                            }
                        }
                        else
                        {
                            CompToken++;
                            CompToken_3 = false;
                            spellb.CompOrbBlocked.enabled = false;
                        }
                        break;
                    case 7: //START
                        if (!boostToken_3)
                        {
                            boostToken--;
                            boostToken_3 = true;
                            if (hastenToken_3)
                            {
                                hastenToken++;
                                hastenToken_3 = false;
                            }
                            if (empowerToken_3)
                            {
                                empowerToken++;
                                empowerToken_3 = false;
                            }
                        }
                        else
                        {
                            boostToken++;
                            boostToken_3 = false;
                            spellb.BoostBlocked.enabled = false;
                        }
                        break;
                    case 8:
                        if (!hastenToken_3)
                        {
                            hastenToken--;
                            hastenToken_3 = true;
                            if (boostToken_3)
                            {
                                boostToken++;
                                boostToken_3 = false;
                            }
                            if (empowerToken_3)
                            {
                                empowerToken++;
                                empowerToken_3 = false;
                            }
                        }
                        else
                        {
                            hastenToken++;
                            hastenToken_3 = false;
                            spellb.HastenBlocked.enabled = false;
                        }
                        break;
                    case 9:
                        if (!empowerToken_3)
                        {
                            empowerToken--;
                            empowerToken_3 = true;
                            if (hastenToken_3)
                            {
                                hastenToken++;
                                hastenToken_3 = false;
                            }
                            if (boostToken_3)
                            {
                                boostToken++;
                                boostToken_3 = false;
                            }
                        }
                        else
                        {
                            empowerToken++;
                            empowerToken_3 = false;
                            spellb.EmpowerBlocked.enabled = false;
                        }
                        break;
                    case 10: //START
                        if (!bhToken_3)
                        {
                            bhToken--;
                            bhToken_3 = true;
                            if (pushToken_3)
                            {
                                pushToken++;
                                pushToken_3 = false;
                            }
                            if (poolToken_3)
                            {
                                poolToken++;
                                poolToken_3 = false;
                            }
                        }
                        else
                        {
                            bhToken++;
                            bhToken_3 = false;
                            spellb.BHBlocked.enabled = false;
                        }
                        break;
                    case 11:
                        if (!pushToken_3)
                        {
                            pushToken--;
                            pushToken_3 = true;
                            if (bhToken_3)
                            {
                                bhToken++;
                                bhToken_3 = false;
                            }
                            if (poolToken_3)
                            {
                                poolToken++;
                                poolToken_3 = false;
                            }
                        }
                        else
                        {
                            pushToken++;
                            pushToken_3 = false;
                            spellb.PushBlocked.enabled = false;
                        }
                        break;
                    case 12:
                        if (!poolToken_3)
                        {
                            poolToken--;
                            poolToken_3 = true;
                            if (pushToken_3)
                            {
                                pushToken++;
                                pushToken_3 = false;
                            }
                            if (bhToken_3)
                            {
                                bhToken++;
                                bhToken_3 = false;
                            }
                        }
                        else
                        {
                            poolToken++;
                            poolToken_3 = false;
                            spellb.PoolBlocked.enabled = false;
                        }
                        break;
                    // START
                    case 13:
                        if (!ChaosToken_3)
                        {
                            ChaosToken--;
                            ChaosToken_3 = true;
                            if (channelingToken_3)
                            {
                                channelingToken++;
                                channelingToken_3 = false;
                            }
                            if (AimToken_3)
                            {
                                AimToken++;
                                AimToken_3 = false;
                            }
                        }
                        else
                        {
                            ChaosToken++;
                            ChaosToken_3 = false;
                            spellb.ChaosOrbBlocked.enabled = false;
                        }
                        break;
                    case 14:
                        if (!channelingToken_3)
                        {
                            channelingToken--;
                            channelingToken_3 = true;
                            if (ChaosToken_3)
                            {
                                ChaosToken++;
                                ChaosToken_3 = false;
                            }
                            if (AimToken_3)
                            {
                                AimToken++;
                                AimToken_3 = false;
                            }
                        }
                        else
                        {
                            channelingToken++;
                            channelingToken_3 = false;
                            spellb.ChannelingBlocked.enabled = false;
                        }
                        break;
                    case 15:
                        if (!AimToken_3)
                        {
                            AimToken--;
                            AimToken_3 = true;
                            if (channelingToken_3)
                            {
                                channelingToken++;
                                channelingToken_3 = false;
                            }
                            if (ChaosToken_3)
                            {
                                ChaosToken++;
                                ChaosToken_3 = false;
                            }
                        }
                        else
                        {
                            AimToken++;
                            AimToken_3 = false;
                            spellb.BlessedAimBlocked.enabled = false;
                        }
                        break;
                }
                break;
        }
    }
    public void SpellsAndItems()
    {
        SpiderWeaponToken = 1;
        BlinkWeaponToken = 1;
        FireWeaponToken = 1;
        IKWeaponToken = 1;
        SpiderArmorToken = 1;
        IlluArmorToken = 1;
        RoidArmorToken = 1;
        IKArmorToken = 1;
        BlobArmorToken = 1;
        BlobWeaponToken = 1;

        meteorToken = 3;
        coneToken = 3;
        ghostToken = 3;
        doubleToken = 3;
        splitToken = 3;
        channelingToken = 3;
        boostToken = 3;
        hastenToken = 3;
        empowerToken = 3;
        bhToken = 3;
        pushToken = 3;
        poolToken = 3;
        ChaosToken = 3;
        CompToken = 3;
        AimToken = 3;
    }


}
