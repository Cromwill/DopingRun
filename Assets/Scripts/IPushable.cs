using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPushable
{
    public void Push(Vector3 direction, float pushSpeed);
}
