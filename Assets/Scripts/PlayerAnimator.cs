using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private string GainMuscle = "GainMuscle";

    public void GainMuscleAnimation()
    {
        _animator.SetBool(GainMuscle, true);
    }
}
