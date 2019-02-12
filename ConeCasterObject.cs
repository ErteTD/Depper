using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeCasterObject : MonoBehaviour {
    public GameObject currentspellObject;
    public float StartDelay;
    public bool Rotating;
    public float RotSpeed;
    private float RotAngel;
    public bool FireType;
    public bool FrostType;
    public float AttackTimer;
    private float AttackTimer_;
    private ParticleSystem BigShield;
    private ParticleSystem SmallShield;
    ParticleSystem.MainModule main;
    ParticleSystem.MainModule main2;

    private bool ChangeColor;
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private float t = 0;
    private bool flag;

    public Color colorIni = Color.yellow;
    public Color colorFin = Color.red;
    private Color lerpedColor;

    void Start () {
        if (!Rotating)
        {
            BigShield = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
            SmallShield = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            main = BigShield.main;
            main2 = SmallShield.main;
            if (transform.parent.name == "FrostCones")
            {
                colorIni = Color.white;
                colorFin = new Color32(0, 220, 255, 255);
            }
            main.startColor = Color.white;
            main2.startColor = Color.white;
            var speed1 = BigShield.colorBySpeed;
            var speed2 = SmallShield.colorBySpeed;
            speed1.enabled = true;
            speed2.enabled = true;
            gradient = new Gradient();
            colorKey = new GradientColorKey[2];
            colorKey[0].color = colorIni;
            colorKey[0].time = 0.0f;
            colorKey[1].color = colorFin;
            colorKey[1].time = 1.0f;
            alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 0.0f;
            alphaKey[1].time = 1.0f;
            gradient.SetKeys(colorKey, alphaKey);
            speed1.color = gradient;
            speed2.color = gradient;
            BigShield.Play();
            SmallShield.Play();
        }
        AttackTimer_ = StartDelay;
	}
	void Update () {
        AttackTimer_ -= Time.deltaTime;
        if (AttackTimer_ <= 0)
        {
            Shoot();
            AttackTimer_ = AttackTimer;
        }
        if (Rotating)
        {
            RotAngel += (RotSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, RotAngel, 0);
        }
        else if (AttackTimer_ <= 1)
        {
            StartChangingColor(colorIni, colorFin, false);
        }
        else if (!Rotating && AttackTimer_ > 1 && AttackTimer_ < AttackTimer - 0.6f)
        {
            StartChangingColor(colorIni, colorFin, true);
        }
    }

    void StartChangingColor(Color StartCol, Color EndCol, bool flagcol)
    {
        flag = flagcol;
            lerpedColor = Color.Lerp(StartCol, EndCol, t);
            colorKey[0].color = lerpedColor;
            gradient.SetKeys(colorKey, alphaKey);

            if (flag == true && t >= 0.01f)
            {
                t -= Time.deltaTime / 0.9f;
            }
            else if (t <= 0.99f)
            {
                t += Time.deltaTime / 0.9f;
            }
            var speed1 = BigShield.colorBySpeed;
            var speed2 = SmallShield.colorBySpeed;

        speed1.color = gradient;
        speed2.color = gradient;
    }

    void Shoot()
    {
        GameObject test123 = Instantiate(currentspellObject, transform.position, transform.rotation, transform);
        SpellProjectile spell = test123.GetComponent<SpellProjectile>();
        if (!Rotating)
        {
            spell.damage = 0.5f;
        }
        else
        {
            spell.damage = 0.2f;
        }
        spell.SlowDuration = 2;
        spell.SlowPercent = 1.25f;
        spell.BurnDuration = 2;
        spell.BurnPercent = 0.3f;
        spell.FireBallBurn = FireType;
        spell.FrostBoltSlow = FrostType;
        spell.cone = true;
        spell.enemyCastingspell = true;
        spell.FireTrailCone = true;
        ShapeCone(test123);
    }
    void ShapeCone(GameObject cone)
    {
        cone.GetComponent<BoxCollider>().center = new Vector3(0,0,-2);
        cone.GetComponent<BoxCollider>().size = new Vector3(3.5f, 3.5f, 0);
        StartCoroutine(ShapeConeNext(0.02f, 10, cone, -2, 0));
    }
    IEnumerator ShapeConeNext(float delay,int iterations, GameObject cone, float x1, float x2)
    {
        yield return new WaitForSeconds(delay);
        if (cone != null)
        {
            Next(cone, iterations, x1, x2);
        }
    }
    void Next(GameObject cone,int iterations, float x1, float x2)
    {
        x1 += 0.4f;
        if (iterations > 5)
        {
            x2 += 0.8f;
        }
        else
        {
            x2 -= 0.8f;
        }
        cone.GetComponent<BoxCollider>().center = new Vector3(0, 0, x1);
        cone.GetComponent<BoxCollider>().size = new Vector3(3.5f, 3.5f,x2);
        if (iterations > 1)
        {
            StartCoroutine(ShapeConeNext(0.11f, iterations - 1, cone, x1, x2));
        }
    }
}
