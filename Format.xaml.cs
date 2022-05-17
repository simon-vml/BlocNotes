using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
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
using System.Windows.Shapes;
using Microsoft.Win32;
using Path = System.IO.Path;

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


        private ObservableCollection<Presets> _observableCollection = new();


        public Format(string police, int taille, string couleur1, string couleur2)
        {
            InitializeComponent();

            Police = police;
            Taille = taille;
            Couleur1 = couleur1;
            Couleur2 = couleur2;

            SetupInfosSurLaFenetre();
            LoadAllPresetsInComboBox();
            this.ShowDialog();
        }

        private void SetupInfosSurLaFenetre()
        {
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
            comboPolice.SelectedIndex = compteurComboBoxPolice - 1;


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
        }

        private void LoadAllPresetsInComboBox()
        {
            string combinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"../../../presets.txt");
            string realPath = Path.GetFullPath(combinedPath);
            List<string> allLinesText = File.ReadAllLines(realPath).ToList();
            foreach (string line in allLinesText)
            {
                string[] tempList = line.Split("#");
                Presets preset = new Presets(tempList[0], tempList[1], Int32.Parse(tempList[2]), tempList[3],
                    tempList[4]);
                _observableCollection.Add(preset);
            }

            foreach (Presets preset in _observableCollection)
            {
                comboBoxPresets.Items.Add(preset.NomDuPreset);
            }
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

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            int indexPreset = comboBoxPresets.SelectedIndex;
            if (indexPreset == -1)
            {
                return;
            }
            Presets selectedPreset = _observableCollection[indexPreset];

            Police = selectedPreset.Police;
            Taille = selectedPreset.Taille;
            Couleur1 = $"#{selectedPreset.Couleur1}";
            Couleur2 = $"#{selectedPreset.Couleur2}";

            txtBoxCouleur1.Text = Couleur1;
            txtBoxCouleur2.Text = Couleur2;

            int compteurPolice = 0;
            foreach (ComboBoxItem item in comboPolice.Items)
            {
                if (item.Content.ToString() == Police)
                {
                    break;
                }
                compteurPolice++;
            }
            comboPolice.SelectedIndex = compteurPolice;


            int compteurTaille = 0;
            foreach (ComboBoxItem item in comboTaille.Items)
            {
                if (item.Content.ToString() == Taille.ToString())
                {
                    break;
                }
                compteurTaille++;
            }
            comboTaille.SelectedIndex = compteurTaille;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nomDuPreset = txtBoxPresets.Text;
            string couleur1 = Couleur1;
            couleur1 = couleur1.Remove(0, 1);
            string couleur2 = Couleur2;
            couleur2 = couleur2.Remove(0, 1);
            string nouveauPreset = $@"{nomDuPreset}#{Police}#{Taille}#{couleur1}#{couleur2}";
            string combinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"../../../presets.txt");
            string realPath = Path.GetFullPath(combinedPath);
            File.AppendAllText(realPath, nouveauPreset + Environment.NewLine);
            Presets preset = new(nomDuPreset, Police, Taille, couleur1, couleur2);
            _observableCollection.Add(preset);
            comboBoxPresets.Items.Add(preset.NomDuPreset);
            txtBoxPresets.Text = "";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Close();
            }
        }
    }
}
