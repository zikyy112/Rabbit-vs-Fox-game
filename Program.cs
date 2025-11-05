namespace Le_jeu_de_la_vie
{
    internal class Program
    {
        static int[,] InitDamier(int M, int N)
        {
            int[,] Damier = null;
            if(M>0 && N>0)
            {
                Random r=new Random();
                Damier = new int[M, N];
                for(int i=0; i<M; i++)
                {
                    for(int j=0; j<N; j++)
                    {
                        Damier[i,j] = r.Next(0,2);
                    }
                }
            }
            return Damier;
        }
        static void AfficheDamier(int[,] damier)
        {
            if(damier!=null && damier.Length>0 && damier.GetLength(0)>0 && damier.GetLength(1)>0)
            {
                for(int i=0; i<damier.GetLength(0);i++)
                {
                    for(int j=0; j<damier.GetLength(1); j++)
                    {
                        if (damier[i,j]==1)
                        {
                            Console.Write("*");
                        }
                        else
                        {
                            if (damier[i,j]==0)
                            {
                                Console.Write("-");
                            }
                        }
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Damier null");
            }
        }
        static int CalculVoisin(int[,] damier, int x, int y) //on considère x : abscisse (colonne) et y : ordonnée (ligne) et qu'elles correspondent à la valeur brute (de 0)
        {
            int nb = 0;
            if(damier!=null && damier.Length>0 && damier.GetLength(0)>0 && damier.GetLength(1)>0)
            {
                if(x>=0 && x<damier.GetLength(1) && y>=0 && y<damier.GetLength(0))
                {
                    for(int i=(y-1); i<(y+2);i++)
                    {
                        for(int j=(x-1); j<(x+2); j++)
                        {
                            if (i < damier.GetLength(0) && i >= 0 && j < damier.GetLength(1) && j >= 0 && (j != x || i != y))
                            {
                                if (damier[i,j]==1)
                                {
                                    nb++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    nb = -1; //en gros la coordonnée n'est pas dans la matrice
                }
            }
            return nb;
        }
        static int[,] nouveauDamier(int[,] damier)
        {
            int[,] newDamier = null;
            if(damier!=null && damier.Length>0 && damier.GetLength(0)>0 && damier.GetLength(1)>0)
            {
                newDamier=new int[damier.GetLength(0),damier.GetLength(1)]; 
                for(int i=0; i<damier.GetLength(0); i++)
                {
                    for(int j=0;j<damier.GetLength(1);j++)
                    {
                        if (damier[i,j]==1)
                        {
                            if (CalculVoisin(damier, j, i) == 2 || CalculVoisin(damier, j, i) == 3)
                            {
                                newDamier[i, j] = 1;
                            }
                            else
                            {
                                newDamier[i, j] = 0;
                            }
                        }
                        else
                        {
                            if (CalculVoisin(damier, j, i) == 3)
                            {
                                newDamier[i, j] = 1;
                            }
                            else
                            {
                                newDamier[i, j] = 0;
                            }
                        }
                    }
                }
            }
            return newDamier;
        }
        static void CopieDamier(int[,] damierSource, int[,] damierCible)
        {
            if(damierCible != null && damierSource!=null && damierSource.GetLength(0)>0 && damierSource.GetLength(1) > 0 && damierCible.GetLength(0) > 0 && damierCible.GetLength(1) > 0)
            {
                if(damierCible.GetLength(0)==damierSource.GetLength(0) && damierSource.GetLength(1)==damierCible.GetLength(1))
                {
                    for (int i = 0; i < damierCible.GetLength(0); i++)
                    {
                        for (int j = 0; j < damierCible.GetLength(1); j++)
                        {
                            damierCible[i, j] = damierSource[i, j];
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Pas possible. ");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Jeu de la vie!");
            int hauteur = -1;
            int largeur = -1;
            do
            {
                Console.WriteLine("Saisir une hauteur de damier : ");
                hauteur = Convert.ToInt32(Console.ReadLine());
            } while (hauteur <= 0);
            do
            {
                Console.WriteLine("Saisir une largeur de damier : ");
                largeur = Convert.ToInt32(Console.ReadLine());
            } while (largeur <= 0);
            int[,] damier = InitDamier(hauteur, largeur);
           /* AfficheDamier(damier);
            Console.WriteLine("On va compter le nombre de cellule vivante voisines d'une cellule : ");
            Console.Write("Entrez sa ième ligne (à partir de 0) : ");
            int ligne = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nEntrez sa ième colonne (à partir de 0) : ");
            int colonne = Convert.ToInt32(Console.ReadLine());
            int nbVoisins = CalculVoisin(damier, colonne, ligne);
            Console.WriteLine("Il y a "+nbVoisins+" voisins à la cellule ligne "+ligne+" et colonne "+colonne+".");
            Console.WriteLine("Le damier de la génération suivante sera : ");
            AfficheDamier(nouveauDamier(damier));*/
            Console.WriteLine("Combien de cycles voulez-vous simuler? ");
            int nbCycles = Convert.ToInt32(Console.ReadLine());
            int[,] newDamier = null;
            for(int i=0; i<= nbCycles; i++)
            {
                Console.WriteLine("Génération " + i);
                AfficheDamier(damier);
                newDamier = nouveauDamier(damier);
                CopieDamier(newDamier, damier);
                Console.ReadLine();
            }
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
    }
}