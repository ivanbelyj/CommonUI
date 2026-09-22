using UnityEngine;

public static class UIThemeExtensions
{
    public static int GetFontSize(this UITheme theme, UIElementRole role) => role switch
    {
        UIElementRole.Display    => theme.typography.display,
        UIElementRole.Heading    => theme.typography.heading,
        UIElementRole.Subheading => theme.typography.subheading,
        UIElementRole.Body       => theme.typography.body,
        UIElementRole.Caption    => theme.typography.caption,
        _ => theme.typography.body
    };

    public static Color GetTextColor(this UITheme theme, UIElementEmphasis emphasis) => emphasis switch
    {
        UIElementEmphasis.Primary   => theme.typography.textPrimary,
        UIElementEmphasis.Secondary => theme.typography.textSecondary,
        UIElementEmphasis.Muted     => theme.typography.textMuted,
        _ => theme.typography.textPrimary
    };

    public static Color GetSurfaceColor(this UITheme theme, UISurface surface) => surface switch
    {
        UISurface.Base    => theme.surfaces.baseSurface,
        UISurface.Panel   => theme.surfaces.panelSurface,
        UISurface.Overlay => theme.surfaces.overlaySurface,
        UISurface.None    => new Color(0, 0, 0, 0),
        _ => theme.surfaces.panelSurface
    };

    public static int Resolve(this UITheme theme, UISpacingSize size) => size switch
    {
        UISpacingSize.Zero => 0,
        UISpacingSize.Xs   => theme.metrics.spaceXs,
        UISpacingSize.Sm   => theme.metrics.spaceSm,
        UISpacingSize.Md   => theme.metrics.spaceMd,
        UISpacingSize.Lg   => theme.metrics.spaceLg,
        UISpacingSize.Xl   => theme.metrics.spaceXl,
        _ => 0
    };
}
