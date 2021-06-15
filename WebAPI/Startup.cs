using BusinessLogic.Abstract;
using BusinessLogic.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Encyption;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSingleton<ICarService,CarManager>();
            //services.AddSingleton<ICarDal, EfCarDal>();
            //services.AddSingleton<IBrandService, BrandManager>();
            //services.AddSingleton<IBrandDal, EfBrandDal>();
            //services.AddSingleton<IColorService, ColorManager>();
            //services.AddSingleton<IColorDal, EfColorDal>();
            //services.AddSingleton<ICustomerService, CustomerManager>();
            //services.AddSingleton<ICustomerDal, EfCustomerDal>();
            //services.AddSingleton<IRentalService, RentalManager>();
            //services.AddSingleton<IRentalDal, EfRentalDal>();
            //services.AddSingleton<IUserService, UserManager>();
            //services.AddSingleton<IUserDal, EfUserDal>();
            services.AddCors();
        
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//appsettings.jsondan gerekli b�l�m� bizim �retti�imiz TokenOptions t�r�nde ald�k
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//JwtBearerDefaults .net core s�r�m�ne g�re y�klenir
                .AddJwtBearer(options => //ayarlar� burada vericez.
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//Issuer bilgisi do�rulans�n m�? evet , issuerdan token verildi�inde bilginin geri gelmesi i�in bu gerekli
                    ValidateAudience = true,
                    ValidateLifetime = true,//Token'�n ya�am �mr� dikkate al�ns�n m�? evet (bizde 10 dakika) 
                    ValidIssuer = tokenOptions.Issuer,//yukar�da �ekti�imiz de�i�kenden geliyor
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,//anahtarda kontrol edilsin mi?
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)//anahtarda helperdan geldi
                };
            });
            services.AddDependencyResolvers(new ICoreModule[] {
               new CoreModule()
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200/").AllowAnyHeader().AllowAnyOrigin());//bu linkten gelen herhangi bir iste�e cevap ver "AllowAnyHeader"

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); //s�ralar� �nemli 

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
