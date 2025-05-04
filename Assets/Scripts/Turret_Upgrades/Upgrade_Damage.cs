using UnityEngine;
using System.Collections.Generic;

public class Upgrade_Damage : Module, IUpgrade
{
    List<IShooter> shooters = new List<IShooter>();
    [SerializeField] private int damageModifier;

    public void ApplyUpgrade(Turret_Base turretBase)
    {
        shooters = turretBase.GetShooters();
        foreach(IShooter shooter in shooters)
        {
            shooter.ModifyDamage(damageModifier);
        }
    }
}
