using UnityEngine;

public abstract class MasterSpell : MonoBehaviour
{
    [TextArea]
    public string effect;
    public string spellname;
    public float damagePure;
    public float damagePercent;
    public float cooldownSeconds;
    public float cooldownPercent;
    [Header("SpellSpecific")]
    public bool XXXXXXXXXXXXXXXX;

    public string SpellClassName()
    {
        return GetType().Name;
    }
}
