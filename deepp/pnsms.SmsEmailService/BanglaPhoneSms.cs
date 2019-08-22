using Newtonsoft.Json;
using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pnsms.SmsEmailService
{
    public class BanglaPhoneSms
    {
        readonly string baseUrl = "https://powersms.banglaphone.net.bd";
        readonly string userId = "projuktinext";
        readonly string password = "projuktiNext123";
        
        public BanglaPhoneSms()
        {            
        }

        public event EventHandler ThreadDone;

        public void SendSms(object param)
        {
            BanglaPhoneSmsParams smsParam = (BanglaPhoneSmsParams)param;

            Sender sender = new Sender();
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new FormUrlEncodedContent(new[] 
                    {
                    new KeyValuePair<string, string>("userId", userId)
                    ,new KeyValuePair<string, string>("password", password)
                    ,new KeyValuePair<string, string>("smsText", smsParam.Sms)
                    ,new KeyValuePair<string, string>("commaSeperatedReceiverNumbers", string.Join(",", smsParam.MobileNumbers))
                    ,new KeyValuePair<string, string>("maskText", smsParam.MaskText)
                    });
                                
                var response = client.PostAsync("/httpapi/sendsms", content).Result;
                 
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        BanglaPhoneSmsReturnOnRequest jsonData = response.Content.ReadAsAsync<BanglaPhoneSmsReturnOnRequest>().Result;
                        sender.Msg = "Send Vendor key: " + jsonData.insertedSmsIds;
                        sender.Entitities = smsParam.Entities;
                        sender.GatewayIds = jsonData.insertedSmsIds;  
                    }
                    catch
                    {
                        sender.Msg = sender.Msg ?? "Send Error: Invalid json data";
                    }
                }
                else
                {
                    sender.Msg = "Send Error: Unsuccessfull";
                }
            }
            //sender.Entitities = smsParam.Entities;
            //sender.GatewayIds = "1234";
            //sender.Msg = "1234";            
            //Thread.Sleep(100);


            if (ThreadDone != null)
                ThreadDone(sender, EventArgs.Empty);
        }

        public void GetDeliveryStatus(object param)
        {
            BanglaPhoneDLRParams smsParam = (BanglaPhoneDLRParams)param;

            Sender sender = new Sender();

            using (var client = new System.Net.Http.HttpClient())
            {
                
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string ids = string.Join(",", smsParam.GatewayId.Take(10));

                var response = client.GetAsync("/httpapi/dlr/details/multiple?userId=" + userId
                    + "&password=" + password
                    + "&commaSeparatedSmsIds=" + ids
                    + "&pageIndex=0").Result;

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        BanglaPhoneDLRReturnOnRequest jsonData = response.Content.ReadAsAsync<BanglaPhoneDLRReturnOnRequest>().Result;
                        sender.Msg = "DLR Recepient Count:"+jsonData.DLRs.Count();

                        sender.DLRs = jsonData.DLRs;

                        for (int i = jsonData.CurrentPageIndex + 1; i < jsonData.TotalPages; i++)
                        {
                            var responseInner = client.GetAsync("/httpapi/dlr/details/multiple?userId=" + userId
                                + "&password=" + password
                                + "&commaSeparatedSmsIds=" + ids
                                + "&pageIndex=" + i
                                ).Result;

                            if (responseInner.IsSuccessStatusCode)
                            {
                                BanglaPhoneDLRReturnOnRequest jsonDataInner = responseInner.Content.ReadAsAsync<BanglaPhoneDLRReturnOnRequest>().Result;
                                sender.Msg += Environment.NewLine + "DLR Recepient Count:" + jsonDataInner.DLRs.Count();

                                sender.DLRs.AddRange(jsonDataInner.DLRs);
                            }
                        }                                      
                    }
                    catch
                    {
                        sender.Msg = "DLR Error: Invalid json data";
                    }
                }
                else
                {
                    sender.Msg = "DLR Error: Unsuccessfull";
                }
            }

            if (ThreadDone != null)
                ThreadDone(sender, EventArgs.Empty);
        }
    }

    public class BanglaPhoneSmsParams
    {        
        public string MaskText { get; set; }
        public string Sms { get; set; }
        public List<string> MobileNumbers { get; set; }
        public List<ShortMessageDetail> Entities { get; set; }
    }

    public class BanglaPhoneSmsReturnOnRequest
    {        
        public string insertedSmsIds { get; set; }
        public bool isError { get; set; }
        public string message { get; set; }
    }

    public class BanglaPhoneDLRParams
    {
        public List<string> GatewayId { get; set; }
        
    }

    public class BanglaPhoneDLRReturnOnRequest
    {
        public int TotalPages { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public List<BanglaPhoneSmsDLR> DLRs { get; set; }
    }

    public class BanglaPhoneSmsDLR
    {
        public int SmsId { get; set; }
        public string CellNumber { get; set; }
        public int Status { get; set; }
        public int ReasonCode { get; set; }
    }

    public class Sender
    {
        public string Msg { get; set; }
        public string GatewayIds { get; set; }
        public List<ShortMessageDetail> Entitities { get; set; }
        public List<BanglaPhoneSmsDLR> DLRs { get; set; }
    }
}
