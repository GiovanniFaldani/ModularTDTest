using UnityEngine;
using System.Collections.Generic;

// Singleton that handles call to build turrets in a given spot
public class Turret_Builder : MonoBehaviour
{
    [SerializeField] GameObject turretBasePrefab;
    [SerializeField] List<GameObject> stackablePrefabs = new List<GameObject>();

    private List<Turret_Base> turretBases = new List<Turret_Base>();
    private float moduleHeightIncrement = 2f;

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
        
    }

    void Update()
    {
        
    }

    public Turret_Base CreateBase(Vector3 position)
    {
        return Instantiate(turretBasePrefab, new Vector3(position.x, 0.3f, position.y), Quaternion.identity).GetComponent<Turret_Base>();
    }

    public IStackable CreateStack(Turret_Base turretBase, int stackIndex, int moduleIndex)
    {
        float yPos = 0.3f + (stackIndex+1) * moduleHeightIncrement;
        return Instantiate(stackablePrefabs[moduleIndex], turretBase.gameObject.transform).GetComponent<IStackable>();
    }

    public void InitializeTurret(Vector3 position, int moduleIndex)
    {
        IStackable toActivate = CreateStack(CreateBase(position), 0, moduleIndex);
        toActivate.ActivateBehavior();
    }
}
