using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject _negativeProbe, _positiveProbe;
    float _totalResistance, _measuredResistance;
    GameObject[] _loads;
    IDevice _selectedDevice, _ammeter, _voltmeter;
    ToggleGroup _toggleGroup;
    Toggle _activToggle;
    float _voltage;
    Button _onButton;
    float _result;

    private void Start()
    {
        _onButton = GameObject.Find("On Button").GetComponent<Button>();
        _onButton.onClick.AddListener(—lose—ircuit);
        _toggleGroup = GameObject.Find("Devices Toggle").GetComponent<ToggleGroup>();
        _activToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
        _ammeter = new Ammeter();
        _voltmeter = new Voltmeter();
        _voltage = GameObject.FindGameObjectWithTag("VoltageSource").GetComponent<VoltageSource>().Voltage;
        _loads = GameObject.FindGameObjectsWithTag("Load");
        _positiveProbe = GameObject.Find("Positive Probe");
        _negativeProbe = GameObject.Find("Negative Probe");
    }

    private void Update()
    {
        if (_toggleGroup.ActiveToggles().FirstOrDefault() != _activToggle)
            _activToggle = _toggleGroup.ActiveToggles().FirstOrDefault();       
    }
    void —lose—ircuit()
    {
        SearchConnectedLoads();
        DeviceSelection();
        Measurement(_selectedDevice);
        Debug.Log("Total res: " + _totalResistance);
        Debug.Log("Measure res: " + _measuredResistance);
        Debug.Log("Result: " + _result);
        _totalResistance = 0;
        _measuredResistance = 0;
        _result = 0;
    }

    void Measurement(IDevice device)
    {
        _result = device.MeasurementResult(_voltage, _totalResistance, _measuredResistance);
    }


    void SearchConnectedLoads()
    {
        for (int i = 0; i < _loads.Length; i++)
        {
            if (_loads[i].transform.position.x < _negativeProbe.transform.position.x && _loads[i].transform.position.x > _positiveProbe.transform.position.x)
            {
                SumMeasuredLoad(_loads[i].GetComponent<Load>());
            }
            SumConnectedLoad(_loads[i].GetComponent<Load>());
        }
    }

    void SumMeasuredLoad(Load load)
    {
        _measuredResistance += load.Resistance;
    }

    void SumConnectedLoad(Load load)
    {
        _totalResistance += load.Resistance;
    }

    void DeviceSelection()
    {
        if (_activToggle.gameObject.name == "Ammeter Toggle")
            _selectedDevice = _ammeter;
        else if (_activToggle.gameObject.name == "Voltmeter Toggle")
            _selectedDevice = _voltmeter;
    }
}