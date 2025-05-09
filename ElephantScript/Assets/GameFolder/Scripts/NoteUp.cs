using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteUp : MonoBehaviour
{
    public RectTransform noteUI; 
    public float showY = 541f;
    public float hideY = -548f; 
    public float moveSpeed = 2f;

    bool movingToShow = false;
    bool movingToHide = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            movingToShow = true;
            movingToHide = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            movingToHide = true;
            movingToShow = false;
        }
    }

    void Update()
    {
        Vector2 pos = noteUI.anchoredPosition;

        if (movingToShow)
        {
            pos.y = Mathf.Lerp(pos.y, showY, Time.deltaTime * moveSpeed);
            noteUI.anchoredPosition = pos;

            if (Mathf.Abs(pos.y - showY) < 0.00001f)
            {
                noteUI.anchoredPosition = new Vector2(pos.x, showY);
                movingToShow = false;
            }
        }

        if (movingToHide)
        {
            pos.y = Mathf.Lerp(pos.y, hideY, Time.deltaTime * moveSpeed);
            noteUI.anchoredPosition = pos;

            if (Mathf.Abs(pos.y - hideY) < 0.1f)
            {
                noteUI.anchoredPosition = new Vector2(pos.x, hideY);
                movingToHide = false;
            }
        }
    }
}
