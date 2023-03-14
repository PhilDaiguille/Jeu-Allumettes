namespace JeuAllumettes
{
    public class Allumettes
    {
        private int allumettesRestantes;
        private bool monTour;
        private int score;
        private Dictionary<int, Allumettes> Final = new Dictionary<int, Allumettes>();

        public Allumettes(int allumettesRestantes, bool monTour)
        {
            this.allumettesRestantes = allumettesRestantes;
            this.monTour = monTour;
            for (int nombre = 1; nombre <= 3 && nombre <= allumettesRestantes; nombre++)
                Final.Add(nombre, new Allumettes(allumettesRestantes - nombre, !monTour));
        }
        public int NombreChoisi()
        {
            int meilleurNombreAllumettes = 1;
            CalculeScores();
            foreach (int nombreAllumettes in Final.Keys)
                meilleurNombreAllumettes = Final[meilleurNombreAllumettes].score > Final[nombreAllumettes].score
                        ? meilleurNombreAllumettes : nombreAllumettes;
            return meilleurNombreAllumettes;
        }
        public string EnChaine(int profondeur)
        {
            string r = "";
            r += "allumettes = " + allumettesRestantes + ", ";
            r += monTour ? "IA" : "Humain";
            r += ", score = " + score;
            r += "\n";
            foreach (Allumettes allumettes in Final.Values)
            {
                for (int i = 1; i <= profondeur; i++)
                    r += " ";
                r += allumettes.EnChaine(profondeur + 1);
            }
            return r;
        }
        public int CalculeScores()
        {
            if (monTour)
            {
                score = -1;
                foreach (Allumettes Final in Final.Values)
                {
                    int scoreFinal = Final.CalculeScores();
                    score = score > scoreFinal ? score : scoreFinal;
                }
            }
            else
            {
                score = 1;
                foreach (Allumettes Final in Final.Values)
                {
                    int scoreFinal = Final.CalculeScores();
                    score = score < scoreFinal ? score : scoreFinal;
                }
            }
            return score;
        }
        public static void Main(string[] args)
        {
            int nombreAllumettes = 17;
            bool tourIA = false;
            while (nombreAllumettes != 0)
            {
                Console.WriteLine($"Il reste {nombreAllumettes} allumettes.");
                if (tourIA)
                {
                    Allumettes allumettes = new Allumettes(nombreAllumettes, true);
                    int nombreChoisi = allumettes.NombreChoisi();
                    Console.WriteLine($"L'IA prend {nombreChoisi} allumette(s).");
                    nombreAllumettes -= nombreChoisi;
                }
                else
                {
                    int nombreChoisi;
                    do
                    {
                        Console.WriteLine("Combien souhaitez-vous en prendre ?");
                        nombreChoisi = Convert.ToInt32(Console.ReadLine());
                    }
                    while (nombreChoisi < 1 || nombreChoisi > 3);
                    Console.WriteLine($"Vous prenez {nombreChoisi} allumette(s).");
                    nombreAllumettes -= nombreChoisi;
                }
                tourIA = !tourIA;
            }
            if (tourIA)
                Console.WriteLine("Vous avez gagné !");
            else
                Console.WriteLine("Vous avez perdu !");
        }

    }

}