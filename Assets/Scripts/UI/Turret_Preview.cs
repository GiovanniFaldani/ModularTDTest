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
        FollowMouse();
        CheckBuildModule();
        CheckUndoBuildModule();
    }

    private void FollowMouse()
    {
        //Raycast mouse position X and Z
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.position = hit.point;
        }
    }

    private void CheckBuildModule()
    {
        if (Input.GetMouseButton(0) && !disableBuild)
        {
            if (turretSelected != null)
            {
                Turret_Builder.Instance.CreateModule(turretSelected, moduleType);
            }
            else if (!IsThisUpgrade())
            {
                Turret_Base turretBase = Turret_Builder.Instance.CreateBase(transform.position);
                Turret_Builder.Instance.CreateModule(turretBase, moduleType);
            }
            else
            {
                Debug.Log("Upgrade must be stacked on a shooter!");
                GameManager.Instance.playerMoney += Turret_Builder.Instance.costs[moduleType];
                UIUpdater.Instance.gameObject.SetActive(true);
            }
            // reenable UI
            UIUpdater.Instance.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void CheckUndoBuildModule()
    {
        if (Input.GetMouseButton(1))
        {
            // undo build
            GameManager.Instance.playerMoney += Turret_Builder.Instance.costs[moduleType];
            UIUpdater.Instance.gameObject.SetActive(true);
            Destroy(gameObject);
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

    private bool IsThisUpgrade()
    {
        // hardcode, ideally I'd build a dict to pair moduleType to the corresponding interface
        if ((moduleType == ModuleTypes.DamageUp) || (moduleType == ModuleTypes.FireRateUp) || (moduleType == ModuleTypes.RangeUp))
            return true;
        return false;
    }
}
