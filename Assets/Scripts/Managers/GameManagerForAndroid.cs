using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Tools;

public class GameManagerForAndroid : MonoBehaviour
{
    [SerializeField] private GameObject[] planetsPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Text score;
    [SerializeField] private GameObject gameOverUI;

    private bool canCompose = false;    //如果可以合成新星球，则设为true。canCompose必须通过CollisionManager设置。
    private bool canSpawn = false;      //如果可以生成新星球，则设为true。
    private bool freeze = false;        //冻结玩家点击，防止频繁设置星球下落位置
    private bool countdown = false;     //设置计时器是否开始计时
    private bool gameOver = false;
    private GameObject candidate;       //候选合成的星球
    private const int countdownTime = 800;    //倒计时的时长，单位为毫秒。
    private int startTime;              //开启计时器的时间，单位毫秒
    private int endTime;                //关闭计时器的时间，单位毫秒
    public float mouseX;               //用于记录每帧鼠标位置
    private ComposeData composeData;    //用于传参的数据结构
    private int spawnFactor = 0;
    private int[] types;
    private int _score = 0;

    Tool tool = new Tool();             //封装了一些工具

    private void Start()
    {
        types = new int[planetsPrefab.Length];
        canSpawn = true;
    }

    private void Update()
    {
        //↓↓↓↓↓↓↓↓↓↓安卓化
        //mouseX = Camera.main.ScreenToWorldPoint(Input.touches[0].position).x;
        //↑↑↑↑↑↑↑↑↑↑安卓化
        //获取鼠标屏幕位置
        //mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //1.星球生成
        SpawnPlanet();
        //2.检测用户输入
        DetectMouseClick();
    }

    //计时器倒计时
    void CountdownClock()
    {
        int interval;
        endTime = DateTime.Now.Millisecond;
        interval = tool.IntervalTime(startTime, endTime);
        //interval = IntervalTime(startTime, endTime);
        if (interval > countdownTime)
        {
            canSpawn = true;
            countdown = false;
        }
    }

    void SpawnPlanet()
    {
        // (1)在星球生成点生成一颗星球
        if (canSpawn)
        {
            spawnFactor = tool.RandomType(types);
            canSpawn = false;
            candidate = Instantiate(planetsPrefab[spawnFactor], spawnPoint.transform.position, spawnPoint.transform.rotation);
            types[spawnFactor]++;
        }
        // (2)合成新星球
        if (canCompose)
        {
            canCompose = false;
            GameObject gameObject = Instantiate(planetsPrefab[composeData.Type], composeData.Position, composeData.Rotation);
            types[composeData.Type]++;
            _score += composeData.Type;
            score.text = _score.ToString();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            gameObject.SendMessage("NotFirst");
        }
    }

    void DetectMouseClick()
    {
        //↓↓↓↓↓↓↓↓↓↓安卓化
        bool playerInput = Input.touchCount >= 1;
        //↑↑↑↑↑↑↑↑↑↑安卓化
        if (playerInput && !freeze)
        {
            freeze = true;      //暂时冻结用户输入
            //↓↓↓↓↓↓↓↓↓↓安卓化
            Touch myTouch = Input.GetTouch(0);
            mouseX = Camera.main.ScreenToWorldPoint(myTouch.position).x;
            //↑↑↑↑↑↑↑↑↑↑安卓化
            if (gameOver)
            {
                GameOver();
            }
            else
            {
                gameOver = false;
                //让星球在鼠标出下落
                if (mouseX > 2.8f)
                {
                    mouseX = 2.8f;
                }
                else if (mouseX < -2.8f)
                {
                    mouseX = -2.8f;
                }

                //设置星球位置到玩家所指位置，并让其下落
                candidate.transform.position = new Vector2(mouseX, candidate.transform.position.y);
                candidate.GetComponent<Rigidbody2D>().gravityScale = 1;

                //倒计时开始，在计时结束后将在spawnPoint处生成新的星球
                countdown = true;       //倒计时开始
                startTime = DateTime.Now.Millisecond;
            }
            ////让星球在鼠标出下落
            //if (mouseX > 2.8f)
            //{
            //    mouseX = 2.8f;
            //}
            //else if (mouseX < -2.8f)
            //{
            //    mouseX = -2.8f;
            //}

            ////设置星球位置到玩家所指位置，并让其下落
            //candidate.transform.position = new Vector2(mouseX, candidate.transform.position.y);
            //candidate.GetComponent<Rigidbody2D>().gravityScale = 1;

            ////倒计时开始，在计时结束后将在spawnPoint处生成新的星球
            //countdown = true;       //倒计时开始
            //startTime = DateTime.Now.Millisecond;

            //如果计时器开始，就在每帧判断一次时间间隔
        }
        if (countdown)
        {
            CountdownClock();
        }
    }

    //游戏结束的时候执行的代码
    void GameOver()
    {
        tool.Rank(_score);
        gameOverUI.SetActive(true);
        Debug.Log("Game Over!");
    }

    //给CollisionManager.cs传递参数用
    void CanCompose()
    {
        canCompose = true;
    }

    //用于碰撞检测，给CollisionManager.cs传递参数用
    void DeliverComposeData(ComposeData composeData)
    {
        this.composeData = composeData;
    }

    //给CollisionManager.cs传递参数用
    void UnFreeze()
    {
        freeze = false;
    }

    //给CollisionManager.cs传递参数用
    void IsGameOver(bool resulte)
    {
        gameOver = resulte;
    }
}
