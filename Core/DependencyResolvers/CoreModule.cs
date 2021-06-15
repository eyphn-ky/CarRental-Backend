using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();//.Net Core'da bu mevcut yazmak zorundayız ki memory cache karşılansın. Bu satır microsoft sistemi kullanılıyorsa olmalıdır. yoksa silinebilir.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>(); //zincirde olmayan interfaceler core'da çözülüyor.
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
