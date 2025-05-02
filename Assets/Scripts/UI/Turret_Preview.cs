using UnityEngine;

public class Turret_Preview : MonoBehaviour
{

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material blockedMaterial;
    public ModuleTypes moduleType;

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
        FollowMouse();
        // TODO call builder
        CheckBuildTurret();
    }

    private void FollowMouse()
    {
        //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = new Vector3(hit.collider.transform.position.x, 0, hit.collider.transform.position.z);

        //Raycast mouse position X and Z
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            
            //transform.position = new Vector3(hit.point.x, 0.3f, hit.point.z);
            transform.position = hit.point;
        }
    }

    private void CheckBuildTurret()
    {
        if (Input.GetMouseButton(0) && !disableBuild)
        {
            if (turretSelected != null)
            {
                Turret_Builder.Instance.CreateModule(turretSelected, moduleType);
            }
            else
            {
                Turret_Base turretBase = Turret_Builder.Instance.CreateBase(transform.position);
                Turret_Builder.Instance.CreateModule(turretBase, moduleType);
            }
            // TODO reenable UI
            UIUpdater.Instance.gameObject.SetActive(true);
            Destroy(gameObject);
        }
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

    public void SetModuleType(ModuleTypes _moduleType)
    {
        moduleType = _moduleType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Path") || other.CompareTag("Goal"))
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Path") || other.CompareTag("Goal"))
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
