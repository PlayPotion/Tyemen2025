using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject GoodImage1;
    public GameObject GoodImage2;
    public GameObject BadImage1;
    public GameObject BadImage2;
    public GameObject The_End_With_Result;

    [Header("GameObjects")]
    public TextMeshProUGUI ResultText;
    int clicks;
    float timer;
    GameObject image1;
    GameObject image2;


    void Start()
    {
        int bugsCount = PCInteractions._totalBugs;
        int Ending = PCInteractions._ending;

        switch (Ending) {
            case 1:
                image1 = GoodImage1;
                image2 = GoodImage2;
                ResultText.text = $"Количество багов: {bugsCount}\nПолучена хорошая концовка";
                break;
            case 2:
                image1 = BadImage1;
                image2 = BadImage2;
                ResultText.text = $"Количество багов: {bugsCount}\nПолучена плохая концовка";
                break;
        }


        image1.SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer >= 2) {
            clicks++;
        }
        switch (clicks) {
            case 1: 
                image1.SetActive(false);
                image2.SetActive(true);
                break;
            case 2:
                image2.SetActive(false);
                The_End_With_Result.SetActive(true);
                break;
        }
    }


}
