using UnityEngine;
using UnityEngine.UI;

public class Probe : MonoBehaviour
{
    Toggle isSelected;
    public float StartPositionX { get; private set; }
    public float CurrentPositionX { get; private set; }
    public float CurrentPositionY { get; private set; }
    private void Start()
    {
        isSelected = GetComponent<Toggle>();
        StartPositionX = transform.position.x;
        CurrentPositionX = StartPositionX;
    }

    public void ProbeAction(Vector3 newPosition)
    {
        if (isSelected.isOn)
        {
            Move(newPosition);
            CurrentPositionX = transform.position.x;
            CurrentPositionY = transform.position.y;
            isSelected.isOn = false;
        }
    }

    public void ActivateToggle()
    {
        isSelected.interactable = true;
    }

    public void DeactivateToggles()
    {
        isSelected.interactable = false;
    }
    void Move(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
