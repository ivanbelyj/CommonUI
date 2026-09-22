using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class ThemedScrollbar : ThemedElement
{
    [SerializeField] private Image track;
    [SerializeField] private Image handle;

    [Header("Overrides")]
    [SerializeField] private Optional<Color> trackColorOverride;
    [SerializeField] private Optional<Color> handleColorOverride;
    [SerializeField] private Optional<int> thicknessOverride;

    private Scrollbar scrollbar;

    protected override void EnsureInitialized()
    {
        base.EnsureInitialized();
        scrollbar = GetComponent<Scrollbar>();
        if (handle == null && scrollbar.handleRect != null)
            handle = scrollbar.handleRect.GetComponent<Image>();
    }

    protected override void ApplyThemeCore(UITheme theme)
    {
        if (theme == null) return;

        // Thickness
        int thickness = thicknessOverride.TryGet(out var t)
            ? t
            : theme.metrics.scrollbarThickness;

        var rt = (RectTransform)transform;
        bool horizontal =
            scrollbar.direction == Scrollbar.Direction.LeftToRight ||
            scrollbar.direction == Scrollbar.Direction.RightToLeft;

        rt.sizeDelta = horizontal
            ? new Vector2(rt.sizeDelta.x, thickness)
            : new Vector2(thickness, rt.sizeDelta.y);

        // Track
        if (track != null)
        {
            track.sprite = theme.sprites.scrollbarTrack;
            track.type = theme.sprites.scrollbarTrack != null
                ? Image.Type.Sliced
                : Image.Type.Simple;

            if (trackColorOverride.TryGet(out var tc)) track.color = tc;
            else track.color = theme.surfaces.scrollbarTrack;
        }

        // Handle
        if (handle != null)
        {
            handle.sprite = theme.sprites.scrollbarHandle;
            handle.type = theme.sprites.scrollbarHandle != null
                ? Image.Type.Sliced
                : Image.Type.Simple;

            if (handleColorOverride.TryGet(out var hc)) handle.color = hc;
            else handle.color = theme.surfaces.scrollbarHandle;
        }
    }
}