﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace ECommerceProject.WebUI.Extensions
{
    public static class SessionExtensionMethods
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key, objectString);
        }

        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            string objectString = session.GetString(key);
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            else
            {
                T value = JsonConvert.DeserializeObject<T>(objectString);
                return value;
            }
        }

    }
}
