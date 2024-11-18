using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScreenUI[] _screens;
    [SerializeField] private Transform _canvas;
    private ScreenUI _currentScreen;

    public void OpenScreen(Enums.ScreenType screenType)
    {
        if (_currentScreen != null)
        {
            Destroy(_currentScreen.gameObject);
        }

        foreach (var screen in _screens) 
        {
            if (screen.ScreenType == screenType)
            {
                ScreenUI newScreen = Instantiate(screen);
                newScreen.transform.SetParent(_canvas, false);

                _currentScreen = newScreen;
            }
        }
    }
}
