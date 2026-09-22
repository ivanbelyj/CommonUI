using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(
    fileName = "UITheme",
    menuName = "UI/Theme",
    order = 52)]
public class UITheme : ScriptableObject
{
    [Serializable]
    public class Typography
    {
        [Header("Fonts")]
        [Tooltip("Default proportional font used for most UI text.")]
        public TMP_FontAsset primary;

        [Tooltip("Monospaced font used for code, terminal output and aligned numeric displays.")]
        public TMP_FontAsset monospace;

        [Header("Sizes")]
        [Tooltip("Largest text. Titles, splash screens, hero headings.")]
        public int display = 64;

        [Tooltip("Section headings and major titles.")]
        public int heading = 44;

        [Tooltip("Sub-section headings, between Heading and Body.")]
        public int subheading = 32;

        [Tooltip("Default body text. Most UI labels use this size.")]
        public int body = 24;

        [Tooltip("Smallest readable text. Footnotes, hints, secondary annotations.")]
        public int caption = 18;

        [Header("Text Colors")]
        [Tooltip("Primary emphasis. Default color for most text.")]
        public Color textPrimary = new Color32(0xEC, 0xEC, 0xEC, 0xFF);

        [Tooltip("Secondary emphasis. Supporting text, less prominent than primary.")]
        public Color textSecondary = new Color32(0xA8, 0xA8, 0xA8, 0xFF);

        [Tooltip("Muted emphasis. Hints, placeholders, disabled-looking static text.")]
        public Color textMuted = new Color32(0x6E, 0x6E, 0x6E, 0xFF);

        [Tooltip("Text drawn on top of accent-colored fills (buttons, active tabs, badges).")]
        public Color textOnAccent = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    }

    [Serializable]
    public class Surfaces
    {
        [Tooltip("Root background. The darkest layer, sits behind everything.")]
        public Color baseSurface = new Color32(0x12, 0x12, 0x14, 0xFF);

        [Tooltip("Panels, focus-mode backdrop, inventory background.")]
        public Color panelSurface = new Color32(0x1C, 0x1C, 0x20, 0xFF);

        [Tooltip("Overlays and modals on top of panels. Slightly lighter to separate.")]
        public Color overlaySurface = new Color32(0x26, 0x26, 0x2C, 0xFF);

        [Tooltip("Scrollbar track. The groove the handle slides along.")]
        public Color scrollbarTrack = new Color32(0x14, 0x14, 0x18, 0xFF);

        [Tooltip("Scrollbar handle. The draggable thumb.")]
        public Color scrollbarHandle = new Color32(0x50, 0x50, 0x58, 0xFF);
    }

    [Serializable]
    public class Accent
    {
        public float fadeDuration = 0.2f;

        [Tooltip("Default fill color for interactive elements (buttons, toggles, active states).")]
        public Color normal = new Color32(0x3B, 0x6E, 0xA5, 0xFF);

        [Tooltip("Fill color on hover.")]
        public Color hover = new Color32(0x4A, 0x82, 0xBE, 0xFF);

        [Tooltip("Fill color while pressed.")]
        public Color pressed = new Color32(0x2E, 0x58, 0x84, 0xFF);

        [Tooltip("Fill color when focused via keyboard or gamepad navigation.")]
        public Color focused = new Color32(0x5A, 0x94, 0xD4, 0xFF);

        [Tooltip("Fill color when the element is disabled.")]
        public Color disabled = new Color32(0x3A, 0x3A, 0x40, 0xFF);

        public ColorBlock Resolve()
        {
            return new ColorBlock
            {
                normalColor = normal,
                highlightedColor = hover,
                pressedColor = pressed,
                selectedColor = focused,
                disabledColor = disabled,
                fadeDuration = fadeDuration,
                colorMultiplier = 1f
            };
        }
    }

    [Serializable]
    public class Sprites
    {
        [Tooltip("Panel background 9-slice.")]
        public Sprite panel;

        [Tooltip("Button 9-slice. Also used by input fields and toggles as a fallback.")]
        public Sprite button;

        [Tooltip("Optional dedicated input field 9-slice. Falls back to button.")]
        public Sprite inputField;

        [Tooltip("Checkmark sprite shown when a toggle is on.")]
        [FormerlySerializedAs(("checkmark"))]
        public Sprite dropdownCheckmark;

        [Tooltip("Arrow sprite (dropdown expand indicator, submenu hint).")]
        [FormerlySerializedAs(("arrow"))]
        public Sprite dropdownArrow;

        [Tooltip("Scrollbar track 9-slice.")]
        public Sprite scrollbarTrack;

        [Tooltip("Scrollbar handle 9-slice.")]
        public Sprite scrollbarHandle;
    }

    [Header("Typography")]
    public Typography typography = new Typography();

    [Header("Surfaces")]
    public Surfaces surfaces = new Surfaces();

    public Color buttonBaseColor = new(0.32f, 0.32f, 0.32f);

    [Header("Accent")]
    [Tooltip("Accent is a fill color for interactive elements, not a text color. " +
             "Use typography.textOnAccent for the label that sits on top of it.")]
    public Accent accent = new();

    [Header("Metrics")]
    public UIMetrics metrics = new();

    [Header("Sprites")]
    public Sprites sprites;
}
