
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour {

    public Animator animator;
    private OneWayDoor door;
    public Text Leveltxt;
    public Text ShowControlstxt;
    public void FadeToLevel(OneWayDoor currentdoor)
    {
        animator.SetTrigger("FadeOut");
        door = currentdoor;
    }

    public void OnFadeComplete()
    {
        animator.SetTrigger("FadeIn");
        door.EnterRoom();
    }

    public void FadeToDeath()
    {
        animator.SetTrigger("Wat");
    }
    public void FadeToLife()
    {
        animator.SetTrigger("wat2");
    }

    public void NewLevelText(string lvl, int level)
    {

        animator.Play("NewFadeIn", -1, 0);
        Leveltxt.text = lvl;
        if (level == 0)
        {
            ShowControlstxt.text = "(Press \"H\" for controls)";
        }
        else
        {
            ShowControlstxt.text = "";
        }
    }

}
