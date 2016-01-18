using UnityEngine;

[RequireComponent(typeof(Pickuppable))]
public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;

    void Awake()
    {
        var p = gameObject.GetComponent<Pickuppable>(); 
        p.onUse.AddListener(Use);
    }

    public void Use(int direction)
    {
        Vector3 projectilePos = gameObject.transform.position;
        projectilePos.x = projectilePos.x + direction;
        var forceBallObject = GameObject.Instantiate(projectile, projectilePos, Quaternion.identity) as GameObject;
        var forceBall = forceBallObject.GetComponent<ForceBall>();
        forceBall.moveDir = direction;
    }
}