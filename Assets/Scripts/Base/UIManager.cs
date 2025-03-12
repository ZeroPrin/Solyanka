using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    public ScreenUI CurrentScreen => _currentScreen;

    [SerializeField] private ScreenUI[] _screens;
    [SerializeField] private Transform _canvas;

    private ScreenUI _currentScreen;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void OpenScreen(Enums.ScreenType screenType)
    {
        if (_currentScreen != null)
        {
            _currentScreen.Deinitialize();
            Destroy(_currentScreen.gameObject);
        }

        foreach (var screen in _screens)
        {
            if (screen.ScreenType == screenType)
            {
                ScreenUI newScreen = _container.InstantiatePrefabForComponent<ScreenUI>(screen);

                newScreen.transform.SetParent(_canvas, false);

                _currentScreen = newScreen;
                _currentScreen.Initialize();
            }
        }
    }
}
