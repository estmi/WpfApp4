using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDesti_Click(object sender, RoutedEventArgs e)
        {
            var fb = new System.Windows.Forms.FolderBrowserDialog();
            var resultat = fb.ShowDialog();
            if (resultat == System.Windows.Forms.DialogResult.OK)
            {
                txtDesti.Text = fb.SelectedPath;
            }
        }

        private void btnOrigen_Click(object sender, RoutedEventArgs e)
        {
            var fb = new System.Windows.Forms.FolderBrowserDialog();
            var resultat = fb.ShowDialog();
            if (resultat == System.Windows.Forms.DialogResult.OK)
            {
                txtOrigen.Text = fb.SelectedPath;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string origen = txtOrigen.Text;
                string desti = txtDesti.Text;
                if (!Directory.Exists(origen) || !Directory.Exists(desti))
                {
                    throw new Exception("Directoris incorrectes");
                }
                p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = $"/c xcopy \"{origen}\" \"{desti}\" /s /y";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.EnableRaisingEvents = true;
                p.Exited += P_Exited;
                p.Start();
                if (p.ExitCode == 0)
                {
                    txtResult.Text = "Tot Be";
                }
                else
                {
                    txtResult.Text = "Error";
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        Process p;
        private void P_Exited(object? sender, EventArgs e)
        {
            if (p != null)
            {
                Action act = null;
                if (p.ExitCode == 0)
                {
                    act = new Action(actualitzaAmbOK);
                }
                else act = new Action(actualitzaAmbERROR);
                Dispatcher.Invoke(act);
            }
        }

        private void actualitzaAmbERROR()
        {
            txtResult.Text = "TOt ERROR";
        }

        private void actualitzaAmbOK()
        {
            txtResult.Text = "TOt Ok";
        }
    }
}
