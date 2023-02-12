using UnityEngine;

public abstract class Load : MonoBehaviour 
{
    public string Unit => "Ohm";
    public abstract string CurrentElectricity { get; }
    public abstract float Resistance { get; }
}