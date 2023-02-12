public class Ammeter : IDevice
{
    public string Unit => "Amper";
    public float MeasurementResult(float voltage, float totalResistance, float measuredResistance)
    {
        float result = voltage / measuredResistance;
        return result;
    }
}