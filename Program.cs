using System;
using System.Collections.Generic;
using System.Linq;

namespace projet_pizza
{

    class pizza
    {
        string nom;
        public float prix { get; private set; }
        public bool vegetarienne { get; private set; }
        public List<string> ingredients { get; private set; }
        /*  public override string ToString()
        {
            return base.ToString();
        }*/

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

            /*var ingredientsAfficher = new List<string>();
            foreach(var ingredient in ingredients)
            {
                ingredientsAfficher.Add(FormatPremiereLettreMajuscules(ingredient));
            }*/

            var ingredientsAfficher = ingredients.Select(i => FormatPremiereLettreMajuscules(i)).ToList(); // collection.Select pour transformer nos éléments directement

            Console.WriteLine(nomAfficher + badgeVegetarienne + " - " + prix + "€");
            Console.WriteLine(string.Join(", ", ingredientsAfficher));
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
               new pizza("indienne", 10.5f, false, new List<string> { "curry", "mozzarella", "poulet", "poivron", "oignon", "coriandre" }),
               new pizza("mexicaine", 13f, false, new List<string> { "boeuf", "mozzarella", "maïs", "tomates", "oignon", "coriandre" }),
               new pizza("margherita", 8f, true, new List<string> { "sauce tomate", "mozzarella", "basilic" }),
               new pizza("calzone", 12f, false, new List<string> { "tomate", "jambon", "persil", "oignon" }),
               new pizza("complète", 9.5f, false, new List<string> { "jambon", "oeuf", "fromage" }),
            };

            // garder uniquement les pizzas végétariennes
            // pizzas = pizzas.Where(p => p.vegetarienne).ToList();

            // pizzas.Select(p => p.ToString());


            // pizzas.Where(p => p.ingredients.Any(i => i.ToLower().Contains("tomate"))).ToList().ForEach(p => p.Afficher()); / version qui inclut le foreach qui permet l'affichage


            pizzas = pizzas.Where(p => p.ingredients.Where(i => i.ToLower().Contains("tomate")).ToList().Count > 0).ToList(); // > 0 veut dire si il y en a au moins 1
            foreach(var pizza in pizzas)
            {
                pizza.Afficher();
            }
        }
    }
}
