using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class ThemedElement : MonoBehaviour
{
#if UNITY_EDITOR
    [ContextMenu("Refresh From Theme")]
    private void RefreshFromThemeFromContextMenu()
    {
        RefreshFromTheme();
    }
#endif

    protected virtual void Awake()
    {
        EnsureInitialized();
    }

    protected virtual void Start()
    {
        // Retry subscription in case ThemeManager.Instance was null during OnEnable.
        Resubscribe();
        ApplyTheme();
    }
    
    protected virtual void OnEnable()
    {
        Subscribe();
        ApplyTheme();
    }

    protected virtual void OnDisable()
    {
        Unsubscribe();
    }

    protected virtual void EnsureInitialized()
    {
        
    }

    protected void Subscribe()
    {
        if (ThemeManager.Instance != null)
        {
            ThemeManager.Instance.ThemeChanged += ApplyTheme;
        }
    }

    protected void Unsubscribe()
    {
        if (ThemeManager.Instance != null)
        {
            ThemeManager.Instance.ThemeChanged -= ApplyTheme;
        }
    }

    private void Resubscribe()
    {
        Unsubscribe();
        Subscribe();
    }

    protected UITheme GetCurrentTheme()
    {
        // ThemeManager -> Resources -> default theme
        if (ThemeManager.Instance != null && ThemeManager.Instance.CurrentTheme != null)
        {
            return ThemeManager.Instance.CurrentTheme;
        }
        
        var theme = ThemeManager.LoadThemeFromResources();
        if (theme != null)
        {
            return theme;
        }
        
        return null;
    }

    protected void ApplyTheme()
    {
        var theme = GetCurrentTheme();
        if (theme != null)
        {
            ApplyThemeCore(theme);
        }
    }

    protected abstract void ApplyThemeCore(UITheme theme);

    #if UNITY_EDITOR
    /// <summary>
    /// Re-applies the current theme to this element. Editor-only,
    /// invoked from context menus and Tools menu.
    /// </summary>
    public void RefreshFromTheme()
    {
        Undo.RegisterFullObjectHierarchyUndo(gameObject, "Refresh from Theme");
        EnsureInitialized();
        ApplyTheme();
    }
    #endif
}