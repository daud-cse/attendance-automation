using sfa.attendance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace sfa.attendance.ApiService
{
  public  class HttpService
    {
        public static String post(String url, String data,LoginReponseModel objLoginReponseModel)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                using (var client = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("token", objLoginReponseModel.Token);
                    var response = httpClient.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        // by calling .Result you are performing a synchronous call
                        var responseContent = response.Content;

                        // by calling .Result you are synchronously reading the result
                        string responseString = responseContent.ReadAsStringAsync().Result;

                        return responseString;
                    }

                }
            }
            catch (Exception ex)
            {
              //  FileReadWrite.ErrorLogging(ex);
            }
            return null;
        }


        public static String get(String url,LoginReponseModel objLoginReponseModel)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
              
                using (var client = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("token", objLoginReponseModel.Token);
                    var response = httpClient.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        // by calling .Result you are performing a synchronous call
                        var responseContent = response.Content;

                        // by calling .Result you are synchronously reading the result
                        string responseString = responseContent.ReadAsStringAsync().Result;

                        return responseString;
                    }
                }
            }
            catch (Exception ex) {
                return ex.Message.ToString();
            }
            return null;        
        }


        public static String get(String url, String proxyUri)
        {
            //try
            //{
            //    HttpClient httpClient = new HttpClient();

            //    HttpClientHandler httpClientHandler = new HttpClientHandler()
            //    //httpClient.GetAsync(url);
            //    using (var client = new HttpClient(httpClientHandler))
            //    {
            //        var response = httpClient.GetAsync(url).Result;
            //        if (response.IsSuccessStatusCode)
            //        {
            //            // by calling .Result you are performing a synchronous call
            //            var responseContent = response.Content;

            //            // by calling .Result you are synchronously reading the result
            //            string responseString = responseContent.ReadAsStringAsync().Result;

            //            return responseString;
            //        }
            //    }
            //}
            //catch (Exception ex) { }
            return null;
        }
    }
}
