using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour 
{
    [SerializeField]
    TextMeshProUGUI _displayDeviceText;
    [SerializeField]
    TextMeshProUGUI _displayUnitText;
    [SerializeField]
    GameObject _devicesToggles;
    ToggleGroup _toggleGroup;
    Toggle _activeToggle;
    GameManager _gameManager;
    [SerializeField]
    GameObject _infoPanel;
    [SerializeField]
    TextMeshProUGUI _infoText;
    [SerializeField]
    Button _close;
    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _toggleGroup = _devicesToggles.GetComponent<ToggleGroup>();
        _activeToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
        _close.onClick.AddListener(CloseInfoPanel);
    }
    private void Update()
    {
        if (_toggleGroup.ActiveToggles().FirstOrDefault() != _activeToggle)
        {
            _activeToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
            UpdateDiviceSelection();
        }
    }

    public void DisplayResult(float value, string unit)
    {
        _displayDeviceText.text = $"{value}";
        _displayUnitText.text = unit;
    }

    void UpdateDiviceSelection()
    {
        if(_activeToggle != null)
            _gameManager.DeviceSelection(_activeToggle.gameObject.name);
        else
            _gameManager.DeviceSelection("Not selected");
    }

    public void DisplayInfo(string _text)
    {
        _infoPanel.SetActive(true);
        _infoText.GetComponent<TextMeshProUGUI>().text = _text;
    }

    void CloseInfoPanel()
    {
        _infoPanel.SetActive(false);
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
}
