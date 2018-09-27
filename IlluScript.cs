using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IlluScript : MonoBehaviour
{

    public float duration;
    private float Boom;
    public Image Durationbar;
    [HideInInspector] public float duration2;
    public GameObject CorpseExplosion;
    private bool OnlyOnce;

    // Use this for initialization
    void Start()
    {
        duration2 = duration;
    }

    // Update is called once per frame
    void Update()
    {

        duration -= Time.deltaTime;
        Durationbar.fillAmount = duration / duration2;

        if (duration <= 0 && !OnlyOnce)
        {
            OnlyOnce = true;
            Collider[] cols = Physics.OverlapSphere(transform.position, 6f);

            foreach (Collider c in cols)
            {
                Monster e = c.GetComponent<Monster>();
                if (e != null)
                {
                    e.GetComponent<Monster>().TakeDamage(2);
                }
            }
            GameObject Exp = Instantiate(CorpseExplosion, transform.position, transform.rotation, transform);
            Exp.transform.parent = null;
            Destroy(gameObject);
        }

    }

    public void TakeDamage(float damage) // not used currently
    {
        Boom += damage;
    }
}
