using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class PCInteractions : MonoBehaviour
{

    [Header("Variables")]
    public int quota = 3;


    [Header("Player")]
    public GameObject player;
    public AudioSource correct;
    public AudioSource wrong;
    MovementThirdPerson _move3rd;

    [Header("HUD")]
    public TextMeshProUGUI Hint_gui;
    public GameObject Hint;
    public GameObject StartCodingButton;
    public GameObject LaptopHUD;
    public GameObject QTE;
    public GameObject TapE;
    public GameObject TapQ;
    public GameObject TapR;


    bool inTrigger;
    bool CanvasOpen;
    int randQTE = 20;
    int lastRand;
    float timer;
    int pressQTE;
    bool QTEOpen;
    int completeQTE;
    


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
            LaptopHUD.SetActive(false);
        }
    }

    void Update()
    {
        TurnOnOff();

        if (randQTE is 0 or 1 or 2) {
            timer += Time.deltaTime;

            if (completeQTE < quota) {
                if (timer <= 5) {
                    switch (randQTE) {
                        case 0: 
                            TapE.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.E)) pressQTE += 1;

                            if (pressQTE >= 10) {
                                TapE.SetActive(false);
                                lastRand = randQTE;
                                completeQTE++;                             
                            }
                            randQTE = 20;
                            timer = 0;
                            pressQTE = 0;

                            break;

                        case 1:
                            TapQ.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.Q)) pressQTE += 1;

                            if (pressQTE >= 10) {
                                TapQ.SetActive(false);
                                lastRand = randQTE;
                                completeQTE++;                             
                            }
                            randQTE = 20;
                            timer = 0;
                            pressQTE = 0;

                            break;

                        case 2:
                            TapR.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.R)) pressQTE += 1;

                            if (pressQTE >= 10) {
                                TapR.SetActive(false);
                                lastRand = randQTE;
                                completeQTE++;                             
                            }
                            randQTE = 20;
                            timer = 0;
                            pressQTE = 0;

                            break;

                        default:
                            break;
                    }
                } else if (pressQTE < 10 && timer < 5) {

                }
            } 
        }
        if (timer > 5 || completeQTE >= quota) {

            if (completeQTE >= quota)
                Debug.Log("Complete - Correct");
                // correct.Play();
            else if (timer > 5)
                Debug.Log("Not complete - Wrong");
                // wrong.Play();

            pressQTE = 0;
            completeQTE = 0;
            timer = 0;
            StartCodingButton.SetActive(true); TapE.SetActive(false); TapQ.SetActive(false); TapR.SetActive(false);
        }
    }

    void TurnOnOff() {
        if (inTrigger && Input.GetKey(KeyCode.E)) {
            Hint_gui.text = "Нажмите [Еsc], чтобы закрыть ноутбук";
            _move3rd.enabled = false;
            CanvasOpen = true;  
            LaptopHUD.SetActive(true);
        }
        
        else if (inTrigger && Input.GetKey(KeyCode.Escape) && !QTEOpen) {
            Hint_gui.text = "Нажмите [Е], чтобы использовать ноутбук";
            _move3rd.enabled = true;
            CanvasOpen = false;
            LaptopHUD.SetActive(false);
        }
    }

    public void StartProg() {
        if (CanvasOpen && inTrigger) {

            QTE.SetActive(true);
            QTEOpen = true;
            Hint.SetActive(false);
            StartCodingButton.SetActive(false);

            randQTE = Random.Range(0,3);

            if (randQTE == lastRand) 
                while (randQTE != lastRand) 
                    randQTE = Random.Range(0,3);

                




        }
    }
}
