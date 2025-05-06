using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideShow : MonoBehaviour
{

    [Header("Variables")]
    public int needClicks;
    int clicks;
    [Header("GameObjects")]
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            clicks++;
        }
        
        switch (clicks) {
            case 1: image2.SetActive(true); break;
            case 2: image3.SetActive(true); break;
            case 3: image4.SetActive(true); break;
            case 4: image5.SetActive(true); break;
            case 5: image6.SetActive(true); break;
        }

        if (clicks >= needClicks) SceneManager.LoadScene("SampleScene");
    }

}
