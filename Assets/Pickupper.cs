using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

public class Pickupper : MonoBehaviour {
    public bool running = true;
    public Collider2D grabCollider;
    public Joint2D attachJoint;
    public bool grabbing = false;
    private PlatformerCharacter2D platformerCharacter2D;

    public string inputId;

    void Reset()
    {
        inputId = "[" + gameObject.name + "] Grab";
    }
    // Use this for initialization
    void Start () {
        StartCoroutine(pickupCheck());
    }

    IEnumerator pickupCheck()
    {
        while (running)
        {
            grabbing = false;
            if (CrossPlatformInputManager.GetButtonDown(inputId))
            {
                while (CrossPlatformInputManager.GetButton(inputId))
                {
                    grabbing = true;
                    yield return null;
                }
                grabbing = false;
                if(grabCollider != null)
                {
                    break;
                }

            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator dropCheck()
    {
        while (running)
        {
            if (CrossPlatformInputManager.GetButtonDown(inputId))
            {
                GameObject.Destroy(attachJoint);

                var projectileDir = platformerCharacter2D.m_FacingRight ? 1 : -1;
                Vector3 projectilePos = gameObject.transform.position;
                projectilePos.x = projectilePos.x + projectileDir;
                grabCollider.transform.position = projectilePos;
                grabCollider.isTrigger = false;
                grabCollider = null;

                while (CrossPlatformInputManager.GetButton(inputId))
                    yield return null;
                StartCoroutine(pickupCheck());
                break;
            }
            yield return null;
        }
        yield return null;
    }

    void Awake()
    {
        platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
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
            }
            //coll.gameObject.SendMessage("ApplyDamage", 10);
        }

    }
}
