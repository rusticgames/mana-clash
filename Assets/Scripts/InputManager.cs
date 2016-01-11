using UnityEngine;
using System.Collections.Generic;
using System;

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
	    suicide,
        menu
    }

    public interface IGeneralInput
    {
        float getContinuousInput();
        bool isDiscreteInput();
        bool isDiscreteInputUp();
        bool isDiscreteInputDown();
    }

    public class DiscreteInput : IGeneralInput
    {
        private KeyCode k;
        public DiscreteInput(KeyCode key)
        {
            this.k = key;
        }
        float IGeneralInput.getContinuousInput()
        {
            return Input.GetKey(k) ? 1f : 0f;
        }

        bool IGeneralInput.isDiscreteInput()
        {
            return Input.GetKey(k);
        }

        bool IGeneralInput.isDiscreteInputDown()
        {
            return Input.GetKeyDown(k);
        }

        bool IGeneralInput.isDiscreteInputUp()
        {
            return Input.GetKeyUp(k);
        }
    }

    public class ContinuousInput : IGeneralInput
    {
        private string a;
        private bool n;
        private delegate float AxisReader();
        private AxisReader reader;
        public ContinuousInput(string axisName, bool negative)
        {
            this.a = axisName;
            this.reader = positiveReader;
            if(negative) this.reader = negativeReader;
        }

        private float positiveReader()
        {
            return Mathf.Clamp(Input.GetAxis(a), 0f, 1f);
        }

        private float negativeReader()
        {
            return - Mathf.Clamp(Input.GetAxis(a), -1f, 0f);
        }

        float IGeneralInput.getContinuousInput()
        {
            return reader();
        }

        bool IGeneralInput.isDiscreteInput()
        {
            return reader() > float.Epsilon;
        }

        bool IGeneralInput.isDiscreteInputDown()
        {
            Debug.LogWarning("calling isDiscreteInputDown on a continuous input. not necessarily wrong, but we never decided what we want to do in this case, so i'm just going to say it's not down");
            return false;
        }

        bool IGeneralInput.isDiscreteInputUp()
        {
            Debug.LogWarning("calling isDiscreteInputUp on a continuous input. not necessarily wrong, but we never decided what we want to do in this case, so i'm just going to say it's not up");
            return false;
        }
    }

    [SerializeField]
    public Dictionary<IGeneralInput, Actions> inputToActionMap;
    [SerializeField]
    public Dictionary<Actions, IGeneralInput> actionToInputMap;

    public bool useAlternateInput;
    public bool useGamepadInput;

    void Awake()
    {
        inputToActionMap = new Dictionary<IGeneralInput, Actions>();
        actionToInputMap = new Dictionary<Actions, IGeneralInput>();

        inputToActionMap[new DiscreteInput(KeyCode.LeftArrow)] = Actions.moveLeft;
        inputToActionMap[new DiscreteInput(KeyCode.RightArrow)] = Actions.moveRight;
        inputToActionMap[new DiscreteInput(KeyCode.UpArrow)] = Actions.jump;
        inputToActionMap[new DiscreteInput(KeyCode.RightShift)] = Actions.grab;
        inputToActionMap[new DiscreteInput(KeyCode.RightAlt)] = Actions.shoot;
        inputToActionMap[new DiscreteInput(KeyCode.DownArrow)] = Actions.crouch;
        inputToActionMap[new DiscreteInput(KeyCode.Delete)] = Actions.suicide;
        inputToActionMap[new DiscreteInput(KeyCode.Break)] = Actions.menu;
        if (useAlternateInput)
        {
            inputToActionMap[new DiscreteInput(KeyCode.A)] = Actions.moveLeft;
            inputToActionMap[new DiscreteInput(KeyCode.D)] = Actions.moveRight;
            inputToActionMap[new DiscreteInput(KeyCode.W)] = Actions.jump;
            inputToActionMap[new DiscreteInput(KeyCode.F)] = Actions.grab;
            inputToActionMap[new DiscreteInput(KeyCode.G)] = Actions.shoot;
            inputToActionMap[new DiscreteInput(KeyCode.S)] = Actions.crouch;
            inputToActionMap[new DiscreteInput(KeyCode.Q)] = Actions.suicide;
            inputToActionMap[new DiscreteInput(KeyCode.Escape)] = Actions.menu;
        }
        if (useGamepadInput)
        {
            inputToActionMap[new ContinuousInput("j1_0", true)] = Actions.moveLeft;
            inputToActionMap[new ContinuousInput("j1_0", false)] = Actions.moveRight;
            inputToActionMap[new DiscreteInput(KeyCode.JoystickButton2)] = Actions.jump;
            inputToActionMap[new DiscreteInput(KeyCode.JoystickButton3)] = Actions.grab;
            inputToActionMap[new DiscreteInput(KeyCode.JoystickButton4)] = Actions.shoot;
            inputToActionMap[new DiscreteInput(KeyCode.JoystickButton5)] = Actions.crouch;
            inputToActionMap[new DiscreteInput(KeyCode.JoystickButton6)] = Actions.suicide;
        }
        foreach (var item in inputToActionMap.Keys)
        {
            actionToInputMap[inputToActionMap[item]] = item;
        }
    }


    public bool isAction(Actions action)
    {
        return actionToInputMap[action].isDiscreteInput();
    }

    public bool isActionDown(Actions action)
    {
        return actionToInputMap[action].isDiscreteInput();
    }

    public bool isActionUp(Actions action)
    {
        return actionToInputMap[action].isDiscreteInput();
    }

    public float getContinuousAction(Actions action)
    {
        return actionToInputMap[action].getContinuousInput();
    }
}
