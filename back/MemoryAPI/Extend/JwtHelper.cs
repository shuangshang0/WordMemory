using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Extend
{
    public class JwtHelper
    {
        //option注入
        private readonly IConfiguration _configuration;
 
    public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 创建jwttoken
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public string CreateToken(string username)
        {
            // 1. 定义需要使用到的Claims
            var claims = new[]
            {
                new Claim("username", username),
           //new Claim(ClaimTypes.Name, "u_admin"),
           //HttpContext.User.Identity.Name
           //new Claim(ClaimTypes.Role, "r_admin"),
           //HttpContext.User.IsInRole("r_admin")
           //new Claim(JwtRegisteredClaimNames.Jti, "admin"),
            //new Claim("User", "Admin"),
            //new Claim("Name", "超级管理员")
            };

            // 2. 从 appsettings.json 中读取SecretKey 密匙并以utf8编码字节输出
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            // 3. 选择加密算法 
            var algorithm = SecurityAlgorithms.HmacSha256;

            // 4. 生成Credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);

            // 5. 根据以上，生成token
            var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],     //Issuer 发布者
            _configuration["Jwt:Audience"],   //Audience 接受者
            claims,                          //Claims, 存放的用户信息
            DateTime.Now,                    //notBefore 发布时间
            DateTime.Now.AddHours(4),    //expires 有效期设置
            signingCredentials               //Credentials 数字签名
        );

            // 6. 将token变为string 生成字符串token
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
        /// <summary>
        /// 获取token里username的值
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public static string GetUsername(string token)
        {
            //解析payload，拿到username
            var handler = new JwtSecurityTokenHandler();
            var payload = handler.ReadJwtToken(token).Payload;
            var claims = payload.Claims;
            return claims.First(claim => claim.Type == "username").Value;
        }
    }
}
