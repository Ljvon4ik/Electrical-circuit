using UnityEngine;

public class ElectricalСircuit : MonoBehaviour
{
    public GameObject[] Loads { get; private set; }
    GameObject VoltageSource { get; set; }
    GameObject CircuitClosingWire { get; set; }
    public GameObject CircuitSwitch { get; private set; }

    private void Awake()
    {
        Loads = GameObject.FindGameObjectsWithTag("Load");
        VoltageSource = GameObject.FindGameObjectWithTag("VoltageSource");
        CircuitClosingWire = GameObject.FindGameObjectWithTag("CircuitClosingWire");
        CircuitSwitch = GameObject.FindGameObjectWithTag("CircuitSwitch");
    }

    public void ActivateCircuitClosingWire()
    {
        CircuitClosingWire.SetActive(true);
    }

    public void DeativateCircuitClosingWire()
    {
        CircuitClosingWire.SetActive(false);
    }
    public float Voltage()
    {
        return VoltageSource.GetComponent<VoltageSource>().Voltage;
    }
}
