using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Upgrade_Range: Module, IUpgrade
{
    List<IShooter> shooters = new List<IShooter>();
    [SerializeField] private float rangeMultiplier;

    public void ApplyUpgrade(Turret_Base turretBase)
    {
        shooters = turretBase.GetShooters();
        foreach(IShooter shooter in shooters)
        {
            shooter.ModifyRange(rangeMultiplier);
        }
    }
}
