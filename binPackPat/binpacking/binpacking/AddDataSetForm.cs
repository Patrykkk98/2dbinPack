using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;

namespace binpacking
{
    public partial class AddDataSetForm : Form
    {
        public AddDataSetForm()
        {
            InitializeComponent();
        }
        public static List<Module> newItems = new List<Module>();
        public int ItemArea = 0;

        private void createNode(string width, string height, string name, XmlTextWriter writer)
        {
            writer.WriteStartElement("Rectangle");
            writer.WriteStartElement("Width");
            writer.WriteString(width);
            writer.WriteEndElement();
            writer.WriteStartElement("Height");
            writer.WriteString(height);
            writer.WriteEndElement();
            writer.WriteStartElement("Name");
            writer.WriteString(name);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        private void createBin(string width, string height, XmlTextWriter writer)
        {
            writer.WriteStartElement("MainRectangle");
            writer.WriteStartElement("Width");
            writer.WriteString(width);
            writer.WriteEndElement();
            writer.WriteStartElement("Height");
            writer.WriteString(height);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        private void AddDataSetForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            XmlTextWriter writer = new XmlTextWriter(txtFilename.Text+".xml", System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("Puzzle");
            createBin(newItems[0].Width.ToString(), newItems[0].Height.ToString(),  writer);
            for(int i=1; i < newItems.Count(); i++)
            {
                createNode(newItems[i].Width.ToString(), newItems[i].Height.ToString(), i.ToString(), writer);
            }
            

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            MessageBox.Show("XML File created ! ");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            label3.Text = "Enter item dimensions";
            Module newItem = new Module();
            newItem.Width =  int.Parse(txtWidth.Text) * 25;
            newItem.Height = int.Parse(txtHeight.Text) * 25;
            if ( newItems.Count == 0 || ItemArea + (newItem.Height * newItem.Width) <= newItems[0].Width * newItems[0].Height )
            {
                newItems.Add(newItem);

                label4.Text = "Items Added: " + newItems.Count;
                label5.Text = "Container area: " + newItems[0].Width * newItems[0].Height / 25;
                if (newItems.Count > 1)
                {
                    ItemArea += newItem.Height * newItem.Width;
                }
                label6.Text = "Total items area: " + ItemArea /25;
                label7.Text = "Available space: " + (((newItems[0].Width * newItems[0].Height) - ItemArea)/25).ToString() ;
            }
            else { MessageBox.Show("Too many items, this package will not fit"); }




        }

        private void txtFilename_TextChanged(object sender, EventArgs e)
        {
            if(txtFilename.Text != "")
            {
                btnCreate.Enabled = true;
            }
            else { btnCreate.Enabled = false; }
        }
    }
}
