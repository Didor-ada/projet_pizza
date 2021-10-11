using System;
using System.Collections.Generic;
using System.Linq;

namespace projet_pizza
{

    class pizza
    {
        string nom;
        public float prix { get; private set; }
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

            // pizzas = pizzas.OrderBy(p => p.prix).ToList(); / OrderByDescending pour avoir de la plus chère à la moins chère

            float prixMin, prixMax;
            pizza pizzaPrixMin = null;
            pizza pizzaPrixMax = null;

            pizzaPrixMin = pizzas[0];
            pizzaPrixMax = pizzas[0];

            foreach (var pizza in pizzas)
            {
                if(pizza.prix < pizzaPrixMin.prix)
                {
                    prixMin = pizza.prix;
                    pizzaPrixMin = pizza;
                }
                if (pizza.prix > pizzaPrixMax.prix)
                {
                    prixMax = pizza.prix;
                    pizzaPrixMax = pizza;
                }
            }

            foreach(var pizza in pizzas)
            {
                pizza.Afficher();
            }

            Console.WriteLine();
            Console.WriteLine("La pizza la moins chère est :");
            pizzaPrixMin.Afficher();
            Console.WriteLine();
            Console.WriteLine("La pizza la plus chère est :");
            pizzaPrixMax.Afficher();
        }
    }
}
