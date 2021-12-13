using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EmployeeAPI.Model
{    
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]

    public class DefaultJsonSerializer : JsonSerializerSettings
    {
        public DefaultJsonSerializer()
        {
            NullValueHandling = NullValueHandling.Ignore;
        }
    }

    [Serializable]
    public class Result<T>
    {
        public long IdentityValue { get; set; }

        public T Data { get; set; }

        public List<T> List { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int ReturnValue { get; set; }

        public long NoOfRecords { get; set; }

        public bool Any()
        {
            throw new NotImplementedException();
        }
    }
}
