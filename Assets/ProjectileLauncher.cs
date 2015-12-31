using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityStandardAssets._2D;

public class ProjectileLauncher : MonoBehaviour
{

  public GameObject projectile;
  public bool isCharger;
  public int projectileDir = 1;
  public float chargePower = 20.0f;
  public float chargedTime = 0f;

  private PlatformerCharacter2D platformerCharacter2D;

  void Awake()
  {
    platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
  }

  void Update()
  {
    projectileDir = platformerCharacter2D.m_FacingRight ? 1 : -1;

    if (isCharger && CrossPlatformInputManager.GetButton("[" + gameObject.name + "] Fire")) {
      chargedTime = chargedTime + Time.deltaTime;
    }

    if (CrossPlatformInputManager.GetButtonUp("[" + gameObject.name + "] Fire")) {
      Vector3 projectilePos = gameObject.transform.position;
      projectilePos.x = projectilePos.x + projectileDir;
      GameObject forceBall = GameObject.Instantiate(projectile, projectilePos, Quaternion.identity) as GameObject;
      forceBall.GetComponent<ForceBall>().moveDir = projectileDir;

      if (isCharger) {
        forceBall.GetComponent<ForceBall>().moveSpeed = chargedTime * chargePower;
        chargedTime = 0f;
      }
    }
	}
}
