using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Net.Mime;

namespace BlocNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Fichier fichier = new Fichier();

        private bool _isFullScreen = false;

        private string _police = "Borg9";
        private int _taille = 18;
        private string _couleur1 = "#262626";
        private string _couleur2 = "#383838";



        public MainWindow()
        {
            InitializeComponent();
            fichier.Labell = windowTitle;
            SetupWindow();
        }
        
        private void SetupWindow()
        {
            var brush1 = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)26, (byte)26, (byte)26));
            borderBarreDeTitre.Background = brush1;
            gridRow1.Background = brush1;
            menuGlobal.Background = brush1;

            var brush2 = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)38, (byte)38, (byte)38));
            borderRichTextBox.Background = brush2;


            FontFamily fontFamily = new FontFamily(_police);
            richTextBox.FontFamily = fontFamily;


            richTextBox.FontSize = _taille;
        }


        private void closeWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            // retire les deux derniers char de textRange.Text car retour à la ligne à enlever
            if (!textRange.IsEmpty)
            {
                textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
                textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
            }
            if (fichier.Chemin == "" && textRange.IsEmpty)
            {
                Close();
            }
            if (textRange.Text != fichier.Texte)
            {
                AvertissementDocumentPasSauvegarde avertissement = new();
                // si result == oui => appel Save() et ferme la fenêtre
                if (avertissement.Choix == "yes")
                {
                    fichier.IsCalledByMenuItemNouveau = true;
                    fichier.Save(textRange);
                    Close();
                }
                // si result == non => ne save pas et ferme la fenêtre
                else if (avertissement.Choix == "no")
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }


        private void BougerFenetre_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }

            if (e.ClickCount == 2)
            {
                fullScreen_MouseUp(sender, e);
            }
        }


        private void MenuItemQuitter_OnClick(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            // retire les deux derniers char de textRange.Text car retour à la ligne à enlever
            if (!textRange.IsEmpty)
            {
                textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
                textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
            }
            if (fichier.Chemin == "" && textRange.IsEmpty)
            {
                Close();
            }
            if (textRange.Text != fichier.Texte)
            {
                AvertissementDocumentPasSauvegarde avertissement = new();
                // si result == oui => appel Save() et ferme la fenêtre
                if (avertissement.Choix == "yes")
                {
                    fichier.IsCalledByMenuItemNouveau = true;
                    fichier.Save(textRange);
                    Close();
                }
                // si result == non => ne save pas et ferme la fenêtre
                else if (avertissement.Choix == "no")
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }


        private void MenuItemEnregistrer_OnClick(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            fichier.Save(textRange);
            richTextBox.CaretPosition = richTextBox.CaretPosition.DocumentEnd;

        }


        private void MenuItemNouveau_OnClick(object sender, RoutedEventArgs e)
        {
            fichier.IsCalledByMenuItemNouveau = true;
            fichier.Nouveau(richTextBox);
        }


        private void menuItemSelectTout_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.SelectAll();
        }


        private void menuItemNouvelleFenetre_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo pInfo = new ProcessStartInfo("BlocNote.exe");
            pInfo.WorkingDirectory = @"C:\Users\Simon\source\repos\simon-vml\BlocNotes\bin\Debug\net6.0-windows";
            Process p = Process.Start(pInfo);
        }


        private void menuItemOuvrir_Click(object sender, RoutedEventArgs e)
        {
            fichier.Open(richTextBox, windowTitle);
        }


        private void richTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (windowTitle.Content.ToString()[0] != '*')
            {
                windowTitle.Content = $"*{windowTitle.Content}";
            }
        }


        private void menuItemEnregistrerSous_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            fichier.SaveSous(textRange);
            richTextBox.CaretPosition = richTextBox.CaretPosition.DocumentEnd;
        }


        private void menuItemHeureDate_Click(object sender, RoutedEventArgs e)
        {
            string heuresMinutes = DateTime.Now.ToString("HH:mm");
            string date = DateTime.Now.ToString("d");
            richTextBox.CaretPosition.InsertTextInRun($"{heuresMinutes} {date}");
        }


        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            RoutedEventArgs aaaaa = new();
            if (e.Key == Key.F5)
            {
                menuItemHeureDate_Click(sender, aaaaa);
            }
            else if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                MenuItemEnregistrer_OnClick(sender, aaaaa);
            }
        }


        private void fullScreen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isFullScreen)
            {
                WindowState = WindowState.Maximized;
                _isFullScreen = true;
                return;
            }
            WindowState = WindowState.Normal;
            _isFullScreen = false;
        }


        private void minimize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void menuItemFormat_Click(object sender, RoutedEventArgs e)
        {
            Format windowFormat = new(_police, _taille, _couleur1, _couleur2);
            _police = windowFormat.Police;
            _taille = windowFormat.Taille;
            _couleur1 = windowFormat.Couleur1;
            _couleur2 = windowFormat.Couleur2;

            richTextBox.FontSize = _taille;
            runRichTextBox.FontFamily = new FontFamily(_police);

            if (windowFormat.Couleur1.Length == 4)
            {
                int R = windowFormat.Couleur1[1];
                int G = windowFormat.Couleur1[2];
                int B = windowFormat.Couleur1[3];
                var brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)R, (byte)G, (byte)B));
                borderBarreDeTitre.Background = brush;
                gridRow1.Background = brush;
                menuGlobal.Background = brush;
            }
            else if (windowFormat.Couleur1.Length == 7)
            {
                int R = Int32.Parse($"{windowFormat.Couleur1[1]}{windowFormat.Couleur1[2]}");
                int G = Int32.Parse($"{windowFormat.Couleur1[3]}{windowFormat.Couleur1[4]}");
                int B = Int32.Parse($"{windowFormat.Couleur1[5]}{windowFormat.Couleur1[6]}");
                var brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)R, (byte)G, (byte)B));
                borderBarreDeTitre.Background = brush;
                gridRow1.Background = brush;
                menuGlobal.Background= brush;
            }


            if (windowFormat.Couleur2.Length == 4)
            {
                int R = windowFormat.Couleur2[1];
                int G = windowFormat.Couleur2[2];
                int B = windowFormat.Couleur2[3];
                var brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)R, (byte)G, (byte)B));
                borderRichTextBox.Background = brush;
            }
            else if (windowFormat.Couleur2.Length == 7)
            {
                int R = Int32.Parse($"{windowFormat.Couleur2[1]}{windowFormat.Couleur2[2]}");
                int G = Int32.Parse($"{windowFormat.Couleur2[3]}{windowFormat.Couleur2[4]}");
                int B = Int32.Parse($"{windowFormat.Couleur2[5]}{windowFormat.Couleur2[6]}");
                var brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)R, (byte)G, (byte)B));
                borderRichTextBox.Background = brush;
            }
        }
    }
}
