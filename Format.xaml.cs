using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
    /// Interaction logic for Format.xaml
    /// </summary>
    public partial class Format : Window
    {
        private string _police;
        public string Police
        {
            get { return _police; }
            set { _police = value; }
        }

        private int _taille;
        public int Taille
        {
            get { return _taille; }
            set { _taille = value; }
        }

        private string _couleur1;
        public string Couleur1
        {
            get { return _couleur1; }
            set { _couleur1 = value; }
        }

        private string _couleur2;
        public string Couleur2
        {
            get { return _couleur2; }
            set { _couleur2 = value; }
        }


        public Format(string police, int taille, string couleur1, string couleur2)
        {
            InitializeComponent();

            Police = police;
            Taille = taille;
            Couleur1 = couleur1;
            Couleur2 = couleur2;

            txtBoxCouleur1.Text = Couleur1;
            txtBoxCouleur2.Text = Couleur2;


            int compteurComboBoxPolice = 1;
            foreach (ComboBoxItem comboBoxItem in comboPolice.Items)
            {
                if (comboBoxItem.Content.ToString() == Police)
                {
                    break;
                }
                compteurComboBoxPolice++;
            }
            comboPolice.SelectedIndex = compteurComboBoxPolice-1;


            int compteurComboBoxTaille = 0;
            foreach (ComboBoxItem comboBoxItem in comboTaille.Items)
            {
                if (comboBoxItem.Content.ToString() == Taille.ToString())
                {
                    break;
                }
                compteurComboBoxTaille++;
            }
            comboTaille.SelectedIndex = compteurComboBoxTaille;
            this.ShowDialog();
        }

        private void closeWindow_MouseUp(object sender, MouseButtonEventArgs e)
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

        private void comboPolice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Police = (e.AddedItems[0] as ComboBoxItem).Content as string; ;
        }

        private void comboTaille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tailleEnString = (e.AddedItems[0] as ComboBoxItem).Content as string;
            Taille = Int32.Parse(tailleEnString);
        }

        private void txtBoxCouleur1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBoxCouleur1.Text.Length == 7 || txtBoxCouleur1.Text.Length == 4)
            {
                try
                {
                    Couleur1 = txtBoxCouleur1.Text;
                    System.Drawing.Color c = ColorTranslator.FromHtml(Couleur1);
                    System.Windows.Media.Color color = System.Windows.Media.Color.FromRgb(c.R, c.G, c.B);
                    aColor1.Color = color;
                }
                catch { }
            }
        }

        private void txtBoxCouleur2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBoxCouleur2.Text.Length == 7 || txtBoxCouleur2.Text.Length == 4)
            {
                try
                {
                    Couleur2 = txtBoxCouleur2.Text;
                    System.Drawing.Color c = ColorTranslator.FromHtml(Couleur2);
                    System.Windows.Media.Color color = System.Windows.Media.Color.FromRgb(c.R, c.G, c.B);
                    aColor2.Color = color;
                }
                catch { }
            }
        }
    }
}
