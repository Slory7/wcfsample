using Client.Core;
using Service.Contracts;
using Service.Contracts.Services;
using Service.Contracts.Services.Order;
using Service.Contracts.ViewModels;
using Service.Contracts.ViewModels.Order;
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

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    var client = ClientGlobals.ClientService.GetClient<IService2>();
        //    //var result = client.GetData(Int32.Parse(this.textBox1.Text));
        //    var objCompositeType2 = new CompositeType2()
        //    {
        //        BoolValue = true,
        //        StringValue = this.textBox1.Text
        //    };
        //    var result = client.GetDataUsingDataContract(objCompositeType2);
        //    this.label1.Text = result.Result.StringValue;
        //    string strResultStatus = Constants.GetResultStatusString(result.Status);
        //    MessageBox.Show(result.Message, strResultStatus);
        //}
        private async void button1_Click(object sender, EventArgs e)
        {
            var client = ClientGlobals.ClientService.GetClient<IOrderBatchService>();
            var day = DateTime.Parse(this.textBox1.Text);
            ResultData result = null;
            await Task.Run(() =>
            {
                var resultData = client.GetOneDayBatch(day);
                current = resultData.Data;
                result = resultData;
            });
            if (result.Status == ResultStatus.Success)
            {
                this.label1.Text = current.Count == 0 ? "未找到" : current.First().sBatchCode;
            }
            string strResultStatus = Constants.GetResultStatusString(result.Status);
            MessageBox.Show(result.Message, strResultStatus);
        }
        List<BS_Order_SalesOrder_BatchDto> current;
        private void button2_Click(object sender, EventArgs e)
        {
            var client = ClientGlobals.ClientService.GetClient<IOrderBatchService>();
            var codes = this.textBox1.Text;
            var result = client.GetBatchsByCodes(codes);
            current = result.Data;
            this.label1.Text = current.Count == 0 ? "未找到" : String.Join(",", current.Select(x => x.sOrderCode));
            string strResultStatus = Constants.GetResultStatusString(result.Status);
            MessageBox.Show(result.Message, strResultStatus);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var client = ClientGlobals.ClientService.GetClient<IOrderBatchService>();
            var result = client.InsertBulk(current);
            this.label1.Text = result.Status == ResultStatus.Success ? "插入成功" : "插入失败";
            string strResultStatus = Constants.GetResultStatusString(result.Status);
            MessageBox.Show(result.Message, strResultStatus);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var client = ClientGlobals.ClientService.GetClient<IOrderService>();
            var objDto = new OrderBiz() { };
            var result = client.ProcessBMOrder(objDto);
            string strResultStatus = Constants.GetResultStatusString(result.Status);
            MessageBox.Show(result.Message, strResultStatus);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var client = ClientGlobals.ClientService.GetClient<IOrderService>();
            var result = await client.GetVoucher();
            string strResultStatus = Constants.GetResultStatusString(result.Status);
            MessageBox.Show(result.Message, strResultStatus);
        }
    }
}
