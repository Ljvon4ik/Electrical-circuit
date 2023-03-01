using UnityEngine;

public interface IDevice
{
    string Unit { get; }
    float MeasurementResult(float voltage, float totalResistance, float measuredResistance);
}