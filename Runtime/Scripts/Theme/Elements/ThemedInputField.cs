using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
[RequireComponent(typeof(Image))]
public class ThemedInputField : ThemedElement
{
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private TMP_Text placeholder;
    [SerializeField] private Image background;
    [SerializeField] private UISurface surface = UISurface.Overlay;
    [SerializeField] private UIButtonSize size = UIButtonSize.Medium;

    [Header("Overrides")]
    [SerializeField] private Optional<Color>  textColorOverride;
    [SerializeField] private Optional<Color>  placeholderColorOverride;
    [SerializeField] private Optional<Color>  selectionColorOverride;
    [SerializeField] private Optional<Color>  caretColorOverride;
    [SerializeField] private Optional<int>    fontSizeOverride;
    [SerializeField] private Optional<Sprite> backgroundSpriteOverride;

    private TMP_InputField inputField;

    protected override void EnsureInitialized()
    {
        base.EnsureInitialized();

        if (inputField == null) inputField = GetComponent<TMP_InputField>();

        if (background == null) background = GetComponent<Image>();

        if (textComponent == null && inputField != null && inputField.textComponent != null)
            textComponent = inputField.textComponent;

        if (placeholder == null && inputField != null && inputField.placeholder != null)
            placeholder = inputField.placeholder as TMP_Text;
    }

    protected override void ApplyThemeCore(UITheme theme)
    {
        if (theme == null) return;

        var (role, padX, padY) = theme.metrics.ResolveButtonPaddings(size);

        ApplyBackground(theme);
        ApplyText(theme, role);
        ApplyPlaceholder(theme, role);
        ApplyInputFieldColors(theme);
        ApplyViewportPadding(padX, padY);
    }

    private void ApplyBackground(UITheme theme)
    {
        var bg = background;
        if (bg == null) return;

        var sprite = backgroundSpriteOverride.TryGet(out var s)
            ? s
            : theme.sprites.inputField;

        bg.sprite = sprite;
        bg.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
        bg.color = theme.GetSurfaceColor(surface);
    }

    private void ApplyText(UITheme theme, UIElementRole role)
    {
        if (textComponent == null) return;

        textComponent.font = theme.typography.primary;
        textComponent.fontSize = fontSizeOverride.TryGet(out var s)
            ? s
            : theme.GetFontSize(role);

        textComponent.color = textColorOverride.TryGet(out var c)
            ? c
            : theme.typography.textPrimary;
    }

    private void ApplyPlaceholder(UITheme theme, UIElementRole role)
    {
        if (placeholder == null) return;

        placeholder.font = theme.typography.primary;
        placeholder.fontSize = fontSizeOverride.TryGet(out var s)
            ? s
            : theme.GetFontSize(role);

        placeholder.color = placeholderColorOverride.TryGet(out var c)
            ? c
            : theme.typography.textMuted;
    }

    private void ApplyInputFieldColors(UITheme theme)
    {
        var field = inputField;
        if (field == null) return;

        if (selectionColorOverride.TryGet(out var sc))
        {
            field.selectionColor = sc;
        }
        else
        {
            var f = theme.accent.focused;
            field.selectionColor = new Color(f.r, f.g, f.b, 0.5f);
        }

        field.caretColor = caretColorOverride.TryGet(out var cc)
            ? cc
            : theme.typography.textPrimary;
    }

    private void ApplyViewportPadding(int padX, int padY)
    {
        var field = inputField;
        if (field == null) return;

        var viewport = field.textViewport;
        if (viewport == null) return;

        viewport.anchorMin = Vector2.zero;
        viewport.anchorMax = Vector2.one;
        viewport.pivot     = new Vector2(0.5f, 0.5f);
        viewport.offsetMin = new Vector2( padX,  padY);
        viewport.offsetMax = new Vector2(-padX, -padY);
    }
}