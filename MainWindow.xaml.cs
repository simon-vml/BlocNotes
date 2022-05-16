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

namespace BlocNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Fichier fichier = new Fichier();
        

        public MainWindow()
        {
            InitializeComponent();
            fichier.Labell = windowTitle;
        }
        

        private void closeWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }


        private void BougerFenetre_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }


        private void MenuItemQuitter_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void MenuItemEnregistrer_OnClick(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            fichier.Save(textRange);
        }


        private void MenuItemNouveau_OnClick(object sender, RoutedEventArgs e)
        {
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
    }
}
