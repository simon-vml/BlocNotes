using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BlocNote
{
    /// <summary>
    /// Interaction logic for AvertissementDocumentPasSauvegarde.xaml
    /// </summary>
    public partial class AvertissementDocumentPasSauvegarde : Window
    {
        private string? _choix;
        public string? Choix
        {
            get { return _choix; }
            set { _choix = value; }
        }

        public AvertissementDocumentPasSauvegarde()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Choix = "yes";
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Choix = "no";
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
