using ServiceContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WcfClient1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = Service2Client.Instance();
            //var result = client.GetData(Int32.Parse(this.textBox1.Text));
            var objCompositeType2 = new CompositeType2()
            {
                BoolValue = true,
                StringValue = this.textBox1.Text
            };
            var result = client.GetDataUsingDataContract(objCompositeType2);
            this.label1.Text = result.StringValue;
        }
    }
}
