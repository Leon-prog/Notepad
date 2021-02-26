using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notebook
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            nameProduct.Text = string.Format("Name product: {0}", Application.ProductName);
            developer.Text = "Developer: Leon_prog";
            copy.Text = "Copyright © HP Inc. 2021";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
