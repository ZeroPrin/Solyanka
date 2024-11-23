using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : ScreenUI
{
    [SerializeField] private Button _scene1;

    public void Initialized()
    {
        _scene1.onClick.AddListener(LoadScene1);
    }

    private void LoadScene1()
    {

    }
}
