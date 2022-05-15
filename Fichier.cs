using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Save()
        {
            if (Chemin != "")
            {
                OpenFileDialog openFileDialog = new();

                if (openFileDialog.ShowDialog() == true)
                {
                    Chemin = openFileDialog.FileName;
                }
            }
            else
            {

            }
        }
    }
}
