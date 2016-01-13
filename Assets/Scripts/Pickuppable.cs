using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class PickuppableUseEvent: UnityEvent<int>{ };
public class Pickuppable : MonoBehaviour
{
    public PickuppableUseEvent onUse;
    public UnityEvent onPickup;
    public UnityEvent onDrop;

}
