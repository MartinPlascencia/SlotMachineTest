using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlotMachineData", menuName = "ScriptableObjects/CreateSlotMachineData", order = 0)]
public class SlotMachineData : ScriptableObject
{
    public float DelayBetweenSpins = 0.3f;
    public float SpinTime = 3f;
}
