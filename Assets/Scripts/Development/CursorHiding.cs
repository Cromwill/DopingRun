using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHiding : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_WEBGL
        Texture2D cursor = new Texture2D(0, 0);
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
#else
         Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
#endif
    }
}
