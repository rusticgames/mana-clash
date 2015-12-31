using UnityEngine;
using System.Collections;

public class Pickuppable : MonoBehaviour
{
    [System.Serializable]
    public enum ProjectileType
    {
        charge,
        kill
    }

    public ProjectileType type = ProjectileType.charge;
  
  public void OnPickup(ProjectileLauncher p)
  {
        if (type == ProjectileType.charge) {
            p.isCharger = true;   
        } else if (type == ProjectileType.kill) {
            p.isKiller = true;   
        }
  }

  public void OnDrop(ProjectileLauncher p)
  {
        if (type == ProjectileType.charge) {
            p.isCharger = false;   
        } else if (type == ProjectileType.kill) {
            p.isKiller = false;   
        }
  }
}
