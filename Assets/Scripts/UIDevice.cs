using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDevice : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField]
    TextMeshProUGUI _displayDeviceText;
    [SerializeField]
    TextMeshProUGUI _displayUnitText;
    [SerializeField]
    GameObject _devicesToggles;
    ToggleGroup _toggleGroup;
    Toggle _activeToggle;

    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _toggleGroup = _devicesToggles.GetComponent<ToggleGroup>();
        _activeToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
    }
    private void Update()
    {
        if (_toggleGroup.ActiveToggles().FirstOrDefault() != _activeToggle)
        {
            _activeToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
            UpdateDiviceSelection();
        }
    }

    void UpdateDiviceSelection()
    {
        if (_activeToggle != null)
            _gameManager.DeviceSelection(_activeToggle.gameObject.name);
        else
            _gameManager.DeviceSelection("Not selected");
    }

    public void DeactivateToggles()
    {
        for (int i = 0; i < _devicesToggles.transform.childCount; i++)
        {
            _devicesToggles.transform.GetChild(i).GetComponent<Toggle>().interactable = false;
        }
    }

    public void ActivateToggles()
    {
        for (int i = 0; i < _devicesToggles.transform.childCount; i++)
        {
            _devicesToggles.transform.GetChild(i).GetComponent<Toggle>().interactable = true;
        }
    }

    public void DisplayResult(float value, string unit)
    {
        _displayDeviceText.text = $"{value}";
        _displayUnitText.text = unit;
    }
}
