using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Helpers
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:44360/";
        public static string APIPathModel = APIBaseUrl + "api/model/";
        public static string APIPathBrand = APIBaseUrl + "api/brand/";
        public static string APIPathColor = APIBaseUrl + "api/color/";
    }
}
