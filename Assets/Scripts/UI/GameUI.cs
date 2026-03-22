using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    public static GameUI Instance;
    public GameObject WinPanel;
    public GameObject OverPanel;

    private void Awake() {
        Instance = this;
        WinPanel = transform.Find("WinPanel").gameObject;
        OverPanel = transform.Find("OverPanel").gameObject;
        WinPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
        OverPanel.transform.Find("BtnReset").GetComponent<Button>().onClick.AddListener(ResetGame);
    }   

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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