using TMPro;
using UI.TestingTool;
using UnityEngine;
using UnityEngine.UI;

namespace DevMenu;

internal static class DevMenuTheme
{
    private static readonly Color Surface = Rgb(0, 0, 0, 0.00f);
    private static readonly Color SurfaceHit = Rgb(0, 0, 0, 0.025f);
    private static readonly Color RowFill = Rgb(0, 0, 0, 0.035f);
    private static readonly Color RowHover = Rgb(170, 205, 255, 0.11f);
    private static readonly Color RowPressed = Rgb(255, 255, 255, 0.15f);
    private static readonly Color Line = Rgb(228, 236, 248, 0.36f);
    private static readonly Color LineSoft = Rgb(228, 236, 248, 0.18f);
    private static readonly Color Accent = Rgb(145, 190, 255, 0.82f);
    private static readonly Color Success = Rgb(95, 214, 161, 0.78f);
    private static readonly Color Danger = Rgb(234, 99, 107, 0.82f);
    private static readonly Color TextPrimary = Rgb(248, 250, 253, 0.98f);
    private static readonly Color TextSecondary = Rgb(205, 214, 226, 0.90f);
    private static readonly Color TextMuted = Rgb(148, 160, 176, 0.86f);
    private static readonly Color TextGold = Rgb(246, 222, 166, 0.98f);
    private static readonly Vector2 TextShadowDistance = new(1f, -1f);

    private static Sprite _solidSprite;

    public static void Apply(UI.TestingTool.Panel panel)
    {
        if (panel == null)
        {
            return;
        }

        LayoutPanel(panel);
        StyleSurface(panel._main, false);
        StyleSurface(panel._mapList, false);
        StyleSurface(panel._gearList, false);
        StyleSurface(panel._log.gameObject, false);
        StyleSurface(panel._dataControl, false);
        StyleSurface(panel._bonusStatPanel, false);
        StyleTree(panel.transform);
    }

    public static void SetActivePanel(UI.TestingTool.Panel panel, UI.TestingTool.Panel.Type active)
    {
        if (panel == null)
        {
            return;
        }

        StyleSurface(panel._main, active == UI.TestingTool.Panel.Type.Main);
        StyleSurface(panel._mapList, active == UI.TestingTool.Panel.Type.MapList);
        StyleSurface(panel._gearList, active == UI.TestingTool.Panel.Type.GearList);
        StyleSurface(panel._log.gameObject, active == UI.TestingTool.Panel.Type.Log);
        StyleSurface(panel._dataControl, active == UI.TestingTool.Panel.Type.DataControl);
        StyleSurface(panel._bonusStatPanel, active == UI.TestingTool.Panel.Type.BonusStat);
    }

    public static void StyleTree(Component root)
    {
        if (root == null)
        {
            return;
        }

        ClearDecorativeImages(root);

        foreach (var button in root.GetComponentsInChildren<Button>(true))
        {
            StyleButton(button, Line);
        }

        StyleSemanticButtons(root);

        foreach (var toggle in root.GetComponentsInChildren<Toggle>(true))
        {
            StyleToggle(toggle);
        }

        foreach (var slider in root.GetComponentsInChildren<Slider>(true))
        {
            StyleSlider(slider);
        }

        foreach (var scrollbar in root.GetComponentsInChildren<Scrollbar>(true))
        {
            StyleScrollbar(scrollbar);
        }

        foreach (var input in root.GetComponentsInChildren<TMP_InputField>(true))
        {
            StyleInput(input);
        }

        foreach (var text in root.GetComponentsInChildren<TMP_Text>(true))
        {
            StyleTmpText(text);
        }

        foreach (var text in root.GetComponentsInChildren<Text>(true))
        {
            StyleLegacyText(text);
        }

        foreach (var element in root.GetComponentsInChildren<PlayerStatElement>(true))
        {
            StylePlayerStatElement(element);
        }
    }

    public static void StyleDataControl(DataControl self)
    {
        if (self == null)
        {
            return;
        }

        LayoutDataControl(self);
        StyleTree(self.transform);
        StyleButton(self._randomItem, Accent);
        StyleButton(self._unlockAllItems, Success);
        StyleButton(self._unlockAllUpgrades, Success);
        StyleButton(self._lockAllUpgrades, Danger);
        StyleButton(self._resetProgress, Danger);
        StyleButton(self._itemReset, Danger);
        StyleButton(self._upgradeReset, Danger);
        StyleButton(self._allGearReset, Danger);
    }

    public static void StyleGearList(GearList self)
    {
        if (self == null)
        {
            return;
        }

        LayoutGearList(self);
        StyleTree(self.transform);
        StyleInput(self._inputField);
        StyleButton(self._head, self._currentFilter == GearList.Filter.Weapon ? Accent : Line);
        StyleButton(self._item, self._currentFilter == GearList.Filter.Item ? Accent : Line);
        StyleButton(self._essence, self._currentFilter == GearList.Filter.Essence ? Accent : Line);
        StyleButton(self._upgrade, self._currentFilter == GearList.Filter.Upgrade ? Accent : Line);

        foreach (var element in self._gearListElements)
        {
            StyleGearElement(element);
        }

        foreach (var button in self._upgradeListElements)
        {
            StyleListButton(button);
        }

        foreach (var button in self._inscriptionListElements)
        {
            StyleListButton(button);
        }
    }

    public static void StyleMapList(MapList self)
    {
        if (self == null)
        {
            return;
        }

        LayoutMapList(self);
        StyleTree(self.transform);
        StyleButton(self._back, Accent);
    }

    public static void StyleLog(Log self)
    {
        if (self == null)
        {
            return;
        }

        LayoutLog(self);
        StyleTree(self.transform);
        StyleButton(self._copy, Accent);
        StyleButton(self._clear, Danger);

        if (self._text != null)
        {
            self._text.color = TextSecondary;
            self._text.fontSize = Mathf.Clamp(self._text.fontSize, 13f, 16f);
            self._text.lineSpacing = 2f;
            AddTextShadow(self._text.gameObject);
        }
    }

    public static void StyleBonusStatPanel(Component root)
    {
        if (root == null)
        {
            return;
        }

        LayoutBonusStatPanel(root);
        StyleTree(root);

        foreach (var element in root.GetComponentsInChildren<PlayerStatElement>(true))
        {
            StylePlayerStatElement(element);
        }

        foreach (var input in root.GetComponentsInChildren<TMP_InputField>(true))
        {
            StyleStatInput(input);
        }

        foreach (var button in root.GetComponentsInChildren<Button>(true))
        {
            if (ButtonText(button).Contains("应用"))
            {
                StyleButton(button, Accent);
            }
        }
    }

    public static void StylePlayerStatElement(PlayerStatElement element)
    {
        if (element == null)
        {
            return;
        }

        var image = Ensure<Image>(element.gameObject);
        image.sprite = SolidSprite();
        image.color = RowFill;
        image.raycastTarget = false;
        EnsureFrame(element.gameObject, LineSoft, 1f);
        ClearShadow(element.gameObject);
        LayoutPlayerStatElement(element);

        if (element._name != null)
        {
            element._name.color = TextPrimary;
            element._name.enableAutoSizing = true;
            element._name.fontSizeMin = 12f;
            element._name.fontSizeMax = 19f;
            element._name.overflowMode = TextOverflowModes.Ellipsis;
            AddTextShadow(element._name.gameObject);
        }

        StyleStatInput(element._percent);
        StyleStatInput(element._percentPoint);
        StyleStatInput(element._constant);
        HideRepeatedStatLabels(element);

        if (element._final != null)
        {
            element._final.color = TextGold;
            element._final.enableAutoSizing = true;
            element._final.fontSizeMin = 12f;
            element._final.fontSizeMax = 19f;
            element._final.overflowMode = TextOverflowModes.Ellipsis;
            AddTextShadow(element._final.gameObject);
        }
    }

    private static void HideRepeatedStatLabels(PlayerStatElement element)
    {
        foreach (var text in element.GetComponentsInChildren<TMP_Text>(true))
        {
            if (text == element._name
                || text == element._final
                || text.GetComponentInParent<Button>() != null
                || text.GetComponentInParent<TMP_InputField>() != null)
            {
                continue;
            }

            text.gameObject.SetActive(false);
        }
    }

    public static void StyleGearElement(GearListElement element)
    {
        if (element == null)
        {
            return;
        }

        StyleListButton(element._button);
        LayoutGearElement(element);

        if (element._thumbnail != null)
        {
            element._thumbnail.preserveAspect = true;
            element._thumbnail.color = Color.white;
        }

        if (element._text != null)
        {
            element._text.enableAutoSizing = true;
            element._text.fontSizeMin = 11f;
            element._text.fontSizeMax = 16f;
            element._text.overflowMode = TextOverflowModes.Ellipsis;
            AddTextShadow(element._text.gameObject);
        }
    }

    public static void StyleListElement(GameObject element)
    {
        if (element == null)
        {
            return;
        }

        var button = element.GetComponentInChildren<Button>(true);
        if (button != null)
        {
            StyleListButton(button);
        }

        foreach (var text in element.GetComponentsInChildren<TMP_Text>(true))
        {
            text.enableAutoSizing = true;
            text.fontSizeMin = 11f;
            text.fontSizeMax = 16f;
            text.color = TextPrimary;
            text.overflowMode = TextOverflowModes.Ellipsis;
            AddTextShadow(text.gameObject);
        }
    }

    private static void LayoutPanel(UI.TestingTool.Panel panel)
    {
        StretchPanel(panel._main, 0.045f, 0.10f, 0.46f, 0.92f);
        StretchPanel(panel._mapList, 0.035f, 0.07f, 0.965f, 0.925f);
        StretchPanel(panel._gearList, 0.035f, 0.07f, 0.965f, 0.925f);
        StretchPanel(panel._dataControl, 0.055f, 0.09f, 0.72f, 0.925f);
        StretchPanel(panel._bonusStatPanel, 0.035f, 0.06f, 0.965f, 0.925f);
        if (panel._log != null)
        {
            StretchPanel(panel._log.gameObject, 0.055f, 0.11f, 0.83f, 0.90f);
        }

        LayoutMainPanel(panel);
    }

    private static void LayoutMainPanel(UI.TestingTool.Panel panel)
    {
        Place(panel._mapName, 0f, 0f, 700f, 52f);
        Place(panel._version, 0f, 52f, 360f, 26f);

        LayoutButtons(new[]
        {
            panel._openMapList,
            panel._openGearList,
            panel._nextStage,
            panel._nextMap
        }, 0f, 92f, 196f, 34f, 2);

        LayoutButtons(new[]
        {
            panel._getGold,
            panel._getDarkquartz,
            panel._getBone,
            panel._getHeartQuartz
        }, 0f, 178f, 196f, 34f, 2);

        LayoutButtons(new[]
        {
            panel._awake,
            panel._rerollSkill,
            panel._damageBuff,
            panel._hp10k,
            panel._noCooldown,
            panel._shield10,
            panel._right3,
            panel._testMap
        }, 0f, 264f, 196f, 34f, 2);

        Place(panel._hardmodeToggle, 0f, 436f, 180f, 30f);
        Place(panel._hardmodeLevelSlider, 0f, 478f, 360f, 28f);
        Place(panel._hardmodeClearedLevelSlider, 0f, 518f, 360f, 28f);
        Place(panel._hardmodeClearedCountSlider, 0f, 558f, 360f, 28f);
        Place(panel._timeScaleSlider, 0f, 606f, 360f, 28f);
        Place(panel._timeScaleReset, 378f, 604f, 90f, 30f);
        Place(panel._infiniteRevive, 0f, 654f, 180f, 30f);
        Place(panel._verification, 202f, 654f, 180f, 30f);
    }

    private static void LayoutGearList(GearList self)
    {
        Place(self._inputField, 0f, 0f, 330f, 34f);
        LayoutButtons(new[] { self._head, self._item, self._essence, self._upgrade }, 348f, 0f, 132f, 34f, 4);
        Place(self._unlockSetting, 0f, 48f, 180f, 28f);
        Place(self._lockSetting, 194f, 48f, 180f, 28f);
        StretchContent(self._gridContainer, 0f, 92f, 0f, 0f);

        var grid = Ensure<GridLayoutGroup>(self._gridContainer.gameObject);
        grid.cellSize = new Vector2(260f, 46f);
        grid.spacing = new Vector2(8f, 8f);
        grid.constraint = GridLayoutGroup.Constraint.Flexible;
        grid.childAlignment = TextAnchor.UpperLeft;
    }

    private static void LayoutMapList(MapList self)
    {
        Place(self._back, 0f, 0f, 120f, 34f);
        Place(self._currentChapterFilterText, 136f, 0f, 260f, 34f);
        Place(self._enemy, 0f, 50f, 150f, 28f);
        Place(self._fieldNPC, 164f, 50f, 150f, 28f);
        Place(self._darkEnemy, 328f, 50f, 170f, 28f);
        LayoutButtons(new[]
        {
            self._tutorial,
            self._castle,
            self._chapter1,
            self._chapter2,
            self._chapter3,
            self._chapter4,
            self._chapter5,
            self._hardmodeCastle,
            self._hardChapter1,
            self._hardChapter2,
            self._hardChapter3,
            self._hardChapter4,
            self._hardChapter5,
            self._hardChapter6
        }, 0f, 94f, 124f, 31f, 7, 8f, 8f);
        StretchContent(self._gridContainer, 0f, 174f, 0f, 0f);

        var grid = Ensure<GridLayoutGroup>(self._gridContainer.gameObject);
        grid.cellSize = new Vector2(255f, 32f);
        grid.spacing = new Vector2(8f, 6f);
        grid.constraint = GridLayoutGroup.Constraint.Flexible;
        grid.childAlignment = TextAnchor.UpperLeft;
    }

    private static void LayoutDataControl(DataControl self)
    {
        LayoutButtons(new[]
        {
            self._randomItem,
            self._unlockAllItems,
            self._unlockAllUpgrades,
            self._firstClear,
            self._dCDefenseCleared,
            self._resetSeed,
            self._lockAllUpgrades,
            self._itemReset,
            self._upgradeReset,
            self._allGearReset,
            self._resetProgress,
            self._dCDefenseHint
        }, 0f, 0f, 210f, 34f, 2, 10f, 10f);

        Place(self._hardmodeClearedCountSlider, 0f, 236f, 430f, 30f);
        Place(self._hintTypeSlider, 0f, 286f, 430f, 30f);

        for (var i = 0; i < self._hintToggles.Length; i++)
        {
            Place(self._hintToggles[i], (i % 3) * 154f, 340f + (i / 3) * 34f, 145f, 28f);
        }
    }

    private static void LayoutLog(Log self)
    {
        Place(self._copy, 0f, 0f, 120f, 34f);
        Place(self._clear, 132f, 0f, 120f, 34f);
        Place(self._text, 0f, 52f, 1080f, 560f);
    }

    private static void LayoutBonusStatPanel(Component root)
    {
        EnsureStatHeader(root);

        var elements = root.GetComponentsInChildren<PlayerStatElement>(true);
        if (elements.Length > 0)
        {
            var parent = elements[0].transform.parent;
            var group = Ensure<VerticalLayoutGroup>(parent.gameObject);
            group.spacing = 6f;
            group.padding = new RectOffset(0, 0, 82, 0);
            group.childControlWidth = false;
            group.childControlHeight = false;
            group.childForceExpandWidth = false;
            group.childForceExpandHeight = false;
        }

        foreach (var element in elements)
        {
            LayoutPlayerStatElement(element);
        }
    }

    private static void LayoutPlayerStatElement(PlayerStatElement element)
    {
        var layout = Ensure<LayoutElement>(element.gameObject);
        layout.preferredWidth = 1180f;
        layout.preferredHeight = 40f;
        layout.minHeight = 40f;
        SetRectSize(element.transform, 1180f, 40f);

        Place(element._name, 10f, 6f, 250f, 28f);
        Place(element._percent, 330f, 6f, 118f, 28f);
        Place(element._percentPoint, 466f, 6f, 132f, 28f);
        Place(element._constant, 616f, 6f, 118f, 28f);

        var apply = element.GetComponentInChildren<Button>(true);
        if (apply != null)
        {
            Place(apply, 754f, 5f, 112f, 30f);
        }

        Place(element._final, 902f, 6f, 260f, 28f);
    }

    private static void EnsureStatHeader(Component root)
    {
        if (root == null || root.transform is not RectTransform)
        {
            return;
        }

        var header = root.transform.Find("DevMenuTurboStatHeader");
        if (header == null)
        {
            var headerObject = new GameObject("DevMenuTurboStatHeader", typeof(RectTransform));
            header = headerObject.transform;
            header.SetParent(root.transform, false);
        }

        Place(header, 0f, 38f, 1180f, 32f);
        EnsureHeaderText(header, "Name", "属性", 10f, 0f, 250f, 28f);
        EnsureHeaderText(header, "Percent", "百分比", 330f, 0f, 118f, 28f);
        EnsureHeaderText(header, "PercentPoint", "百分点", 466f, 0f, 132f, 28f);
        EnsureHeaderText(header, "Constant", "固定值", 616f, 0f, 118f, 28f);
        EnsureHeaderText(header, "Action", "操作", 754f, 0f, 112f, 28f);
        EnsureHeaderText(header, "Final", "最终值", 902f, 0f, 260f, 28f);
    }

    private static void EnsureHeaderText(Transform parent, string name, string value, float x, float y, float width, float height)
    {
        var child = parent.Find(name);
        if (child == null)
        {
            var childObject = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
            child = childObject.transform;
            child.SetParent(parent, false);
        }

        Place(child, x, y, width, height);
        var text = child.GetComponent<TextMeshProUGUI>();
        text.SetText(value);
        text.color = TextSecondary;
        text.alignment = TextAlignmentOptions.MidlineLeft;
        text.enableAutoSizing = true;
        text.fontSizeMin = 11f;
        text.fontSizeMax = 15f;
        text.overflowMode = TextOverflowModes.Ellipsis;
        AddTextShadow(text.gameObject);
    }

    private static void LayoutGearElement(GearListElement element)
    {
        SetRectSize(element._button.transform, 260f, 46f);
        Place(element._thumbnail, 8f, 7f, 32f, 32f);
        Place(element._text, 48f, 5f, 200f, 36f);
    }

    private static void StyleSurface(GameObject surface, bool active)
    {
        if (surface == null)
        {
            return;
        }

        var image = Ensure<Image>(surface);
        image.sprite = SolidSprite();
        image.color = active ? SurfaceHit : Surface;
        image.raycastTarget = false;
        ClearOutline(surface);
        ClearShadow(surface);
        EnsureFrame(surface, active ? Accent : LineSoft, active ? 2f : 1f);
    }

    private static void StyleButton(Button button, Color lineColor)
    {
        if (button == null)
        {
            return;
        }

        var image = button.targetGraphic as Image ?? button.GetComponent<Image>();
        if (image == null)
        {
            image = button.gameObject.AddComponent<Image>();
            button.targetGraphic = image;
        }

        image.sprite = SolidSprite();
        image.color = RowFill;
        image.raycastTarget = true;
        button.transition = Selectable.Transition.ColorTint;
        button.colors = new ColorBlock
        {
            normalColor = RowFill,
            highlightedColor = RowHover,
            pressedColor = RowPressed,
            selectedColor = WithAlpha(lineColor, 0.16f),
            disabledColor = Rgb(0, 0, 0, 0.02f),
            colorMultiplier = 1f,
            fadeDuration = 0.08f
        };

        ClearOutline(button.gameObject);
        ClearShadow(button.gameObject);
        EnsureFrame(button.gameObject, lineColor, 1f);

        foreach (var tmp in button.GetComponentsInChildren<TMP_Text>(true))
        {
            StyleButtonText(tmp);
        }

        foreach (var text in button.GetComponentsInChildren<Text>(true))
        {
            StyleButtonText(text);
        }
    }

    private static void StyleListButton(Button button)
    {
        StyleButton(button, LineSoft);

        if (button == null)
        {
            return;
        }

        foreach (var tmp in button.GetComponentsInChildren<TMP_Text>(true))
        {
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
        }

        foreach (var text in button.GetComponentsInChildren<Text>(true))
        {
            text.alignment = TextAnchor.MiddleLeft;
        }
    }

    private static void StyleToggle(Toggle toggle)
    {
        if (toggle == null)
        {
            return;
        }

        toggle.transition = Selectable.Transition.ColorTint;
        toggle.colors = new ColorBlock
        {
            normalColor = RowFill,
            highlightedColor = RowHover,
            pressedColor = RowPressed,
            selectedColor = WithAlpha(Accent, 0.16f),
            disabledColor = Rgb(0, 0, 0, 0.02f),
            colorMultiplier = 1f,
            fadeDuration = 0.08f
        };

        if (toggle.targetGraphic != null)
        {
            toggle.targetGraphic.color = RowFill;
            if (toggle.targetGraphic is Image image)
            {
                image.sprite = SolidSprite();
            }
            EnsureFrame(toggle.targetGraphic.gameObject, LineSoft, 1f);
        }

        if (toggle.graphic != null)
        {
            toggle.graphic.color = Accent;
        }
    }

    private static void StyleSlider(Slider slider)
    {
        if (slider == null)
        {
            return;
        }

        if (slider.targetGraphic != null)
        {
            slider.targetGraphic.color = Accent;
        }

        if (slider.fillRect != null)
        {
            var fill = slider.fillRect.GetComponent<Image>();
            if (fill != null)
            {
                fill.color = Accent;
            }
        }

        if (slider.handleRect != null)
        {
            var handle = slider.handleRect.GetComponent<Image>();
            if (handle != null)
            {
                handle.color = TextPrimary;
            }
            EnsureFrame(slider.handleRect.gameObject, Accent, 1f);
        }
    }

    private static void StyleScrollbar(Scrollbar scrollbar)
    {
        if (scrollbar == null)
        {
            return;
        }

        scrollbar.transition = Selectable.Transition.ColorTint;
        scrollbar.colors = new ColorBlock
        {
            normalColor = WithAlpha(Line, 0.20f),
            highlightedColor = WithAlpha(Line, 0.34f),
            pressedColor = WithAlpha(Accent, 0.44f),
            selectedColor = WithAlpha(Accent, 0.34f),
            disabledColor = Rgb(0, 0, 0, 0.02f),
            colorMultiplier = 1f,
            fadeDuration = 0.08f
        };

        if (scrollbar.targetGraphic != null)
        {
            scrollbar.targetGraphic.color = WithAlpha(Line, 0.20f);
        }
    }

    private static void StyleInput(TMP_InputField input)
    {
        if (input == null)
        {
            return;
        }

        var image = Ensure<Image>(input.gameObject);
        image.sprite = SolidSprite();
        image.color = input.interactable ? RowFill : Rgb(0, 0, 0, 0.015f);
        image.raycastTarget = true;

        input.caretColor = Accent;
        input.selectionColor = WithAlpha(Accent, 0.28f);
        input.transition = Selectable.Transition.ColorTint;
        input.colors = new ColorBlock
        {
            normalColor = image.color,
            highlightedColor = RowHover,
            pressedColor = RowPressed,
            selectedColor = WithAlpha(Accent, 0.14f),
            disabledColor = Rgb(0, 0, 0, 0.015f),
            colorMultiplier = 1f,
            fadeDuration = 0.08f
        };

        if (input.textComponent != null)
        {
            input.textComponent.color = input.interactable ? TextPrimary : TextMuted;
            input.textComponent.enableAutoSizing = true;
            input.textComponent.fontSizeMin = 11f;
            input.textComponent.fontSizeMax = 16f;
            AddTextShadow(input.textComponent.gameObject);
        }

        if (input.placeholder is TMP_Text placeholder)
        {
            placeholder.color = TextMuted;
            placeholder.enableAutoSizing = true;
            placeholder.fontSizeMin = 11f;
            placeholder.fontSizeMax = 15f;
            AddTextShadow(placeholder.gameObject);
        }

        ClearOutline(input.gameObject);
        ClearShadow(input.gameObject);
        EnsureFrame(input.gameObject, input.interactable ? LineSoft : Rgb(228, 236, 248, 0.08f), 1f);
    }

    private static void StyleStatInput(TMP_InputField input)
    {
        StyleInput(input);

        if (input == null || input.textComponent == null)
        {
            return;
        }

        input.textComponent.alignment = TextAlignmentOptions.MidlineLeft;
        input.textComponent.fontSizeMax = 15f;
    }

    private static void StyleTmpText(TMP_Text text)
    {
        if (text == null)
        {
            return;
        }

        text.enableWordWrapping = false;
        text.enableAutoSizing = true;
        text.fontSizeMin = 10f;
        text.fontSizeMax = Mathf.Clamp(text.fontSize <= 0 ? 18f : text.fontSize, 12f, 24f);
        text.overflowMode = TextOverflowModes.Ellipsis;
        text.color = text.GetComponentInParent<GearListElement>() == null ? TextPrimary : text.color;
        AddTextShadow(text.gameObject);
    }

    private static void StyleLegacyText(Text text)
    {
        if (text == null)
        {
            return;
        }

        text.color = TextPrimary;
        text.resizeTextForBestFit = true;
        text.resizeTextMinSize = 10;
        text.resizeTextMaxSize = Mathf.Clamp(text.fontSize <= 0 ? 16 : text.fontSize, 11, 21);
        AddTextShadow(text.gameObject);
    }

    private static void StyleButtonText(TMP_Text text)
    {
        if (text == null)
        {
            return;
        }

        text.color = TextPrimary;
        text.enableAutoSizing = true;
        text.fontSizeMin = 10f;
        text.fontSizeMax = Mathf.Clamp(text.fontSize <= 0 ? 16f : text.fontSize, 12f, 20f);
        text.overflowMode = TextOverflowModes.Ellipsis;
        AddTextShadow(text.gameObject);
    }

    private static void StyleButtonText(Text text)
    {
        if (text == null)
        {
            return;
        }

        text.color = TextPrimary;
        text.resizeTextForBestFit = true;
        text.resizeTextMinSize = 10;
        text.resizeTextMaxSize = Mathf.Clamp(text.fontSize <= 0 ? 16 : text.fontSize, 11, 20);
        AddTextShadow(text.gameObject);
    }

    private static void StyleSemanticButtons(Component root)
    {
        foreach (var button in root.GetComponentsInChildren<Button>(true))
        {
            var label = ButtonText(button);
            if (string.IsNullOrWhiteSpace(label))
            {
                continue;
            }

            if (label.Contains("重置") || label.Contains("锁定") || label.Contains("清空"))
            {
                StyleButton(button, Danger);
            }
            else if (label.Contains("解锁") || label.Contains("随机") || label.Contains("应用"))
            {
                StyleButton(button, Success);
            }
            else if (label.Contains("返回")
                     || label.Contains("地图")
                     || label.Contains("装备")
                     || label.Contains("日志")
                     || label.Contains("数据")
                     || label.Contains("属性")
                     || label.Contains("头骨")
                     || label.Contains("道具")
                     || label.Contains("精髓")
                     || label.Contains("魔镜能力"))
            {
                StyleButton(button, Accent);
            }
        }
    }

    private static void LayoutButtons(Button[] buttons, float x, float y, float width, float height, int columns, float gapX = 10f, float gapY = 8f)
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null)
            {
                continue;
            }

            var col = i % columns;
            var row = i / columns;
            Place(buttons[i], x + col * (width + gapX), y + row * (height + gapY), width, height);
        }
    }

    private static void StretchPanel(GameObject target, float left, float bottom, float right, float top)
    {
        if (target == null || target.transform is not RectTransform rt)
        {
            return;
        }

        rt.anchorMin = new Vector2(left, bottom);
        rt.anchorMax = new Vector2(right, top);
        rt.pivot = new Vector2(0f, 1f);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    private static void StretchContent(Transform transform, float left, float top, float right, float bottom)
    {
        if (transform is not RectTransform rt)
        {
            return;
        }

        rt.anchorMin = new Vector2(0f, 0f);
        rt.anchorMax = new Vector2(1f, 1f);
        rt.pivot = new Vector2(0f, 1f);
        rt.offsetMin = new Vector2(left, bottom);
        rt.offsetMax = new Vector2(-right, -top);
    }

    private static void Place(Component component, float x, float y, float width, float height)
    {
        if (component == null)
        {
            return;
        }

        Place(component.transform, x, y, width, height);
    }

    private static void Place(Transform transform, float x, float y, float width, float height)
    {
        if (transform is not RectTransform rt)
        {
            return;
        }

        rt.anchorMin = new Vector2(0f, 1f);
        rt.anchorMax = new Vector2(0f, 1f);
        rt.pivot = new Vector2(0f, 1f);
        rt.anchoredPosition = new Vector2(x, -y);
        rt.sizeDelta = new Vector2(width, height);
    }

    private static void SetRectSize(Transform transform, float width, float height)
    {
        if (transform is RectTransform rt)
        {
            rt.sizeDelta = new Vector2(width, height);
        }
    }

    private static void EnsureFrame(GameObject target, Color color, float thickness)
    {
        if (target == null || target.transform is not RectTransform)
        {
            return;
        }

        var frame = target.transform.Find("DevMenuTurboFrame");
        if (frame == null)
        {
            var frameObject = new GameObject("DevMenuTurboFrame", typeof(RectTransform));
            frame = frameObject.transform;
            frame.SetParent(target.transform, false);
        }

        var frameRect = (RectTransform)frame;
        frameRect.anchorMin = Vector2.zero;
        frameRect.anchorMax = Vector2.one;
        frameRect.offsetMin = Vector2.zero;
        frameRect.offsetMax = Vector2.zero;
        frameRect.SetAsLastSibling();

        EnsureLine(frame, "Top", color, thickness, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, thickness), new Vector2(0f, 0f));
        EnsureLine(frame, "Bottom", color, thickness, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0.5f, 0f), new Vector2(0f, thickness), new Vector2(0f, 0f));
        EnsureLine(frame, "Left", color, thickness, new Vector2(0f, 0f), new Vector2(0f, 1f), new Vector2(0f, 0.5f), new Vector2(thickness, 0f), new Vector2(0f, 0f));
        EnsureLine(frame, "Right", color, thickness, new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(1f, 0.5f), new Vector2(thickness, 0f), new Vector2(0f, 0f));
    }

    private static void EnsureLine(Transform parent, string name, Color color, float thickness, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot, Vector2 size, Vector2 position)
    {
        var line = parent.Find(name);
        if (line == null)
        {
            var lineObject = new GameObject(name, typeof(RectTransform), typeof(Image));
            line = lineObject.transform;
            line.SetParent(parent, false);
        }

        var rt = (RectTransform)line;
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.pivot = pivot;
        rt.sizeDelta = size;
        rt.anchoredPosition = position;

        var image = line.GetComponent<Image>();
        image.sprite = SolidSprite();
        image.color = color;
        image.raycastTarget = false;
    }

    private static void ClearDecorativeImages(Component root)
    {
        foreach (var image in root.GetComponentsInChildren<Image>(true))
        {
            if (ShouldPreserveImage(image))
            {
                continue;
            }

            image.sprite = SolidSprite();
            image.color = Rgb(0, 0, 0, 0f);
            image.raycastTarget = false;
        }
    }

    private static bool ShouldPreserveImage(Image image)
    {
        if (image == null)
        {
            return true;
        }

        if ((image.name ?? "").StartsWith("DevMenuTurbo"))
        {
            return true;
        }

        var parent = image.transform.parent;
        while (parent != null)
        {
            if ((parent.name ?? "").StartsWith("DevMenuTurbo"))
            {
                return true;
            }

            parent = parent.parent;
        }

        if (image.GetComponentInParent<Button>() != null
            || image.GetComponentInParent<Toggle>() != null
            || image.GetComponentInParent<Slider>() != null
            || image.GetComponentInParent<Scrollbar>() != null
            || image.GetComponentInParent<TMP_InputField>() != null)
        {
            return true;
        }

        var gearElement = image.GetComponentInParent<GearListElement>();
        if (gearElement != null && image == gearElement._thumbnail)
        {
            return true;
        }

        var spriteName = image.sprite != null ? image.sprite.name : "";
        var objectName = image.name ?? "";
        return spriteName.IndexOf("icon", System.StringComparison.OrdinalIgnoreCase) >= 0
               || spriteName.IndexOf("thumbnail", System.StringComparison.OrdinalIgnoreCase) >= 0
               || objectName.IndexOf("icon", System.StringComparison.OrdinalIgnoreCase) >= 0
               || objectName.IndexOf("thumbnail", System.StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static string ButtonText(Button button)
    {
        if (button == null)
        {
            return "";
        }

        var tmp = button.GetComponentInChildren<TMP_Text>(true);
        if (tmp != null)
        {
            return tmp.text ?? "";
        }

        var text = button.GetComponentInChildren<Text>(true);
        return text != null ? text.text ?? "" : "";
    }

    private static void AddTextShadow(GameObject gameObject)
    {
        var shadow = EnsureExact<Shadow>(gameObject);
        shadow.effectColor = Rgb(0, 0, 0, 0.88f);
        shadow.effectDistance = TextShadowDistance;
        shadow.useGraphicAlpha = true;
    }

    private static void ClearOutline(GameObject gameObject)
    {
        foreach (var outline in gameObject.GetComponents<Outline>())
        {
            outline.effectColor = Rgb(0, 0, 0, 0f);
        }
    }

    private static void ClearShadow(GameObject gameObject)
    {
        foreach (var shadow in gameObject.GetComponents<Shadow>())
        {
            if (shadow.GetType() == typeof(Shadow))
            {
                shadow.effectColor = Rgb(0, 0, 0, 0f);
            }
        }
    }

    private static T Ensure<T>(GameObject gameObject) where T : Component
    {
        var component = gameObject.GetComponent<T>();
        return component != null ? component : gameObject.AddComponent<T>();
    }

    private static T EnsureExact<T>(GameObject gameObject) where T : Component
    {
        foreach (var component in gameObject.GetComponents<T>())
        {
            if (component.GetType() == typeof(T))
            {
                return component;
            }
        }

        return gameObject.AddComponent<T>();
    }

    private static Sprite SolidSprite()
    {
        if (_solidSprite != null)
        {
            return _solidSprite;
        }

        var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        _solidSprite = Sprite.Create(texture, new Rect(0f, 0f, 1f, 1f), new Vector2(0.5f, 0.5f), 1f);
        _solidSprite.name = "DevMenuTurboThemeSolid";
        return _solidSprite;
    }

    private static Color Rgb(byte r, byte g, byte b, float a)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a);
    }

    private static Color WithAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}
