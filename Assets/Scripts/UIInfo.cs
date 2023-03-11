using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInfo : MonoBehaviour 
{
    [SerializeField]
    GameObject _infoPanel;
    [SerializeField]
    TextMeshProUGUI _infoText;
    [SerializeField]
    Button _close;
    [SerializeField]
    Button _help;
    string _helpText = "1. Select a device (bottom right).\r\n\r\n2. Connect 2 probes (slightly to the left of the device selection) to the yellow dots on the electrical circuit.\r\n\r\n3. Close the circuit by pressing the button above the positive battery terminal.\r\n\r\n4. Record the instrument readings.";
    private void Start()
    {
        _close.onClick.AddListener(CloseInfoPanel);
        _help.onClick.AddListener(Help);
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

    void Help()
    {
        DisplayInfo(_helpText);
    }
}
