using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ThemedPanel : ThemedElement
{
    [SerializeField] private UISurface surface = UISurface.Panel;

    [Header("Overrides")]
    [SerializeField] private Optional<Color> colorOverride;

    private Image image;

    protected override void EnsureInitialized()
    {
        base.EnsureInitialized();
        image = GetComponent<Image>();
    }

    protected override void ApplyThemeCore(UITheme theme)
    {
        if (theme == null) return;

        if (colorOverride.TryGet(out var c)) image.color = c;
        else image.color = theme.GetSurfaceColor(surface);

        image.sprite = theme.sprites.panel;
        image.type = theme.sprites.panel != null
            ? Image.Type.Sliced
            : Image.Type.Simple;
    }
}
