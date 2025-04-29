using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Upgrade_FireRate : Module, IUpgrade
{
    List<IShooter> shooters = new List<IShooter>();
    [SerializeField] private float fireRateDivider;

    public void ApplyUpgrade(Turret_Base turretBase)
    {
        shooters = turretBase.GetShooters();
        foreach(IShooter shooter in shooters)
        {
            shooter.ModifyFireRate(fireRateDivider);
        }
    }
}
