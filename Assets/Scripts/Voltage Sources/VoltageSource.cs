using UnityEngine;

public abstract class VoltageSource : MonoBehaviour
{
    public string Unit => "Volt";
    public abstract string CurrentElectricity { get; }
    public abstract float Voltage { get; }
}