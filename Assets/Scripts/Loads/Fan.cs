using UnityEngine;

public class Fan : Load
{    
    public override string CurrentElectricity => "DC";
    public override float Resistance => 2;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public override void IsWorking()
    {
        _animator.enabled = true;
    }

    public override void NotWorking()
    {
        _animator.enabled = false;
    }
}