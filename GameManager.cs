using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public Texture2D DefCursor;
    public GameObject EscMenu;
    public static int minRoom_, maxRoom_, CurrentLevel_;
    public static bool StartLevel_, GiveLoot_;

    [Header("Inventory Weapon")]
    public GameObject SpiderWeapon;
    public int SpiderWeaponToken;
    public GameObject BlinkWeapon;
    public int BlinkWeaponToken;
    public GameObject FireWeapon;
    public int FireWeaponToken;
    public GameObject IKWeapon;
    public int IKWeaponToken;

    [Header("Inventory Armor")]
    public GameObject SpiderArmor;
    public int SpiderArmorToken;
    public GameObject IlluArmor;
    public int IlluArmorToken;
    public GameObject RoidArmor;
    public int RoidArmorToken;
    public GameObject IKArmor;
    public int IKArmorToken;


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
    public Text meteorText;
    public Text coneText;
    public Text ghostText;
    public Text doubleText;
    public Text splitText;
    public Text channelingText;
    public Text boostText;
    public Text hastenText;
    public Text empowerText;
    public Text bhText;
    public Text pushText;
    public Text poolText;

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


    void Start()
    {
        Vector2 asd = new Vector2(0, 0);
        MiniCamera = GameObject.Find("MiniMapCamera");
        Cursor.SetCursor(DefCursor, asd, CursorMode.Auto);
        SetTime = Time.timeScale;
    }
    // Update is called once per frame


    public void SetCurrentRoom(GameObject room) // not used currently
    {
        CurrentRoom = room;
    }
    public GameObject GetCurrentRoom() // not used currently
    {
        return CurrentRoom;
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

    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MiniCamera.transform.position += new Vector3(10f, 0, 0f) * Time.deltaTime;
            changeInX += 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MiniCamera.transform.position += new Vector3(-10f, 0, 0f) * Time.deltaTime;
            changeInX += -10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MiniCamera.transform.position += new Vector3(0, 0, 10f) * Time.deltaTime;
            changeInZ += 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MiniCamera.transform.position += new Vector3(0f, 0, -10f) * Time.deltaTime;
            changeInZ += -10 * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ShowHide2 = ShowHide2 ? false : true;
            ControlMenu.SetActive(ShowHide2);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            MiniCamera.transform.position -= new Vector3(changeInX, 0, changeInZ);
            changeInZ = 0;
            changeInX = 0;

        }

        // Reset scene with R
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}

        //if (Input.GetKeyDown(KeyCode.T)) //BUG currently, tooltip wont hide if active when this is hidden.
        //{
        //    ShowHide = ShowHide ? false : true;
        //    TokenTT1T.SetActive(false);
        //    TokenTT2T.SetActive(false);
        //    TokenTT3T.SetActive(false);
        //    TokenTT4T.SetActive(false);
        //    TokenTT5T.SetActive(false);
        //    TokenTT1.SetActive(ShowHide);
        //    TokenTT2.SetActive(ShowHide);
        //    TokenTT3.SetActive(ShowHide);
        //    TokenTT4.SetActive(ShowHide);
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();    

            ShowEscMenu();

        }
        CurrentTokens();





        //TokenStoken.
        if (SpiderWeaponToken >= 1)
        {
            SpiderWeapon.SetActive(true);
        }
        if (BlinkWeaponToken >= 1)
        {
            BlinkWeapon.SetActive(true);
        }
        if (FireWeaponToken >= 1)
        {
            FireWeapon.SetActive(true);
        }
        if (IKWeaponToken >= 1)
        {
            IKWeapon.SetActive(true);
        }
        //Armors
        if (SpiderArmorToken >= 1)
        {
            SpiderArmor.SetActive(true);
        }
        if (IlluArmorToken >= 1)
        {
            IlluArmor.SetActive(true);
        }
        if (RoidArmorToken >= 1)
        {
            RoidArmor.SetActive(true);
        }
        if (IKArmorToken >= 1)
        {
            IKArmor.SetActive(true);
        }
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

    public void EscButton()
    {
        Application.Quit();
    }
    public void RestartButton()
    {
        Time.timeScale = SetTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void CurrentTokens() // not used atm.
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
