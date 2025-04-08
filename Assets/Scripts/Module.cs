using UnityEngine;

public abstract class Module : MonoBehaviour
{
    public bool active = false;

    public void AddToBase(Turret_Base turretBase)
    {
        turretBase.AddModule(this);
    }

    public void ActivateBehavior()
    {
        active = true;
    }

    public void DeactivateBehavior()
    {
        active = false;
    }
}
