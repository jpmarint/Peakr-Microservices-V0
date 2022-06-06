using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SolicitudesAPI.Helpers;
using SolicitudesAPI.Models;
using SolicitudesAPI.Servicios;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SolicitudesAPI
{
    public class Startup
    {

        private readonly string _MyCors = "MyCors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }
      
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                var archivoXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var rutaXML = Path.Combine(AppContext.BaseDirectory, archivoXML);
                c.IncludeXmlComments(rutaXML);
            });
           


          
                //Add a bunch of service configurations here
                // ...

                // It's probably better to externalize the Swagger config to it's own private helper method
                services.AddSwaggerGen(swagger =>
                {
                    // Setup your Swagger doc, security etc here
                });

                // Customize the Swagger generator here
                services.ConfigureSwaggerGen(options =>
                {
                    // Use fully qualified schema object names to ensure uniqueness
                    options.CustomSchemaIds(configuration => configuration.FullName);
                });
           


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //configurando automapper
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(FiltroErrores));
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _MyCors,
                builder =>
                 {
                     //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                     //.AllowAnyHeader()
                     //.AllowAnyMethod();
                     //{
                     builder.WithOrigins("https://localhost:7218",
                                          "http://localhost:4200",
                                          "https://apirequest.io")
                                                  .AllowAnyHeader()
                                                   .AllowAnyMethod();
                 });
            });

        }    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

 

            if (env.IsDevelopment())
            {
               
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
           
            app.UseRouting();

            app.UseCors(_MyCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        }
    }
