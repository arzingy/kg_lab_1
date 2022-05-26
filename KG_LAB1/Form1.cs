using KG_LAB1.Models;
using KG_LAB1.Models.ColorSchemas;
using System;
using System.Windows.Forms;

namespace KG_LAB1
{
    public partial class Form : System.Windows.Forms.Form
    {
        private RGB _rgb;

        private CMYK _cmyk;

        private HSV _hsv;

        private ColorsManager _manager;

        private bool rgbChangedP = false;

        private bool cmykChangeP = false;

        private bool hsvChangeP = false;

        public Form()
        {
            InitializeComponent();

            colorDialog.FullOpen = true;

            _rgb = new RGB();
            _cmyk = new CMYK();
            _hsv = new HSV();

            _manager = new ColorsManager(
                _rgb,
                _cmyk,
                _hsv,
                resultColorBox);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;

            // установка цвета формы
            
            _rgb.ChangeRColorWithNotify(colorDialog.Color.R);
            _rgb.ChangeGColorWithNotify(colorDialog.Color.G);
            _rgb.ChangeBColorWithNotify(colorDialog.Color.B);

            Recalculate(() => {
                RevalueRGB();
                RevalueHSV();
                RevalueCMYK();
            });

            resultColorBox.BackColor = colorDialog.Color;
        }

        private void r_scroll_Scroll(object sender, System.EventArgs e)
        {
            _rgb.ChangeRColorWithNotify(r_scroll.Value);
            r_nd.Value = _rgb.R;
            RevalueFromRGBControlls();
        }

        private void c_nd_ValueChanged(object sender, System.EventArgs e)
        {
            if (!IsInRange(0, 100, (double)c_nd.Value))
            {
                c_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 100 :)");
                return;
            }

            if (cmykChangeP)
                return;

            Recalculate(() =>
            {
                _cmyk.ChangeCColorWithNotify((double)(c_nd.Value / 100));
                c_scroll.Value = (int)(_cmyk.C * 100);
                RevalueFromCMYKControlls();
            });

        }

        private void m_nd_ValueChanged(object sender, System.EventArgs e)
        {
            if (!IsInRange(0, 100, (double)m_nd.Value))
            {
                m_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 100 :)");
                return;
            }

            if (cmykChangeP)
                return;

            Recalculate(() =>
            {
                _cmyk.ChangeMColorWithNotify((double)(m_nd.Value / 100));
                m_scroll.Value = (int)(_cmyk.M * 100);
                RevalueFromCMYKControlls();
            });
        }

        private void y_nd_ValueChanged(object sender, System.EventArgs e)
        {
            if (!IsInRange(0, 100, (double)y_nd.Value))
            {
                y_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 100 :)");
                return;
            }

            if (cmykChangeP)
                return;

            Recalculate(() =>
            {
                _cmyk.ChangeYColorWithNotify((double)(y_nd.Value / 100));
                y_scroll.Value = (int)(_cmyk.Y * 100);
                RevalueFromCMYKControlls();
            });
        }

        private void k_nd_ValueChanged(object sender, System.EventArgs e)
        {
            if (!IsInRange(0, 100, (double)k_nd.Value))
            {
                k_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 100 :)");
                return;
            }

            if (cmykChangeP)
                return;

            Recalculate(() =>
            {
                _cmyk.ChangeKColorWithNotify((double)(k_nd.Value / 100));
                k_scroll.Value = (int)(_cmyk.K * 100);
                RevalueFromCMYKControlls();
            });
        }

        private void r_nd_ValueChanged(object sender, System.EventArgs e)
        {
            if (!IsInRange(0, 255, (double)r_nd.Value))
            {
                r_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 255 :)");
                return;
            }

            if (rgbChangedP)
                return;

            Recalculate(() =>
            {
                _rgb.ChangeRColorWithNotify((int)r_nd.Value);
                r_scroll.Value = _rgb.R;
                RevalueFromRGBControlls();
            });
        }

        private void g_nd_ValueChanged(object sender, System.EventArgs e)
        {
            if (!IsInRange(0, 255, (double)g_nd.Value))
            {
                g_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 255 :)");
                return;
            }

            if (rgbChangedP)
                return;

            Recalculate(() =>
            {
                _rgb.ChangeGColorWithNotify((int)g_nd.Value);
                g_scroll.Value = _rgb.G;
                RevalueFromRGBControlls();
            });
        }

        private void b_nd_ValueChanged(object sender, System.EventArgs e)
        {
            if (!IsInRange(0, 255, (double)b_nd.Value))
            {
                b_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 255 :)");
                return;
            }

            if (rgbChangedP)
                return;

            Recalculate(() =>
            {
                _rgb.ChangeBColorWithNotify((int)b_nd.Value);
                b_scroll.Value = _rgb.B;
                RevalueFromRGBControlls();
            });
        }

        private void c_scroll_Scroll(object sender, System.EventArgs e)
        {
            Recalculate(() =>
            {
                _cmyk.ChangeCColorWithNotify(c_scroll.Value / 100.0);
                c_nd.Value = (decimal)(_cmyk.C * 100);
                RevalueFromCMYKControlls();
            });
        }

        private void m_scroll_Scroll(object sender, System.EventArgs e)
        {
            Recalculate(() =>
            {
                _cmyk.ChangeMColorWithNotify(m_scroll.Value / 100.0);
                m_nd.Value = (decimal)(_cmyk.M * 100);
                RevalueFromCMYKControlls();
            });
        }

        private void y_scroll_Scroll(object sender, System.EventArgs e)
        {
            Recalculate(() =>
            {
                _cmyk.ChangeYColorWithNotify(y_scroll.Value / 100.0);
                y_nd.Value = (decimal)(_cmyk.Y * 100);
                RevalueFromCMYKControlls();
            });
        }

        private void k_scroll_Scroll(object sender, System.EventArgs e)
        {
            Recalculate(() =>
            {
                _cmyk.ChangeKColorWithNotify(k_scroll.Value / 100.0);
                k_nd.Value = (decimal)(_cmyk.K * 100);
                RevalueFromCMYKControlls();
            });
        }

        private void g_scroll_Scroll(object sender, System.EventArgs e)
        {
            _rgb.ChangeGColorWithNotify(g_scroll.Value);
            g_nd.Value = _rgb.G;
            RevalueFromRGBControlls();
        }

        private void b_scroll_Scroll(object sender, System.EventArgs e)
        {
            _rgb.ChangeBColorWithNotify(b_scroll.Value);
            b_nd.Value = _rgb.B;
            RevalueFromRGBControlls();
        }

        private void h_nd_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInRange(0, 360, (double)h_nd.Value))
            {
                h_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 360 :)");
                return;
            }

            if (hsvChangeP)
                return;

            Recalculate(() =>
            {
                _hsv.ChangeHColorWithNotify((double)h_nd.Value);
                h_scroll.Value = (int)_hsv.H;
                RevalueFromHSVControlls();
            });
        }

        private void s_nd_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInRange(0, 100, (double)s_nd.Value))
            {
                s_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 100 :)");
                return;
            }

            if (hsvChangeP)
                return;

            Recalculate(() =>
            {
                _hsv.ChangeSColorWithNotify((double)s_nd.Value / 100);
                s_scroll.Value = (int)(_hsv.S * 100);
                RevalueFromHSVControlls();
            });
        }

        private void v_nd_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInRange(0, 100, (double)v_nd.Value))
            {
                v_nd.Value = 0;
                MessageBox.Show("Значение должно лежать в промежутке от 0 до 100 :)");  
                return;
            }

            if (hsvChangeP)
                return;

            Recalculate(() =>
            {
                _hsv.ChangeVColorWithNotify((double)v_nd.Value / 100);
                v_scroll.Value = (int)(_hsv.V * 100);
                RevalueFromHSVControlls();
            });
        }

        private void h_scroll_Scroll(object sender, EventArgs e)
        {
            Recalculate(() =>
            {
                _hsv.ChangeHColorWithNotify(h_scroll.Value);
                h_nd.Value = (decimal)_hsv.H;
                RevalueFromHSVControlls();
            });
        }

        private void s_scroll_Scroll(object sender, EventArgs e)
        {
            Recalculate(() =>
            {
                _hsv.ChangeSColorWithNotify(s_scroll.Value / 100.0);
                s_nd.Value = (decimal)(_hsv.S * 100);
                RevalueFromHSVControlls();
            });
        }

        private void v_scroll_Scroll(object sender, EventArgs e)
        {
            Recalculate(() =>
            {
                _hsv.ChangeVColorWithNotify(v_scroll.Value / 100.0);
                v_nd.Value = (decimal)(_hsv.V * 100);
                RevalueFromHSVControlls();
            });
        }

        private void Recalculate(Action action)
        {
            rgbChangedP = true;
            cmykChangeP = true;
            hsvChangeP = true;

            action();

            hsvChangeP = false;
            rgbChangedP = false;
            cmykChangeP = false;
        }

        private void RevalueFromRGBControlls()
        {
            RevalueCMYK();
            RevalueHSV();
        }

        private void RevalueFromCMYKControlls()
        {
            RevalueHSV();
            RevalueRGB();
        }

        private void RevalueFromHSVControlls()
        {
            RevalueRGB();
            RevalueCMYK();
        }

        private void RevalueHSV()
        {
            var h = _hsv.H;
            var s = _hsv.S * 100;
            var v = _hsv.V * 100;
            h_nd.Value = (decimal)h;
            h_scroll.Value = (int)h;
            s_nd.Value = (decimal)s;
            s_scroll.Value = (int)s;
            v_nd.Value = (decimal)v;
            v_scroll.Value = (int)v;
        }

        private void RevalueCMYK()
        {
            var c = _cmyk.C * 100;
            var m = _cmyk.M * 100;
            var y = _cmyk.Y * 100;
            var k = _cmyk.K * 100;
            c_scroll.Value = (int)c;
            c_nd.Value = (decimal)c;
            m_scroll.Value = (int)m;
            m_nd.Value = (decimal)m;
            y_scroll.Value = (int)y;
            y_nd.Value = (decimal)y;
            k_scroll.Value = (int)k;
            k_nd.Value = (decimal)k;
        }

        private void RevalueRGB()
        {
            r_nd.Value = _rgb.R;
            g_nd.Value = _rgb.G;
            b_nd.Value = _rgb.B;
            r_scroll.Value = _rgb.R;
            g_scroll.Value = _rgb.G;
            b_scroll.Value = _rgb.B;
        }

        private bool IsInRange(double min, double max, double value)
        {
            return value >= min && value <= max;
        }
    }
}
