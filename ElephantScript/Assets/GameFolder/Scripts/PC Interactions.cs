using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PCInteractions : MonoBehaviour
{
    public int quota = 3;

    [Header("Player")]
    public GameObject player;
    public AudioSource correct;
    public AudioSource wrong;
    MovementThirdPerson _move3rd;

    [Header("HUD")]
    public TextMeshProUGUI CodeText;
    public GameObject EndButton;
    public TextMeshProUGUI BugCount_gui;
    public GameObject bugCount_object;
    public TextMeshProUGUI Hint_gui;
    public GameObject Hint;
    public TextMeshProUGUI CodingButtonText;
    public GameObject StartCodingButton;
    public GameObject LaptopHUD;
    public GameObject QTE;
    public GameObject TapE;
    public GameObject TapQ;
    public GameObject TapR;

    bool inTrigger;
    bool CanvasOpen;
    bool QTEOpen;
    enum QTEKey { None, E, Q, R }
    QTEKey currentKey = QTEKey.None;
    QTEKey lastKey = QTEKey.None;
    float timer;
    int level = 1;
    int bugCount;
    int pressCount;
    int completeCount;
    int currentLineIndex = 0;

    string fullCodeText = @"для каждый предмет в мебель:
использовать(""хобот"")
переместить(предмет, ""лаборатория"")
сказать(""Мебель расставлена. Уют обеспечен!"")


пусть завал =
осмотреть(""лаборатория"").найти(""обвал"")
убрать_мусор(завал)
восстановить(""скрытая_дверь"")
сказать(""Секретная комната открыта."")
сказать(""Завал расчищен, проход свободен!"")


пусть шкаф =
осмотреть(""лаборатория"").найти(""шкаф"")
подойти(шкаф)
использовать(шкаф).надеть(шкаф.""форма"")
сказать(""Форма надета. Статус: Слоник-Лаборант"")";

    string[] level1Parts;
    string[] level2Parts;
    string[] level3Parts;
    public static int _ending;
    public static int _totalBugs;

    void Start()
    {
        _move3rd = player.GetComponent<MovementThirdPerson>();
        PrepareCodeChunks();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
            Hint.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
        HandleLaptopToggle();

        if (QTEOpen)
        {
            HandleQTE();
        }

        BugCount_gui.text = $"Количество багов: {bugCount}";
        bugCount_object.SetActive(bugCount >= 1);

        switch (level)
        {
            case 2: CodingButtonText.text = "Начать программировать [2]"; break;
            case 3: CodingButtonText.text = "Начать программировать [3]"; break;
        }
    }

    void HandleLaptopToggle()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E) && !CanvasOpen)
        {
            Hint_gui.text = "Нажмите [Esc], чтобы закрыть ноутбук";
            _move3rd.enabled = false;
            CanvasOpen = true;
            LaptopHUD.SetActive(true);
        }
        else if (CanvasOpen && Input.GetKeyDown(KeyCode.Escape) && !QTEOpen)
        {
            Hint_gui.text = "Нажмите [Е], чтобы использовать ноутбук";
            _move3rd.enabled = true;
            CanvasOpen = false;
            LaptopHUD.SetActive(false);
        }
    }

    public void StartProg()
    {
        if (!CanvasOpen || !inTrigger) return;

        QTE.SetActive(true);
        QTEOpen = true;
        Hint.SetActive(false);
        StartCodingButton.SetActive(false);
        timer = 0;
        pressCount = 0;
        completeCount = 0;
        currentLineIndex = 0;
        NextQTEKey();
    }

    void HandleQTE()
    {
        timer += Time.deltaTime;

        TapE.SetActive(currentKey == QTEKey.E);
        TapQ.SetActive(currentKey == QTEKey.Q);
        TapR.SetActive(currentKey == QTEKey.R);

        if (Input.GetKeyDown(KeyCode.E) && currentKey == QTEKey.E)
        {
            pressCount++;
            AddCodeChunk();
        }
        if (Input.GetKeyDown(KeyCode.Q) && currentKey == QTEKey.Q)
        {
            pressCount++;
            AddCodeChunk();
        }
        if (Input.GetKeyDown(KeyCode.R) && currentKey == QTEKey.R)
        {
            pressCount++;
            AddCodeChunk();
        }

        int targetPresses = 10;
        float maxTime = 5f;

        switch (level)
        {
            case 2: targetPresses = 15; maxTime = 4.25f; break;
            case 3: targetPresses = 20; maxTime = 3.75f; break;
        }

        if (pressCount >= targetPresses)
        {
            completeCount++;
            pressCount = 0;
            timer = 0;

            if (completeCount >= quota)
            {
                QTEComplete();
                return;
            }

            NextQTEKey();
        }
        else if (timer > maxTime)
        {
            bugCount++;
            pressCount = 0;
            timer = 0;
            NextQTEKey();
        }
    }

    void QTEComplete()
    {
        if (level != 3)
        {
            QTEOpen = false;
            level++;
            QTE.SetActive(false);
            TapE.SetActive(false);
            TapQ.SetActive(false);
            TapR.SetActive(false);
            StartCodingButton.SetActive(true);
            Hint.SetActive(true);

            currentKey = QTEKey.None;
            lastKey = QTEKey.None;
            timer = 0;
            pressCount = 0;
            completeCount = 0;
            currentLineIndex = 0;
        } else {
            CanvasOpen = false;
            QTEOpen = false;
            level++;
            QTE.SetActive(false);
            TapE.SetActive(false);
            TapQ.SetActive(false);
            TapR.SetActive(false);
            StartCodingButton.SetActive(false);
            Hint.SetActive(false);
            EndButton.SetActive(true);

            currentKey = QTEKey.None;
            lastKey = QTEKey.None;
            timer = 0;
            pressCount = 0;
            completeCount = 0;
            currentLineIndex = 0;
        }
    }

    void NextQTEKey()
    {
        QTEKey newKey;
        do
        {
            newKey = (QTEKey)Random.Range(1, 4);
        } while (newKey == lastKey);

        lastKey = currentKey;
        currentKey = newKey;
    }

    void PrepareCodeChunks()
    {
        string cleaned = fullCodeText.Replace("\r\n", "\n").Replace("\r", "\n"); // очистка
        string[] paragraphs = cleaned.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);

        if (paragraphs.Length < 3) return;
        

        level1Parts = SplitIntoChunks(paragraphs[0], 31);
        level2Parts = SplitIntoChunks(paragraphs[1], 45);
        level3Parts = SplitIntoChunks(paragraphs[2], 53);
    }


    string[] SplitIntoChunks(string text, int chunkCount)
    {
        int chunkSize = Mathf.CeilToInt((float)text.Length / chunkCount);
        string[] chunks = new string[chunkCount];

        for (int i = 0; i < chunkCount; i++)
        {
            int start = i * chunkSize;
            int length = Mathf.Min(chunkSize, text.Length - start);

            if (start >= text.Length)
            {
                chunks[i] = "";
            }
            else
            {
                chunks[i] = text.Substring(start, length);
            }
        }

        return chunks;
    }

    void AddCodeChunk()
    {
        string[] chunks = GetCurrentLevelChunks();

        if (currentLineIndex < chunks.Length)
        {
            CodeText.text += chunks[currentLineIndex];
            currentLineIndex++;
        }
    }

    string[] GetCurrentLevelChunks()
    {
        switch (level)
        {
            case 1: return level1Parts;
            case 2: return level2Parts;
            case 3: return level3Parts;
            default: return new string[0];
        }
    }

    public void EndTheGame() {
        if (bugCount >= 3) PCInteractions._ending = 2;
        else PCInteractions._ending = 1;
        // 1 - good, 2 - bad
        PCInteractions._totalBugs = bugCount;
        SceneManager.LoadScene("TheEnd");
        
    }
}
