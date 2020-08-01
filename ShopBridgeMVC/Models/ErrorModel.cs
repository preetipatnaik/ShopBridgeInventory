using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridgeMVC.Models
{
    public class ErrorModel
    {
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrack { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}