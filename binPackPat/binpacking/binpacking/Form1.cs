using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Linq.Expressions;
using System.Reflection;
using System.IO;

namespace binpacking
{
    public partial class MainForm : Form
    {
        int PopulationSize = Convert.ToInt32(ProgramSettings.Default.PopulationSize);
        double MutationsRate = Convert.ToDouble(ProgramSettings.Default.MutationsRate);
        double CrossoverRate = Convert.ToDouble(ProgramSettings.Default.CrossoverRate);
        int NumberOfGenerations = Convert.ToInt32(ProgramSettings.Default.NumberOfGenerations);
        int NumOfIndividualToUpdate = (Convert.ToInt32(ProgramSettings.Default.PercentOfUpdate) * Convert.ToInt32(ProgramSettings.Default.PopulationSize)) / 100;
        //               70 =          (70 * 100)/100                                
        public static List<Module> Rectangles = new List<Module>();
        public static Module MainRectangle;
        //public static List<Module> MainRectangles = new List<Module>();

        public static System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        

        GeneticAlgorithm GA;
        List<Generation> GenerationCollection;

        public static Thread oThread;
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            


            int Width = -1;
            int Height = -1;
            List<Module> OpenXMLRectangles = new List<Module>();
            XMLopenFileDialog.Filter = "XML Documents (*.xml)|*xml";
            if (XMLopenFileDialog.ShowDialog() == DialogResult.OK && XMLopenFileDialog.FileName != "")
            {
                XmlTextReader textReader = new XmlTextReader(XMLopenFileDialog.FileName);
                OpenXMLRectangles = new List<Module>();
                while (textReader.Read())
                {
                    XmlNodeType nType = textReader.NodeType;
                    if (nType == XmlNodeType.Element)
                    {
                        if (textReader.Name == "Width" || textReader.Name == "Height")
                        {
                            if (textReader.Name == "Width")
                            {
                                textReader.Read();
                                Width = Convert.ToInt32(textReader.Value);
                            }
                            if (textReader.Name == "Height")
                            {
                                textReader.Read();
                                Height = Convert.ToInt32(textReader.Value);
                            }
                            if (Width != -1 && Height != -1)
                            {
                                Module newItem = new Module();
                                newItem.Width = Width;
                                newItem.Height = Height;
                                OpenXMLRectangles.Add(newItem);
                                Width = -1; Height = -1;
                            }
                        }
                    }
                }
                MainRectangle = new Module();
                MainRectangle.Width = OpenXMLRectangles[0].Width;
                MainRectangle.Height = OpenXMLRectangles[0].Height;
                Rectangles = OpenXMLRectangles.ToList<Module>();
                //////////////////////
                //MainRectangles.Add(MainRectangle);
                //MainRectangles.Add(MainRectangle);
                ///////////////////////
                Rectangles.RemoveAt(0);
               
                
                
                
                PopulationSize = Convert.ToInt32(ProgramSettings.Default.PopulationSize);
                NumberOfGenerations = Convert.ToInt32(ProgramSettings.Default.NumberOfGenerations); //50 by default
                NumOfIndividualToUpdate = (Convert.ToInt32(ProgramSettings.Default.PercentOfUpdate) * Convert.ToInt32(ProgramSettings.Default.PopulationSize)) / 100;
                MainDataGridView.Rows.Clear();
                //chartImage.Series[0].Points.Clear();

                GA = new GeneticAlgorithm(PopulationSize, MutationsRate, CrossoverRate,
                    NumberOfGenerations, NumOfIndividualToUpdate, MainRectangle, Rectangles,  pictureBoxDraw,
                    NumberGenerationLabel, panelPaint, BestFitnesslabel, TimeElapsedlabel, lblBin, MainDataGridView);

                GenerationCollection = GA.GenerationCollection;
                oThread = new Thread(new ThreadStart(GA.Genetic_Algorithm));

            }

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (Rectangles.Count > 0)
            {
                stopWatch.Start();
                if (oThread.ThreadState == ThreadState.Unstarted)
                {
                    oThread.Start();
                }

            }
        }


       

        private void btnBestSolution_Click(object sender, EventArgs e)
        {
            if (Rectangles.Count > 0)
            {
                if (GenerationCollection != null)
                {
                    GA.FillDataGridView(GenerationCollection);
                }
                else
                {
                    MessageBox.Show(" You should run Genetic Aglorithm first ");
                }
            }
            else
            {
                MessageBox.Show(" you should choose XML file first ");
            }
        }

        private void MainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedCell = MainDataGridView.Rows[e.RowIndex].Cells["ChromosomeColumn"].Value;
            if (selectedCell == null)
                return;

            string[] All_items = selectedCell.ToString().Split(',');

            Individual individual = new Individual();
            individual.chromosome = new List<Module>();

            Random rand2 = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < All_items.Length; i++)
            {
                Module new_Item = new Module();

                new_Item.Name = All_items[i].Trim();
                new_Item.Width = Rectangles[Convert.ToInt32(All_items[i])].Width;
                new_Item.Height = Rectangles[Convert.ToInt32(All_items[i])].Height;
                int red = rand2.Next(0, byte.MaxValue + 1);
                int green = rand2.Next(0, byte.MaxValue + 1);
                int blue = rand2.Next(0, byte.MaxValue + 1);
                new_Item.brush_colour = new System.Drawing.SolidBrush(Color.FromArgb(red, green, blue));
                individual.chromosome.Add(new_Item);

            }
            GA.Draw_Packing_ForGridView(individual);
        }

        private void btnDataSet_Click(object sender, EventArgs e)
        {
            AddDataSetForm frm = new AddDataSetForm();
            frm.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
        //    stopWatch.Reset();
        //    TimeElapsedlabel.Text = "Time";

        //    if (GenerationCollection != null)
        //    {
        //        for (int i = 0; i < GenerationCollection.Count; i++)
        //        {
        //            for (int j = 0; j < GenerationCollection[i].People.Count; j++)
        //            {
        //                for (int k = 0; k < GenerationCollection[i].People[j].chromosome.Count; k++)
        //                {
        //                    GenerationCollection[i].People[j].chromosome[k] = null;
        //                    GenerationCollection[i].People[j].chromosome.RemoveAt(k);
        //                    k = 0;
        //                }
        //                GenerationCollection[i].People[j] = null;
        //                GenerationCollection[i].People.RemoveAt(j);
        //                j = 1;
        //            }
        //        }
        //        GC.Collect();
        //    }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettingsForm ProgramSettingForm = new SettingsForm(PopulationSize,CrossoverRate,MutationsRate ,NumberOfGenerations, NumOfIndividualToUpdate
                                                       , MainDataGridView);
            ProgramSettingForm.ShowDialog();
        }
    }
}
