using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public ARPlaneManager planeManager;
    public Text scoreText;
    public Text gameOverText;

    public Text TimerText;

    private List<ARPlane> planes = new List<ARPlane>();
    private int score;
    private bool isGameOver;
    private bool isFirst = true;
    private int timer = 60;

    private void Start()
    {
        planeManager.planesChanged += HandlePlanesChanged;
        score = 0;
        isGameOver = false;
        UpdateScoreText();
        // 在游戏开始时，设置 TimerText.text 的值为 "01:00"
        TimerText.text = "01:00";
        gameOverText.gameObject.SetActive(false);
    }

    private void HandlePlanesChanged(ARPlanesChangedEventArgs args)
    {
        planes.AddRange(args.added);
        planes.RemoveAll(plane => args.removed.Contains(plane) || plane.alignment != PlaneAlignment.HorizontalUp);
        if (isFirst)
        {
            isFirst = false;
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        // 如果游戏已经结束或者没有检测到平面，则不生成敌人
        if (isGameOver) return;
        if (planes.Count == 0) return;
        var plane = planes[Random.Range(0, planes.Count)];
        Vector2 planeBoundsSize = new Vector2(plane.extents.x * 2f, plane.extents.y * 2f);
        Vector3 position = new Vector3(Random.Range(-planeBoundsSize.x / 2f, planeBoundsSize.x / 2f), plane.center.y, Random.Range(-planeBoundsSize.y / 2f, planeBoundsSize.y / 2f));

        var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.GetComponent<Enemy>().OnEnemyDestroyed += () =>
        {
            if (!isGameOver)
            {
                score++;
                UpdateScoreText();
                if (score == 1)
                {
                    StartCoroutine(StartCountdown());
                }
            }
            SpawnEnemy();
        };
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

private IEnumerator StartCountdown()
{
    while (timer > 0)
    {
        timer--;
        int minutes = timer / 60;
        int seconds = timer % 60;
        TimerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        yield return new WaitForSeconds(1);
    }
    GameOver();
}



    private void GameOver()
    {
        isGameOver = true;
        gameOverText.text = "Game Over! Your final score is: " + score;
        gameOverText.gameObject.SetActive(true);
        score = 0;
        UpdateScoreText();
    }
}

