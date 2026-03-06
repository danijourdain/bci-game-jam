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
            if (SceneManager.GetActiveScene().name != "World")
            {
                SceneManager.LoadScene("World");
            }

            GameManager.Instance.StartGame();
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
