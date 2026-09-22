using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ThemedLayout : ThemedElement
{
    [SerializeField] private UISpacingSize spacing = UISpacingSize.Sm;
    [SerializeField] private UISpacingSize padding = UISpacingSize.Md;

    private HorizontalOrVerticalLayoutGroup layoutGroup;

    protected override void EnsureInitialized()
    {
        base.EnsureInitialized();
        layoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
    }

    protected override void ApplyThemeCore(UITheme theme)
    {
        if (layoutGroup == null || theme == null) return;

        layoutGroup.spacing = theme.Resolve(spacing);
        int pad = theme.Resolve(padding);
        
        layoutGroup.padding = new RectOffset(pad, pad, pad, pad);
    }
}