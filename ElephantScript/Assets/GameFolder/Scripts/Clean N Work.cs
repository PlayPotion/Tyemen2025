using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleanNWork : MonoBehaviour
{

    public GameObject hintCanvas;
    public TextMeshProUGUI hintText;
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public static int numbersObject;
    bool inTrigger;

    void Update()
    {
        
        if (Quests.Quest == "Распаковать кресло, стол и растение" && Input.GetKey(KeyCode.E) && inTrigger) {
            hintText.text = "Нажмите [Е], чтобы достать вещи из коробки";
            object1.SetActive(true);
            object2.SetActive(true);
            object3.SetActive(false);
            numbersObject++;
            numbersObject++;

            if (numbersObject >= 4) {
                Quests.Quest = "Решить задание на компьютере [2]";
                hintCanvas.SetActive(false);
                numbersObject = 0;
            }
        }


        if (Quests.Quest == "Убраться на складе" && Input.GetKey(KeyCode.E) && inTrigger) {
            hintText.text = "Нажмите [Е], чтобы убраться";
            object1.SetActive(false);
            object2.SetActive(false);
            object3.SetActive(false);
            numbersObject++;
            numbersObject++;

            if (numbersObject >= 4) {
                Quests.Quest = "Решить задание на компьютере [3]";
                hintCanvas.SetActive(false);
                numbersObject = 0;
            }


        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (Quests.Quest == "Убраться на складе") hintText.text = "Нажмите [Е], чтобы убраться";
            inTrigger = true;
            hintCanvas.SetActive(true);
        }
    }

        void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            
            inTrigger = false;
            hintCanvas.SetActive(false);
        }
    }

}
