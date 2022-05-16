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
    class Fichier
    {
        private string _chemin = "";
        public string Chemin
        {
            get { return _chemin; }
            set { _chemin = value; }
        }

        private string _texte;
        public string Texte
        {
            get { return _texte; }
            set { _texte = value; }
        }

        public Fichier() { }

        public Fichier(string chemin)
        {
            Chemin = chemin;
        }

        public void Save(TextRange textRange)
        {
            if (Chemin == "")
            {
                SaveFileDialog saveFileDialog = new()
                {
                    Filter = "Text file (*.txt)|*.txt",
                    //InitialDirectory = @"D:\Documents\Bloc notes"
                    InitialDirectory = @"D:\Bureau"
                };
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
            Texte = textRange.Text;
        }

        public void Close(RichTextBox richTextBox)
        {
            TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            // si le texte != dernier texte enregistré => proposer d'enregistrer le texte
            if (textRange.Text != Texte)
            {
                MessageBoxResult result = MessageBox.Show("Votre document n'est pas enregistré.\nSouhaitez-vous enregistrer le document actif avant de le fermer?", "Document non enregistré", MessageBoxButton.YesNoCancel);
                // si result == oui => appel Save(), clear Chemin et vide la richTextBox
                if (result == MessageBoxResult.Yes)
                {
                    Save(textRange);
                    Chemin = "";
                    richTextBox.Document.Blocks.Clear();
                }
                // si result == non => ne save pas, clear Chemin et vide la richTextBox
                else if (result == MessageBoxResult.No)
                {
                    Chemin = "";
                    richTextBox.Document.Blocks.Clear();
                }
            }
            // si le texte == le dernier texte enregistré => clear Chemin et vide la richTextBox
            else
            {
                Chemin = "";
                richTextBox.Document.Blocks.Clear();
            }
        }
    }
}
