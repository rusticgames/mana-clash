using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

public class Pickupper : MonoBehaviour
{
    public bool running = true;
    public Collider2D grabCollider;
    public Joint2D attachJoint;
    public bool grabbing = false;
    private Platformer2DUserControl platformerControls;

    void Awake()
    {
        platformerControls = GetComponent<Platformer2DUserControl>();
    }

    void Start()
    {
        StartCoroutine(pickupCheck());
    }

    IEnumerator pickupCheck()
    {
        grabbing = false;
        yield return new WaitUntil(() => platformerControls.inputManager.isActionDown(InputManager.Actions.grab));
        grabbing = true;
        yield return new WaitWhile(() => platformerControls.inputManager.isAction(InputManager.Actions.grab));
        grabbing = false;
        if (grabCollider == null) StartCoroutine(pickupCheck());
    }

    IEnumerator dropCheck()
    {
        yield return new WaitUntil(() => platformerControls.inputManager.isActionDown(InputManager.Actions.grab));
        GameObject.Destroy(attachJoint);
        Vector3 projectilePos = gameObject.transform.position;
        projectilePos.x = projectilePos.x + platformerControls.lastFacingDirection;
        grabCollider.transform.position = projectilePos;
        grabCollider.isTrigger = false;
        var pickup = grabCollider.gameObject.GetComponent<Pickuppable>();
        pickup.OnDrop(gameObject.GetComponent<ProjectileLauncher>());
        grabCollider = null;
        yield return new WaitWhile(() => platformerControls.inputManager.isAction(InputManager.Actions.grab));
        StartCoroutine(pickupCheck());
    }
    
    void OnCollisionEnter2D(Collision2D coll)
{
    if (grabbing)
    {
        var pickup = coll.gameObject.GetComponent<Pickuppable>();
        if (pickup != null)
        {
            grabCollider = coll.collider;
            grabbing = false;
            coll.collider.transform.position = gameObject.transform.position;
            var joint = gameObject.AddComponent<DistanceJoint2D>();
            joint.enableCollision = false;
            coll.collider.isTrigger = true;
            joint.connectedBody = coll.collider.GetComponent<Rigidbody2D>();
            joint.distance = 0f;
            attachJoint = joint;
            StartCoroutine(dropCheck());
            pickup.OnPickup(gameObject.GetComponent<ProjectileLauncher>());
        }
        //coll.gameObject.SendMessage("ApplyDamage", 10);
    }

}
}
