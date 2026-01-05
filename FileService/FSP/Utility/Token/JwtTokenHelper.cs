using Jose;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;
using System.Configuration;
using Utility.Extentions;
using System.Security.Claims;
using Utility.EMIC2;
using EMIC2.Models.Interface;
using EMIC2.Models;
using EMIC2.Models.Repository;

namespace Utility.Token
{
    /// <summary>
    /// JWTToken元件
    /// </summary>
    public class JwtTokenHelper : ITokenHelper
    {

        public enum JwtStatus
        {
            /// <summary>
            /// 無
            /// </summary>
            None,

            /// <summary>
            /// Token過期
            /// </summary>
            expired,

            /// <summary>
            /// 錯誤
            /// </summary>
            Error
        }
        private string Issuer { get; set; } = string.Empty;
        private string Audience { get; set; } = string.Empty;
        private string Key { get; set; } = string.Empty;

        //// 取得Web.config中定義的URI位置
        ///// <summary>
        ///// 發布者
        ///// </summary>
        //public static string Issuer = ConfigurationManager.AppSettings["Issuer"];

        ///// <summary>
        ///// 訂閱者
        ///// </summary>
        //public static string Audience = ConfigurationManager.AppSettings["Audience"];

        ///// <summary>
        ///// Token KEY
        ///// </summary>
        //public static string Key = ConfigurationManager.AppSettings["KEY"];

        /// <summary>
        /// JwtTokenService 建構式
        /// </summary>
        /// <param name="issuser">發行者</param>
        /// <param name="audience">訂閱者</param>
        /// <param name="key">token Key</param>
        public JwtTokenHelper(string issuser,string audience,string key)
        {
            Issuer = issuser;
            Audience = audience;
            Key = key;
        }

        /// <summary>
        /// 將資料加密
        /// </summary>
        /// <param name="payload">UserInfo</param>
        /// <returns>TokenString</returns>
        public string EnCode(Dictionary<string, object> payload)
        {
            //產生token
            return JWT.Encode(payload, Encoding.UTF8.GetBytes(Key), JwsAlgorithm.HS512);
        }

        /// <summary>
        /// token 解密
        /// </summary>
        /// <param name="token">Token字串</param>
        /// <returns>JWT物件</returns>
        public JwtTokenModel DeCode(string token)
        {
            // 檢查CheckParam是否有帶入
            JwtTokenModel itoken = new JwtTokenModel();
            if (string.IsNullOrEmpty(token)) return null;

            try
            {
                Dictionary<string, object> jwttoken = JWT.Decode<Dictionary<string, object>>(token, Encoding.UTF8.GetBytes(Key), JwsAlgorithm.HS512);
                itoken.Payload = jwttoken;
                itoken.Audience = jwttoken["aud"].ToString();
                itoken.Issuer = jwttoken["iss"].ToString();
                itoken.Expire = jwttoken["exp"].ToString();
                itoken.Value = token;
                itoken.Message = "ok";
            }
            catch
            {
                return null;
            }

            return itoken;
        }

        /// <summary>
        /// 檢查Token正確性
        /// </summary>
        /// <param name="tokenstr">token字串</param>
        /// <param name="bCheckTime">是否檢查token時間</param>
        /// <returns>回傳檢查結果</returns>
        public ReturnModel Check(string tokenstr, bool bCheckTime)
        {
            ReturnModel rtn = new ReturnModel();

            if (string.IsNullOrEmpty(tokenstr))
            {
                Log.Debug("token string is null ...");
                return new ReturnModel()
                {
                    Message = "請輸入token",
                    Status = false
                };
            }

            IRepository<SSO2_USER_TOKEN> repository = new GenericRepository<SSO2_USER_TOKEN>();
            var user = repository.Get(o => o.TOKEN == tokenstr);
            if (user == null)
            {
                Log.Fatal("token not found in DB : " + tokenstr);
                return new ReturnModel()
                {
                    Message = "token not found in DB",
                    Status = false
                };
            }

            //tokenstr = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1NTU5MjE0OTgsIlVTRVJfSUQiOiJIT1UiLCJpc3MiOiJUV05FQyIsImF1ZCI6WyJodHRwOi8vU1NPLmNvbS50dy8iXSwiaWF0IjoxNTU1OTIxNDM4fQ.kZMD3xuUlVZ5wILjNgn_Lv-OzdXDu6ve32qjC3wFcN8TyLPqNZx77dO_Czo8DF8FMBwibAjf7m4CIYQ6NRPSPg";

            return ValidToken(tokenstr, bCheckTime);
        }

        /// <summary>
        /// 檢查Token正確性for外部系統
        /// </summary>
        /// <param name="tokenstr">token字串</param>
        /// <param name="bCheckTime">是否檢查token時間</param>
        /// <returns>回傳檢查結果</returns>
        public ReturnModel CheckEx(string tokenstr, bool bCheckTime)
        {
            ReturnModel rtn = new ReturnModel();

            if (string.IsNullOrEmpty(tokenstr))
            {
                Log.Debug("token string is null ...");
                return new ReturnModel()
                {
                    Message = "請輸入token",
                    Status = false
                };
            }

            return ValidToken(tokenstr, bCheckTime);
        }

        private ReturnModel ValidToken(string tokenstr, bool bCheckTime)
        {
            ReturnModel rtn = new ReturnModel();
            //驗證參數
            TokenValidationParameters validationParameters = null;
            if (bCheckTime)
            {
                validationParameters = new TokenValidationParameters()
                {
                    ClockSkew = new TimeSpan(0),
                    ValidIssuer = Issuer,                       //除了SSO必須設定正確的發性者
                    ValidAudience = Audience,                     //Client 驗證訂閱者
                    ValidateLifetime = true,                //驗證Token存活時間
                    ValidateAudience = false,                //驗證訂閱者
                    ValidateIssuer = true,                  //驗證發行者
                    ValidateIssuerSigningKey = true,        //驗證KEY
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)),//拿到SecurityKey
                };
            }
            else
            {
                validationParameters = new TokenValidationParameters()
                {
                    ClockSkew = new TimeSpan(0),
                    ValidIssuer = Issuer,                       //除了SSO必須設定正確的發性者
                    ValidAudience = Audience,                     //Client 驗證訂閱者
                    ValidateLifetime = false,                //驗證Token存活時間
                    ValidateAudience = false,                //驗證訂閱者
                    ValidateIssuer = true,                  //驗證發行者
                    ValidateIssuerSigningKey = true,        //驗證KEY
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)),//拿到SecurityKey
                };
            }

            try
            {
                //System.IdentityModel.Tokens.JwtSecurityToken
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(tokenstr);

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                SecurityToken validatedToken = null;

                tokenHandler.ValidateToken(tokenstr, validationParameters, out validatedToken);

                rtn.Status = true;
                rtn.Message = "驗證成功";
                rtn.Data = JwtStatus.None;

            }
            catch (SecurityTokenException ex)
            {
                Log.Error(string.Format("{0}[{1}]", "SecurityTokenException", tokenstr), ex);
                if (bCheckTime)
                {
                    // TODO 這邊必須知道驗證錯誤是時間過期，寫在Message。
                    JwtTokenModel token = this.DeCode(tokenstr);
                    if (token != null)
                    {
                        string exp = token.Payload["exp"].ToString();
                        var currentTime = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                        if (long.Parse(exp) - currentTime < 0)
                        {
                            Log.Error(string.Format("{0}[{1}]", "SecurityTokenException", "time out"), ex);
                            rtn.Data = JwtStatus.expired;
                            rtn.Message = ex.Message;
                        }
                    }
                }

                //驗證失敗會往這邊跑，返回失敗原因
                rtn.Message = ex.Message;
                rtn.Data = JwtStatus.Error;
            }
            catch (Exception e)
            {
                Log.Error(string.Format("{0}[{1}]", "Token Exception", tokenstr), e);
                //應用程式錯誤
                rtn.Message = e.Message;
                rtn.Data = JwtStatus.Error;
            }

            return rtn;
        }
    }
}
