using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    public bool isCharger;
    public bool isKiller;
    public int projectileDir = 1;
    public float chargePower = 20.0f;
    public float chargedTime = 0f;
    public string inputId;

    private PlatformerCharacter2D platformerCharacter2D;
    
    void Awake()
    {
        inputId = "[" + gameObject.name + "] Fire";
        platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
    }

    void Update()
    {
        var projectileDir = platformerCharacter2D.m_FacingRight ? 1 : -1;

        if (isCharger && CrossPlatformInputManager.GetButton(inputId)) {
          chargedTime = chargedTime + Time.deltaTime;
        }

        if (CrossPlatformInputManager.GetButtonUp(inputId)) {
            Vector3 projectilePos = gameObject.transform.position;
            projectilePos.x = projectilePos.x + projectileDir;
            var forceBallObject = GameObject.Instantiate(projectile, projectilePos, Quaternion.identity) as GameObject;
            var forceBall = forceBallObject.GetComponent<ForceBall>();
            forceBall.isKiller = isKiller;
            forceBall.moveDir = projectileDir;


            if (isCharger) {
               forceBall.moveSpeed = chargedTime * chargePower;
               chargedTime = 0f;
            }
        }
  }
}