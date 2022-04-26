<<<<<<< HEAD
using Microsoft.AspNetCore.Authentication.JwtBearer;
=======
>>>>>>> 7fdb0c8c89466031244ac1949b4ec0895781f6c8
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
=======
using Microsoft.OpenApi.Models;
>>>>>>> 7fdb0c8c89466031244ac1949b4ec0895781f6c8
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
<<<<<<< HEAD
using System.Text;
=======
>>>>>>> 7fdb0c8c89466031244ac1949b4ec0895781f6c8
using System.Threading.Tasks;

namespace SwiftCode.BBS.API
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
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v0.1.0",
                    Title = "SwiftCode.BBS.API",
                    Description = "���˵���ĵ�",
                    Contact = new OpenApiContact
                    {
                        Name = "SwiftCode",
                        Email="SwiftCode@xxx.com",
                    }
                }) ;

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath,"SwiftCode.BBS.API.xml");
                c.IncludeXmlComments(xmlPath, true);

                var xmlModelPath = Path.Combine(basePath, "SwiftCode.BBS.Model.xml"); //Model���xml�ļ���
                c.IncludeXmlComments(xmlModelPath);
<<<<<<< HEAD

                // ����С��
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // ��header�����token�����ݵ���̨
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {

                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
            });
            #endregion

            // ��֤
            services.AddAuthentication(x =>
            {
                // ��ϸ��������� ��ͼ�д������ʾ����Ǹ�
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o => {

                //��ȡ�����ļ�
                //var audienceConfig = Configuration.GetSection("Audience");
                //var symmetricKeyAsBase64 = audienceConfig["Secret"];
                var audienceConfig = Configuration[("Audience:Audience")];
                var symmetricKeyAsBase64 = Configuration["Audience:Secret"];
                var iss = Configuration["Audience:Issuer"];
                var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
                var signingKey = new SymmetricSecurityKey(keyByteArray);

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    //ValidIssuer = audienceConfig["Issuer"],//������
                    ValidIssuer = iss,//������
                    ValidateAudience = true,
                    //ValidAudience = audienceConfig["Audience"],//������
                    ValidAudience = audienceConfig,//������
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,//����ǻ������ʱ�䣬Ҳ����˵����ʹ���������˹���ʱ�䣬����ҲҪ���ǽ�ȥ������ʱ��+���壬Ĭ�Ϻ�����7���ӣ������ֱ������Ϊ0
                    RequireExpirationTime = true,
                };
            });

            // 1����Ȩ����������ϱߵ�����ͬ�����ô����ǲ�����controller�У�д��� roles ��
            // Ȼ����ôд [Authorize(Policy = "Admin")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());//������ɫ
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));//��Ĺ�ϵ
                options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));//�ҵĹ�ϵ
            });
=======
            });
            #endregion
>>>>>>> 7fdb0c8c89466031244ac1949b4ec0895781f6c8
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
                #endregion
            }

            app.UseRouting();
<<<<<<< HEAD
            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
=======

>>>>>>> 7fdb0c8c89466031244ac1949b4ec0895781f6c8
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
