using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Probe _negativeProbe, _positiveProbe;
    float _negativePositionX, _positivePositionX;
    float _totalResistance, _measuredResistance;
    GameObject[] _loads;
    IDevice _selectedDevice, _ammeter, _voltmeter;
    float _voltage;
    Toggle _circuitSwitch;
    float _result;
    UIDevice _uiDevice;
    UIInfo _uiInfo;
    string _noSelectedDevice = "To start the measurement, select the device!";
    string _noConnectionProbe = "You need to connect both probes to the circuit!";
    string _noConnectionLoad = "You need to connect the load. Otherwise the ammeter will burn out!";
    string _notConnectedCorrectly = "Ammeter must be connected in series!";
    Electrical—ircuit _electrical—ircuit;

    private void Start()
    {
        _electrical—ircuit = GameObject.FindGameObjectWithTag("ElectricalCircuit").GetComponent<Electrical—ircuit>();
        _uiDevice = GameObject.Find("Canvas").GetComponent<UIDevice>();
        _uiInfo = GameObject.Find("Canvas").GetComponent<UIInfo>();
        _circuitSwitch = _electrical—ircuit.CircuitSwitch.GetComponent<Toggle>();
        _circuitSwitch.onValueChanged.AddListener(delegate { —ircuitSwitch();});
        _ammeter = new Ammeter();
        _voltmeter = new Voltmeter();
        _voltage = _electrical—ircuit.Voltage();
        _loads = _electrical—ircuit.Loads;
        _positiveProbe = GameObject.Find("Positive Probe").GetComponent<Probe>();
        _negativeProbe = GameObject.Find("Negative Probe").GetComponent<Probe>();
        UpdateProbesPosition();
    }


    void —ircuitSwitch()
    {
        UpdateProbesPosition();
        SearchConnectedLoads();
        ErrorChecking();

        if (_circuitSwitch.isOn)
        {
            DeactivateSelected();
            Measurement(_selectedDevice);
            DisplayResult();
        }
        else
        {
            ActivateSelected();
            ResetResult();
            TurnOffLoad();
            DisplayResult();
        }
    }

    void Measurement(IDevice device)
    {
        _result = device.MeasurementResult(_voltage, _totalResistance, _measuredResistance);
    }

    void SearchConnectedLoads()
    {
        if(_selectedDevice == _ammeter)
        {
            for (int i = 0; i < _loads.Length; i++)
            {
                if (_loads[i].transform.position.x < _positivePositionX)
                {
                    SumMeasuredLoad(_loads[i].GetComponent<Load>());
                    TurnOnLoad(_loads[i].GetComponent<Load>());
                    SumConnectedLoad(_loads[i].GetComponent<Load>());
                }
            }
        }
        else if(_selectedDevice == _voltmeter)
        {
            for (int i = 0; i < _loads.Length; i++)
            {
                if (_loads[i].transform.position.x < _negativePositionX && _loads[i].transform.position.x > _positivePositionX)
                {
                    SumMeasuredLoad(_loads[i].GetComponent<Load>());
                }
                SumConnectedLoad(_loads[i].GetComponent<Load>());
                TurnOnLoad(_loads[i].GetComponent<Load>());
            }
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
                _electrical—ircuit.DeativateCircuitClosingWire();
                break;
            case "Voltmeter Toggle":
                _selectedDevice = _voltmeter;
                _electrical—ircuit.ActivateCircuitClosingWire();
                break;
            default:
                _selectedDevice = null;
                _electrical—ircuit.ActivateCircuitClosingWire();
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
            _loads[i].GetComponent<Load>().NotWorking();
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
            _uiDevice.DisplayResult(_result, _selectedDevice.Unit);
        else
            _uiDevice.DisplayResult(_result, "UNIT");
    }

    void UpdateProbesPosition()
    {
        _negativePositionX = _negativeProbe.CurrentPositionX;
        _positivePositionX = _positiveProbe.CurrentPositionX;

        if(_negativePositionX < _positivePositionX)
        {
            float newPosXPositive = _negativePositionX;
            _negativePositionX = _positivePositionX;
            _positivePositionX = newPosXPositive;
        }
    }

    void Error(string text)
    {
        _uiInfo.DisplayInfo(text);
        _circuitSwitch.isOn = false;
    }

    void ErrorChecking()
    {
        if (_selectedDevice == null)
        {
            Error(_noSelectedDevice);
        }
        else if (_negativePositionX == _negativeProbe.StartPositionX || _positivePositionX == _positiveProbe.StartPositionX)
        {
            Error(_noConnectionProbe);
        }
        else if(_selectedDevice == _ammeter)
        {
            if (_negativeProbe.CurrentPositionY == _positiveProbe.CurrentPositionY)
            {
                Error(_notConnectedCorrectly);
            }
            else if(_measuredResistance == 0)
            {
                Error(_noConnectionLoad);
            }
        }
    }

    void ActivateSelected()
    {
        _uiDevice.ActivateToggles();
        _negativeProbe.ActivateToggle();
        _positiveProbe.ActivateToggle();
    }

    void DeactivateSelected()
    {
        _uiDevice.DeactivateToggles();
        _negativeProbe.DeactivateToggles();
        _positiveProbe.DeactivateToggles();
    }
}