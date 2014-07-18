using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.SqlClient;


using ImportExport;
using ImportExport.DBFProvider;
using ImportExport.DBFProvider.Attributes;
using ImportExport.Enum;

namespace KLADR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<Kladr> GetNewRows(List<Kladr> oldTable, List<Kladr> newTable)
        {
            var outList = new List<Kladr>();

            for (int i = 0; i < newTable.Count; i++)
            {
                label1.Text = i.ToString();

                bool findSuccess = false;

                for (int j = 0; j < oldTable.Count; j++)
                {
                    label1.Text = j.ToString();

                    if (Equals(newTable[i], oldTable[i]))
                    //if (
                    //    newTable[i].NAME == oldTable[j].NAME & 
                    //    newTable[i].GNINMB == oldTable[j].GNINMB &
                    //    newTable[i].CODE == oldTable[j].CODE &
                    //    newTable[i].INDEX == oldTable[j].INDEX &
                    //    newTable[i].OCATD == oldTable[j].OCATD &
                    //    newTable[i].SOCR == oldTable[j].SOCR &
                    //    newTable[i].STATUS == oldTable[j].STATUS &
                    //    newTable[i].UNO == oldTable[j].UNO
                    //    ) 
                    {
                        findSuccess = true;
                        break;
                    }
                }

                if (findSuccess == true)
                {
                    outList.Add(new Kladr 
                    { 
                        NAME = newTable[i].NAME,
                        CODE = newTable[i].CODE,
                        GNINMB = newTable[i].GNINMB,
                        INDEX = newTable[i].INDEX,
                        OCATD = newTable[i].OCATD,
                        SOCR = newTable[i].SOCR,
                        STATUS = newTable[i].STATUS,
                        UNO = newTable[i].UNO
                    });
                }
            }

            return outList;
        }

        public List<Kladr> DbfToList(string filePath)
        {
            var DataList = new List<Kladr>();

            var dataInitialize = new DBFProviderDataInitialize { FilePath = filePath };
            var transferEngine = new TransferEngine(ProviderType.DBF, dataInitialize);
            var templates = transferEngine.GetAll<TestTemplate>();

            foreach (var template in templates)
            {
                DataList.Add(new Kladr {
                    NAME = template.C1,
                    SOCR = template.C2,
                    CODE = template.C3,
                    INDEX = template.C4,
                    GNINMB = template.C5,
                    UNO = template.C6,
                    OCATD = template.C7,
                    STATUS = template.C8
                });
            }
            return DataList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var newRows = GetNewRows(
            //    DbfToList(@"D:\ProjectVS\KLADR-SMD\KLADR\bin\Debug\KLADRold.DBF"),
            //    DbfToList(@"D:\ProjectVS\KLADR-SMD\KLADR\bin\Debug\KLADRnew.DBF"));

            var A = DbfToList(@"D:\ProjectVS\KLADR-SMD\KLADR\bin\Debug\KLADRold.DBF");
            var B = DbfToList(@"D:\ProjectVS\KLADR-SMD\KLADR\bin\Debug\KLADRnew.DBF");
            MessageBox.Show("Считали!");

            //var n = a.Except(b);
            var n = A.Where(a => B.Contains(a)).ToList();

            //var newRows = GetNewRows(a, b);

            MessageBox.Show("Сравнили!");

            //dataGridView1.DataSource = newRows;
        }
        public class Kladr 
        {
            public string NAME { get; set; }
            public string SOCR { get; set; }
            public string CODE { get; set; }
            public string INDEX { get; set; }
            public string GNINMB { get; set; }
            public string UNO { get; set; }
            public string OCATD { get; set; }
            public string STATUS { get; set; }
        }
        public class TestTemplate
        {
            [DBFColumn(Name = "NAME")]
            [DBFColumnLenght(128)]
            public string C1 { get; set; }

            [DBFColumn(Name = "SOCR")]
            [DBFColumnLenght(128)]
            public string C2 { get; set; }

            [DBFColumn(Name = "CODE")]
            [DBFColumnLenght(128)]
            public string C3 { get; set; }

            [DBFColumn(Name = "INDEX")]
            [DBFColumnLenght(128)]
            public string C4 { get; set; }

            [DBFColumn(Name = "GNINMB")]
            [DBFColumnLenght(128)]
            public string C5 { get; set; }

            [DBFColumn(Name = "UNO")]
            [DBFColumnLenght(128)]
            public string C6 { get; set; }

            [DBFColumn(Name = "OCATD")]
            [DBFColumnLenght(128)]
            public string C7 { get; set; }

            [DBFColumn(Name = "STATUS")]
            [DBFColumnLenght(128)]
            public string C8 { get; set; }
        }
    }
}
