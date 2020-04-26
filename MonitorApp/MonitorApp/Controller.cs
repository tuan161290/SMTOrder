using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WiSolSMTRepo.Model;
using MonitorApp.Model;

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
            //_httpClient.BaseAddress = new Uri("http://10.70.10.52:6789/api/");            
            //_httpClient.BaseAddress = new Uri("http://10.70.10.52:4567/api/");
            _httpClient.BaseAddress = new Uri("http://localhost:6789/api/");
            //_httpClient.BaseAddress = new Uri("http://localhost:6789/api/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.MaxResponseContentBufferSize = 256000;
            TimeSpan timeout = TimeSpan.FromSeconds(4);
            _httpClient.Timeout = timeout;
        }

        public async Task<List<LineInfo>> GetRunningPlan()
        {
            List<LineInfo> ListLine = null;
            try
            {
                string url = "Plan/GetRunningPlans/";
                using (var response = await _httpClient.GetAsync(url))
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

        public void Dispose()
        {
            if (_httpClient != null)
                _httpClient.Dispose();
            if (_httpClientHandler != null)
                _httpClientHandler.Dispose();
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
                        return resMsg;
                }
            }
            return Now;
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
        public bool CreateOrder(Order obj)
        {
            var jsonObj = JsonConvert.SerializeObject(obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = _httpClient.PostAsync("production-dtl", content).Result;
                    var res = ret.Content.ReadAsStringAsync().Result;
                    return ret.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public async Task<bool> UpdateOrder(Order Obj)
        {
            var jsonObj = JsonConvert.SerializeObject(Obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = await _httpClient.PutAsync("Orders/PutOrder/" + Obj.OrderID, content);
                    var res = ret.Content.ReadAsStringAsync().Result;
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

        public async Task<List<Order>> getLstOrderNotFinishAsync()
        {
            List<Order> LstOrderNotFinish = null;
            try
            {
                string url = "Orders/GetUnconfirmOrders/";
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
