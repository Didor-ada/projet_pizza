using System;
using System.Collections.Generic;
using System.Linq;

namespace projet_pizza
{

    class pizza
    {
        string nom;
        float prix;
        bool vegetarienne;
        List<string> ingredients;

        public pizza(string nom, float prix, bool vegetarienne, List<string> ingredients)
        {
            this.nom = nom;
            this.prix = prix;
            this.vegetarienne = vegetarienne;
            this.ingredients = ingredients;
        }

        public void Afficher()
        {
            
            string badgeVegetarienne = vegetarienne ? " (V)" : ""; // ternaire version 1 ligne :

            string nomAfficher = FormatPremiereLettreMajuscules(nom);

            Console.WriteLine(nomAfficher + badgeVegetarienne + " - " + prix + "€");
            Console.WriteLine(string.Join(", ", ingredients));
            Console.WriteLine();
        }

        private static string FormatPremiereLettreMajuscules(string s) // static car elle ne dépend pas des données de notre pizza
        {
            /* if ((s == null) || (s.Length == 0))
            return s; 
            en dessous un équivalent*/

            if (string.IsNullOrEmpty(s))
                return s;

            string minuscules = s.ToLower();
            string majuscules = s.ToUpper();

            string resultat = majuscules[0] + minuscules[1..];

            return resultat;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            /*var pizza1 = new pizza("4 fromages", 11.5f, true);
            pizza1.afficher();*/

            var pizzas = new List<pizza>() {
               new pizza("4 fromages", 11.5f, true, new List<string> { "cantal", "mozzarella", "fromage de chèvre", "gruyère" }),
               new pizza("indienne", 10.5f, false, new List<string> { "curry", "mozzarella", "poulet de chèvre", "poivron", "oignon", "coriandre" }),
               new pizza("mexicaine", 13f, false, new List<string> { "boeuf", "mozzarella", "maïs", "tomates", "oignon", "coriandre" }),
               new pizza("margherita", 8f, true, new List<string> { "sauce tomate", "mozzarella", "basilic" }),
               new pizza("calzone", 12f, false, new List<string> { "tomate", "jambon", "persil", "oignon" }),
               new pizza("complète", 9.5f, false, new List<string> { "jambon", "oeuf", "fromage" }),
            };

            foreach(var pizza in pizzas)
            {
                pizza.Afficher();
            }
        }
    }
}
