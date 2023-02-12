public class Voltmeter : IDevice
{
    public string Unit => "Volt";
    public float MeasurementResult(float voltage, float totalResistance, float measuredResistance)
    {
        float result = voltage/totalResistance * measuredResistance;
        return result;
    }
}