using AngularLastFMWebApi.Azure;
using Business;
using Business.Interfaces;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceAgent;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using DataAccess.Implementation.Mongo;
using DataAccess.Implementation.SQL;
using DataAccess.Implementation.SQL.Search;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AngularLastFMWebApi
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
		    services.AddCors();
		    //services.AddDefaultIdentity<Account>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

		    var azureServiceTokenProvider = new AzureServiceTokenProvider();
		    var keyVaultClient = new KeyVaultClient(
		        new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            services.Configure<Settings>(
                options =>
                {
	                //options.AzureCosmosDBConnectionString = GetSecretValueFromKeyVault(keyVaultClient, "AzureCosmosDBConnectionString");
	                //options.AzureCosmosDatabaseBookmarks = Configuration.GetSection("AzureCosmosDatabaseBookmarks").Value;
                   // options.AzureSQLDBConnectionString = GetSecretValueFromKeyVault(keyVaultClient, "AzureSQLDBConnectionString");
                    options.SQLDBConnectionString = Configuration.GetConnectionString("SQLDBConnectionString");
                    options.MongoDbEnabled = Configuration.GetSection("MongoDbEnabled").Value == "true";
                    options.jWTKey = Configuration["Jwt:Key"];
                    options.jWTIssuer = Configuration["Jwt:Issuer"];
                });

            Info info = new Info
			{
				Title = "Last FM API",
				Version = "v1"
			};

			info.Description = "Last FM API v1";

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", info);

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				if (File.Exists(xmlPath))
				{
					c.IncludeXmlComments(xmlPath);
				}
			});

		    var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
		    services.AddAuthentication(x =>
		        {
		            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		        })
		        .AddJwtBearer(x =>
		        {
		            x.RequireHttpsMetadata = false;
		            x.SaveToken = true;
		            x.TokenValidationParameters = new TokenValidationParameters
		            {
		                ValidateIssuer = true,
		                ValidateAudience = true,
		                ValidateLifetime = true,
		                ValidateIssuerSigningKey = true,
		                ValidIssuer = Configuration["Jwt:Issuer"],
		                ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
		            };
		        });

		   // services.AddTransient<UserManager<Account>>();
            services.AddScoped<ILastFmServiceAgent, LastFmServiceAgent>();
			services.AddScoped<IFavoriteAlbumRepository, DataBaseStrategy>();
            services.AddScoped<IFavoriteAlbumBusiness, FavoriteAlbumBusiness>();
		    services.AddScoped<IFavoriteArtistRepository, FavoriteArtistRepository>();
		    services.AddScoped<IFavoriteArtistBusiness, FavoriteArtistBusiness>();
            services.AddScoped<MongoFavoriteAlbumRepository>();
		    services.AddScoped<FavoriteAlbumRepository>();
		    services.AddScoped<SQLSearchFavoriteAlbumRepository>();
		    services.AddScoped<IAccountRepository, AccountRepository>();
		    services.AddScoped<IAccountBusiness, AccountBusiness>();

		    // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});

		    //AddBlobStorageFile(keyVaultClient);
        }

	    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}

	    private void AddBlobStorageFile(KeyVaultClient keyVaultClient)
	    {
	        var storageSecret = GetSecretValueFromKeyVault(keyVaultClient, "AzureBlobStorageConnectionString");
            var storageContainerSecret = GetSecretValueFromKeyVault(keyVaultClient, "AzureBlobStorageContainerName");

	        AzureBlobStorage storage = new AzureBlobStorage(new AzureBlobSetings(
	            storageConnectionString: storageSecret,
	            containerName: storageContainerSecret));
	        var (localFileName, sourceFile) = StartupInfoFileHelper.CreateStartupFile();
	        storage.UploadAsync(localFileName, sourceFile).GetAwaiter().GetResult();
	    }

	    private string GetSecretValueFromKeyVault(KeyVaultClient keyVaultClient, string keyVaultUri)
	    {
	        var storageSecret = string.Empty;

            var storageVaultUri = Configuration.GetSection(keyVaultUri).Value;
	        if (!string.IsNullOrWhiteSpace(storageVaultUri))
	        {
	            var storageSecretResult = keyVaultClient.GetSecretAsync(storageVaultUri).GetAwaiter().GetResult();
	            storageSecret = storageSecretResult.Value;
	        }
            
	        return storageSecret;
	    }
    }
}
