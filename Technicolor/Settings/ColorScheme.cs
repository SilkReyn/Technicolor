namespace Technicolor.Settings
{
    public class SchemeColor
    {
        public double r { get; set; }
        public double g { get; set; }
        public double b { get; set; }
        public double a { get; set; }

        public override string ToString()
        {
            return $"{{\"r\":{r},\"g\":{g},\"b\":{b},\"a\":{a}}}";
        }

        public UnityEngine.Color ToUeColor()
        {
            return new UnityEngine.Color((float)r, (float)g, (float)b, (float)a);
        }

        public SchemeColor()
        {
            r = g = b = a = 0.0;
        }

        public SchemeColor(double red, double green, double blue, double alpha = 1.0)
        {
            r = red;
            g = green;
            b = blue;
            a = alpha;
        }

        public SchemeColor(UnityEngine.Color col)
        {
            r = col.r;
            g = col.g;
            b = col.b;
            a = col.a;
        }
    }

    public class ColorScheme
    {
        public string colorSchemeId { get; set; } = "default";
        public SchemeColor saberAColor { get; set; } = new SchemeColor(1, 0, 0);
        public SchemeColor saberBColor { get; set; } = new SchemeColor(0, 0, 1);
        public SchemeColor environmentColor0 { get; set; } = new SchemeColor(1, 0.5f, 0);
        public SchemeColor environmentColor1 { get; set; } = new SchemeColor(0, 0.5f, 1);
        public SchemeColor obstaclesColor { get; set; } = new SchemeColor(1, 1, 1);

        public override string ToString()
        {
            return $"{{\"colorSchemeId\":\"{colorSchemeId}\",\"saberAColor\":{saberAColor.ToString()},\"saberBColor\":{saberBColor.ToString()},\"environmentColor0\":{environmentColor0.ToString()},\"environmentColor1\":{environmentColor1.ToString()},\"obstaclesColor\":{obstaclesColor.ToString()}}}";
        }

        public SchemeColor[] ToArray()
        {
            return new SchemeColor[] { saberAColor, saberBColor, environmentColor0, environmentColor1, obstaclesColor };
        }
    }
}
