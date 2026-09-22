using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ThemedButton : ThemedElement
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private UIButtonSize size = UIButtonSize.Medium;

    [Header("Overrides")]
    [SerializeField] private Optional<Color> labelColorOverride;
    [SerializeField] private Optional<int> labelSizeOverride;

    private Image background;
    private Button button;

    protected override void EnsureInitialized()
    {
        base.EnsureInitialized();
        background = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    protected override void ApplyThemeCore(UITheme theme)
    {
        if (theme == null) return;

        // Background sprite
        background.sprite = theme.sprites.button;
        background.type = theme.sprites.button != null
            ? Image.Type.Sliced
            : Image.Type.Simple;

        if (label == null) return;

        var (role, paddingX, paddingY) = theme.metrics.ResolveButtonPaddings(size);

        int fontSize = labelSizeOverride.TryGet(out var s)
            ? s
            : theme.GetFontSize(role);

        label.font = theme.typography.primary;
        label.fontSize = fontSize;

        var lrt = label.rectTransform;
        lrt.anchorMin = Vector2.zero;
        lrt.anchorMax = Vector2.one;
        lrt.pivot = new Vector2(0.5f, 0.5f);
        lrt.offsetMin = new Vector2(paddingX, paddingY);
        lrt.offsetMax = new Vector2(-paddingX, -paddingY);

        // Grow the button to fit the label, but only if nothing else owns its size
        if (OwnsOwnSize())
        {
            label.ForceMeshUpdate();

            float w = label.preferredWidth  + paddingX * 2f;
            float h = label.preferredHeight + paddingY * 2f;

            var rt = (RectTransform)transform;
            rt.sizeDelta = new Vector2(w, h);
        }

        // Label color
        if (labelColorOverride.TryGet(out var lc)) label.color = lc;
        else label.color = theme.typography.textOnAccent;

        ApplyThemeColors(theme);
    }

    private void ApplyThemeColors(UITheme theme)
    {
        var colors = theme.accent.Resolve();

        bool wasEnabled = button.enabled;
        button.enabled = false; // Avoid flicker

        button.colors = colors;

        if (background != null)
        {
            background.color = theme.buttonBaseColor;
        }

        button.enabled = wasEnabled;
    }

    private bool OwnsOwnSize()
    {
        if (GetComponent<ContentSizeFitter>() != null) return false;

        var le = GetComponent<LayoutElement>();
        if (le != null && le.ignoreLayout) return true;

        // Stretched anchors: RectTransform size is driven by the parent's rect, not by us
        var rt = (RectTransform)transform;
        bool isStretched = !Mathf.Approximately(rt.anchorMin.x, rt.anchorMax.x)
                      || !Mathf.Approximately(rt.anchorMin.y, rt.anchorMax.y);
        if (isStretched) return false;

        if (IsSizeControlledByLayout()) return false;

        return true;
    }

    private bool IsSizeControlledByLayout()
    {
        var lg = FindOwningLayoutGroup();
        if (lg == null) return false;

        return lg switch
        {
            HorizontalOrVerticalLayoutGroup hvg => hvg.childControlWidth || hvg.childControlHeight,
            GridLayoutGroup => true,
            _ => false
        };
    }

    private LayoutGroup FindOwningLayoutGroup()
    {
        var t = transform.parent;
        while (t != null)
        {
            var lg = t.GetComponent<LayoutGroup>();
            if (lg != null) return lg;
            t = t.parent;
        }
        return null;
    }
}