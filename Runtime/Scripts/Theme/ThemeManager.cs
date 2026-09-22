using UnityEngine;
using System;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance { get; private set; }

    [SerializeField]
    private UITheme currentTheme;
    public UITheme CurrentTheme => currentTheme;

    public event Action ThemeChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        if (currentTheme == null)
        {
            currentTheme = LoadThemeFromResources();
        }
    }

    public static UITheme LoadThemeFromResources()
    {
        var result = Resources.Load<UITheme>("UITheme");
        if (result == null)
        {
            result = Resources.Load<UITheme>("DefaultUITheme");
        }
        return result;
    }

    public void SetTheme(UITheme newTheme)
    {
        currentTheme = newTheme;
        
        ThemeChanged?.Invoke();
    }
}
