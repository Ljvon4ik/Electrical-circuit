using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject _negativeProbe, _positiveProbe;
    float _totalResistance, _measuredResistance;
    GameObject[] _loads;
    IDevice _selectedDevice, _ammeter, _voltmeter;
    float _voltage;
    Toggle _onButton;
    float _result;
    UiManager ui;
    string _error = "To start the measurement, select the device";
    private void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UiManager>();
        _onButton = GameObject.Find("On Button").GetComponent<Toggle>();
        _onButton.onValueChanged.AddListener(delegate { Switch—ircuit();});
        _ammeter = new Ammeter();
        _voltmeter = new Voltmeter();
        _voltage = GameObject.FindGameObjectWithTag("VoltageSource").GetComponent<VoltageSource>().Voltage;
        _loads = GameObject.FindGameObjectsWithTag("Load");
        _positiveProbe = GameObject.Find("Positive Probe");
        _negativeProbe = GameObject.Find("Negative Probe");
    }


    void Switch—ircuit()
    {
        if (_selectedDevice == null)
        {
            ui.DisplayInfo(_error);
            _onButton.isOn = false;
        }
        else
        {
            if (_onButton.isOn)
            {
                ui.DeactivateToggles();
                SearchConnectedLoads();
                Measurement(_selectedDevice);
                DisplayResult();
                ResetResult();
            }
            else
            {
                ui.ActivateToggles();
                TurnOffLoad();
                DisplayResult();
            }
        }

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
                TurnOnLoad(_loads[i].GetComponent<Load>());
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

    public void DeviceSelection(string _nameToggle)
    {
        switch (_nameToggle)
        {
            case "Ammeter Toggle":
                _selectedDevice = _ammeter;
                break;
            case "Voltmeter Toggle":
                _selectedDevice = _voltmeter;
                break;
            default:
                _selectedDevice = null;
                break;
        }
        DisplayResult();
    }

    void TurnOnLoad(Load load)
    {
        load.IsWorking();
    }

    void TurnOffLoad()
    {
        for (int i = 0; i < _loads.Length; i++)
        {
            if (_loads[i].transform.position.x < _negativeProbe.transform.position.x && _loads[i].transform.position.x > _positiveProbe.transform.position.x)
            {
                _loads[i].GetComponent<Load>().NotWorking();
            }
        }
    }
    
    void ResetResult()
    {
        _totalResistance = 0;
        _measuredResistance = 0;
        _result = 0;
    }
    
    void DisplayResult()
    {
        if(_selectedDevice != null)
            ui.DisplayResult(_result, _selectedDevice.Unit);
        else
            ui.DisplayResult(_result, "UNIT");
    }
}