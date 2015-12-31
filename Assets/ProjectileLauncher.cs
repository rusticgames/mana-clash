using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityStandardAssets._2D;

public class ProjectileLauncher : MonoBehaviour
{

    public GameObject projectile;
    public int projectileDir = 1;
    private PlatformerCharacter2D platformerCharacter2D;

    public string inputId;

    void Reset()
    {
        inputId = "[" + gameObject.name + "] Fire";
    }
    void Awake()
    {
        platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
    }

    void Update()
    {
        projectileDir = platformerCharacter2D.m_FacingRight ? 1 : -1;
        if (CrossPlatformInputManager.GetButtonDown(inputId))
        {
            Vector3 projectilePos = gameObject.transform.position;
            projectilePos.x = projectilePos.x + projectileDir;
            GameObject forceBall = GameObject.Instantiate(projectile, projectilePos, Quaternion.identity) as GameObject;
            forceBall.GetComponent<ForceBall>().moveDir = projectileDir;
        }
    }
}