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
using System.Diagnostics;

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

        private Label _labell;
        public Label Labell
        {
            get { return _labell; }
            set { _labell = value; }
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
                    textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
                    textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
                    File.WriteAllText(Chemin, textRange.Text);
                    Labell.Content = $"{saveFileDialog.SafeFileName} - Bloc-notes";
                }
            }
            else
            {
                textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
                textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
                File.WriteAllText(Chemin, textRange.Text);
                string newLabel = Labell.Content.ToString().Remove(0, 1);
                Labell.Content = newLabel;
            }
            Texte = textRange.Text;
        }


        // fonction a refaire avec une fenêtre personnalisée plutôt qu'une MessageBox
        public void Nouveau(RichTextBox richTextBox)
        {
            TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            // retire les deux derniers char de textRange.Text car retour à la ligne à enlever
            if (!textRange.IsEmpty)
            {
                textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
                textRange.Text = textRange.Text.Remove(textRange.Text.Length - 1);
            }
            
            if (Chemin == "" && textRange.IsEmpty)
            {
                return;
            }

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
                    Labell.Content = "*Sans titre - Bloc-notes";
                }
                // si result == non => ne save pas, clear Chemin et vide la richTextBox
                else if (result == MessageBoxResult.No)
                {
                    Chemin = "";
                    richTextBox.Document.Blocks.Clear();
                    Labell.Content = "*Sans titre - Bloc-notes";
                }
            }
            // si le texte == le dernier texte enregistré => clear Chemin et vide la richTextBox
            else
            {
                Chemin = "";
                richTextBox.Document.Blocks.Clear();
                Labell.Content = "*Sans titre - Bloc-notes";
            }
        }
            

        public void Open(RichTextBox richTextBox, Label labelTitre)
        {
            TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            if (Chemin != "" || !textRange.IsEmpty)
            {
                Nouveau(richTextBox);
            }

            OpenFileDialog openFileDialog = new()
            {
                InitialDirectory = @"D:\Bureau",
                Filter = "Text file (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var sr = new StreamReader(openFileDialog.FileName);
                richTextBox.Document.Blocks.Add(new Paragraph(new Run(sr.ReadToEnd())));
                Chemin = openFileDialog.FileName;
                Labell.Content = $"{openFileDialog.SafeFileName} - Bloc-notes";

                // enlever retour à la ligne quand on ouvre un fichier au démarage
            }
        }
    }
}
