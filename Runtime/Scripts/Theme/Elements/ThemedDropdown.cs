using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Dropdown))]
[RequireComponent(typeof(Image))]
public class ThemedDropdown : ThemedElement
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Image arrow;
    [SerializeField] private Image itemCheckmark;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Toggle itemToggle;

    [Header("Overrides")]
    [SerializeField] private Optional<Color>  labelColorOverride;
    [SerializeField] private Optional<Color>  arrowColorOverride;
    [SerializeField] private Optional<int>    fontSizeOverride;

    private Image background;
    private TMP_Dropdown dropdown;

    protected override void EnsureInitialized()
    {
        base.EnsureInitialized();

        if (background == null) background = GetComponent<Image>();
        if (dropdown == null) dropdown = GetComponent<TMP_Dropdown>();

        if (label == null && dropdown != null && dropdown.captionText != null)
            label = dropdown.captionText;

        if (arrow == null)
        {
            var arrowTransform = transform.Find("Arrow");
            if (arrowTransform != null) arrow = arrowTransform.GetComponent<Image>();
        }
    }

    protected override void ApplyThemeCore(UITheme theme)
    {
        if (theme == null) return;

        ApplyBackground(theme);
        ApplyLabel(theme);
        ApplyArrow(theme);
        ApplyColors(theme);

        ApplyItem(theme);
    }

    private void ApplyItem(UITheme theme)
    {
        var checkmarkSprite = theme.sprites.dropdownCheckmark;
        if (checkmarkSprite != null) itemCheckmark.sprite = checkmarkSprite;

        if (itemBackground != null)
        {
            itemBackground.color = theme.buttonBaseColor;
        }
        if (itemToggle != null)
        {
            itemToggle.colors = theme.accent.Resolve();
        }
    }

    private void ApplyBackground(UITheme theme)
    {
        var bg = background;
        if (bg == null) return;

        var sprite = theme.sprites.button;
        bg.sprite = sprite;
        bg.type = sprite != null ? Image.Type.Sliced : Image.Type.Simple;
        bg.color = theme.buttonBaseColor;
    }

    private void ApplyLabel(UITheme theme)
    {
        if (label == null) return;

        label.font = theme.typography.primary;
        label.fontSize = fontSizeOverride.TryGet(out var fs)
            ? fs
            : theme.GetFontSize(UIElementRole.Body);

        label.color = labelColorOverride.TryGet(out var c)
            ? c
            : theme.typography.textOnAccent;
    }

    private void ApplyArrow(UITheme theme)
    {
        if (arrow == null) return;

        var sprite = theme.sprites.dropdownArrow;
        if (sprite != null) arrow.sprite = sprite;

        arrow.color = arrowColorOverride.TryGet(out var c)
            ? c
            : theme.typography.textOnAccent;

        arrow.raycastTarget = false;
    }

    private void ApplyColors(UITheme theme)
    {
        if (dropdown == null) return;

        var a = theme.accent;

        dropdown.colors = theme.accent.Resolve();
    }
}