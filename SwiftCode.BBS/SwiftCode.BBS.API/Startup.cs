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
                    Description = "框架说明文档",
                    Contact = new OpenApiContact
                    {
                        Name = "SwiftCode",
                        Email="SwiftCode@xxx.com",
                    }
                }) ;

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath,"SwiftCode.BBS.API.xml");
                c.IncludeXmlComments(xmlPath, true);

                var xmlModelPath = Path.Combine(basePath, "SwiftCode.BBS.Model.xml"); //Model层的xml文件名
                c.IncludeXmlComments(xmlModelPath);
<<<<<<< HEAD

                // 开启小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {

                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
            #endregion

            // 认证
            services.AddAuthentication(x =>
            {
                // 仔细看这个单词 上图中错误的提示里的那个
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o => {

                //读取配置文件
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
                    //ValidIssuer = audienceConfig["Issuer"],//发行人
                    ValidIssuer = iss,//发行人
                    ValidateAudience = true,
                    //ValidAudience = audienceConfig["Audience"],//订阅人
                    ValidAudience = audienceConfig,//订阅人
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                    RequireExpirationTime = true,
                };
            });

            // 1【授权】、这个和上边的异曲同工，好处就是不用在controller中，写多个 roles 。
            // 然后这么写 [Authorize(Policy = "Admin")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());//单独角色
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));//或的关系
                options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));//且的关系
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
            // 先开启认证
            app.UseAuthentication();
            // 然后是授权中间件
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
