using UnityEngine;
using System.Collections.Generic;

public class Turret_Base : MonoBehaviour
{
    [SerializeField] private Module[] modules = {null, null, null}; // massimo 3 elementi stackabili
    private float moduleHeightIncrement = 2f; // how much higher to put a module compared to previous one

    public Turret_Base() { }

    public void AddModule(Module newModule)
    {
        for(int i = 0; i < modules.Length; i++)
        {
            if (modules[i] == null)
            {
                modules[i] = newModule;
                newModule.gameObject.transform.parent = transform;
                newModule.gameObject.transform.position = new Vector3(
                    this.transform.position.x,
                    this.transform.position.y - 1f + (i+1) * moduleHeightIncrement, // Trial and error position for module placement :)
                    this.transform.position.z
                    );
                if (newModule is IUpgrade)
                {
                    IUpgrade upgrade = newModule as IUpgrade;
                    upgrade.ApplyUpgrade(this);
                }
                break;
            }
        }
    }

    public void ActivateAllModules()
    {
        foreach(Module module in modules)
        {
            if(module != null) module.ActivateBehavior();
        }
    }

    public bool HasFreeSpace()
    {
        foreach(Module module in modules)
        {
            if (module == null) return true;
        }
        return false;
    }

    public bool HasShooters()
    {
        foreach (Module module in modules)
        {
            if (module is IShooter)
            {
                return true;
            }
        }
        return false;
    }

    public List<IShooter> GetShooters()
    {
        List<IShooter> shooters = new List<IShooter>();
        foreach (Module module in modules)
        {
            if (module is IShooter)
            {
                IShooter shooter = module as IShooter;
                shooters.Add(shooter);
            }
        }
        return shooters;
    }

}
