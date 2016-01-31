using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace sockfile_visual
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.ShowDialog();

            textBox1.Text = abrir.SafeFileName;
        }

        public void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            numeros(e);
        }

        public void numeros(KeyPressEventArgs e)
        {

            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                if (char.IsNumber(e.KeyChar))
                    e.Handled = false;
                else
            if (e.KeyChar == ('.'))
                    e.Handled = false;
                else
                if (char.IsControl(e.KeyChar))
                    e.Handled = false;
                else
                        e.Handled = true;
            }
            
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Elija un archivo", "Error");
                button1.Focus();
            }
            else
            {
                if (!textBox1.Text.Contains(".cia"))
                {
                    MessageBox.Show("Elija un archivo .CIA","Error");
                    button1.Focus();
                }
                else
                {
                    if (!textBox2.Text.Contains("192.168."))
                    {
                        MessageBox.Show("Ingrese una ip válida (chafivalidación)","Error");
                        textBox2.Focus();
                    }
                    else
                    {

                        Process p = new Process();
                        string quote = "\"";

                        string dir = Application.StartupPath + "/sockfile.cmd";
                        using (StreamWriter wr = File.CreateText(dir))
                        {
                            wr.WriteLine("java -jar sockfile.jar " + textBox2.Text + " " + quote + textBox1.Text + quote);
                        }
                        try
                        {
                            ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "/sockfile.cmd");
                            p.StartInfo = psi;
                            p.Start();
                        }
                        catch (Win32Exception)
                        {
                            MessageBox.Show("Sockfile.cmd no encontrado", "Error");
                        }
                    }
                }
            }

        }
    }
}
