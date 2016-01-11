using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DebugUtils : MonoBehaviour
{
    public bool watchKeyInputs;
    public bool watchJoystickInputs;
    public delegate string ItemProcessor(string s,string t);
    public delegate void LogWriter(string s);
    public ItemProcessor currentProcessor;
    public LogWriter currentWriter;
    public List<string> axisNames;

    void Awake()
    {
        axisNames = new List<string>();
        for (int j = 0; j < 11; j++)
        {
            for (int a = 0; a < 28; a++)
            {
                axisNames.Add("j" + (j + 1) + "_" + a);
            }
        }
    }

    string initialItemProcessor(string currentString, string nextItem)
    {
        currentWriter = Debug.Log;
        currentProcessor = standardItemProcessor;
        return nextItem;
    }

    string standardItemProcessor(string currentString, string nextItem)
    {
        return currentString + " | " + nextItem;
    }

    void Update()
    {
        if (watchKeyInputs)
        {
            currentProcessor = initialItemProcessor;
            currentWriter = (s) => { };
            string downKeys = "";
            foreach (KeyCode item in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(item))
                {
                    downKeys = currentProcessor(downKeys, item.ToString());
                }
            }
            currentWriter(downKeys);
        }
        if (watchJoystickInputs)
        {
            currentProcessor = initialItemProcessor;
            currentWriter = (s) => { };
            float t = 0;
            string movedAxes = "";
            foreach (var item in axisNames)
            {
                t = Input.GetAxis(item);
                if (t != 0) movedAxes = currentProcessor(movedAxes, item + ": " + t.ToString());
            }

            currentWriter(movedAxes);
        }
    }
}
