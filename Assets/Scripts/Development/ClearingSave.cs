using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearingSave : MonoBehaviour
{
    public void Clear()
    {
        ProgressSaver.Instance.Save(new Saving(0, 1, new Item[] { }, new Item[] { }));
    }
}
