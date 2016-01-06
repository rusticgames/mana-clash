using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    public bool isCharger;
    public bool isKiller;
    public float chargePower = 20.0f;
    public float chargedTime = 0f;

    private Platformer2DUserControl platformerControls;
    
    void Awake()
    {
        platformerControls = GetComponent<Platformer2DUserControl>();
    }

    void Update()
    {
        if (isCharger && platformerControls.inputManager.isAction(InputManager.Actions.shoot)) {
          chargedTime = chargedTime + Time.deltaTime;
        }

        if (platformerControls.inputManager.isActionUp(InputManager.Actions.shoot)) {
            Vector3 projectilePos = gameObject.transform.position;
            projectilePos.x = projectilePos.x + platformerControls.lastFacingDirection;
            var forceBallObject = GameObject.Instantiate(projectile, projectilePos, Quaternion.identity) as GameObject;
            var forceBall = forceBallObject.GetComponent<ForceBall>();
            forceBall.isKiller = isKiller;
            forceBall.moveDir = platformerControls.lastFacingDirection;


            if (isCharger) {
               forceBall.moveSpeed = chargedTime * chargePower;
               chargedTime = 0f;
            }
        }
  }
}