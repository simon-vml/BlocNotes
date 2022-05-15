using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;

namespace BlocNote
{
    internal class Fichier
    {
        private string _chemin = "";
        public string Chemin
        {
            get { return _chemin; }
            set { _chemin = value; }
        }

        public Fichier()
        {

        }

        public Fichier(string chemin)
        {
            Chemin = chemin;
        }

        public void Save(TextRange textRange)
        {
            if (Chemin == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text file (*.txt)|*.txt";
                saveFileDialog.InitialDirectory = @"D:\Documents\Bloc notes";
                if (saveFileDialog.ShowDialog() == true)
                {
                    Chemin = saveFileDialog.FileName;
                    File.WriteAllText(Chemin, textRange.Text);
                }
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                File.WriteAllText(Chemin, textRange.Text);
            }
        }

        public void Close(RichTextBox richTextBox)
        {
            TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            if (textRange.Text == "")
            {
                Chemin = "";
                // à finir
                richTextBox.Document.Blocks.Clear();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Souhaitez-vous enregistrer le document actif ?", "Document non enregistré", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    Save(textRange);
                }
            }
        }
    }
}
