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
            //_httpClient.BaseAddress = new Uri("http://10.70.10.52:5000/api/v0.1/");
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/v0.1/");
            _httpClient.MaxResponseContentBufferSize = 256000;
            TimeSpan timeout = TimeSpan.FromSeconds(4);
            _httpClient.Timeout = timeout;
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/apng,*/*;q=0.8");
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            _httpClient.DefaultRequestHeaders.Add("Pragma", "no-cache");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko; Google Page Speed Insights) Chrome/27.0.1453 Safari/537.36");
        }
        public void Dispose()
        {
            if (_httpClient != null)
                _httpClient.Dispose();
            if (_httpClientHandler != null)
                _httpClientHandler.Dispose();
        }
        public async Task<List<Shift>> GetShifts()
        {
            string url = "product/getShifts";
            List<Shift> shiftList = null;
            using (var response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Response<List<Shift>> resMsg = JsonConvert.DeserializeObject<Response<List<Shift>>>(content);
                    if (resMsg.Data != null)
                        shiftList = (List<Shift>)resMsg.Data;
                }
                return shiftList;
            }
        }
        public async Task<List<Product>> GetProducts()
        {
            string url = "product";
            List<Product> productList = null;
            using (var response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Response<List<Product>> resMsg = JsonConvert.DeserializeObject<Response<List<Product>>>(content);
                    if (resMsg.Data != null)
                        productList = resMsg.Data;
                }
            }
            return productList;
        }

        public List<ProductionPlan> GetProductionPlan(int lineID)
        {
            string url = "production-plan/1/" + lineID;
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

        public async Task<List<ProductionPlan>> GetProductionPlanAsync(int lineID)
        {
            string url = "production-plan/1/" + lineID;
            List<ProductionPlan> plans = null;
            using (var response = await _httpClient.GetAsync(url))
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

        public bool NewProductionPlan(ProductionPlan obj)
        {
            var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                var response = _httpClient.PostAsync("production-plan", content).Result;
                var res = response.Content.ReadAsStringAsync().Result;
                Response<object> ret = JsonConvert.DeserializeObject<Response<object>>(res);
                return response.IsSuccessStatusCode;
            }
        }

        public bool CreateOrder(ProductionDtl obj)
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

        public bool ConfirmOrder(ProductionDtl obj)
        {
            obj.Finished = true;
            var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = _httpClient.PutAsync("production-dtl", content).Result;
                    return ret.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<ProductionDtl> getLstOrderNotFinish(int lineID)
        {
            List<ProductionDtl> LstOrderNotFinish = null;
            try
            {
                string url = "production-dtl/order-not-finished/" + lineID;
                using (var response = _httpClient.GetAsync(url).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Response<List<ProductionDtl>> resMsg = JsonConvert.DeserializeObject<Response<List<ProductionDtl>>>(content);
                        if (resMsg.Data != null)
                            LstOrderNotFinish = resMsg.Data;
                    }
                }
            }
            catch (Exception)
            {

            }
            return LstOrderNotFinish;
        }

        public async Task<List<ProductionDtl>> getLstOrderNotFinishAsync(int lineID)
        {
            List<ProductionDtl> LstOrderNotFinish = null;
            try
            {
                string url = "production-dtl/order-not-finished/" + lineID;
                using (var response = await _httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Response<List<ProductionDtl>> resMsg = JsonConvert.DeserializeObject<Response<List<ProductionDtl>>>(content);
                        if (resMsg.Data != null)
                            LstOrderNotFinish = resMsg.Data;
                    }
                }
            }
            catch (Exception)
            {

            }
            return LstOrderNotFinish;
        }

        public List<LineInfo> getLstLine()
        {
            List<LineInfo> ListLine = null;
            try
            {
                string url = "prod-line/1";
                using (var response = _httpClient.GetAsync(url).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Response<List<LineInfo>> resMsg = JsonConvert.DeserializeObject<Response<List<LineInfo>>>(content);
                        if (resMsg.Data != null)
                            ListLine = resMsg.Data;
                    }
                }
            }
            catch (Exception)
            {

            }
            return ListLine;
        }
        public async Task<bool> UpdatePlan(ProductionPlan Obj)
        {
            var jsonObj = JsonConvert.SerializeObject(Obj);
            using (var content = new StringContent(jsonObj, Encoding.UTF8, "application/json"))
            {
                try
                {
                    var ret = await _httpClient.PutAsync("production-plan", content);
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
