using UnityEngine;

public interface IStackable
{
    public void AddToBase(Turret_Base turretBase);

    public void ActivateBehavior();

    public void DeactivateBehavior();
}
