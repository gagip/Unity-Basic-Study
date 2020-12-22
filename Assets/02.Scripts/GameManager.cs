using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instanse;

    [Header("게임")]
    public int score;

    [Header("게임 UI")]
    public GameObject gameOverPenal;
    public Text scoreText;
    public Image hpBar;

    private void Awake()
    {
        // 싱글턴
        if (instanse == null)
        {
            instanse = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
    
    /// <summary>
    /// 게임 오버
    /// </summary>
    public void GameOver()
    {
        gameOverPenal.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// 재시작
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene("InGame");
    }
    
    public void UpdateHpImage(int hp, int maxHp)
    {
        hpBar.fillAmount = (float) hp / maxHp;
    }
}
