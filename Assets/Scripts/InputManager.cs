using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    public enum Actions
    {
        moveLeft,
        moveRight,
        jump,
        grab,
        shoot,
        crouch,
	suicide
    }

    [SerializeField]
    public Dictionary<KeyCode, Actions> inputToActionMap;
    [SerializeField]
    public Dictionary<Actions, KeyCode> actionToInputMap;

    public bool useAlternateInput;

    void Awake()
    {
        inputToActionMap = new Dictionary<KeyCode, Actions>();
        actionToInputMap = new Dictionary<Actions, KeyCode>();
    
        if (!useAlternateInput) {
            inputToActionMap[KeyCode.LeftArrow]  = Actions.moveLeft;
            inputToActionMap[KeyCode.RightArrow] = Actions.moveRight;
            inputToActionMap[KeyCode.UpArrow]    = Actions.jump;
            inputToActionMap[KeyCode.RightShift] = Actions.grab;
            inputToActionMap[KeyCode.RightAlt]   = Actions.shoot;
            inputToActionMap[KeyCode.DownArrow]  = Actions.crouch;
            inputToActionMap[KeyCode.Delete]     = Actions.suicide;
        } else {
            inputToActionMap[KeyCode.A]     = Actions.moveLeft;
            inputToActionMap[KeyCode.D]     = Actions.moveRight;
            inputToActionMap[KeyCode.F]     = Actions.jump;
            inputToActionMap[KeyCode.W]     = Actions.grab;
            inputToActionMap[KeyCode.G]     = Actions.shoot;
            inputToActionMap[KeyCode.S]     = Actions.crouch;
            inputToActionMap[KeyCode.Q]     = Actions.suicide;
        }

        foreach (var item in inputToActionMap.Keys)
        {
            actionToInputMap[inputToActionMap[item]] = item;
        }
    }


    public bool isAction(Actions action)
    {
        return Input.GetKey(actionToInputMap[action]);
    }

    public bool isActionDown(Actions action)
    {
        return Input.GetKeyDown(actionToInputMap[action]);
    }

    public bool isActionUp(Actions action)
    {
        return Input.GetKeyUp(actionToInputMap[action]);
    }
}
