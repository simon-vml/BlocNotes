using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocNote
{
    internal class Presets
    {
        private string _nomDuPreset;
        public string NomDuPreset
        {
            get { return _nomDuPreset; }
            set { _nomDuPreset = value; }
        }

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


        public Presets(string nom, string police, int taille, string couleur1, string couleur2)
        {
            NomDuPreset = nom;
            Police = police;
            Taille = taille;
            Couleur1 = couleur1;
            Couleur2 = couleur2;
        }
    }
}
