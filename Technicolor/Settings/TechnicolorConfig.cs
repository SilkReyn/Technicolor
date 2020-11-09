namespace Technicolor.Settings
{
    using Color = UnityEngine.Color;

    public class TechnicolorConfig
    {
        internal static readonly Color[] DEFAULT_WARM_PALETTE = new Color[4] { new Color(1, 0, 0), new Color(1, 0, 1), new Color(1, 0.6f, 0), new Color(1, 0, 0.4f) };
        internal static readonly Color[] DEFAULT_COLD_PALETTE = new Color[4] { new Color(0, 0.501f, 1), new Color(0, 1, 0), new Color(0, 0, 1), new Color(0, 1, 0.8f) };
        internal static readonly ColorScheme USER0_SCHEME = new ColorScheme {
            // What is in my colorSchemesSettings.json
            colorSchemeId = "User0",
            saberAColor = new SchemeColor(.305, .303, .303),
            saberBColor = new SchemeColor(.722, .593, .449),
            environmentColor0 = new SchemeColor(.411, .504, .532),
            environmentColor1 = new SchemeColor(.392, .322, .452),
            obstaclesColor = new SchemeColor(.561,.133,.144)
        };

        internal const string  DEFAULT_SCHEME_JSON_STR = @"[{""r"":1.0,""g"":0.0,""b"":0.0,""a"":1.0},{""r"":1.0,""g"":0.0,""b"":1.0,""a"":1.0},{""r"":1.0,""g"":0.6,""b"":0.0,""a"":1.0},{""r"":1.0,""g"":0.0,""b"":0.4,""a"":1.0},{""r"":0.0,""g"":0.5,""b"":1.0,""a"":1.0},{""r"":0.0,""g"":1.0,""b"":0.0,""a"":1.0},{""r"":0.0,""g"":0.0,""b"":1.0,""a"":1.0},{""r"":0.0,""g"":1.0,""b"":0.8,""a"":1.0}]";
        
        private ColorPresetType _colPreset = ColorPresetType.RAINBOW;


        public static TechnicolorConfig Instance { get; set; }

        public bool TechnicolorEnabled { get; set; } = true;

        public TechnicolorStyle TechnicolorLightsStyle { get; set; } = TechnicolorStyle.WARM_COLD;

        public TechnicolorLightsGrouping TechnicolorLightsGrouping { get; set; } = TechnicolorLightsGrouping.STANDARD;

        public float TechnicolorLightsFrequency { get; set; } = 0.1f;

        public TechnicolorStyle TechnicolorSabersStyle { get; set; } = TechnicolorStyle.OFF;

        public TechnicolorStyle TechnicolorBlocksStyle { get; set; } = TechnicolorStyle.OFF;

        public TechnicolorStyle TechnicolorWallsStyle { get; set; } = TechnicolorStyle.GRADIENT;

        public TechnicolorStyle TechnicolorBombsStyle { get; set; } = TechnicolorStyle.PURE_RANDOM;

        public bool Desync { get; set; } = false;

        public ColorPresetType TechnicolorPreset
        {
            get { return _colPreset; }
            set
            {
                _colPreset = value;
                LoadColorScheme(_colPreset);
            }
        }

        public string SchemeColors_Rainbow { get; set; } = DEFAULT_SCHEME_JSON_STR;
        public string SchemeColors_User0 { get; set; }  // Can IPA-config serialize from Object automatically?
        public string SchemeColors_User1 { get; set; }
        public string SchemeColors_User2 { get; set; }
        public string SchemeColors_User3 { get; set; }

        public void LoadColorScheme(ColorPresetType preset)
        {
            if (preset == ColorPresetType.RAINBOW)
            {
                TechniLogger.Log($"Load default rainbow colour scheme");
                TechnicolorController.TechnicolorWarmPalette = DEFAULT_WARM_PALETTE;
                TechnicolorController.TechnicolorColdPalette = DEFAULT_COLD_PALETTE;
                return;
            }
            
            string scStr = SchemeColors_Rainbow;
            string colorSchemeName = "Rainbow";
            switch (preset)
            {
            case ColorPresetType.USER0:
                if (System.String.IsNullOrEmpty(SchemeColors_User0))
                {
                    SchemeColors_User0 = $"[{USER0_SCHEME.saberAColor.ToString()}, {USER0_SCHEME.saberBColor.ToString()}, {USER0_SCHEME.environmentColor0.ToString()}, {USER0_SCHEME.environmentColor1.ToString()}, {USER0_SCHEME.obstaclesColor.ToString()}]";
                }
                scStr = SchemeColors_User0;
                colorSchemeName = "User0";
                break;

            case ColorPresetType.USER1:
                scStr = SchemeColors_User1;
                colorSchemeName = "User1";
                break;

            case ColorPresetType.USER2:
                scStr = SchemeColors_User2;
                colorSchemeName = "User2";
                break;

            case ColorPresetType.USER3:
                scStr = SchemeColors_User3;
                colorSchemeName = "User3";
                break;

            default:
                scStr = SchemeColors_Rainbow;
                break;
            }
            if (System.String.IsNullOrWhiteSpace(scStr))
            {
                scStr = SchemeColors_Rainbow = DEFAULT_SCHEME_JSON_STR;
            }

            var scLst = new System.Collections.Generic.List<SchemeColor>();
            try
            {
                var jar = Newtonsoft.Json.Linq.JArray.Parse(scStr);
                if (jar != null)
                {
                    foreach (var item in jar)
                    {
                        if (item == null)
                        {
                            continue;
                        }
                        if (item.Type == Newtonsoft.Json.Linq.JTokenType.Object)
                        {
                            scLst.Add(item.ToObject<SchemeColor>());
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                TechniLogger.Log($"Error parsing ColorScheme from config: {ex.Message} At: {ex.StackTrace}");
            }

            if (scLst == null || scLst.Count < 1)
            {
                return;
            }

            TechniLogger.Log($"Load scheme {colorSchemeName} with {scLst.Count} colors");
            if (scLst.Count == 5)
            {
                TechnicolorController.TechnicolorWarmPalette = new Color[] { scLst[0].ToUeColor(), scLst[2].ToUeColor(), scLst[4].ToUeColor() };
                TechnicolorController.TechnicolorColdPalette = new Color[] { scLst[1].ToUeColor(), scLst[3].ToUeColor(), scLst[4].ToUeColor() };
            }
            else if (0 == (scLst.Count % 2))
            {
                int div2 = scLst.Count / 2;
                var cols = new Color[div2];
                var colsB = new Color[div2];
                for (var i = 0; i < div2; ++i)
                {
                    cols[i] = scLst[i].ToUeColor();
                }
                TechnicolorController.TechnicolorWarmPalette = cols;
                for (var j = 0; j < div2; ++j)
                {
                    colsB[j] = scLst[div2 + j].ToUeColor();
                }
                TechnicolorController.TechnicolorColdPalette = colsB;
            }
            else
            {
                var cols = new Color[scLst.Count];
                for (var i = 0; i < scLst.Count; ++i)
                {
                    cols[i] = scLst[i].ToUeColor();
                }
                TechnicolorController.TechnicolorWarmPalette = cols;
                TechnicolorController.TechnicolorColdPalette = cols;
            }
#if DEBUG
            var sb = new System.Text.StringBuilder();
            var warmPal = TechnicolorController.TechnicolorWarmPalette;
            var coldPal = TechnicolorController.TechnicolorColdPalette;
            sb.Append("Warm palette: |");
            foreach (var col in warmPal)
            {
                sb.Append(col.ToString());
                sb.Append(" | ");
            }
            sb.Append("Cold palette: |");
            foreach (var col in coldPal)
            {
                sb.Append(col.ToString());
                sb.Append(" | ");
            }
            TechniLogger.Log(sb.ToString());
#endif
            return;
        }
    }
}
