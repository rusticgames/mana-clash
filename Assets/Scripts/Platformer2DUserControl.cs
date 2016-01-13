using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformerCharacter2D))]
[RequireComponent(typeof(InputManager))]
public class Platformer2DUserControl : MonoBehaviour
{
    private PlatformerCharacter2D m_Character;
    private bool m_Jump;
    public InputManager inputManager;
    public int lastFacingDirection = 1;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (inputManager.isAction(InputManager.Actions.menu)) m_Character.Menu();
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            // m_Jump = CrossPlatformInputManager.GetButtonDown("[" + gameObject.name + "] Jump");
            m_Jump = inputManager.isActionDown(InputManager.Actions.jump);
        }

        if (inputManager.isAction(InputManager.Actions.suicide)) m_Character.Die();
				if (inputManager.isActionDown(InputManager.Actions.shoot)) m_Character.inventory.useHeldItems();
    }

    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = false;
        //float h = CrossPlatformInputManager.GetAxis("[" + gameObject.name + "] Horizontal");
        int h = 0;
        if (inputManager.isAction(InputManager.Actions.moveLeft)) h = -1;
        if (inputManager.isAction(InputManager.Actions.moveRight)) h = 1;
        lastFacingDirection = (h != 0) ? h : lastFacingDirection;
        // Pass all parameters to the character control script.
        m_Character.Move((float)h, crouch, m_Jump);
        m_Jump = false;
    }
}