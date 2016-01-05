using UnityEngine;
using System.Collections;

public class ForceBall : MonoBehaviour
{

    public float moveSpeed;
    public int moveDir;
    public bool isKiller;

    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
	m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update () 
    {
	m_Rigidbody2D.velocity = new Vector2(moveSpeed * moveDir, m_Rigidbody2D.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
	Debug.Log("I'm forceball");
	if (isKiller) GameObject.Destroy(coll.gameObject);
	GameObject.Destroy(gameObject, 0.1f);
    }
}
