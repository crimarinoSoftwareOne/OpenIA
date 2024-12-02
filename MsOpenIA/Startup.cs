namespace MsOpenIA
{
    using Amazon.DynamoDBv2;
    using Amazon.Runtime;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MsOpenIA.Application.Business;
    using MsOpenIA.Application.Mappers;
    using MsOpenIA.Application.Services;
    using MsOpenIA.Application.Utilities;
    using MsOpenIA.Domain.Interfaces.Business;
    using MsOpenIA.Domain.Interfaces.Mappers;
    using MsOpenIA.Domain.Interfaces.Repositories;
    using MsOpenIA.Domain.Interfaces.Services;
    using MsOpenIA.Domain.Interfaces.Utilities;
    using MsOpenIA.Infrastructure.Configuration;
    using MsOpenIA.Infrastructure.Repositories;
    using MsOpenIA.Handlers;
    using Amazon.Extensions.NETCore.Setup;

    public class Startup
    {

        private readonly string _headerCollection = "Authorization";
        private readonly string _typeAuthCollection = "Bearer";

        public Startup(IConfiguration configuration) => 
            Configuration = configuration;
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            ConfigureAWS(services);
            ConfigureDynamoDB(services);
            ConfigureOpenIA(services);

            services.AddScoped<HandlerService>();
            services.AddScoped<IMapperResponse, MapperResponse>();
            services.AddScoped<IBusinessBase, BusinessBase>();
            services.AddTransient<IFactoryOpenaAI, FactoryOpenaAI>();
            services.AddTransient<IFactoryTokenizer, FactoryTokenizer>();
            services.AddTransient<IFactory, Factory>();
        }

        private void ConfigureAWS(IServiceCollection services)
        {
            services.Configure<AWSSettings>(Configuration.GetSection("AWS"));
            AWSSettings? awsSettings = Configuration.GetSection("AWS").Get<AWSSettings>();

            AWSOptions awsOptions = Configuration.GetAWSOptions();
            awsOptions.Credentials = new BasicAWSCredentials(
                awsSettings.AccessKey,
                awsSettings.SecretKey
            );
            awsOptions.Region = Amazon.RegionEndpoint.GetBySystemName(awsSettings.Region);

            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonDynamoDB>();
        }

        private void ConfigureDynamoDB(IServiceCollection services)
        {
            services.Configure<DynamoDbSettings>(Configuration.GetSection("DynamoDB"));
            services.AddScoped<IRepositoryDynamoDb, RepositoryDynamoDb>();
            services.AddScoped<IBusinessDynamoDb, BusinessDynamoDb>();
            services.AddScoped<IServiceDynamoDb, ServiceDynamoDb>();
        }

        private void ConfigureOpenIA(IServiceCollection services)
        {
            services.Configure<OpenIaSettings>(Configuration.GetSection("OpenIA"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<OpenIaSettings>>().Value);

            services.AddHttpClient<IRepositoryOpenAI, RepositoryOpenAI>((sp, client) =>
            {
                OpenIaSettings? settings = sp.GetRequiredService<OpenIaSettings>();
                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Add($"{_headerCollection}", $"{_typeAuthCollection} {settings.ApiKey}");
            });

            services.AddScoped<IRepositoryOpenAI, RepositoryOpenAI>(sp =>
            {
                OpenIaSettings? settings = sp.GetRequiredService<OpenIaSettings>();
                return new RepositoryOpenAI(settings.ApiKey, settings.Type_Model);
            });

            services.AddScoped<IBusinessOpenAI, BusinessOpenAI>();
            services.AddScoped<IBusinessTokenizer, BusinessTokenizer>();
            services.AddScoped<IServiceOpenAI, ServiceOpenAI>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}