using UnityEngine;

public interface IShooter
{
    public void Shoot();

    public void ModifyDamage(int multiplier);

    public void ModifyFireRate(float multiplier);

    public void ModifyRange(float multiplier);
}
