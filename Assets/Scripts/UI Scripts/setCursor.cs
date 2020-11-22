using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCursor : MonoBehaviour
{
    public Texture2D cursor;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorOffset = new Vector2(cursor.width / 2, cursor.height/2);

        Cursor.SetCursor(cursor, cursorOffset, CursorMode.Auto);

    }
}
