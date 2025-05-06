using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideShow : MonoBehaviour
{

    [Header("Variables")]
    public int needClicks;
    int clicks;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            clicks++;
            Debug.Log(clicks);
        }
        
        if (clicks >= needClicks) SceneManager.LoadScene("SampleScene");
    }

}
