using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WisolSMTLineApp.Model
{
    public class Orders
    {
        public static async void Create()
        {
            var Orders = await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.ID);
            if (Orders != null)
            {
                if (Orders.Count > 0)
                {
                    return;
                }
            }
            var ProductionDtl = new ProductionDtl()
            {
                Amount = Setting.DefaultLots,
                Factory_ID = 1,
                Working_Date = App.TodayDate,
                Shift_ID = App.CurrentShift,
                Line_ID = Setting.SelectedLine.ID,
                Product_ID = Setting.SelectedProduct.ID,
                Message = "waiting"
            };
            if (Api.Controller.CreateOrder(ProductionDtl))
            {
                //LstOrderNotFinish.Clear();
                //Api.Controller.getLstOrderNotFinish(2)?.ForEach(x => LstOrderNotFinish.Add(x));
                MessageBox.Show("Create order successfully");
            }
            else
                MessageBox.Show("Create order failed, something happened");
        }
    }
}
