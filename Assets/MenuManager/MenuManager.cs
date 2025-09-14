using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Scene to load when pressing Play")]
    [SerializeField] private string gameSceneName = "Level1";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        if (string.IsNullOrWhiteSpace(gameSceneName))
        {
            return;
        }
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); 
        #endif
    }
}
