using Extend;
using Microsoft.AspNetCore.Authentication.JwtBearer;//
using Microsoft.IdentityModel.Tokens;//
using Microsoft.OpenApi.Models;
using System.Text;//
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;//

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//配置swagger
builder.Services.AddSwaggerGen(s =>
{
    //添加安全定义
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    //添加安全要求
    s.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme{
                Reference =new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                }
            },new string[]{ }
        }
    });
});

//jwt
builder.Services.AddSingleton(new JwtHelper(configuration));
builder.Services.AddAuthentication(options =>
{
options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
options.TokenValidationParameters = new TokenValidationParameters()
{
ValidateIssuer = true, //是否验证Issuer
ValidIssuer = configuration["Jwt:Issuer"], //发行人Issuer
ValidateAudience = true, //是否验证Audience
ValidAudience = configuration["Jwt:Audience"], //订阅人Audience
ValidateIssuerSigningKey = true, //是否验证SecurityKey
IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])), //SecurityKey
ValidateLifetime = true, //是否验证失效时间
ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
RequireExpirationTime = true,
};
});
//
//crus跨域
builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));//运行访问的前端url地址
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
//调用中间件：UseAuthentication（认证），必须在所有需要身份认证的中间件前调用，比如 UseAuthorization（授权）。
app.UseAuthentication();//请求来的时候，把请求中的token、session、cookie做解析取出用户信息
app.UseAuthorization();//授权 得到用户信息用来判断用户是否可以访问当前资源
app.MapControllers();
app.Run();
