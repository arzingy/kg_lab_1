using KG_LAB1.Models.ColorSchemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG_LAB1.Models
{
    class ColorsManager
    {
        public RGB Rgb { get; }

        public CMYK Cmyk { get; }

        public HSV Hsv { get; }

        public PictureBox ResultColorBox { get; }

        public ColorsManager(
            RGB rgb,
            CMYK _cmyk,
            HSV _hsv,
            PictureBox resultColorox)
        {
            Rgb = rgb;
            Rgb.SetColorsManager(this);

            Cmyk = _cmyk;
            Cmyk.SetColorsManager(this);

            Hsv = _hsv;
            Hsv.SetColorsManager(this);

            ResultColorBox = resultColorox;
        }

        public void Notify(BaseColorSchema sender, string eventName)
        {
            if (eventName.StartsWith("RgbChanged"))
            {
                Cmyk.K = GetK(
                    Rgb.R / 255.0,
                    Rgb.G / 255.0,
                    Rgb.B / 255.0);

                Cmyk.C = RecalculateFromRgbToCmyk(Rgb.R / 255.0);
                Cmyk.M = RecalculateFromRgbToCmyk(Rgb.G / 255.0);
                Cmyk.Y = RecalculateFromRgbToCmyk(Rgb.B / 255.0);

                RecalculateFromRgbToHsv(
                    Rgb.R / 255.0,
                    Rgb.G / 255.0,
                    Rgb.B / 255.0);
            }
            else if (eventName.StartsWith("CmykChanged"))
            {
                double _r = Rgb.R / 255.0;
                double _g = Rgb.G / 255.0;
                double _b = Rgb.B / 255.0;

                if (eventName == "CmykChangedC")
                {
                    _r = RecalculateFromCmykToRgb(Cmyk.C, Cmyk.K);
                    Rgb.R = (int)(_r * 255);
                }
                else if (eventName == "CmykChangedM")
                {
                    _g = RecalculateFromCmykToRgb(Cmyk.M, Cmyk.K);
                    Rgb.G = (int)(_g * 255);
                }
                else if (eventName == "CmykChangedY")
                {
                    _b = RecalculateFromCmykToRgb(Cmyk.Y, Cmyk.K);
                    Rgb.B = (int)(_b * 255);
                }
                else if (eventName == "CmykChangedK")
                {
                    _r = RecalculateFromCmykToRgb(Cmyk.C, Cmyk.K);
                    Rgb.R = (int)(_r * 255);
                    _g = RecalculateFromCmykToRgb(Cmyk.M, Cmyk.K);
                    Rgb.G = (int)(_g * 255);
                    _b = RecalculateFromCmykToRgb(Cmyk.Y, Cmyk.K);
                    Rgb.B = (int)(_b * 255);
                }
                else
                {
                    throw new Exception("Incorrect CMYK change");
                }

                RecalculateFromRgbToHsv(
                    _r,
                    _g,
                    _b);
            }
            else if (eventName.StartsWith("HsvChanged"))
            {
                RecalculateFromHsvToRgb(out double r, out double g, out double b);
                Cmyk.K = GetK(r, g, b);
                Cmyk.C = RecalculateFromRgbToCmyk(r);
                Cmyk.M = RecalculateFromRgbToCmyk(g);
                Cmyk.Y = RecalculateFromRgbToCmyk(b);
            }

            ResultColorBox.BackColor = System.Drawing.Color.FromArgb(Rgb.R, Rgb.G, Rgb.B);
        }

        private double RecalculateFromRgbToCmyk(double _v)
        {
            if (Cmyk.K == 1)
            {
                return 0;
            }

            return (1 - _v - Cmyk.K) / (1 - Cmyk.K);
        }

        private double RecalculateFromCmykToRgb(
            double _v,
            double _k)
        {
            return (1 - _v) * (1 - _k);
        }

        private void RecalculateFromHsvToRgb(
            out double r,
            out double g,
            out double b)
        {
            var c = Hsv.V * Hsv.S;
            var x = c * (1 - Math.Abs(((Hsv.H / 60) % 2) - 1));
            var m = Hsv.V - c;

            r = g = b = 0;

            if (Hsv.H >= 0 && Hsv.H < 60)
            {
                r = c;
                g = x;
            }
            else if (Hsv.H >= 60 && Hsv.H < 120)
            {
                r = x;
                g = c;
            }
            else if (Hsv.H >= 120 && Hsv.H < 180)
            {
                g = c;
                b = x;
            }
            else if (Hsv.H >= 180 && Hsv.H < 240)
            {
                g = x;
                b = c;
            }
            else if (Hsv.H >= 240 && Hsv.H < 300)
            {
                r = x;
                b = c;
            }
            else if (Hsv.H >= 300 && Hsv.H <= 360)
            {
                r = c;
                b = x;
            }

            r += m;
            g += m;
            b += m;

            Rgb.R = (int)(r * 255);
            Rgb.G = (int)(g * 255);
            Rgb.B = (int)(b * 255);
        }

        private void RecalculateFromRgbToHsv(
            double _r,
            double _g,
            double _b)
        {
            var cMax = Math.Max(_r, Math.Max(_g, _b));
            var cMin = Math.Min(_r, Math.Min(_g, _b));
            var delta = cMax - cMin;

            if (delta == 0)
            {
                Hsv.H = 0;
            }
            else if (cMax == _r)
            {
                Hsv.H = (((_g - _b) / delta) % 6);
            }
            else if (cMax == _g)
            {
                Hsv.H = ((_b - _r) / delta) + 2;
            }
            else
            {
                Hsv.H = ((_r - _g) / delta) + 4;
            }

            Hsv.H *= 60;
            Hsv.S = cMax == 0
                ? 0
                : delta / cMax;
            Hsv.V = cMax;
        }

        private double GetK(double _r, double _g, double _b)
        {
            return 1 - Math.Max(_r, Math.Max(_g, _b));
        }
    }
}
