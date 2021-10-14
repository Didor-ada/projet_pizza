using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace projet_pizza
{

    class PizzaPersonnalisee : Pizza
    {

        static int nbPizzasPersonnalisees = 0;

        public PizzaPersonnalisee() : base("Personnalisée", 5, false, null)
        {

            nbPizzasPersonnalisees++;
            nom = "Personnalisée " + nbPizzasPersonnalisees;

            ingredients = new List<string>();

            while(true)
            {
                Console.Write("Rentrez un ingrédient pour la pizza personnalisée " + nbPizzasPersonnalisees + " (ENTER pour terminer) : ");
                var ingredient = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(ingredient))
                {
                    break;
                }
                if (ingredients.Contains(ingredient))
                {
                    Console.WriteLine("Erreur : cet ingrédient est déjà présent dans la pizza");
                }
                else
                {
                    ingredients.Add(ingredient);
                    Console.WriteLine(string.Join(", ", ingredients));
                }

                Console.WriteLine();
            }

            prix = 5 + ingredients.Count * 1.5f;

        }
    }

    class Pizza
    {
        public string nom { get; protected set; }
        public float prix { get; protected set; }
        public bool vegetarienne { get; private set; }
        public List<string> ingredients { get; protected set; }
        /*  public override string ToString()
        {
            return base.ToString();
        }*/

        public Pizza(string nom, float prix, bool vegetarienne, List<string> ingredients)
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

        static List<Pizza> GetPizzasFromCode()
        {
            var pizzas = new List<Pizza>() {
               new Pizza("4 fromages", 11.5f, true, new List<string> { "cantal", "mozzarella", "fromage de chèvre", "gruyère" }),
               new Pizza("indienne", 10.5f, false, new List<string> { "curry", "mozzarella", "poulet", "poivron", "oignon", "coriandre" }),
               new Pizza("mexicaine", 13f, false, new List<string> { "boeuf", "mozzarella", "maïs", "tomates", "oignon", "coriandre" }),
               new Pizza("margherita", 8f, true, new List<string> { "sauce tomate", "mozzarella", "basilic" }),
               new Pizza("calzone", 12f, false, new List<string> { "tomate", "jambon", "persil", "oignon" }),
               new Pizza("complète", 9.5f, false, new List<string> { "jambon", "oeuf", "fromage" }),
               // new PizzaPersonnalisee(),
               // new PizzaPersonnalisee()
            };
            return pizzas;
        }

        static List<Pizza> GetPizzasFromFile(string filename)
        {
            string json = null;
            try
            {
                json = File.ReadAllText(filename);
            }
            catch
            {
                Console.WriteLine("Erreur de lecture du fichier : " + filename);
                return null;
            }


            // Console.WriteLine(json);
            List<Pizza> pizzas = null;
            try
            {
                pizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
            }
            catch
            {
                Console.WriteLine("Erreur : Les données JSON ne sont pas valides");
                return null;
            }

            return pizzas;
        }

        static void GenerateJsonFile(List<Pizza> pizzas, string filename)
        {
            string json = JsonConvert.SerializeObject(pizzas);
            File.WriteAllText(filename, json);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var filename = "pizzas.json";

            var pizzas = GetPizzasFromFile(filename);

            foreach (var pizza in pizzas)
            {
                pizza.Afficher();
            }
        }
    }
}
