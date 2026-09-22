using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ThemedText : ThemedElement
{
    [SerializeField] private UIElementRole role = UIElementRole.Body;
    [SerializeField] private UIElementEmphasis emphasis = UIElementEmphasis.Primary;
    [SerializeField] private bool useMonospace;

    [Header("Overrides")]
    [SerializeField] private Optional<TMP_FontAsset> fontOverride;
    [SerializeField] private Optional<int> fontSizeOverride;
    [SerializeField] private Optional<Color> colorOverride;

    private TMP_Text text;

    protected override void EnsureInitialized()
    {
        base.EnsureInitialized();
        text = GetComponent<TMP_Text>();
    }

    protected override void ApplyThemeCore(UITheme theme)
    {
        // Font
        if (fontOverride.TryGet(out var f)) text.font = f;
        else if (theme != null) text.font = useMonospace ? theme.typography.monospace : theme.typography.primary;

        // Size
        if (fontSizeOverride.TryGet(out var s)) text.fontSize = s;
        else if (theme != null) text.fontSize = theme.GetFontSize(role);

        // Color
        if (colorOverride.TryGet(out var c)) text.color = c;
        else if (theme != null) text.color = theme.GetTextColor(emphasis);
    }
}