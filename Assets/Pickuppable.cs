using UnityEngine;
using System.Collections;

public class Pickuppable : MonoBehaviour
{
  public void OnPickup(ProjectileLauncher p)
  {
    p.isCharger = true;   
  }

  public void OnDrop(ProjectileLauncher p)
  {
    p.isCharger = false;   
  }
}
