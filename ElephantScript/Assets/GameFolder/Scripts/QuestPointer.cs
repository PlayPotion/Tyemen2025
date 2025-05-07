using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject there;

    void Update()
    {
        left.SetActive(false);
        right.SetActive(false);
        there.SetActive(false);

        switch (Quests.Quest)
        {
            case "Убраться на складе":
                left.SetActive(true);
                break;

            case "Распаковать кресло, стол и растение":
                right.SetActive(true);
                break;

            case "Найти дверь":
                there.SetActive(true);
                break;
        }
    }
}
