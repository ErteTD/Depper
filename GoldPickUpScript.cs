using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class GoldPickUpScript : MonoBehaviour {
	ParticleSystem ps;
	ParticleSystem.Particle[] m_Particles;
	public Transform target;
	public float speed = 5f;
	int numParticlesAlive;
    private int MiniDist;
    private bool TriggerOnce;
    public int GoldAmount;
    public float GoldEffectDuration;
    private GameManager gm;
    private float goldDur;

    public AudioSource GoldPickUpSound;
    
	void Start () {
        gm = FindObjectOfType<GameManager>();

        ps = GetComponent<ParticleSystem>();
		if (!GetComponent<Transform>()){
			GetComponent<Transform>();
		}
        target = GameObject.Find("Player").transform;
        MiniDist = 10;

        GoldEffectDuration = 0.1f + (Mathf.InverseLerp(1, 50, GoldAmount) * 2);
        goldDur = (GoldEffectDuration*2) / GoldAmount;
        ps.Stop(true);
        var main = ps.main;

        main.duration = GoldEffectDuration;

        //    var MSS = ps.main.startSize.constantMin;
        var MinVal = Mathf.Clamp(GoldAmount/2, 2, 15);
        var MaxVal = Mathf.Clamp(GoldAmount/1.5f, 6, 30);

        main.startSize = new ParticleSystem.MinMaxCurve(MinVal, MaxVal);

        ps.Play(true);




    }
	void Update () {

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist < MiniDist)
        {
            m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
            numParticlesAlive = ps.GetParticles(m_Particles);
            float step = speed * Time.deltaTime;
            for (int i = 0; i < numParticlesAlive; i++)
            {
                m_Particles[i].position = Vector3.LerpUnclamped(m_Particles[i].position, target.position, step);
            }
            ps.SetParticles(m_Particles, numParticlesAlive);
            if (!TriggerOnce)
            {
                StopParticleSystem();
            }
        }
	}

    void StopParticleSystem()
    {
        StartCoroutine(GoldOverTime(goldDur, GoldAmount));
        TriggerOnce = true;
        MiniDist = 50;
        var main = ps.main;
        main.loop = false;
        ps.time = 0;
        Destroy(gameObject, (GoldEffectDuration*2) + 2);
    }


    IEnumerator GoldOverTime(float delay, int goldamount)
    {
        yield return new WaitForSeconds(delay);
        GoldPickUpSound.Play();
        gm.GainGold(1);
        if (goldamount > 0)
        {
            StartCoroutine(GoldOverTime(goldDur, goldamount-1));
        }
    }

}
