using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    public Sprite cursorTexture;
    public Image cursorImage;
    public RectTransform rectTransform;

    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            cursorImage.sprite = cursorTexture;
        }
        // Устанавливаем позицию объекта на позицию мыши
        rectTransform.position = Input.mousePosition;
    }
}
