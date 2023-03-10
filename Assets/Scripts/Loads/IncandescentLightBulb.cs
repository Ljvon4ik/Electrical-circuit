using UnityEngine;

public class IncandescentLightBulb : Load
{
    public override string CurrentElectricity => "DC";
    public override float Resistance => 4;

    Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public override void IsWorking()
    {
        _animator.SetBool("IsWorking", true);
    }

    public override void NotWorking()
    {
        _animator.SetBool("IsWorking", false);
    }
}