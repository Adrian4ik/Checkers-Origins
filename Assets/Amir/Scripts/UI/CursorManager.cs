using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursor1;
    [SerializeField] Texture2D cursor2;

    Vector2 _cursorHotspot = new Vector2(40, 5);



    #region Singleton
    private static CursorManager _instance;
    public static CursorManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<CursorManager>();

            return _instance;
        }
    }
    #endregion



    public void SetStandartCursor()
    {
        if (cursor1 != null)
            Cursor.SetCursor(cursor1, _cursorHotspot, CursorMode.Auto);
    }

    public void SetHandCursor()
    {
        if (cursor2 != null)
            Cursor.SetCursor(cursor2, _cursorHotspot, CursorMode.Auto);
    }
}
