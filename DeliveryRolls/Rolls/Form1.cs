using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClassLibrary3;

namespace DeliveryRolls
{

    public partial class Form1 : Form
    {
        new int Click = 0;
        List<Person> Human = new List<Person>();
        List<Treatment> HumanTreat = new List<Treatment>();

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "XML files(*.xml)|*.xml";
            saveFileDialog1.Filter = "XML files(*.xml)|*.xml";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            Click++;
            Human.Add(new Person());
            Draw(Click - 1);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delete(1);
        }

        private void Draw(int element)
        {
            tableLayoutPanel1.RowCount++;
            for (int column = 0; column < 7; column++)
            {
                tableLayoutPanel1.Controls.Add(PersonData(element, column), column, tableLayoutPanel1.RowCount - 2);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                tableLayoutPanel1.RowStyles[1 + element].Height = 30;
                PersonData(element, column).Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left);
                PersonData(element, column).Font = new System.Drawing.Font("Times New Roman", this.Height / 30);
            }
            Human.ElementAt(element).Label.Text = (tableLayoutPanel1.RowCount - 2).ToString();
            Human.ElementAt(element).Price.Maximum = 400;
        }

        private void Delete(int Row)
        {
            this.tableLayoutPanel1.SuspendLayout();
            for (int i = 0; i < Row; i++)
            {
                if (tableLayoutPanel1.RowCount > 2)
                {
                    tableLayoutPanel1.RowCount--;
                    for (int column = 0; column < 7; column++)
                        tableLayoutPanel1.Controls.Remove(PersonData(Human.Count - 1, column));
                    Human.RemoveAt(Human.Count - 1);
                    Click--;
                }
            }
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
        }

        private void OpenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Delete(tableLayoutPanel1.RowCount - 2);
            Xml xmlDeSerializer = new Xml();
            HumanTreat = xmlDeSerializer.DeSerializer(openFileDialog1.FileName, HumanTreat);
            DataTransferForm();
        }

        private void DataTransferForm()
        {
            for (int i = 0; i < HumanTreat.Count; i++)
            {
                Human.Add(new Person());
                Human.ElementAt(i).Label.Text = HumanTreat.ElementAt(i).Label;
                Human.ElementAt(i).FIO.Text = HumanTreat.ElementAt(i).FIO;
                Human.ElementAt(i).Address.Text = HumanTreat.ElementAt(i).Address;
                Human.ElementAt(i).Rolls.Text = HumanTreat.ElementAt(i).Rolls;
                Human.ElementAt(i).Kol.Text = HumanTreat.ElementAt(i).Kol;
                Human.ElementAt(i).Dop.Text = HumanTreat.ElementAt(i).Dop;
                Human.ElementAt(i).Price.Value = HumanTreat.ElementAt(i).Price;
                Draw(i);
                Click++;
            }
            HumanTreat.Clear();
        }

        private void SaveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveData();
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Xml xmlSerializer = new Xml();
            xmlSerializer.Serializer(saveFileDialog1.FileName, HumanTreat);
            HumanTreat.Clear();
        }

        private void SaveData()
        {
            for (int i = 0; i < Human.Count; i++)
            {
                HumanTreat.Add(new Treatment());
                HumanTreat.ElementAt(i).Label = Human.ElementAt(i).Label.Text;
                HumanTreat.ElementAt(i).FIO = Human.ElementAt(i).FIO.Text;
                HumanTreat.ElementAt(i).Address = Human.ElementAt(i).Address.Text;
                HumanTreat.ElementAt(i).Rolls = Human.ElementAt(i).Rolls.Text;
                HumanTreat.ElementAt(i).Kol = Human.ElementAt(i).Kol.Text;
                HumanTreat.ElementAt(i).Dop = Human.ElementAt(i).Dop.Text;
                HumanTreat.ElementAt(i).Price = Convert.ToInt32(Human.ElementAt(i).Price.Value);
            }
        }

        private Control PersonData(int i, int column)
        {
            switch (column)
            {
                case 0:
                    return Human.ElementAt(i).Label;
                case 1:
                    return Human.ElementAt(i).FIO;
                case 2:
                    return Human.ElementAt(i).Address;
                case 3:
                    return Human.ElementAt(i).Rolls;
                case 4:
                    return Human.ElementAt(i).Kol;
                case 5:
                    return Human.ElementAt(i).Dop;
                default:
                    return Human.ElementAt(i).Price;
            }
        }
    }
}