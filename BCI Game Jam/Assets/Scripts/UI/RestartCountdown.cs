using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartCountdown : MonoBehaviour
{
    private float timeLeft = 0;
    [SerializeField] private float countdownTime = 5;
    [SerializeField] private TextMeshPro restartText;
    private bool restarted = false;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        restartText.text = timeLeft.ToString("0");
        if (timeLeft < 0 && !restarted)
        {
            SceneManager.LoadScene (sceneName:"Put the name of the scene here");
        }
    }

    public void BeginTimer()
    {
        timeLeft = countdownTime;
        restartText.text = timeLeft.ToString("0");
        restartText.gameObject.SetActive(true);
        restarted = false;
    }
}