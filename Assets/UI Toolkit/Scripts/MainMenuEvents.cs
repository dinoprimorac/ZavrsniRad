using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;
    private Button button;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        button = document.rootVisualElement.Q("StartGameButton") as Button;

        button.RegisterCallback<ClickEvent>(OnPlayGameClick);

    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Start game button pressed!");
    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }
}
