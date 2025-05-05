using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PCInteractions : MonoBehaviour
{

    public TextMeshProUGUI Hint_gui;
    public GameObject Hint;
    public GameObject player;
    MovementThirdPerson _move3rd;

    bool inTrigger;
    bool CanvasOpen;
    


    void Start()
    {
        if (_move3rd == null) _move3rd = player.GetComponent<MovementThirdPerson>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            inTrigger = true;
            Hint.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            inTrigger = false;
            Hint.SetActive(false);
            _move3rd.enabled = true;
            Hint_gui.text = "Нажмите [Е], чтобы использовать ноутбук";
            CanvasOpen = false;
        }
    }

    void Update()
    {
        TurnOnOff();
    }

    void TurnOnOff() {
        if (inTrigger && Input.GetKey(KeyCode.E)) {
            Hint_gui.text = "Нажмите [Еsc], чтобы закрыть ноутбук";
            _move3rd.enabled = false;
            CanvasOpen = true;  
        }
        else if (inTrigger && Input.GetKey(KeyCode.Escape)) {
            Hint_gui.text = "Нажмите [Е], чтобы использовать ноутбук";
            _move3rd.enabled = true;
            CanvasOpen = false;
        }
    }

    void ElephantPC() {
        if (CanvasOpen && inTrigger) {
            
        }
    }
}
