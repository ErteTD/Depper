
using UnityEngine;

public class LoadScreen : MonoBehaviour {

    public Animator animator;
    private OneWayDoor door;

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


}
