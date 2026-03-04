using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
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
            restarted = true;
            restartText.gameObject.SetActive(false);
            // GridManager.Instance.RestartGame();
        }
    }

    public void Restart()
    {
        timeLeft = countdownTime;
        restartText.text = timeLeft.ToString("0");
        restartText.gameObject.SetActive(true);
        restarted = false;
    }
}