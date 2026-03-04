using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.digit0Key.wasPressedThisFrame || Keyboard.current.numpad0Key.wasPressedThisFrame)
        {
            // start 2048   
            if (SceneManager.GetActiveScene().name != "2048")
            {
                SceneManager.LoadScene("2048");
            }

            // GridManager.Instance.gameMode = GameModes.GAME_2048;
            // GridManager.Instance.RestartGame();
        }
        else if (Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            // quit game
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
