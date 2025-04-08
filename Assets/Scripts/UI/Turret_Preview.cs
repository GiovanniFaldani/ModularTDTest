using UnityEngine;

public class Turret_Preview : MonoBehaviour
{

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material blockedMaterial;

    private bool disableBuild = false; // prevents building turrets on top of each other
    private Turret_Base turretSelected = null;
    private MeshRenderer mesh;

    void Start()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
    }


    void Update()
    {
        // TODO follow mouse on x and z axis
        // TODO update color if disablebuild
    }

    private void FollowMouse()
    {

    }

    public void Place(Vector3 position,ModuleTypes moduleType)
    {
        if (!disableBuild)
        {
            // TODO use initializeturret if turretSelected is null, otherwise add module
            // Turret_Builder.Instance.CreateBase();
            if(turretSelected == null)
            {
                Turret_Builder.Instance.InitializeTurret(position, moduleType);
            }
            else
            {
                Turret_Builder.Instance.CreateModule(turretSelected, moduleType);
            }
        }
        else
        {
            Debug.Log("Building blocked!");
        }
    }

    public void DisableBuild()
    {
        disableBuild = true;
        mesh.material = blockedMaterial;
    }

    public void EnableBuild()
    {
        disableBuild = false;
        mesh.material = defaultMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            DisableBuild();
        }
        else if (other.CompareTag("Base"))
        {
            if (other.GetComponent<Turret_Base>().HasFreeSpace())
            {
                EnableBuild();
                turretSelected = other.GetComponent<Turret_Base>();
            }
            else
            {
                DisableBuild();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            EnableBuild();
        }
        else if (other.CompareTag("Base"))
        {
            EnableBuild();
            turretSelected = null;
        }
    }
}
