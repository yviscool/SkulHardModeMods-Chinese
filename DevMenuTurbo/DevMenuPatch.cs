using System.Collections.Generic;
using System;
using TMPro;
using UI.TestingTool;
using HarmonyLib;
using UnityEngine;
using GameResources;
using Singletons;
using Services;
using Level;
using Characters.Gear.Items;
using Characters.Gear.Synergy.Inscriptions;
using UnityEngine.UI;

namespace DevMenu;

[HarmonyPatch]
public class DevMenuTurboPatch
{
    [HarmonyPatch(typeof(Panel), "canUse", MethodType.Getter)]
    static bool Prefix(ref Panel __instance, ref bool __result)
    {
        __result = true;
        return false;
    }

    [HarmonyPatch(typeof(Panel), "Awake")]
    [HarmonyPostfix]
    static void TranslatePanel(ref Panel __instance)
    {
        ref var self = ref __instance;

        SetText(self._openMapList, "地图");
        SetText(self._nextStage, "下一章节");
        SetText(self._nextMap, "下一地图");
        SetText(self._openGearList, "装备清单");
        SetText(self._hideUI, "隐藏界面");
        SetText(self._getGold, "+10000 金币");
        SetText(self._getDarkquartz, "+1000 魔石");
        SetText(self._getBone, "+100 碎骨");
        SetText(self._getHeartQuartz, "+100 魔石核心");
        SetText(self._testMap, "测试地图");
        SetText(self._awake, "升级头骨");
        SetText(self._damageBuff, "伤害加成");
        SetText(self._hp10k, "+10000 生命");
        SetText(self._noCooldown, "无冷却");
        SetText(self._shield10, "+10 护盾");
        SetText(self._hardmodeToggle, "魔镜");
        SetText(self._rerollSkill, "重置技能");
        SetText(self._timeScaleReset, "重置");
        SetText(self._infiniteRevive, "不朽");
        SetText(self._verification, "地图回血");
        SetText(self._right3, "全部加成 ->");
        SetText(self._chapter1, "第1章");
        SetText(self._chapter2, "第2章");
        SetText(self._chapter3, "第3章");
        SetText(self._chapter4, "第4章");
        SetText(self._chapter5, "第5章");
        SetText(self._chapter6, "第6章");

        SetText2(self._hardmodeLevelSlider, "魔镜等级");
        SetText2(self._hardmodeClearedLevelSlider, "魔镜解锁");
        SetText2(self._hardmodeClearedCountSlider, "通关次数");
        SetText2(self._timeScaleSlider, "速度倍率");

        SetText(self._gearList.transform, "返回");
        TranslateTree(self.transform);

        // Disable showing your time zone, just in case
        self._localNow.gameObject.SetActive(false);
        self._utcNow.gameObject.SetActive(false);
    }

    [HarmonyPatch(typeof(Panel), "OpenMain")]
    [HarmonyPostfix]
    static void TranslatePanelOnOpenMain(ref Panel __instance)
    {
        TranslateTree(__instance.transform);
    }

    [HarmonyPatch(typeof(Panel), "OpenMapList")]
    [HarmonyPostfix]
    static void TranslatePanelOnOpenMapList(ref Panel __instance)
    {
        TranslateTree(__instance.transform);
    }

    [HarmonyPatch(typeof(Panel), "OpenGearList")]
    [HarmonyPostfix]
    static void TranslatePanelOnOpenGearList(ref Panel __instance)
    {
        NormalizeGearList(__instance._gearList.GetComponent<GearList>());
        TranslateTree(__instance.transform);
    }

    [HarmonyPatch(typeof(Panel), "OpenLog")]
    [HarmonyPostfix]
    static void TranslatePanelOnOpenLog(ref Panel __instance)
    {
        TranslateTree(__instance.transform);
    }

    [HarmonyPatch(typeof(Panel), "OpenDataControl")]
    [HarmonyPostfix]
    static void TranslatePanelOnOpenDataControl(ref Panel __instance)
    {
        TranslateTree(__instance.transform);
    }

    [HarmonyPatch(typeof(Panel), "OpenBonusStat")]
    [HarmonyPostfix]
    static void TranslatePanelOnOpenBonusStat(ref Panel __instance)
    {
        TranslateTree(__instance.transform);
    }

    [HarmonyPatch(typeof(DataControl), "Awake")]
    [HarmonyPostfix]
    static void TranslateDataControl(ref DataControl __instance)
    {
        var self = __instance;
        SetText(self._resetSeed, "重置种子");
        SetText(self._firstClear, "设置首通");
        SetText(self._resetProgress, "重置进度");
        SetText(self._randomItem, "随机道具");
        SetText(self._unlockAllItems, "解锁全部道具");
        SetText(self._unlockAllUpgrades, "解锁全部魔镜能力");
        SetText(self._lockAllUpgrades, "锁定全部魔镜能力");
        SetText(self._itemReset, "重置道具记录");
        SetText(self._upgradeReset, "重置能力记录");
        SetText(self._allGearReset, "重置全部装备记录");
        SetText(self._dCDefenseCleared, "魔镜防御通关");
        SetText2(self._hardmodeClearedCountSlider, "魔镜通关次数");
        SetText2(self._hintTypeSlider, "线索类型");
        TranslateTree(self.transform);
    }

    [HarmonyPatch(typeof(MapList), "Awake")]
    [HarmonyPostfix]
    static void TranslateMapListAwake(ref MapList __instance)
    {
        TranslateMapList(__instance);
    }

    [HarmonyPatch(typeof(MapList), "FilterMapList")]
    [HarmonyPostfix]
    static void TranslateMapListFilter(ref MapList __instance)
    {
        TranslateMapList(__instance);
    }

    private static void TranslateMapList(MapList self)
    {
        SetText(self._back, "返回");
        SetText(self._enemy, "敌人");
        SetText(self._fieldNPC, "NPC");
        SetText(self._darkEnemy, "暗黑敌人");
        SetText(self._tutorial, "教程");
        SetText(self._castle, "城堡");
        SetText(self._chapter1, "第1章");
        SetText(self._chapter2, "第2章");
        SetText(self._chapter3, "第3章");
        SetText(self._chapter4, "第4章");
        SetText(self._chapter5, "第5章");
        SetText(self._hardmodeCastle, "魔镜城堡");
        SetText(self._hardChapter1, "魔镜第1章");
        SetText(self._hardChapter2, "魔镜第2章");
        SetText(self._hardChapter3, "魔镜第3章");
        SetText(self._hardChapter4, "魔镜第4章");
        SetText(self._hardChapter5, "魔镜第5章");
        SetText(self._hardChapter6, "魔镜第6章");
        TranslateTree(self.transform);
    }

    [HarmonyPatch(typeof(GearList), "Awake")]
    [HarmonyPostfix]
    static void TranslateGearListAwake(ref GearList __instance)
    {
        TranslateGearList(__instance);
        FixDarkAbilitySearch(__instance);
        NormalizeGearList(__instance);
    }

    [HarmonyPatch(typeof(GearList), "SetFilter")]
    [HarmonyPostfix]
    static void TranslateGearListSetFilter(ref GearList __instance)
    {
        TranslateGearList(__instance);
        NormalizeGearList(__instance);
    }

    [HarmonyPatch(typeof(GearList), "FilterGearList")]
    [HarmonyPostfix]
    static void TranslateGearListFilter(ref GearList __instance)
    {
        TranslateGearList(__instance);
        NormalizeGearList(__instance);
    }

    [HarmonyPatch(typeof(GearList), "FilterUpgrade")]
    [HarmonyPostfix]
    static void TranslateUpgradeListFilter(ref GearList __instance)
    {
        TranslateGearList(__instance);
        NormalizeGearList(__instance);
    }

    private static void TranslateGearList(GearList self)
    {
        if (self == null)
        {
            return;
        }

        SetText(self._head, "头骨");
        SetText(self._item, "道具");
        SetText(self._essence, "精髓");
        SetText(self._upgrade, "魔镜能力");
        SetText(self._unlockSetting, "掉落时解锁");
        SetText(self._lockSetting, "掉落时锁定");
        SetInputPlaceholder(self._inputField, "搜索");
        TranslateTree(self.transform);
    }

    [HarmonyPatch(typeof(Log), "Awake")]
    [HarmonyPostfix]
    static void TranslateLogAwake(ref Log __instance)
    {
        TranslateLog(__instance);
    }

    [HarmonyPatch(typeof(Log), "OnEnable")]
    [HarmonyPostfix]
    static void TranslateLogOnEnable(ref Log __instance)
    {
        TranslateLog(__instance);
    }

    private static void TranslateLog(Log self)
    {
        if (self == null)
        {
            return;
        }

        SetText(self._copy, "复制");
        SetText(self._clear, "清空");
        TranslateTree(self.transform);
    }

    private static void SetText(Component obj, string text)
    {
        if (obj == null)
        {
            return;
        }

        if (obj is TMP_Text tmpText)
        {
            tmpText.SetText(text);
            return;
        }

        if (obj is Text uiText)
        {
            uiText.text = text;
            return;
        }

        obj.GetComponentInChildren<TMP_Text>(true)?.SetText(text);
        var textComponent = obj.GetComponentInChildren<Text>(true);
        if (textComponent != null)
        {
            textComponent.text = text;
        }
    }

    private static void SetText2(Component obj, string text)
    {
        if (obj == null)
        {
            return;
        }

        var texts = obj.GetComponentsInChildren<TMP_Text>(true);
        if (texts != null && texts.Length > 1)
        {
            texts[1].SetText(text);
        }
    }

    private static void SetInputPlaceholder(TMP_InputField inputField, string text)
    {
        if (inputField == null)
        {
            return;
        }

        if (inputField.placeholder is TMP_Text placeholder)
        {
            placeholder.SetText(text);
        }
    }

    private static void TranslateTree(Component root)
    {
        if (root == null)
        {
            return;
        }

        foreach (var tmpText in root.GetComponentsInChildren<TMP_Text>(true))
        {
            TranslateTmpText(tmpText);
        }

        foreach (var uiText in root.GetComponentsInChildren<Text>(true))
        {
            TranslateUiText(uiText);
        }
    }

    private static void TranslateTmpText(TMP_Text text)
    {
        if (text == null)
        {
            return;
        }

        var translated = TranslateText(text.text);
        if (translated != text.text)
        {
            text.SetText(translated);
        }
    }

    private static void TranslateUiText(Text text)
    {
        if (text == null)
        {
            return;
        }

        var translated = TranslateText(text.text);
        if (translated != text.text)
        {
            text.text = translated;
        }
    }

    private static void NormalizeGearList(GearList self)
    {
        if (self == null)
        {
            return;
        }

        foreach (var element in self._gearListElements)
        {
            if (element == null || element.gearReference == null)
            {
                continue;
            }

            ApplyGearReferencePresentation(element, element.gearReference);
        }

        foreach (var button in self._upgradeListElements)
        {
            NormalizeUpgradeButton(button);
        }

        foreach (var button in self._inscriptionListElements)
        {
            NormalizeInscriptionButton(button);
        }
    }

    private static void NormalizeUpgradeButton(Button button)
    {
        if (button == null)
        {
            return;
        }

        var name = button.GetComponentInChildren<Text>(true);
        if (name == null)
        {
            return;
        }

        var translated = TranslateText(name.text);
        if (!string.IsNullOrWhiteSpace(translated))
        {
            name.text = translated;
            button.name = translated;
        }
    }

    private static void NormalizeInscriptionButton(Button button)
    {
        if (button == null)
        {
            return;
        }

        var name = button.GetComponentInChildren<Text>(true);
        if (name == null)
        {
            return;
        }

        var translated = TranslateInscriptionName(name.text);
        if (!string.IsNullOrWhiteSpace(translated))
        {
            name.text = translated;
            button.name = translated;
        }
    }

    [HarmonyPatch(typeof(GearListElement), "Set")]
    [HarmonyPostfix]
    static void FillInventoryOnClick(ref GearListElement __instance, GearReference gearReference)
    {
        ref var self = ref __instance;

        ApplyGearReferencePresentation(self, gearReference);

        if (ShouldHideGearReference(gearReference))
        {
            self.gameObject.SetActive(false);
            return;
        }

        if (gearReference.type != Characters.Gear.Gear.Type.Item)
        {
            TranslateTree(self.transform);
            return;
        }

        var handler = self.gameObject.AddComponent<ButtonRightClickHandler>();
        handler.OnRightClick += delegate
        {
            GearRequest request = gearReference.LoadAsync();
            request.WaitForCompletion();

            LevelManager manager = Singleton<Service>.Instance.levelManager;
            var inventory = manager.player.playerComponents.inventory.item;

            Item item = null;

            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i] == null)
                {
                    if (item == null)
                    {
                        item = (Item)manager.DropGear(request, Vector3.zero);
                        inventory.EquipAt(item, i);
                    }
                    else
                    {
                        Item clone = manager.DropItem(item, Vector3.zero);
                        inventory.EquipAt(clone, i);
                    }
                }
            }
        };

        TranslateTree(self.transform);
    }

    private static void ApplyGearReferencePresentation(GearListElement element, GearReference gearReference)
    {
        if (element == null || gearReference == null)
        {
            return;
        }

        var text = element.GetComponentInChildren<TMP_Text>(true);
        text = element._text ?? text;
        if (text != null)
        {
            var localizedName = GetGearDisplayName(gearReference);
            if (!string.IsNullOrWhiteSpace(localizedName))
            {
                text.SetText(localizedName);
            }
            else
            {
                text.SetText(TranslateInternalName(gearReference.name) ?? gearReference.name);
            }
        }

        ApplyGearThumbnailFallback(element, gearReference);
        if (ShouldHideGearReference(gearReference))
        {
            element.gameObject.SetActive(false);
        }
    }

    private static string GetGearDisplayName(GearReference gearReference)
    {
        if (gearReference == null)
        {
            return "";
        }

        if (!string.IsNullOrWhiteSpace(gearReference.displayNameKey)
            && TryGetSimplifiedChineseString(gearReference.displayNameKey, out var chinese)
            && !string.IsNullOrWhiteSpace(chinese))
        {
            return TranslateText(chinese);
        }

        var current = "";
        if (!string.IsNullOrWhiteSpace(gearReference.displayNameKey))
        {
            current = Localization.GetLocalizedString(gearReference.displayNameKey);
        }

        if (!string.IsNullOrWhiteSpace(current))
        {
            return TranslateText(current);
        }

        return TranslateInternalName(gearReference.name) ?? "";
    }

    private static bool TryGetSimplifiedChineseString(string key, out string text)
    {
        text = "";
        if (string.IsNullOrWhiteSpace(key) || LocalizationStringResource.instance == null)
        {
            return false;
        }

        var resource = LocalizationStringResource.instance;
        var keyHash = StringComparer.OrdinalIgnoreCase.GetHashCode(key);
        if (resource._keyHashToIndex == null)
        {
            resource.CreateHashToIndexDictionary();
        }

        if (!resource._keyHashToIndex.TryGetValue(keyHash, out var row)
            || resource._stringsByLanguage == null
            || row < 0
            || row >= resource._stringsByLanguage.Length)
        {
            return false;
        }

        var strings = resource._stringsByLanguage[row].strings;
        if (strings == null)
        {
            return false;
        }

        var index = 3;
        if (strings.Length > index && !string.IsNullOrWhiteSpace(strings[index]))
        {
            text = strings[index];
            return true;
        }

        if (strings.Length > 0 && !string.IsNullOrWhiteSpace(strings[0]))
        {
            text = strings[0];
            return true;
        }

        return false;
    }

    private static void ApplyGearThumbnailFallback(GearListElement element, GearReference gearReference)
    {
        var image = element._thumbnail;
        if (image == null)
        {
            return;
        }

        var sprite = gearReference.thumbnail;
        if (sprite == null)
        {
            sprite = gearReference.icon;
        }

        if (sprite == null && GearResource.instance != null)
        {
            sprite = GearResource.instance.GetGearThumbnail(gearReference.name);
        }

        if (sprite != null)
        {
            image.sprite = sprite;
            image.enabled = true;
        }
    }

    private static bool ShouldHideGearReference(GearReference gearReference)
    {
        if (gearReference == null)
        {
            return true;
        }

        var name = gearReference.name ?? "";
        if (LooksLikeDevOnlyGearName(name))
        {
            return true;
        }

        var hasDisplayName = !string.IsNullOrWhiteSpace(GetGearDisplayName(gearReference));
        var hasAnyIcon = gearReference.thumbnail != null
                         || gearReference.icon != null
                         || (GearResource.instance != null && GearResource.instance.GetGearThumbnail(name) != null);

        return !hasDisplayName && !hasAnyIcon;
    }

    private static bool LooksLikeDevOnlyGearName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return true;
        }

        if (name.StartsWith("Ref_", StringComparison.OrdinalIgnoreCase)
            || name.StartsWith("_", StringComparison.OrdinalIgnoreCase)
            || name.StartsWith("Former_", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return name.IndexOf("Test", StringComparison.OrdinalIgnoreCase) >= 0
               || name.IndexOf("Former", StringComparison.OrdinalIgnoreCase) >= 0
               || name.IndexOf("Legacy", StringComparison.OrdinalIgnoreCase) >= 0
               || name.IndexOf("ChronoSample", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static void FixDarkAbilitySearch(GearList __instance)
    {
        var self = __instance;
        foreach (var button in self._upgradeListElements)
        {
            Text name = button.GetComponentInChildren<Text>();
            if (name == null)
            {
                continue;
            }

            name.fontSize = 29;
            TranslateUiText(name);
            button.name = name.text;
        }

        self._inputField.onValueChanged.RemoveAllListeners();
        self._inputField.onValueChanged.AddListener(delegate
        {
            if (self._currentFilter == GearList.Filter.Upgrade)
                self.FilterUpgrade();
            else
                self.FilterGearList();
        });
    }

    private static string TranslateText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var trimmed = text.Trim();
        if (ExactTranslations.TryGetValue(trimmed, out var exact))
        {
            return PreserveOuterWhitespace(text, exact);
        }

        var internalName = TranslateInternalName(trimmed);
        if (internalName != null)
        {
            return PreserveOuterWhitespace(text, internalName);
        }

        if (!LooksLikeDevMenuText(trimmed))
        {
            return text;
        }

        var translated = text;
        foreach (var pair in FragmentTranslations)
        {
            translated = translated.Replace(pair.Key, pair.Value);
        }

        return translated;
    }

    private static bool LooksLikeDevMenuText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        if (text.Contains("/"))
        {
            return false;
        }

        if (ContainsKorean(text))
        {
            return true;
        }

        return ExactTranslations.ContainsKey(text)
               || text.Contains("Buff")
               || text.Contains("Cooldown")
               || text.Contains("DMG")
               || text.Contains("HP")
               || text.Contains("UI")
               || text.Contains("Hard Mode")
               || text.Contains("Dark Ability")
               || text.Contains("Copy")
               || text.Contains("Clear")
               || text.Contains("Stat");
    }

    private static string TranslateInternalName(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        if (InternalNameTranslations.TryGetValue(text, out var exact))
        {
            return exact;
        }

        var normalized = text.Trim();
        var suffix = "";
        var parenIndex = normalized.IndexOf(" (", StringComparison.Ordinal);
        if (parenIndex > 0 && normalized.EndsWith(")", StringComparison.Ordinal))
        {
            suffix = normalized.Substring(parenIndex + 2, normalized.Length - parenIndex - 3);
            normalized = normalized.Substring(0, parenIndex);
        }

        if (InternalNameTranslations.TryGetValue(normalized, out exact))
        {
            var translatedSuffix = TranslateInternalSuffix(suffix);
            return string.IsNullOrWhiteSpace(translatedSuffix) ? exact : $"{exact}（{translatedSuffix}）";
        }

        var withoutLeadingUnderscore = normalized.TrimStart('_');
        if (InternalNameTranslations.TryGetValue(withoutLeadingUnderscore, out exact))
        {
            var translatedSuffix = TranslateInternalSuffix(suffix);
            return string.IsNullOrWhiteSpace(translatedSuffix) ? exact : $"{exact}（{translatedSuffix}）";
        }

        var chapterPrefix = "Chapter";
        if (normalized.StartsWith(chapterPrefix) && normalized.Length > chapterPrefix.Length)
        {
            var index = chapterPrefix.Length;
            var chapterNumber = "";
            while (index < normalized.Length && char.IsDigit(normalized[index]))
            {
                chapterNumber += normalized[index];
                index++;
            }

            if (chapterNumber.Length > 0)
            {
                var rest = normalized.Substring(index).TrimStart('_');
                var translatedRest = TranslateInternalSuffix(rest);
                if (!string.IsNullOrEmpty(translatedRest))
                {
                    return $"第{chapterNumber}章 {translatedRest}";
                }

                return $"第{chapterNumber}章";
            }
        }

        var generic = TranslateInternalSuffix(withoutLeadingUnderscore);
        if (!string.IsNullOrWhiteSpace(generic) && generic != withoutLeadingUnderscore)
        {
            var translatedSuffix = TranslateInternalSuffix(suffix);
            return string.IsNullOrWhiteSpace(translatedSuffix) ? generic : $"{generic}（{translatedSuffix}）";
        }

        return null;
    }

    private static string TranslateInternalSuffix(string suffix)
    {
        if (string.IsNullOrEmpty(suffix))
        {
            return "";
        }

        if (InternalSuffixTranslations.TryGetValue(suffix, out var exact))
        {
            return exact;
        }

        var parts = suffix.Replace("-", "_").Split('_');
        var translated = new List<string>();
        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                continue;
            }

            if (InternalSuffixTranslations.TryGetValue(part, out var value))
            {
                translated.Add(value);
            }
            else
            {
                translated.Add(part);
            }
        }

        return string.Join(" ", translated);
    }

    private static string TranslateInscriptionName(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        var trimmed = text.Trim();
        if (InscriptionTranslations.TryGetValue(trimmed, out var exact))
        {
            return PreserveOuterWhitespace(text, exact);
        }

        if (Enum.TryParse<Inscription.Key>(trimmed, out var key))
        {
            var localized = Inscription.GetName(key);
            if (!string.IsNullOrWhiteSpace(localized))
            {
                return PreserveOuterWhitespace(text, TranslateText(localized));
            }
        }

        return TranslateText(text);
    }

    private static bool ContainsKorean(string text)
    {
        foreach (var c in text)
        {
            if (c >= '\uac00' && c <= '\ud7a3')
            {
                return true;
            }
        }

        return false;
    }

    private static string PreserveOuterWhitespace(string original, string replacement)
    {
        var prefixLength = original.Length - original.TrimStart().Length;
        var suffixLength = original.Length - original.TrimEnd().Length;
        return original.Substring(0, prefixLength)
               + replacement
               + original.Substring(original.Length - suffixLength, suffixLength);
    }

    private static readonly Dictionary<string, string> ExactTranslations = new()
    {
        { "Maps", "地图" },
        { "Map", "地图" },
        { "Next Stage", "下一章节" },
        { "Next Map", "下一地图" },
        { "Gear List", "装备清单" },
        { "Data Control", "数据控制" },
        { "Log", "日志" },
        { "View Log", "日志查看" },
        { "Log Viewer", "日志查看" },
        { "Hide UI", "隐藏界面" },
        { "+10k Gold", "+10000 金币" },
        { "+1k Quartz", "+1000 魔石" },
        { "+100 Bones", "+100 碎骨" },
        { "+100 Core Quartz", "+100 魔石核心" },
        { "Test Map", "测试地图" },
        { "Awaken", "升级头骨" },
        { "DMG Buff", "伤害加成" },
        { "10k HP", "+10000 生命" },
        { "+10000 HP", "+10000 生命" },
        { "No Cooldown", "无冷却" },
        { "+10 Shield", "+10 护盾" },
        { "Hard Mode", "魔镜" },
        { "Reroll Skills", "重置技能" },
        { "Reset", "重置" },
        { "Immortal", "不朽" },
        { "Map Heals", "地图回血" },
        { "All 3 buffs ->", "全部加成 ->" },
        { "All 3 Buffs ->", "全部加成 ->" },
        { "DM Level", "魔镜等级" },
        { "DM Unlocked", "魔镜解锁" },
        { "Time Scale", "速度倍率" },
        { "Return", "返回" },
        { "Back", "返回" },
        { "Head", "头骨" },
        { "Skull", "头骨" },
        { "Item", "道具" },
        { "Essence", "精髓" },
        { "Quintessence", "精髓" },
        { "Upgrade", "魔镜能力" },
        { "Dark Ability", "魔镜能力" },
        { "Unlock", "解锁" },
        { "Lock", "锁定" },
        { "Search", "搜索" },
        { "Enemy", "敌人" },
        { "Field NPC", "NPC" },
        { "Dark Enemy", "暗黑敌人" },
        { "Tutorial", "教程" },
        { "Castle", "城堡" },
        { "Chapter 1", "第1章" },
        { "Chapter 2", "第2章" },
        { "Chapter 3", "第3章" },
        { "Chapter 4", "第4章" },
        { "Chapter 5", "第5章" },
        { "Chapter 6", "第6章" },
        { "Hardmode Castle", "魔镜城堡" },
        { "Demon Castle", "魔王城" },
        { "DemonCastle", "魔王城" },
        { "DLC", "扩展" },
        { "Stat", "属性" },
        { "Copy", "复制" },
        { "Clear", "清空" },
        { "Random Item", "随机道具" },
        { "Reset Seed", "重置种子" },
        { "First Clear", "设置首通" },
        { "Reset Progress", "重置进度" },
        { "Item Reset", "重置道具记录" },
        { "Upgrade Reset", "重置能力记录" },
        { "All Gear Reset", "重置全部装备记录" },
        { "Unlock All Items", "解锁全部道具" },
        { "Unlock All Upgrades", "解锁全部魔镜能力" },
        { "Lock All Upgrades", "锁定全部魔镜能力" },
        { "Lock All Dark Abilities", "锁定全部魔镜能力" },
        { "Unlock All Dark Abilities", "解锁全部魔镜能力" },
        { "Hint Type", "线索类型" },
        { "Health", "生命" },
        { "AttackDamage", "攻击力/全部" },
        { "PhysicalAttackDamage", "攻击力/物理" },
        { "MagicAttackDamage", "攻击力/魔法" },
        { "TakingDamage", "受到伤害减少/全部" },
        { "BasicAttackSpeed", "攻击速度/普通攻击" },
        { "SkillAttackSpeed", "攻击速度/技能" },
        { "MovementSpeed", "移动速度" },
        { "ChargingSpeed", "蓄力速度" },
        { "SkillCooldownSpeed", "技能冷却速度" },
        { "SwapCooldownSpeed", "替换冷却速度" },
        { "EssenceCooldownSpeed", "精髓冷却速度" },
        { "CriticalChance", "暴击率" },
        { "CriticalDamage", "暴击伤害" },
        { "지도", "地图" },
        { "맵", "地图" },
        { "데이터 컨트롤", "数据控制" },
        { "로그 보기", "日志查看" },
        { "로그", "日志" },
        { "보기", "查看" },
        { "데이터", "数据" },
        { "컨트롤", "控制" },
        { "다음 스테이지", "下一章节" },
        { "다음 맵", "下一地图" },
        { "장비 목록", "装备清单" },
        { "장비 리스트", "装备清单" },
        { "UI 숨기기", "隐藏界面" },
        { "테스트 맵", "测试地图" },
        { "각성", "升级头骨" },
        { "데미지 버프", "伤害加成" },
        { "대미지 버프", "伤害加成" },
        { "쿨타임 없음", "无冷却" },
        { "무적", "不朽" },
        { "불사", "不朽" },
        { "초기화", "重置" },
        { "스킬 리롤", "重置技能" },
        { "신화", "神话" },
        { "하드모드", "魔镜" },
        { "다크 미러", "魔镜" },
        { "마왕성 방어전용", "魔王城防御战" },
        { "n회 클리어", "通关次数" },
        { "부활횟수", "复活次数" },
        { "무한부활", "无限复活" },
        { "단서", "线索" },
        { "클리어", "通关" },
        { "뒤로", "返回" },
        { "돌아가기", "返回" },
        { "해골", "头骨" },
        { "아이템", "道具" },
        { "정수", "精髓" },
        { "어빌리티", "能力" },
        { "다크 어빌리티", "魔镜能力" },
        { "검색", "搜索" },
        { "적", "敌人" },
        { "튜토리얼", "教程" },
        { "성", "城堡" },
        { "체력", "生命" },
        { "공격력/모두", "攻击力/全部" },
        { "공격력/물리", "攻击力/物理" },
        { "공격력/마법", "攻击力/魔法" },
        { "받는피해감소/모두", "受到伤害减少/全部" },
        { "공격속도/기본", "攻击速度/普通攻击" },
        { "공격속도/스킬", "攻击速度/技能" },
        { "이동속도", "移动速度" },
        { "공격속도/차지", "蓄力速度" },
        { "쿨다운가속/스킬", "技能冷却速度" },
        { "쿨다운가속/교대", "替换冷却速度" },
        { "쿨다운가속/정수", "精髓冷却速度" },
        { "치명타 확률", "暴击率" },
        { "치명타 피해량", "暴击伤害" },
    };

    private static readonly Dictionary<string, string> InternalNameTranslations = new()
    {
        { "BlackMarket", "黑市" },
        { "DemonCastle", "魔王城" },
        { "ChiefGuard", "近卫队长" },
        { "Dracula", "德古拉" },
        { "Former_RockStar", "前摇滚明星" },
        { "Former_RockStar2", "前摇滚明星2" },
        { "PlagueDoctor", "瘟疫医生" },
        { "Skul_Skin1", "小骨皮肤1" },
        { "Skul_Tutorial", "教程小骨" },
        { "TestHead", "测试头骨" },
        { "_Test_Spear", "测试长矛" },
        { "Ref_Berserker_2", "狂战士参考2" },
        { "Ref_Berserker_Polymorph", "狂战士变形参考" },
        { "GhostRider_3_Swap_Polymorph", "幽灵骑士3 交换变身" },
        { "GhostRider_3_Swap_Polymorph (Legacy)", "幽灵骑士3 交换变身（旧版）" },
        { "_GhostRider Chain Test", "幽灵骑士链条测试" },
        { "GhostRider Chain Test", "幽灵骑士链条测试" },
        { "Ref_GhostRider_3", "幽灵骑士3参考" },
        { "Ref_GhostRider_3 (Former)", "幽灵骑士3参考（旧版）" },
        { "Ref_Ghoul_2", "食尸鬼参考2" },
        { "GlacialSkull_ChronoSample", "冰河小骨时间样本" },
        { "Ref_GraveDigger", "掘墓人参考" },
        { "Ref_HighWarlock_2", "大魔导师参考2" },
        { "Ref_HighWarlock_2_Passive", "大魔导师被动参考2" },
    };

    private static readonly Dictionary<string, string> InternalSuffixTranslations = new()
    {
        { "Entry", "入口" },
        { "Entry1", "入口1" },
        { "Entry2", "入口2" },
        { "Entry1_2", "入口1-2" },
        { "Boss", "首领" },
        { "Terminal", "终点" },
        { "Adventurer", "冒险家" },
        { "Reference", "参考" },
        { "Ref", "参考" },
        { "Former", "旧版" },
        { "Legacy", "旧版" },
        { "Passive", "被动" },
        { "Polymorph", "变身" },
        { "Swap", "交换" },
        { "Chain", "链条" },
        { "Test", "测试" },
        { "ChronoSample", "时间样本" },
        { "Sample", "样本" },
        { "Skin1", "皮肤1" },
        { "Tutorial", "教程" },
        { "GhostRider", "幽灵骑士" },
        { "Ghoul", "食尸鬼" },
        { "GlacialSkull", "冰河小骨" },
        { "GraveDigger", "掘墓人" },
        { "HighWarlock", "大魔导师" },
        { "Berserker", "狂战士" },
        { "ChiefGuard", "近卫队长" },
        { "Dracula", "德古拉" },
        { "RockStar", "摇滚明星" },
        { "PlagueDoctor", "瘟疫医生" },
        { "Skul", "小骨" },
        { "Spear", "长矛" },
        { "Hardmode", "魔镜" },
        { "Castle", "城堡" },
        { "BlackMarket", "黑市" },
    };

    private static readonly Dictionary<string, string> InscriptionTranslations = new()
    {
        { "None", "无" },
        { "Antique", "古董" },
        { "Arms", "武器" },
        { "Artifact", "魔工学" },
        { "Bone", "骨头" },
        { "Brave", "勇气" },
        { "FairyTale", "童话" },
        { "Duel", "决斗" },
        { "Fortress", "堡垒" },
        { "Arson", "纵火" },
        { "Execution", "处刑" },
        { "Strike", "强击" },
        { "Manatech", "魔工学" },
        { "Soar", "飞升" },
        { "Relic", "遗物" },
        { "Heirloom", "传家宝" },
        { "Mutation", "突变" },
        { "Chase", "疾驰" },
        { "ManaCycle", "魔力循环" },
        { "Misfortune", "厄运" },
        { "AbsoluteZero", "绝对零度" },
        { "Spoils", "战利品" },
        { "Brawl", "乱斗" },
        { "SunAndMoon", "日月" },
        { "Rapidity", "迅速" },
        { "Revenge", "复仇" },
        { "Poisoning", "中毒" },
        { "ExcessiveBleeding", "过量出血" },
        { "Wisdom", "智慧" },
        { "Masterpiece", "杰作" },
        { "HiddenBlade", "隐刃" },
        { "Heritage", "传承" },
        { "Treasure", "宝藏" },
        { "Dizziness", "眩晕" },
        { "Omen", "预兆" },
        { "Sin", "罪" },
        { "Mystery", "神秘" },
    };

    private static readonly KeyValuePair<string, string>[] FragmentTranslations =
    {
        new("Buff", "加成"),
        new("buff", "加成"),
        new("DMG", "伤害"),
        new("HP", "生命"),
        new("UI", "界面"),
        new("Cooldown", "冷却"),
        new("Hard Mode", "魔镜"),
        new("Dark Ability", "魔镜能力"),
        new("Dark Mirror", "魔镜"),
        new("Data Control", "数据控制"),
        new("View Log", "日志查看"),
        new("Log Viewer", "日志查看"),
        new("Log", "日志"),
        new("Copy", "复制"),
        new("Clear", "清空"),
        new("Stat", "属性"),
        new("DLC", "扩展"),
        new("버프", "加成"),
        new("데미지", "伤害"),
        new("대미지", "伤害"),
        new("체력", "生命"),
        new("HP", "生命"),
        new("쿨타임", "冷却"),
        new("하드모드", "魔镜"),
        new("다크 미러", "魔镜"),
        new("다크", "暗黑"),
        new("미러", "魔镜"),
        new("마왕성", "魔王城"),
        new("방어전용", "防御战"),
        new("방어전", "防御战"),
        new("클리어", "通关"),
        new("회", "次"),
        new("신화", "神话"),
        new("다크 어빌리티", "魔镜能力"),
        new("어빌리티", "能力"),
        new("스킬", "技能"),
        new("리롤", "重置"),
        new("리스트", "列表"),
        new("로그", "日志"),
        new("보기", "查看"),
        new("데이터", "数据"),
        new("컨트롤", "控制"),
        new("목록", "列表"),
        new("장비", "装备"),
        new("해골", "头骨"),
        new("아이템", "道具"),
        new("정수", "精髓"),
        new("골드", "金币"),
        new("마석", "魔石"),
        new("뼈", "碎骨"),
        new("보호막", "护盾"),
        new("실드", "护盾"),
        new("지도", "地图"),
        new("맵", "地图"),
        new("스테이지", "章节"),
        new("챕터", "章节"),
        new("다음", "下一"),
        new("모든", "全部"),
        new("초기화", "重置"),
        new("부활횟수", "复活次数"),
        new("무한부활", "无限复活"),
        new("단서", "线索"),
        new("잠금 해제", "解锁"),
        new("잠금", "锁定"),
        new("검색", "搜索"),
        new("필터", "筛选"),
        new("뒤로", "返回"),
        new("돌아가기", "返回"),
    };
}
