using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChangeCamera : MonoBehaviour
{

    public GameObject _setActive;
    public GameObject _setDeactive;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _setActive.SetActive(true);
            _setDeactive.SetActive(false);
        }
    }

}
