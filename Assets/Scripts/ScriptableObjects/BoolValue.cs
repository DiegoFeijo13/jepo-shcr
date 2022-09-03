using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool InitialValue;

    [HideInInspector]
    public bool RunTimeValue;

    public void OnAfterDeserialize()
    {
        RunTimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}
