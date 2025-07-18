using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Helopper
{
    public static class ServiceHelper
    {
        public static IServiceProvider Services { get; set; }

        public static T GetService<T>() => Services.GetRequiredService<T>();
    }
}
