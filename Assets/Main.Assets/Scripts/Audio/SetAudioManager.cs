using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioManager : ScriptableObject ,ISerializationCallbackReceiver
{
    [SerializeField]private float _initiMaster;
    [NonSerialized] public float MasterVolume;

    [SerializeField]private float _initiBGMMaster;
    [NonSerialized] public float BGMMasterVolume;

    [SerializeField]private float _initiSEMaster;
    [NonSerialized]public float SEMasterVolume;

    public void OnBeforeSerialize() { }
    public void OnAfterDeserialize()
    {
        MasterVolume = _initiMaster;
        BGMMasterVolume = _initiBGMMaster;
        SEMasterVolume = _initiSEMaster;
    }
}

