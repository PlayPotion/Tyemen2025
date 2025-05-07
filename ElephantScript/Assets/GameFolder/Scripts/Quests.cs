using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public static string Quest = "Решить задание на компьютере [1]";

    void Update()
    {
        questText.text = Quests.Quest;
    }

}
