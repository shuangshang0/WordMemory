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
//����swagger
builder.Services.AddSwaggerGen(s =>
{
    //��Ӱ�ȫ����
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "������token,��ʽΪ Bearer xxxxxxxx��ע���м�����пո�",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    //��Ӱ�ȫҪ��
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
ValidateIssuer = true, //�Ƿ���֤Issuer
ValidIssuer = configuration["Jwt:Issuer"], //������Issuer
ValidateAudience = true, //�Ƿ���֤Audience
ValidAudience = configuration["Jwt:Audience"], //������Audience
ValidateIssuerSigningKey = true, //�Ƿ���֤SecurityKey
IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])), //SecurityKey
ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
ClockSkew = TimeSpan.FromSeconds(30), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
RequireExpirationTime = true,
};
});
//
//crus����
builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));//���з��ʵ�ǰ��url��ַ
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
//�����м����UseAuthentication����֤����������������Ҫ�����֤���м��ǰ���ã����� UseAuthorization����Ȩ����
app.UseAuthentication();//��������ʱ�򣬰������е�token��session��cookie������ȡ���û���Ϣ
app.UseAuthorization();//��Ȩ �õ��û���Ϣ�����ж��û��Ƿ���Է��ʵ�ǰ��Դ
app.MapControllers();
app.Run();
