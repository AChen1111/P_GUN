using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    public static GameUI Instance;
    public GameObject WinPanel;
    public GameObject OverPanel;
    public Text HPText;

    private void Awake() {
        Instance = this;
        WinPanel = transform.Find("WinPanel").gameObject;
        OverPanel = transform.Find("OverPanel").gameObject;
        //HPText = transform.Find("HPText").GetComponent<Text>();
        UpdateHPText();
    }
    //订阅事件
    private void OnEnable() {
        WinPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
        OverPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
        Global.OnHPChange += UpdateHPText;
    }
    //取消订阅事件
    private void OnDisable() {
        WinPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
        OverPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.RemoveListener(ResetGame);
        Global.OnHPChange -= UpdateHPText;
    }
    //更新HP文本
    private void UpdateHPText() {
        HPText.text = "HP: " + Global.HP;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Global.Restart();
        Time.timeScale = 1;
    }
    
    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ShowOverPanel()
    {
        OverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}