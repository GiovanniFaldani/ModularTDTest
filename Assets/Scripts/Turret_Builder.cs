using UnityEngine;
using System.Collections.Generic;

public enum ModuleTypes
{
    RapidFire,
    Flamethrower,
    AreaDamage,
    FireRateUp,
    DamageUp,
    DamageOverTime
}

// Singleton that handles call to build turrets in a given spot
public class Turret_Builder : MonoBehaviour
{
    [SerializeField] GameObject turretBasePrefab;
    [SerializeField] List<GameObject> modulePrefabs = new List<GameObject>();

    private List<Turret_Base> turretBases = new List<Turret_Base>();

    private static Turret_Builder instance;
    public static Turret_Builder Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // test methods
        Turret_Base turret_Base = CreateBase(new Vector3(0, 0, 0));
        CreateModule(turret_Base, ModuleTypes.RapidFire);
        CreateModule(turret_Base, ModuleTypes.RapidFire);
        CreateModule(turret_Base, ModuleTypes.RapidFire);
    }

    void Update()
    {
        
    }

    public Turret_Base CreateBase(Vector3 position)
    {
        Turret_Base created = Instantiate(turretBasePrefab, new Vector3(position.x, 0.3f, position.y), Quaternion.identity).GetComponent<Turret_Base>();
        turretBases.Add(created);
        return created;
    }

    public void CreateModule(Turret_Base turretBase, ModuleTypes moduleType)
    {
        Module module = Instantiate(modulePrefabs[(int)moduleType], turretBase.gameObject.transform).GetComponent<Module>();
        module.AddToBase(turretBase);
        turretBase.ActivateAllModules();
    }

    // Make a new turret with selected module
    public void InitializeTurret(Vector3 position, ModuleTypes moduleType)
    {
        CreateModule(CreateBase(position), moduleType);
    }
}
