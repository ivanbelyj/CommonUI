using System;
using UnityEngine;

[Serializable]
public class UIMetrics
{
    [Header("Spacing scale (px)")]
    [Tooltip("Extra-small step. Tight gaps between related elements.")]
    public int spaceXs = 4;
    [Tooltip("Small step. Gaps inside a component.")]
    public int spaceSm = 8;
    [Tooltip("Medium step. Default gap between sibling components.")]
    public int spaceMd = 16;
    [Tooltip("Large step. Gaps between sections.")]
    public int spaceLg = 24;
    [Tooltip("Extra-large step. Margins around major blocks.")]
    public int spaceXl = 32;

    [Range(0.25f, 1f)]
    [Tooltip("Vertical padding as a fraction of horizontal padding.")]
    public float buttonPaddingYRatio = 0.5f;

    [Header("Scrollbar")]
    [Tooltip("Thickness of scrollbar track and handle in pixels.")]
    public int scrollbarThickness = 32;

    public (UIElementRole role, int paddingX, int paddingY) ResolveButtonPaddings(UIButtonSize size)
    {
        var (role, paddingX) = ResolveButtonPaddingX(size);
        int paddingY = Mathf.RoundToInt(paddingX * buttonPaddingYRatio);
        return (role, paddingX, paddingY);
    }

    private (UIElementRole role, int padding) ResolveButtonPaddingX(UIButtonSize size) => size switch
    {
        UIButtonSize.Small  => (UIElementRole.Caption, spaceSm),
        UIButtonSize.Medium => (UIElementRole.Body,    spaceMd),
        UIButtonSize.Large  => (UIElementRole.Heading, spaceLg),
        _ => (UIElementRole.Body, spaceMd)
    };
}
