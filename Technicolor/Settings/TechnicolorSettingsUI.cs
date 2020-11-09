namespace Technicolor.Settings
{
    using System.Collections.Generic;
    using BeatSaberMarkupLanguage.Attributes;

    internal class TechnicolorSettingsUI : PersistentSingleton<TechnicolorSettingsUI>
    {
#pragma warning disable IDE0052 // Remove unread private members

        [UIValue("techlightschoices")]
        private readonly List<object> _techlightsChoices = new List<object> { TechnicolorStyle.OFF, TechnicolorStyle.WARM_COLD, TechnicolorStyle.PURE_RANDOM, TechnicolorStyle.GRADIENT };

        [UIValue("techbarrierschoices")]
        private readonly List<object> _techbarrierschoices = new List<object> { TechnicolorStyle.OFF, TechnicolorStyle.PURE_RANDOM, TechnicolorStyle.GRADIENT };

        [UIValue("lightsgroupchoices")]
        private readonly List<object> _lightsgroupChoices = new List<object>() { TechnicolorLightsGrouping.STANDARD, TechnicolorLightsGrouping.ISOLATED_GROUP, TechnicolorLightsGrouping.ISOLATED };

        [UIValue("lightsfreqchoices")]
        private readonly List<object> _lightsfreqChoices = new List<object>() { 0.05f, 0.1f, 0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.4f, 0.45f, 0.5f, 0.55f, 0.6f, 0.65f, 0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 0.95f, 1f };

#pragma warning restore IDE0052 // Remove unread private members

        [UIValue("lightsPresetOptions")]
        private readonly List<object> _lightsPresetOptions = new List<object> {
            ColorPresetType.RAINBOW,
            ColorPresetType.USER0,
            ColorPresetType.USER1,
            ColorPresetType.USER2,
            ColorPresetType.USER3
        };

        [UIValue("technicolor")]
        public bool TechnicolorEnabled
        {
            get => TechnicolorConfig.Instance.TechnicolorEnabled;
            set => TechnicolorConfig.Instance.TechnicolorEnabled = value;
        }

        [UIValue("techlights")]
        public TechnicolorStyle TechnicolorLightsStyle
        {
            get => TechnicolorConfig.Instance.TechnicolorLightsStyle;
            set => TechnicolorConfig.Instance.TechnicolorLightsStyle = value;
        }

        [UIValue("lightsgroup")]
        public TechnicolorLightsGrouping TechnicolorLightsGroup
        {
            get => TechnicolorConfig.Instance.TechnicolorLightsGrouping;
            set => TechnicolorConfig.Instance.TechnicolorLightsGrouping = value;
        }

        [UIValue("lightsfreq")]
        public float TechnicolorLightsFrequency
        {
            get => TechnicolorConfig.Instance.TechnicolorLightsFrequency;
            set => TechnicolorConfig.Instance.TechnicolorLightsFrequency = value;
        }

        [UIValue("techbarriers")]
        public TechnicolorStyle TechnicolorWallsStyle
        {
            get => TechnicolorConfig.Instance.TechnicolorWallsStyle;
            set => TechnicolorConfig.Instance.TechnicolorWallsStyle = value;
        }

        [UIValue("techbombs")]
        public TechnicolorStyle TechnicolorBombsStyle
        {
            get => TechnicolorConfig.Instance.TechnicolorBombsStyle;
            set => TechnicolorConfig.Instance.TechnicolorBombsStyle = value;
        }

        [UIValue("technotes")]
        public TechnicolorStyle TechnicolorBlocksStyle
        {
            get => TechnicolorConfig.Instance.TechnicolorBlocksStyle;
            set => TechnicolorConfig.Instance.TechnicolorBlocksStyle = value;
        }

        [UIValue("techsabers")]
        public TechnicolorStyle TechnicolorSabersStyle
        {
            get => TechnicolorConfig.Instance.TechnicolorSabersStyle;
            set => TechnicolorConfig.Instance.TechnicolorSabersStyle = value;
        }

        [UIValue("desync")]
        public bool Desync
        {
            get => !TechnicolorConfig.Instance.Desync;
            set => TechnicolorConfig.Instance.Desync = !value;
        }
        
        [UIValue("lightsPreset")]
        public ColorPresetType TechnicolourPreset
        {
            get => TechnicolorConfig.Instance.TechnicolorPreset;
            set => TechnicolorConfig.Instance.TechnicolorPreset = value;
        }

#pragma warning disable IDE0051 // Remove unused private members
        [UIAction("techlightform")]
        private string TechlightFormat(TechnicolorStyle t)
        {
            switch (t)
            {
                case TechnicolorStyle.GRADIENT:
                    return "Gradient";

                case TechnicolorStyle.PURE_RANDOM:
                    return "True Random";

                case TechnicolorStyle.WARM_COLD:
                    return "Warm/Cold";

                case TechnicolorStyle.OFF:
                default:
                    return "Off";
            }
        }

        [UIAction("techgroupform")]
        private string TechgroupingFormat(TechnicolorLightsGrouping t)
        {
            switch (t)
            {
                case TechnicolorLightsGrouping.ISOLATED:
                    return "Isolated (Mayhem)";

                case TechnicolorLightsGrouping.ISOLATED_GROUP:
                    return "Isolated Event";

                case TechnicolorLightsGrouping.STANDARD:
                default:
                    return "Standard";
            }
        }

        [UIAction("percentfreq")]
        private string PercentfreqDisplay(float percent)
        {
            return $"{percent * 100f}%" + (percent == 0.1f ? " (Def)" : string.Empty);
        }

#pragma warning restore IDE0051 // Remove unused private members

        [UIAction("lightsPresetForm")]
        private string lightsPresetFormat(ColorPresetType presetType)
        {
            switch (presetType)
            {
            case ColorPresetType.RAINBOW:
                return "Rainbow (default)";
            case ColorPresetType.USER0:
                return "User preset 0";
            case ColorPresetType.USER1:
                return "User preset 1";
            case ColorPresetType.USER2:
                return "User preset 2";
            case ColorPresetType.USER3:
                return "User preset 3";
            default:
                return "Unsupported preset";
            }
        }
    }
}
