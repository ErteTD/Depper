using UnityEngine;

public abstract class MasterItem : MonoBehaviour {

    [TextArea]
    public string effect;
    public string itemname;
    public int itemID;
    public float cooldown;
    public GameObject ItemObject;
    public Color ItemColor;

    public string ItemClassName()
    {
        return GetType().Name;
    }
}
