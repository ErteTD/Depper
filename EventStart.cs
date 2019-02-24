using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStart : MonoBehaviour {
    public GameObject Player_;
    public Light EventLight;
    public GameObject FadeEffect;
    public Room Room_;
    private bool clicked;
    private GameManager manag;
    // Use this for initialization
    void Start () {
        manag = GameManager.FindObjectOfType<GameManager>();

        Player_ = GameObject.Find("Player");

    }
	
	// Update is called once per frame
	void Update ()
    {

        float dist = Vector3.Distance(Player_.transform.position, transform.position);
        if (dist < 6 && clicked == true )
        {
            clicked = false;
            GameObject Fade = Instantiate(FadeEffect, transform.position, FadeEffect.transform.rotation, transform);
            Fade.transform.localPosition = new Vector3(0, 0.5f, 0);
            Fade.transform.parent = null;
            Destroy(Fade, 3);

            Room_.StartEvent();
        }
    }

    private void OnMouseOver()
    {
        manag.SelectCursor(true);
        EventLight.intensity = 10f;
        EventLight.range = 5f;
        if (Input.GetMouseButton(MenuScript.MouseMovement))
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
