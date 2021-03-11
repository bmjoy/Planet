using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Tools;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject rankPanel;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text rank;
    public void DisplayRank()
    {
        Tool tool = new Tool();
        int[] rankArray = tool.Rank();
        rank.text = "排行榜\r\n";
        for(int i = 0; i < rankArray.Length; i++)
        {
            string display = "第" + (i + 1) + "名：" + rankArray[i];
            rank.text += (display + "\r\n");
            
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenRankPanel()
    {
        rankPanel.SetActive(true);
        DisplayRank();
    }

    public void CloseRankPanel()
    {
        rankPanel.SetActive(false);
    }
}
