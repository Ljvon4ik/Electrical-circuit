using UnityEngine;
using UnityEngine.UI;

public class ConnectionPoint : MonoBehaviour
{
    Toggle isSelected;
    GameObject[] probes;
    private void Start()
    {
        isSelected = GetComponent<Toggle>();
        probes = GameObject.FindGameObjectsWithTag("Probe");
        isSelected.onValueChanged.AddListener(delegate { TransferPositionToProbe(); });
    }

    void TransferPositionToProbe()
    {
        for (int i = 0; i < probes.Length; i++)
        {
            probes[i].GetComponent<Probe>().ProbeAction(transform.position);
        }
    }
}
