﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


namespace ShopBridgeMVC
{
    public static class GlobalVariables
    {
        public static HttpClient client = new HttpClient();

        static GlobalVariables()
        {
            client.BaseAddress = new Uri("http://localhost:57151/api/");
            //client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}