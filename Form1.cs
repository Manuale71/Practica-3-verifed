using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Practica_2
{
    public partial class Practica2 : Form
    {
        private Size originalFormSize;
        private Dictionary<Control, RectangleF> originalBounds = new();
        private Dictionary<Control, float> originalFontSizes = new();

        public Practica2()
        {
            InitializeComponent();
            this.Load += Practica2_Load;
            this.Resize += Practica2_Resize;
        }
        private void Practica2_Resize(object? sender, EventArgs e)
        {
            float scaleX = (float)this.ClientSize.Width / originalFormSize.Width;
            float scaleY = (float)this.ClientSize.Height / originalFormSize.Height;
            float scale = (scaleX + scaleY) / 2;

            foreach (Control ctrl in this.Controls)
            {
                if (!originalBounds.ContainsKey(ctrl)) continue;

                RectangleF bounds = originalBounds[ctrl];

                ctrl.Left = (int)(bounds.Left * this.ClientSize.Width);
                ctrl.Top = (int)(bounds.Top * this.ClientSize.Height);
                ctrl.Width = (int)(bounds.Width * this.ClientSize.Width);
                ctrl.Height = (int)(bounds.Height * this.ClientSize.Height);

                float originalFont = originalFontSizes[ctrl];
                ctrl.Font = new Font(ctrl.Font.FontFamily, originalFont * scale, ctrl.Font.Style);
            }
        }

        private void Practica2_Load(object? sender, EventArgs e)
        {
            originalFormSize = this.ClientSize;

            foreach (Control ctrl in this.Controls)
            {
                ctrl.AutoSize = false;
                ctrl.Anchor = AnchorStyles.None;
                ctrl.Dock = DockStyle.None;

                var bounds = new RectangleF(
                    (float)ctrl.Left / this.ClientSize.Width,
                    (float)ctrl.Top / this.ClientSize.Height,
                    (float)ctrl.Width / this.ClientSize.Width,
                    (float)ctrl.Height / this.ClientSize.Height
                );

                originalBounds[ctrl] = bounds;
                originalFontSizes[ctrl] = ctrl.Font.Size;
            }


            //donde empieza el ejercicio
            btnordenar.Click += (s, ev) =>
            {
                int[] numeros = new int[10];

                if (tbt1.Text == "" || tbt2.Text == "" || tbt3.Text == "" || tbt4.Text == "" || tbt5.Text == "" ||
                    tbt6.Text == "" || tbt7.Text == "" || tbt8.Text == "" || tbt9.Text == "" || tbt10.Text == "")
                {
                    MessageBox.Show("Asegurese de haber ingresado 10 números.","ErroR");
                    return;
                }

                numeros[0] = Convert.ToInt32(tbt1.Text);
                numeros[1] = Convert.ToInt32(tbt2.Text);
                numeros[2] = Convert.ToInt32(tbt3.Text);
                numeros[3] = Convert.ToInt32(tbt4.Text);
                numeros[4] = Convert.ToInt32(tbt5.Text);
                numeros[5] = Convert.ToInt32(tbt6.Text);
                numeros[6] = Convert.ToInt32(tbt7.Text);
                numeros[7] = Convert.ToInt32(tbt8.Text);
                numeros[8] = Convert.ToInt32(tbt9.Text);
                numeros[9] = Convert.ToInt32(tbt10.Text);

                Ordenador ordenador;

                if (rbtnup.Checked == true)
                {
                    ordenador = new OrdenAscendente();
                }
                else if (rbtndown.Checked == true)
                {
                    ordenador = new OrdenDescendente();
                }
                else
                {
                    MessageBox.Show("Seleccione un orden, ascendente o descendente.","Ordenar");
                    return;
                }

                int[] resultado = ordenador.Ordenar(numeros);

                lbl1.Text = resultado[0].ToString();
                lbl2.Text = resultado[1].ToString();
                lbl3.Text = resultado[2].ToString();
                lbl4.Text = resultado[3].ToString();
                lbl5.Text = resultado[4].ToString();
                lbl6.Text = resultado[5].ToString();
                lbl7.Text = resultado[6].ToString();
                lbl8.Text = resultado[7].ToString();
                lbl9.Text = resultado[8].ToString();
                lbl10.Text = resultado[9].ToString();
            };

            btnborrar.Click += (s, ev) =>
            {
                tbt1.Text = ""; tbt2.Text = ""; tbt3.Text = ""; tbt4.Text = ""; tbt5.Text = "";
                tbt6.Text = ""; tbt7.Text = ""; tbt8.Text = ""; tbt9.Text = ""; tbt10.Text = "";

                lbl1.Text = ""; lbl2.Text = ""; lbl3.Text = ""; lbl4.Text = ""; lbl5.Text = "";
                lbl6.Text = ""; lbl7.Text = ""; lbl8.Text = ""; lbl9.Text = ""; lbl10.Text = "";
            };

            btnayuda.Click += (s, ev) =>
            {
                MessageBox.Show("Ingrese 10 números, seleccione Ascendente o Descendente y presione Ordenar.\nPresione Borrar para limpiar ambas partes.", "Funcionamiento");
            };
        }
    }

    public class Ordenador
    {
        public virtual int[] Ordenar(int[] listaNumeros)
        {
            return listaNumeros;
        }
    }

    public class OrdenAscendente : Ordenador
    {
        public override int[] Ordenar(int[] listaNumeros)
        {
            int posact, posig, temp;

            for (posact = 0; posact < 10; posact++)
            {
                for (posig = posact + 1; posig < 10; posig++)
                {
                    if (listaNumeros[posact] > listaNumeros[posig])
                    {
                        temp = listaNumeros[posact];
                        listaNumeros[posact] = listaNumeros[posig];
                        listaNumeros[posig] = temp;
                    }
                }
            }

            return listaNumeros;
        }
    }

    public class OrdenDescendente : Ordenador
    {
        public override int[] Ordenar(int[] listaNumeros)
        {
            int posact, posig, temp;

            for (posact = 0; posact < 10; posact++)
            {
                for (posig = posact + 1; posig < 10; posig++)
                {
                    if (listaNumeros[posact] < listaNumeros[posig])
                    {
                        temp = listaNumeros[posact];
                        listaNumeros[posact] = listaNumeros[posig];
                        listaNumeros[posig] = temp;
                    }
                }
            }

            return listaNumeros;
        }
    }


}
