using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("游戏记录")]
    public int count_NormalCell;
    public int count_CancerCell;
    public int count_DiseaseCell;

    public float timer_GameEnd;

    [Header("游戏状态")]
    public bool gameStart = false;
    public bool gameEnd = false;
    public bool playerLose = false;

    [Header("游戏设定")]
    [Range(300,600)]
    public float gameTime_Seconds;

    [Range(5, 10)]
    public int initialNumber = 5;

    [Range(50,100)]
    public int threshold_Cancercell;

    [Header("对象")]
    public GameObject startPoint;
    public GameObject prefab_Cell;

    [Header("Audio管理器")]
    public MainGameMusicAudio mainGameMusicAudio;
    public MainGameSoundAudio mainGameSoundAudio;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            CheckTimer();
            CheckThreadhold();
        }
        else {
            if (!gameEnd && Input.anyKeyDown) {
                StartGame();
            }
        }

        if (gameEnd) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    /// <summary>
    /// 时间定时器 归0后玩家胜利
    /// </summary>
    public void CheckTimer() {
        if (timer_GameEnd <= 0)
        {
            PlayerLose();
        }
        else {
            timer_GameEnd -= Time.deltaTime;
        }

        ReflashTimer();
    }
    /// <summary>
    /// 检测不同种类细胞数目判断游戏胜利
    /// </summary>
    public void CheckThreadhold() {
        if (count_CancerCell >= threshold_Cancercell || (count_NormalCell <= 0 && count_DiseaseCell<=0)) {
            PlayerLose();
        }
    }

    public void ResetGame() {
        count_CancerCell = 0;
        count_DiseaseCell = 0;
        count_NormalCell = 0;

        gameStart = false;

        timer_GameEnd = gameTime_Seconds;
    }

    public void StartGame()
    {
        gameStart = true;
        StartCoroutine(CreateCell());

        GUIManager.instance.StartGame(threshold_Cancercell);

        mainGameMusicAudio.Play_BGM();
        mainGameSoundAudio.Play_StartGame();
    }

    IEnumerator CreateCell() {
        for (int i = 0; i < initialNumber; i++) {
            GameObject go = GameObject.Instantiate(prefab_Cell, startPoint.transform.position + new Vector3(Mathf.Sin(Random.Range(0,100f)),0), Quaternion.identity);

            go.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1,1),-Random.Range(-1,1))* 2.0f;
            yield return new WaitForSeconds(Random.Range(0.2f,0.5f));
        }
    }

    public void PlayerWin() {
        mainGameSoundAudio.Play_PlayerVictory();

        GUIManager.instance.DisplayVictoryPanel();

        gameStart = false;
        playerLose = false;
        gameEnd = true;
    }
    public void PlayerLose() {
        mainGameSoundAudio.Play_PlayerLose();

        GUIManager.instance.DisplayLosePanel();

        gameStart = false;
        playerLose = true;
        gameEnd = true;
    }

    #region 细胞数记录操作
    public void AddNormalCell()
    {
        count_NormalCell++;

        GUIManager.instance.ReflashCount();
    }
    public void AddCancerCell()
    {
        count_CancerCell++;
        count_NormalCell--;

        GUIManager.instance.ReflashCount();
    }
    public void AddDiseaseCell()
    {
        count_DiseaseCell++;
        count_NormalCell--;

        GUIManager.instance.ReflashCount();
    }
    public void ReduceNormalCell()
    {
        count_NormalCell--;

        GUIManager.instance.ReflashCount();
    }
    public void ReduceCancerCell()
    {
        count_CancerCell--;
        count_NormalCell++;

        GUIManager.instance.ReflashCount();
    }
    public void ReduceDiseaseCell()
    {
        count_DiseaseCell--;
        count_NormalCell++;

        GUIManager.instance.ReflashCount();
    }
    #endregion

    public bool GameStatu() { return gameStart; }

    public void ReflashTimer() {
        float value = (gameTime_Seconds - timer_GameEnd) / gameTime_Seconds;
        GUIManager.instance.ReflashTimer(value);
    }
   
}