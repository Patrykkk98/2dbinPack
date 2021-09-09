using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Reflection;



namespace binpacking
{
    public class GeneticAlgorithm
    {
        public static bool IS_Finished = false;

        public List<Generation> GenerationCollection;
        int PopulationSize;
        double MutationsRate;
        double CrossoverRate;
        int NumberOfGenerations;
        int NumOfIndividualToUpdate;
        Module MainRectangle;
        List<Module> Rectangles;
        double totalFitness = 0;
        DataGridView MainDataGridView;
        PictureBox PictureBoxDraw;
        
        Random rand = new Random(DateTime.Now.Millisecond);
        int binNumber = 0;
        public static Individual Individual_To_Draw;

        double Area_MainRectangle = Math.BigMul(MainForm.MainRectangle.Width, MainForm.MainRectangle.Height);

        SkyLineBinPack SkyLine;

        Label GenerationNumberLabel;
        Label bestFitnesslabel;
        Label TimeElapsedLabel;
        Label binLabel;

   
        Panel paintPanel;
        
        

        
        public GeneticAlgorithm(int populationSize, double mutationrate, double crossoverrate
            , int numberofgenerations, int numberofindividualtoupdate, Module mainrectangle
            , List<Module> rectangles,  PictureBox pictureBoxDraw,  Label generationnumberLabel, Panel PaintPanel, Label BestFitnesslabel
            ,Label timeElapsedlabel, Label binLabel, DataGridView mainDataGridView
            )
        {
            this.PopulationSize = populationSize; //default 100
            this.CrossoverRate = crossoverrate; //default 0.70
            this.MutationsRate = mutationrate; //default 0.70
            this.NumOfIndividualToUpdate = numberofindividualtoupdate; //default 70
            this.NumberOfGenerations = numberofgenerations; //default 50
            this.MainRectangle = mainrectangle;
            this.Rectangles = rectangles;
            
            this.PictureBoxDraw = pictureBoxDraw;

            this.MainDataGridView = mainDataGridView;
           
            this.binLabel = binLabel;
            this.bestFitnesslabel = BestFitnesslabel;
            

            this.GenerationNumberLabel = generationnumberLabel;

            this.paintPanel = PaintPanel;

            this.TimeElapsedLabel = timeElapsedlabel;

            GenerationCollection = new List<Generation>();

        }

       


        private Generation Initialization()
        {
            // Random Functions
            var rnd = new Random();

            Generation generation = new Generation();
                                                                            //intialise the generation and its memeber generation -> people -> chromsomes
            Individual individual = new Individual();
            individual.chromosome = new List<Module>();

            
            for (int i = 0; i < Rectangles.Count; i++)
            {
                Module new_Item = new Module();
                new_Item.Name = i.ToString();                           //intialising each rectangle
                new_Item.Width = Rectangles[i].Width;                   //setting box width, height and number
                new_Item.Height = Rectangles[i].Height;
                int red = rand.Next(0, byte.MaxValue + 1);
                int green = rand.Next(0, byte.MaxValue + 1);                    //random rectangle colour
                int blue = rand.Next(0, byte.MaxValue + 1);
                new_Item.brush_colour = new System.Drawing.SolidBrush(Color.FromArgb(red, green, blue));
                individual.chromosome.Add(new_Item);                    //add module to chromsome
            }

            List<Individual> NewPeople_Settings = new List<Individual>();
            Individual Randomindividual = (Individual)individual.Clone();
                    
            //apply these constraints if user picks to sort by highest/shortest/widest/large/small
            if (Convert.ToBoolean(ProgramSettings.Default.HeightModuleFirst))
            {
                List<Module> NewChromsome = new List<Module>();                                             
                for (int n = 0; n < individual.chromosome.Count; n++)
                {
                    Dictionary<int, Module> dic = FindHighestModule(Randomindividual);
                    Randomindividual.chromosome.RemoveAt(dic.Keys.ElementAt(0));

                    NewChromsome.Add(dic.Values.ElementAt(0));
                }

                Randomindividual.chromosome = NewChromsome;
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
            }

            if (Convert.ToBoolean(ProgramSettings.Default.ShortModuleFirst))
            {
                List<Module> NewChromsome = new List<Module>();                         
                for (int n = 0; n < individual.chromosome.Count; n++)
                {
                    Dictionary<int, Module> dic = FindShortModule(Randomindividual);
                    Randomindividual.chromosome.RemoveAt(dic.Keys.ElementAt(0));

                    NewChromsome.Add(dic.Values.ElementAt(0));
                }

                Randomindividual.chromosome = NewChromsome;
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
            }

            if (Convert.ToBoolean(ProgramSettings.Default.WideModuleFirst))
            {
                List<Module> NewChromsome = new List<Module>();
                for (int n = 0; n < individual.chromosome.Count; n++)               
                {
                    Dictionary<int, Module> dic = FindWideModule(Randomindividual);
                    Randomindividual.chromosome.RemoveAt(dic.Keys.ElementAt(0));

                    NewChromsome.Add(dic.Values.ElementAt(0));
                }

                Randomindividual.chromosome = NewChromsome;
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
            }

            if (Convert.ToBoolean(ProgramSettings.Default.LargeModuleFirst))
            {
                List<Module> NewChromsome = new List<Module>();             //new list that will be sorted in order of size
                for (int n = 0; n < individual.chromosome.Count; n++)   
                {
                    Dictionary<int, Module> dic = FindLargeModule(Randomindividual);        //find largest module add it to the new list and then remove from the old one
                    Randomindividual.chromosome.RemoveAt(dic.Keys.ElementAt(0));       //large first

                    NewChromsome.Add(dic.Values.ElementAt(0));
                }

                Randomindividual.chromosome = NewChromsome;
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
            }

            if (Convert.ToBoolean(ProgramSettings.Default.SmallModuleFirst))
            {
                List<Module> NewChromsome = new List<Module>();
                for (int n = 0; n < individual.chromosome.Count; n++)               //smallfirst
                {
                    Dictionary<int, Module> dic = FindSmallModule(Randomindividual);
                    Randomindividual.chromosome.RemoveAt(dic.Keys.ElementAt(0));

                    NewChromsome.Add(dic.Values.ElementAt(0));
                }

                Randomindividual.chromosome = NewChromsome;
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
                NewPeople_Settings.Add(Randomindividual);
            }

            List<Individual> NewPeople = new List<Individual>();

            for (int i = 0; i < NewPeople_Settings.Count; i++)
            {
                NewPeople.Add(NewPeople_Settings[i]);  //apply the settings to the generation
            }

            for (int n = 0; n < PopulationSize - NewPeople_Settings.Count; n++)//create a list of 100 individuals of random order: there are 100 people per generation
            {
                Randomindividual = new Individual();
                Randomindividual.chromosome = individual.chromosome.OrderBy(x => rnd.Next()).ToList();
                NewPeople.Add(Randomindividual);
            }

            generation.People = NewPeople;
            return generation;
        }


        private double CalcFitness(Individual person)
        {
            SkyLine = new SkyLineBinPack(MainForm.MainRectangle.Width); //initialte sky line with the container width

            SkyLine.ApplySkyLine(person.chromosome);//run bin packing method on the particular set of rectangles
            
            int Out_Module_Area = 0; //this variable calculates the efficiency of the bin-packing the more out of container pixels the less efficient
            for (int i = 0; i < person.chromosome.Count; i++)
            {
                if (person.chromosome[i].Y + person.chromosome[i].Height > MainForm.MainRectangle.Height && person.chromosome[i].Y <= MainForm.MainRectangle.Height) //if the rectangle starts in the container but part of it is outside
                {
                    Out_Module_Area += ((person.chromosome[i].Y + person.chromosome[i].Height) - MainForm.MainRectangle.Height) * person.chromosome[i].Width;  //add portion of the rectangle to out of module variable
                }
                else if (person.chromosome[i].Y > MainForm.MainRectangle.Height) //if the rectangles position is completely outside of the container
                {
                    Out_Module_Area += person.chromosome[i].Height * person.chromosome[i].Width; //add area to the out of module variable
                }
            }
            ////IF OUT OF MUDULE AREA TOO BIG MOVE TO THE OTHER CONTAINER 
            double _fitness = Out_Module_Area;
            return _fitness;
        }






        private void SortByFitness(Generation generation)
        {       //sort generation by fitness compare each individual to each other by their fitness value
            generation.People.Sort(delegate (Individual c1, Individual c2) { return c1.fitness.CompareTo(c2.fitness); }); 
        }                                                                                                                   
        private void Evaluate_Population(Generation generation)
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                generation.People[i].fitness = CalcFitness(generation.People[i]); //assign fitness value to the set the *the lower the better
            }
            SortByFitness(generation);//sort by fitness *the lower the better
        }

        private Individual CrossOver(Individual Father, Individual Mother)
        {
            Individual copy_Father = (Individual)Father.Clone(); //clonea reference of these individuals
            Individual copy_Mother = (Individual)Mother.Clone();

            if (rand.NextDouble() < 0.5)
            {
                int index = rand.Next(0, Rectangles.Count - 1); 

                copy_Father.chromosome.RemoveRange(index, copy_Father.chromosome.Count - 1 - index); //remove all the rectangles past the index number

                bool found = false;
                for (int i = 0; i < copy_Mother.chromosome.Count; i++)
                {
                    found = false;
                    for (int j = 0; j < copy_Father.chromosome.Count; j++)
                    {
                        if (copy_Mother.chromosome[i].Name == copy_Father.chromosome[j].Name)//if module already exists in the list skip over to next item
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        copy_Father.chromosome.Insert(index, copy_Mother.chromosome[i]);//if module does not exist in this chromosome insert it
                    }
                }
            }
            else
            {
                int index = rand.Next(0, Rectangles.Count - 1); 

                copy_Father.chromosome.RemoveRange(0, index); //remove a random amount of modules from the start of the list

                bool found = false;
                for (int i = 0; i < copy_Mother.chromosome.Count; i++)
                {
                    found = false;
                    for (int j = 0; j < copy_Father.chromosome.Count; j++)
                    {
                        if (copy_Mother.chromosome[i].Name == copy_Father.chromosome[j].Name) //if module already exists in the list skip over to next
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        copy_Father.chromosome.Insert(0, copy_Mother.chromosome[i]);        //if module does not exist in this chromosome insert it
                    }
                }
            }

            return copy_Father;
        }

   

        private Individual Mutation(Individual individual)
        {
            Individual copy_Individual = (Individual)individual.Clone();
            int probability = rand.Next(1, 3);
            //mutation 1 : randomly exchange two items 0,1,2....n
            if (probability == 1)
            {
                int index = rand.Next(0, Rectangles.Count - 1);
                int index2 = rand.Next(0, Rectangles.Count - 1);
                while (index == index2)
                {
                    index = rand.Next(0, Rectangles.Count - 1);
                }

                //Swap between item in index and index2
                Module tempOperand = copy_Individual.chromosome.ElementAt(index);
                copy_Individual.chromosome[index] = copy_Individual.chromosome.ElementAt(index2);
                copy_Individual.chromosome[index2] = tempOperand;
            }
            //mutation 2 : move an item to another position
            else
            {
                int index = rand.Next(0, Rectangles.Count - 1);
                int index2 = rand.Next(0, Rectangles.Count - 1);
                while (index == index2)
                {
                    index = rand.Next(0, Rectangles.Count - 1);
                }
                Module temp = copy_Individual.chromosome.ElementAt(index);
                copy_Individual.chromosome.RemoveAt(index);
                copy_Individual.chromosome.Insert(index2, temp);
            }

            return copy_Individual;
        }


        public void Draw_Packing(Individual Best_Individual)//this method draws the skyline in a picture box using Graphics
        {
            
            Individual_To_Draw = Best_Individual;
            //////////////////
            int binNumber = 0;
            //////////////////
            double Max_YY = 0; // find out max height by adding all y of all modules
            for (int i = 0; i < Best_Individual.chromosome.Count; i++)
            {
                if (Best_Individual.chromosome[i].Y + Best_Individual.chromosome[i].Height > Max_YY)
                {
                    Max_YY = Best_Individual.chromosome[i].Y + Best_Individual.chromosome[i].Height;
                }
            }

            double Max_XX = MainRectangle.Width; //starting x axis +item width cannot be outside of container width  
                                            //if container width = 400, items starting x axis + width cannot go beyond that

            double num = 0;
            double num2 = 0;

            //if (Convert.ToBoolean(ProgramSettings.Default.ShowAllRectangleWhileDrawing))
                num = double.Parse(((double)paintPanel.Height / Max_YY).ToString()) - 0.05;
            //else
            //    num = double.Parse(((double)paintPanel.Height / MainForm.MainRectangle.Height).ToString()) - 0.05;

            //if (Convert.ToBoolean(ProgramSettings.Default.ShowAllRectangleWhileDrawing))
                num2 = double.Parse(((double)paintPanel.Width / Max_XX).ToString()) - 0.05;
            //else
            //    num2 = double.Parse(((double)paintPanel.Width / MainForm.MainRectangle.Width).ToString()) - 0.05;

            if (num > num2)
            {
                num = num2;
            }
            else
            {
                num2 = num;
            }

            double Max_Y = 0;
            for (int i = 0; i < Best_Individual.chromosome.Count; i++)
            {
                if (Best_Individual.chromosome[i].Y * num + Best_Individual.chromosome[i].Height * num > Max_Y)
                {
                    Max_Y = Best_Individual.chromosome[i].Y * num + Best_Individual.chromosome[i].Height * num;
                }
            }
            //MainRectangle.Width = 400;
            //MainRectangle.Height = 400;
            double floorPlanWidth = MainForm.MainRectangle.Width * num2 + 5;
            double floorPlanHeight = Max_Y;
            //double floorPlanWidth = 1500;
            //double floorPlanHeight = 1000;
            /////////////////////////////////////////////////////////////////////////////////////

            
            /////////////////////////////////////////////////////////////////////////////////////
            PictureBoxDraw.Width = Convert.ToInt32(floorPlanWidth);
              
            if (floorPlanHeight > MainRectangle.Height)
            {
                
                PictureBoxDraw.Height = Convert.ToInt32(floorPlanHeight) + 5;
               
            }
            else
            {
                PictureBoxDraw.Height = MainRectangle.Height + 5;
                
            }

            Bitmap bmp = new Bitmap(PictureBoxDraw.Width, PictureBoxDraw.Height);
          

            Brush brush = Brushes.Black;
            Graphics g = Graphics.FromImage(bmp);
            
           
            g.Clear(Color.White);
           

            bool color_or_not = Convert.ToBoolean(ProgramSettings.Default.ColorOrNot);
            

            for (int i = 0; i < Best_Individual.chromosome.Count; i++)
            {
                if (color_or_not)
                {
                    //Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.
                    //fill rectangle with colour, and x,y ,width and height
                    g.FillRectangle((Best_Individual.chromosome[i].brush_colour), (float)(Best_Individual.chromosome[i].X * num2), (float)(Best_Individual.chromosome[i].Y * num), (float)(Best_Individual.chromosome[i].Width * num2), (float)(Best_Individual.chromosome[i].Height * num));

                }
                else
                {
                    //Draws a rectangle specified by a Rectangle structure.
                    //A Pen that determines the color, width, and style of the rectangle
                    //Draws a rectangle specified by a coordinate pair, a width, and a height.
                    g.DrawRectangle(new Pen(Color.Green, 2), (float)(Best_Individual.chromosome[i].X * num2), (float)(Best_Individual.chromosome[i].Y * num), (float)(Best_Individual.chromosome[i].Width * num2), (float)(Best_Individual.chromosome[i].Height * num));
                }

                if (!Convert.ToBoolean(ProgramSettings.Default.HideModuleNames))
                    g.DrawString(Best_Individual.chromosome[i].Name, new Font(FontFamily.GenericSerif, 15), brush, new PointF((float)(Best_Individual.chromosome[i].X * num2), (float)(Best_Individual.chromosome[i].Y * num)));
            }

            Rectangle mainRect = new Rectangle((int)(MainRectangle.X * num2), (int)(MainRectangle.Y * num), (int)(MainRectangle.Width * num2), (int)(MainRectangle.Height * num));
            g.DrawRectangle(new Pen(Color.Yellow, 2), mainRect);
            
          


            PictureBoxDraw.Image = bmp;
         
        }

        public int RouletteWheelSelection(double total_Fitness, Generation generation)
        {
            double rnd;
            double tmp;
            int idx = 0;
            rnd = rand.NextDouble() * totalFitness;
            for (idx = 0; idx < PopulationSize && rnd > 0; idx++)                   
            {               
                tmp = generation.People.ElementAt(idx).fitness;             
                rnd -= generation.People.ElementAt(idx).fitness;            //elements with higher fitness value will be selected and their index passed on 
            }                                                               
            return idx - 1;
        }

       

        public void Genetic_Algorithm()
        {
            Individual Random_individual;

            // Parent Generation
            Generation generation = Initialization(); //create a generation full of people, with random chromosome assortment
            Evaluate_Population(generation);         //evaluate the population of people by assigning their fitness and cost variables
            GenerationCollection.Add(generation);    //add to the list of generations

            // Childs Generations     
            int Individual_1 = 0;
            int Individual_2 = 0;
            Individual New_Individual;
            Generation NewGeneration;

            int NO_Generation = 0;
            int Draw_Each_Num_Generation = Convert.ToInt32(ProgramSettings.Default.DrawEachNumGeneration);

            int Free_in_Generation = 0;
            if (MainForm.Rectangles.Count < 50)
                Free_in_Generation = 1000;
            else if (MainForm.Rectangles.Count < 100)
                Free_in_Generation = 500;
            else if (MainForm.Rectangles.Count < 200)           
                Free_in_Generation = 300;
            else if (MainForm.Rectangles.Count < 400)
                Free_in_Generation = 100;
            else if (MainForm.Rectangles.Count < 1000)
                Free_in_Generation = 75;
            else
                Free_in_Generation = 30;

            while (true)
            {
                
                IS_Finished = false;
                
                NO_Generation++;
                int GenFree = 0;
                if (NO_Generation % Free_in_Generation == 0)
                {
                    for (int i = GenFree; i < GenerationCollection.Count - 2; i++)
                    {
                        for (int j = 1; j < GenerationCollection[i].People.Count; j++)
                        {
                            for (int k = 0; k < GenerationCollection[i].People[j].chromosome.Count; k++)
                            {
                                GenerationCollection[i].People[j].chromosome[k] = null;   //deleting all chromosomes and individuals 
                                GenerationCollection[i].People[j].chromosome.RemoveAt(k); 
                                k = 0;
                            }
                            GenerationCollection[i].People[j] = null;
                            GenerationCollection[i].People.RemoveAt(j);
                            j = 1;
                        }
                    }
                    GenFree += 1000;
                    GC.Collect(); //Forces an immediate garbage collection of all generations.
                }
                NewGeneration = (Generation)generation.Clone();
                double tempcost = 0;
                string msg = "";
                for (int i = 0; i < generation.People.Count; i++)
                {
                    tempcost += (double)(1 / generation.People[i].fitness);
                    generation.People[i].cost = tempcost;
                    msg += i.ToString() + generation.People[i].cost.ToString() + "  " + (1 / generation.People[i].fitness).ToString() + Environment.NewLine;
                }

                totalFitness = 0; 
                for (int i = 0; i < generation.People.Count; i++)
                {
                    totalFitness += generation.People[i].fitness;               //calculate the overall fitness of a generation
                }

                for (int i = 0; i < NumOfIndividualToUpdate; i++)
                {
                    Individual_1 = RouletteWheelSelection(totalFitness, generation);//pick 2 parent individuals
                    Individual_2 = RouletteWheelSelection(totalFitness, generation);
                    ///////???????
                    if(Individual_1 == -1 && Individual_2 == -1) { Individual_1 = 1; Individual_2 = 1; } //error correction
                    //////////
                    if (rand.NextDouble() < CrossoverRate)//higher the crossover rate the likely this method will run 0 - 0.9
                        //create child individual using crossover of two parent individuals
                        New_Individual = CrossOver(GenerationCollection[GenerationCollection.Count - 1].People.ElementAt(Individual_1), GenerationCollection[GenerationCollection.Count - 1].People.ElementAt(Individual_2));     
                    else
                    {
                        New_Individual = GenerationCollection[GenerationCollection.Count - 1].People.ElementAt(0);
                        Random_individual = new Individual();
                        Random_individual.chromosome = New_Individual.chromosome.OrderBy(x => rand.Next()).ToList();
                        Random_individual.fitness = New_Individual.fitness;
                        New_Individual = (Individual)Random_individual.Clone();
                        Random_individual = null;
                    }
                    //mutate the child individual to maintain population diversity 
                    if (rand.NextDouble() < MutationsRate)//mutation rate
                        New_Individual = Mutation(New_Individual);

                    NewGeneration.People.ElementAt(generation.People.Count - 1 - i).chromosome = null;

                    NewGeneration.People.RemoveAt(generation.People.Count - 1 - i);
                    NewGeneration.People.Insert(generation.People.Count - 1 - i, New_Individual);
                    New_Individual.fitness = CalcFitness(New_Individual);
                    //create a new child population, evaluate and sort by fitness

                }
                SortByFitness(NewGeneration);

                GenerationCollection.Add(NewGeneration);

                generation = NewGeneration;

                MethodInvoker action = delegate
                {
                    GenerationNumberLabel.Text = NO_Generation.ToString();
                    bestFitnesslabel.Text = (Math.Round((GenerationCollection[GenerationCollection.Count - 1].People[0].fitness + Area_MainRectangle) / Area_MainRectangle, 3)).ToString() + " %" + "  -  " + GenerationCollection[GenerationCollection.Count - 1].People[0].fitness;
                };
                GenerationNumberLabel.BeginInvoke(action);

                Draw_Each_Num_Generation = Convert.ToInt32(ProgramSettings.Default.DrawEachNumGeneration);
                if (NO_Generation % Draw_Each_Num_Generation == 0)
                {
                    MethodInvoker action2 = delegate
                    {
                        Draw_Packing(NewGeneration.People[0]);
                    };
                    PictureBoxDraw.BeginInvoke(action2);
                }

                if (NewGeneration.People[0].fitness <= 0)  //end the whole process as an ideal solution has been found and break the while loop 
                {
                    break;
                }
            }

            IS_Finished = true;

            MethodInvoker action3 = delegate
            {
                GenerationNumberLabel.Text ="Generation Number: " + NO_Generation.ToString();
                binLabel.Text = "Container Height(ft): " +(MainRectangle.Height / 25).ToString() + " Width(ft): " + (MainRectangle.Width/25).ToString();

                //////binLabel.Text ="Name" + GenerationCollection[GenerationCollection.Count - 1].People[0].chromosome[9].Name.ToString() + "y:" + GenerationCollection[GenerationCollection.Count - 1].People[0].chromosome[9].Y.ToString() 
                //////+ " x " + GenerationCollection[GenerationCollection.Count - 1].People[0].chromosome[9].X.ToString() + " height " + GenerationCollection[GenerationCollection.Count - 1].People[0].chromosome[9].Height.ToString()
                //////+ "width " + GenerationCollection[GenerationCollection.Count - 1].People[0].chromosome[9].Width.ToString();
                

                MainForm.stopWatch.Stop();

                TimeSpan ts = MainForm.stopWatch.Elapsed;
                TimeElapsedLabel.Text = "Time elapsed: " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                         ts.Hours, ts.Minutes, ts.Seconds,
                                         ts.Milliseconds / 10);

                //update statistics on GUI with threading
                Draw_Packing(GenerationCollection[GenerationCollection.Count - 1].People[0]);
            };
            binLabel.BeginInvoke(action3);
            GenerationNumberLabel.BeginInvoke(action3);
            MainForm.oThread.Abort();
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private Dictionary<int, Module> FindHighestModule(Individual Person)
        {
            int height = 0;
            Module m = new Module();
            int index = 0;
            Dictionary<int, Module> dic = new Dictionary<int, Module>();
            for (int i = 0; i < Person.chromosome.Count; i++)
            {
                if (Person.chromosome[i].Height > height)
                {
                    height = Person.chromosome[i].Height;
                    m = Person.chromosome[i];
                    index = i;
                }
            }
            dic.Add(index, m);
            return dic;
        }

        private Dictionary<int, Module> FindShortModule(Individual Person)
        {
            int height = int.MaxValue;
            Module m = new Module();
            int index = 0;
            Dictionary<int, Module> dic = new Dictionary<int, Module>();
            for (int i = 0; i < Person.chromosome.Count; i++)
            {
                if (Person.chromosome[i].Height < height)
                {
                    height = Person.chromosome[i].Height;
                    m = Person.chromosome[i];
                    index = i;
                }
            }
            dic.Add(index, m);
            return dic;
        }

        private Dictionary<int, Module> FindLargeModule(Individual Person)
        {
            int area = 0;
            Module m = new Module();
            int index = 0;
            Dictionary<int, Module> dic = new Dictionary<int, Module>();
            for (int i = 0; i < Person.chromosome.Count; i++)
            {
                if (Person.chromosome[i].Height > area)             //when new largest item is found 
                {
                    area = Person.chromosome[i].Height * Person.chromosome[i].Width;   //save it's area, index and item itself
                    m = Person.chromosome[i];
                    index = i;
                }
            }
            dic.Add(index, m);
            return dic;
        }

        private Dictionary<int, Module> FindSmallModule(Individual Person)
        {
            int area = int.MaxValue;
            Module m = new Module();
            int index = 0;
            Dictionary<int, Module> dic = new Dictionary<int, Module>();
            for (int i = 0; i < Person.chromosome.Count; i++)
            {
                if (Person.chromosome[i].Height < area)
                {
                    area = Person.chromosome[i].Height * Person.chromosome[i].Width;
                    m = Person.chromosome[i];
                    index = i;
                }
            }
            dic.Add(index, m);
            return dic;
        }

        private Dictionary<int, Module> FindWideModule(Individual Person)
        {
            int width = 0;
            Module m = new Module();
            int index = 0;
            Dictionary<int, Module> dic = new Dictionary<int, Module>();
            for (int i = 0; i < Person.chromosome.Count; i++)
            {
                if (Person.chromosome[i].Width > width)
                {
                    width = Person.chromosome[i].Width;
                    m = Person.chromosome[i];
                    index = i;
                }
            }
            dic.Add(index, m);
            return dic;
        }




        public void FillDataGridView(List<Generation> Generation_Collection)
        {
            int row = 0;
            for (int n = 0; n < Generation_Collection.Count; n++)
            {
                int numb = Generation_Collection[n].People[0].chromosome.Count;
                string s = "";
                for (int i = 0; i < numb; i++)
                {
                    s += Generation_Collection[n].People[0].chromosome[i].Name;
                    if (i < numb - 1)
                    {
                        s += " , ";
                    }
                }
                MainDataGridView.Rows.Add();
                MainDataGridView.Rows[row].Cells["GenerationNumber"].Value = n.ToString();
                MainDataGridView.Rows[row].Cells["ChromosomeColumn"].Value = s;
                MainDataGridView.Rows[row].Cells["FitnessColumn"].Value = (Math.Round((Generation_Collection[n].People[0].fitness + Area_MainRectangle) / Area_MainRectangle, 3)) + " %" + "   -   " + Generation_Collection[n].People[0].fitness;
                row++;
            }
        }

        public void Draw_Packing_ForGridView(Individual Best_Individual) //drawing bin packing for the selected chromsome from the grid view
        {
            Individual_To_Draw = Best_Individual;

            // SkyLine = new SkyLineBinPack(MainForm.MainRectangle.Width, Convert.ToBoolean(ProgramSettings.Default.SkyLineWasteSpace));
            SkyLine = new SkyLineBinPack(MainForm.MainRectangle.Width);
             SkyLine.ApplySkyLine(Best_Individual.chromosome);


            double Max_YY = 0;
            for (int i = 0; i < Best_Individual.chromosome.Count; i++)
            {
                if (Best_Individual.chromosome[i].Y + Best_Individual.chromosome[i].Height > Max_YY)
                {
                    Max_YY = Best_Individual.chromosome[i].Y + Best_Individual.chromosome[i].Height;
                }
            }


            double Max_XX = MainRectangle.Width;

            double num = 0;
            double num2 = 0;


            num = double.Parse(((double)paintPanel.Height / Max_YY).ToString()) - 0.05;

            num2 = double.Parse(((double)paintPanel.Width / Max_XX).ToString()) - 0.05;

            if (num > num2)
            {
                num = num2;
            }
            else
            {
                num2 = num;
            }

            double Max_Y = 0;
            for (int i = 0; i < Best_Individual.chromosome.Count; i++)
            {
                if (Best_Individual.chromosome[i].Y * num + Best_Individual.chromosome[i].Height * num > Max_Y)
                {
                    Max_Y = Best_Individual.chromosome[i].Y * num + Best_Individual.chromosome[i].Height * num;
                }
            }

            double floorPlanWidth = MainForm.MainRectangle.Width * num2 + 5;
            double floorPlanHeight = Max_Y;

            PictureBoxDraw.Width = Convert.ToInt32(floorPlanWidth);

            if (floorPlanHeight > MainRectangle.Height)
            {
                PictureBoxDraw.Height = Convert.ToInt32(floorPlanHeight) + 5;
            }
            else
            {
                PictureBoxDraw.Height = MainRectangle.Height + 5;
            }

            Bitmap bmp = new Bitmap(PictureBoxDraw.Width, PictureBoxDraw.Height);
            Brush brush = Brushes.Black;
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            bool color_or_not = Convert.ToBoolean(ProgramSettings.Default.ColorOrNot);
            for (int i = 0; i < Best_Individual.chromosome.Count; i++)
            {
                if (color_or_not)
                {
                    g.FillRectangle((Best_Individual.chromosome[i].brush_colour), (float)(Best_Individual.chromosome[i].X * num2), (float)(Best_Individual.chromosome[i].Y * num), (float)(Best_Individual.chromosome[i].Width * num2), (float)(Best_Individual.chromosome[i].Height * num));
                }
                else
                {
                    g.DrawRectangle(new Pen(Color.Green, 2), (float)(Best_Individual.chromosome[i].X * num2), (float)(Best_Individual.chromosome[i].Y * num), (float)(Best_Individual.chromosome[i].Width * num2), (float)(Best_Individual.chromosome[i].Height * num));
                }

                if (!Convert.ToBoolean(ProgramSettings.Default.HideModuleNames))
                    g.DrawString(Best_Individual.chromosome[i].Name, new Font(FontFamily.GenericSerif, 15), brush, new PointF((float)(Best_Individual.chromosome[i].X * num2), (float)(Best_Individual.chromosome[i].Y * num)));
            }
            Rectangle mainRect = new Rectangle((int)(MainRectangle.X * num2), (int)(MainRectangle.Y * num), (int)(MainRectangle.Width * num2), (int)(MainRectangle.Height * num));
            g.DrawRectangle(new Pen(Color.Yellow, 2), mainRect);

            PictureBoxDraw.Image = bmp;
        }
    }
}
