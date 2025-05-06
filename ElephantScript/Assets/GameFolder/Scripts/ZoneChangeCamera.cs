using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChangeCamera : MonoBehaviour
{

    public GameObject _setActive;
    public GameObject other1;
    public GameObject other2;
    public GameObject other3;
    public GameObject other4;
    public GameObject other5;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _setActive.SetActive(true);
            other1.SetActive(false);
            other2.SetActive(false);
            other3.SetActive(false);
            other4.SetActive(false);
            other5.SetActive(false);
        }
    }

}
