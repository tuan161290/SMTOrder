using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisolSMTLineApp.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace WisolSMTLineApp
{
    public class Controller
    {
        public String _token;
        private HttpClient _httpClient;
        private HttpClientHandler _httpClientHandler;

        public Controller()
        {
            _httpClientHandler = new HttpClientHandler();
            _httpClientHandler.AllowAutoRedirect = false;
            _httpClientHandler.UseDefaultCredentials = true;
            _httpClient = new HttpClient(_httpClientHandler);
            //_httpClient.BaseAddress = new Uri("http://45.119.212.111:5000/");
            //_httpClient.BaseAddress = new Uri("http://192.168.0.5:5000/api/v0.1/");
            _httpClient.BaseAddress = new Uri("http://10.70.10.52:6789/api/");
            //_httpClient.BaseAddress = new Uri("http://10.70.10.52:4567/api/");
            //_httpClient.BaseAddress = new Uri("http://localhost:50479/api/");
            //_httpClient.BaseAddress = new Uri("http://localhost:6789/api/");
            //_httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.MaxResponseContentBufferSize = 256000;
            TimeSpan timeout = TimeSpan.FromSeconds(4);
            _httpClient.Timeout = timeout;
        }
        public void Dispose()
        {
            if (_httpClient != null)
                _httpClient.Dispose();
            if (_httpClientHandler != null)
                _httpClientHandler.Dispose();
        }

        public async Task<List<Product>> GetProducts()
        {
            string url = "Products/GetProducts";
            List<Product> productList = null;
            using (var response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Product> resMsg = JsonConvert.DeserializeObject<List<Product>>(content);
                    if (resMsg != null)
                        return resMsg;
                }
            }
            return productList;
        }

        public List<ProductionPlan> GetProductionPlan(int lineID)
        {
            string url = "plan/GetCurrentPlan/" + lineID;
            List<ProductionPlan> plans = null;
            using (var response = _httpClient.GetAsync(url).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Response<List<ProductionPlan>> resMsg = JsonConvert.DeserializeObject<Response<List<ProductionPlan>>>(content);
                    if (resMsg.Data != null)
                        plans = resMsg.Data;
                }
            }
            return plans;
        }

        public async Task<PlanInfo> GetProductionPlanAsync(int lineID, int ProductID)
        {
            string url = $"plan/GetCurrentPlan/{lineID}/{ProductID}";
            PlanInfo plan = null;
            using (var response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    PlanInfo resMsg = JsonConvert.DeserializeObject<PlanInfo>(content);
                    if (resMsg != null)
                        plan = resMsg;
                }
            }
            return plan;
        }

        public string NewProductionPlan(PlanInfo obj)
        {
            var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                var response = _httpClient.PostAsync("Plan/CreatePlan", content).Result;
                var res = response.Content.ReadAsStringAsync().Result;
                //Response<object> ret = JsonConvert.DeserializeObject<Response<object>>(res);
                return res;
            }
        }

        public bool CreateOrder(Order obj)
        {
            var jsonObj = JsonConvert.SerializeObject(obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = _httpClient.PostAsync("Orders/PostOrder", content).Result;
                    var res = ret.Content.ReadAsStringAsync().Result;
                    return ret.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ConfirmOrder(Order obj)
        {
            obj.IsConfirmed = true;
            obj.ConfirmedTime = App.Now;
            var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = _httpClient.PutAsync("Orders/ConfirmOrder/" + obj.OrderID, content).Result;
                    return ret.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        public async Task<List<Order>> getLstOrderNotFinishAsync(int lineID)
        {
            List<Order> LstOrderNotFinish = null;
            try
            {
                string url = "Orders/GetUnconfirmOrders/" + lineID;
                using (var response = await _httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        List<Order> resMsg = JsonConvert.DeserializeObject<List<Order>>(content);
                        if (resMsg != null)
                            LstOrderNotFinish = resMsg;
                    }
                }
            }
            catch (Exception)
            {

            }
            return LstOrderNotFinish;
        }

        public async Task<bool> DiscardRemainOrder(PlanInfo Plan)
        {

            var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(Plan);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = await _httpClient.PutAsync("Orders/DiscardRemainOrder/", content);
                    return ret.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateFluxOrder(FluxOrder Obj)
        {
            var jsonObj = JsonConvert.SerializeObject(Obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = await _httpClient.PutAsync("FluxOrders/PutFluxOrder/" + Obj.FluxOrderID, content);
                    var res = await ret.Content.ReadAsStringAsync();
                    return ret.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> CreateFluxOrderAsync(FluxOrder fluxOrder)
        {
            var jsonObj = JsonConvert.SerializeObject(fluxOrder);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = await _httpClient.PostAsync("FluxOrders/CreateFluxOrder", content);
                    var res = ret.Content.ReadAsStringAsync();
                    return ret.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<FluxOrder> GetUnfinishFluxOrderAsync(int lineID)
        {
            FluxOrder UnfinishFluxOrder = null;
            try
            {
                string url = "FluxOrders/GetUnfinishFluxOrder/" + lineID;
                using (var response = await _httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        FluxOrder resMsg = JsonConvert.DeserializeObject<FluxOrder>(content);
                        if (resMsg != null)
                            UnfinishFluxOrder = resMsg;
                    }
                }
            }
            catch (Exception)
            {

            }
            return UnfinishFluxOrder;
        }

        public List<LineInfo> getLstLine()
        {
            List<LineInfo> ListLine = null;
            try
            {
                string url = "LineInfos/GetLineInfos/";
                using (var response = _httpClient.GetAsync(url).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        List<LineInfo> resMsg = JsonConvert.DeserializeObject<List<LineInfo>>(content);
                        if (resMsg != null)
                            ListLine = resMsg;
                    }
                }
            }
            catch (Exception)
            {

            }
            return ListLine;
        }

        public async Task<LineInfo> GetLineInfoAsync(int lineID)
        {
            string url = $"LineInfos/GetActiveLineInfo/{lineID}";
            LineInfo LineInfo = null;
            using (var response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    LineInfo resMsg = JsonConvert.DeserializeObject<LineInfo>(content);
                    if (resMsg != null)
                        LineInfo = resMsg;
                }
            }
            return LineInfo;
        }

        public async Task<DateTime> GetDateTimeFromServer()
        {
            string url = $"Time/GetDateTimeFromServer/";
            DateTime Now = DateTime.Now;
            using (var response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    DateTime resMsg = JsonConvert.DeserializeObject<DateTime>(content);
                    if (resMsg != null)
                        Now = resMsg;
                }
            }
            return Now;
        }

        public async Task<bool> UpdatePlan(PlanInfo Obj)
        {
            var jsonObj = JsonConvert.SerializeObject(Obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = await _httpClient.PutAsync("Plan/PutPlanInfo/" + Obj.LineInfoID, content);
                    var res = ret.Content.ReadAsStringAsync().Result;
                    return ret.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public class Response<T>
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }
        }
    }

    public class Api
    {
        public static Controller Controller { get; set; } = new Controller();
    }
}
