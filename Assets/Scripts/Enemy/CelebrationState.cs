using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CelebrationState : State
{ 
    private void OnEnable()
    {
        Debug.Log($"win {transform.name}");
        
    }
}
