using UnityEngine;

public class Turret_Base : MonoBehaviour
{
    private IStackable[] modules = new IStackable[3]; // massimo 3 elementi stackabili
    private bool disableBuild = false; // prevents building turrets on top of each other

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            other.gameObject.GetComponent<Turret_Base>().DisableBuild();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            other.gameObject.GetComponent<Turret_Base>().DisableBuild();
        }
    }

    public void DisableBuild()
    {
        disableBuild = true;
    }

    public void EnableBuild()
    {
        disableBuild = false;
    }

}
