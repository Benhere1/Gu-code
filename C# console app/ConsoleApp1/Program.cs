//Copy from here and paste in a c# console or any c# compiler online by deleting the main class
public static class Program
{
    public class Stats
    {
        //Character stats
        public string name;
        public string[] talent = { "null", "A", "B", "C", "D", "Verdant Great Sun Physique", "Desolate Ancient Moon Physique", "Northern Dark Ice Soul Physique", "Boundless Forest Samsara Physique", "Blazing Glory Lightning Brilliance Physique", "Myriad Gold Wondrous Essence Physique", "Great Strength True Martial Physique", "Carefree Wisdom Heart Physique", "Universe Great Derivation", "Central Earth Essence" };
        public int rank = 1;
        public float lifespan; // no use currently
        public float Essence; // player primival essence
        public float essencecap; // the maximum primival essence a player can have related to talent
        public float essencerg; // the recovery of player's essence related to talent
        //Aperture stats
        public float apprehp = 200.0f; // the durability of the apreture walls when 0 breakthrough occurs
        public float appreinc = 0.0f; //increases appreture hp after breathrough
        public int apprerg = 2; // the recovery of the aporeture during rest increases every major realm
        public string[] stages = { "Initial", "Middle", "Upper", "Peak" };
        public int stageN = 0; //used to go through stages array
        //Gu stats
        public string guName;
        public int guRank;
        public float Hp;
        public float atk;
        //Primival stone stats
        public float stones; //number of primival stones
        public float stonergn = 50.0f; // translation rate of stones to primival essence. decreases by half each realm
        public bool con = true;
 
    }
 
    //Player ckass contains methods to creat character, cultivate the apreture and use stones
    public class GuChar : Stats
    {
        Random rand = new Random();
        //Gives random stats to a gu character
        public void reRoll()
        {
            stones = rand.Next(20, 101);
            lifespan = rand.Next(75, 101);
            int ranTal = rand.Next(0, 101);
            if (ranTal == 100)
            {
                Essence = 100.0f;
                essencecap = Essence;
                essencerg = 12;
                int ranPhy = rand.Next(5, 15);
                talent[0] = talent[ranPhy];
            }
            else if (ranTal >= 95)
            {
                Essence = rand.Next(80, 100);
                essencecap = Essence;
                essencerg = 8;
                talent[0] = talent[1];
            }
            else if (ranTal >= 80)
            {
                Essence = rand.Next(60, 80);
                essencecap = Essence;
                essencerg = 6;
                talent[0] = talent[2];
            }
            else if (ranTal >= 50)
            {
                Essence = rand.Next(40, 60);
                essencecap = Essence;
                essencerg = 4;
                talent[0] = talent[3];
            }
            else if (ranTal < 50)
            {
                Essence = rand.Next(20, 40);
                essencecap = Essence;
                essencerg = 2;
                talent[0] = talent[4];
            }
 
        }
        // Refines the apreture with player Essence
        public void refinewall()
        {
            apprehp = apprehp - Essence;
 
            if (apprehp > Essence)
            {
                Essence = 0;
            }
            else if (apprehp < 0)
            {
                Essence = apprehp * -1;
                apprehp = 0;
            }
            Console.WriteLine("Apreture Hp: " + apprehp + "\nPrimival Essence: " + Essence + "%");
 
        }
        // Recovers player's Essence but also restores appreture walls. 
        // Rest for 6 hours
        public void recoverapr()
        {
            Essence = Essence + (essencerg * 6);
            apprehp = apprehp + (apprerg * 6);
 
            if (Essence > essencecap)
            {
                Essence = essencecap;
            }
            if (apprehp > 200 + appreinc)
            {
                apprehp = 200 + appreinc;
            }
            Console.WriteLine("Apreture Hp: " + apprehp + "\nPrimival Essence: " + Essence + "%");
        }
        //Lets player turn the primival stones into primival essence
        public void stoneRgn()
        {
            while (con == true)
            {
                Console.WriteLine("\nPrimival stones: " + stones + "\nPrimival Stone ratio 1 = " + stonergn + "%" + "\nHow many stones do you want to consume");
                int resp = Convert.ToInt32(Console.ReadLine());
                if (stones < resp)
                {
                    Console.WriteLine("Primival stones are not available ");
                }
                else
                {
                    Essence = Essence + (resp * stonergn);
                }
                if (Essence > essencecap)
                {
                    Essence = essencecap;
                }
                if (stones > resp)
                {
                    stones = stones - resp;
                }
                Console.WriteLine("Primival Essence: " + Essence + "%\n");
                Console.WriteLine("Do you want to use more stone\nInput 1 to use more stones OR Input any key to continue\n");
                string respon = Console.ReadLine();
                if (!respon.Equals("1"))
                {
                    con = false;
                }
            }
            con = true;
        }
        //Checks player primival wall hp to then determine if the breakthrough a minor realm
        public void MinorBreakthrough()
        {
            if (apprehp <= 0 && stageN != 3)
            {
                stageN += 1;
                appreinc += 50;
                apprehp = 200 + appreinc;
                stonergn = stonergn / 2;
                Console.WriteLine("Breakthrough to rank " + rank + " " + stages[stageN]);
            }
        }
        //Checks if player is at the peak ofvthe realm with a fully refined wall to break through to the next rsnk
        public void MajorBreakthrough()
        {
            if (stageN == 3 && rank == 5)
            {
                Console.WriteLine("\nYou have reached the mortal peak\nCan no longer refine apreture any longer");
            }
            else if (apprehp <= 0 && stageN == 3)
            {
                rank += 1;
                stageN = 0;
                appreinc += 50;
                apprehp = 200 + appreinc;
                apprerg += 2;
                stonergn = stonergn / 2;
                Console.WriteLine("Major breakthrough to rank " + rank + " " + stages[stageN]);
            }
 
        }
        //Prints players stats in console
        public void CharStats()
        {
            Console.WriteLine("\nSTATS\n" + "Name: " + name + "\nRank: " + rank + " " + stages[stageN] + "\nLifespan: " + lifespan + "\nTalent: " + talent[0] + "\nPrimival essence: " + Essence + " % " + "\nPrimival stones: " + stones);
        }
 
    }
 
    //Gu worm class no use for now
    public class GuWorm : Stats
    {
        public GuWorm(string guName, int guRank, int hp, int atk)
        {
            this.guName = guName;
            Hp = hp;
            this.atk = atk;
            this.guRank = guRank;
        }
        public void Stats()
        {
            Console.WriteLine("Name: " + name + "\nRank: " + rank + "\nHp: " + Hp + "\nAttack: " + atk);
        }
    }
 
    // Input method that takes string input from user
    static void input(ref string input)
    {
        input = Console.ReadLine();
    }
 
    // Main method
    public static void Main()
    {
        string input1 = ""; //main input variable
        bool con1 = true;//conditions to run while loops
        bool con2 = true;//
 
 
        GuChar player = new GuChar();
 
 
        Console.WriteLine("Enter a name");
        player.name = Console.ReadLine();
        //Character reroll section
        while (con1 == true)
        {
            con2 = true;
            Console.Clear();
            while (con2 == true)
            {
                player.reRoll();
                player.CharStats();

                Console.WriteLine("\nEnter 1 to Reroll stats\nOR enter any other key to continue");
                input(ref input1);

                if (input1.Equals("1"))
                {
                    con2 = false;
                }
                else
                {
                    con2 = false;
                    con1 = false;
                }
                Console.Clear();
            }
        }
        //Choose tasks section
            while (con1 == false)
            {
                Console.Clear();
                Console.WriteLine("\nInput the number of the task to perform\n(1)Cultivate apreture\n(2)Rest\n(3)Use primival stones\n(4)Check current stats\n(5)Check game Instructions");
                input(ref input1);

                if (input1.Equals("1"))
                {
                    player.refinewall();
                    player.MinorBreakthrough();
                    player.MajorBreakthrough();
                }
                else if (input1.Equals("2"))
                {
                    player.recoverapr();
                }
                else if (input1.Equals("3"))
                {
                    player.stoneRgn();
                }
                else if (input1.Equals("4"))
                {
                    player.CharStats();
                }
                else if (input1.Equals("5"))
                {
                    Console.WriteLine("Cultivating apreture uses your essence to decrease apreture Hp.\nWhen apreture Hp reaches zero you make a breakthrough and apreture hp increases by 50.\nWhen you rest you recover essence according to talent for 6 hours.\nBut you appreture Hp also recovers the recovery rate increasing per rank.\nYou can also use stones to recover you essence but the rate halves per breakthrough.");
                    Console.WriteLine("\nInput any key to continue");
                    input(ref input1);
                }
            }
    }
}
