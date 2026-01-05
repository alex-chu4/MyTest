using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Extentions;
using Utility.Model;

namespace Utility.Token
{
    /// <summary>
    /// Defines the <see cref="IJwtServiceEx" />
    /// </summary>
    public interface IJwtServiceEx
    {
        /// <summary>
        /// 透過cookie的token取得使用者資料
        /// </summary>
        /// <returns>The <see cref="Token_User_InfoModel"/></returns>
        Token_User_InfoModel GetUserInfo(string token);

        /// <summary>
        /// 檢查access token
        /// </summary>
        /// <returns></returns>
        int CheckToken(string token);

        /// <summary>
        /// 移除Cache資料
        /// </summary>
        string RealeaseToken(string token);

        /// <summary>
        /// The GeneratorToken
        /// </summary>
        /// <param name="userinfo">The userinfo<see cref="Token_User_InfoModel"/></param>
        /// <returns>token <see cref="TokenModel"/></returns>
        string GenToken(Token_User_InfoModel tokenUserInfo, int time = 0);

        /// <summary>
        /// 重新設定Token
        /// </summary>
        /// <param name="tokenUserInfo">tokenUserInfo</param>
        /// <returns>token</returns>
        string ResetToken(string token, int time = 0);
    }

    public class JwtServiceEx : IJwtServiceEx
    {
        private static JwtTokenHelper tokenHelper = new JwtTokenHelper(JwtService.Issuer, JwtService.Audience, JwtService.KEY);

        private static int expireTime = int.Parse(ConfigurationManager.AppSettings["Exprie_Refresh"]);

        /// <summary>
        /// client認可的發行者
        /// </summary>
        public static string Issuer { get; set; } = ConfigurationManager.AppSettings["Issuer"];

        /// <summary>
        /// client的domain
        /// </summary>
        public static string Audience { get; set; } = "TWNEC";

        /// <summary>
        /// client取得的token key
        /// </summary>
        public static string KEY { get; set; } = ConfigurationManager.AppSettings["KEY"];

        /// <summary>
        /// 透過cookie的token取得使用者資料
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>The <see cref="Token_User_InfoModel"/></returns>
        public Token_User_InfoModel GetUserInfo(string token)
        {
            // 透過accesstoken取得Refreashtoken
            if (string.IsNullOrEmpty(token))
                return null;

            JwtTokenModel jwtToken = tokenHelper.DeCode(token);
            var userinfo = jwtToken.Payload["UserInfo"];
            var payload = userinfo as Dictionary<string, object>;
            Token_User_InfoModel token_User_InfoModel = new Token_User_InfoModel();
            token_User_InfoModel.USERID = payload["USERID"] != null ? payload["USERID"].ToString() : string.Empty;
            token_User_InfoModel.ACCOUNT_TYPE = payload["ACCOUNT_TYPE"] != null ? payload["ACCOUNT_TYPE"].ToString() : string.Empty;
            token_User_InfoModel.NAME = payload["NAME"] != null ? payload["NAME"].ToString() : string.Empty;
            token_User_InfoModel.EMAIL = payload["EMAIL"] != null ? payload["EMAIL"].ToString() : string.Empty;
            token_User_InfoModel.COMPANY = payload["COMPANY"] != null ? payload["COMPANY"].ToString() : string.Empty;
            token_User_InfoModel.USER_OID = payload["USER_OID"] != null ? payload["USER_OID"].ToString() : string.Empty;
            token_User_InfoModel.DEPARTMENT = payload["DEPARTMENT"] != null ? payload["DEPARTMENT"].ToString() : string.Empty;
            token_User_InfoModel.TITLE = payload["TITLE"] != null ? payload["TITLE"].ToString() : string.Empty;
            token_User_InfoModel.GROUP_CODE = payload["GROUP_CODE"] != null ? payload["GROUP_CODE"].ToString() : string.Empty;
            token_User_InfoModel.ROLE_CODE = payload["ROLE_CODE"] != null ? payload["ROLE_CODE"].ToString() : string.Empty;
            token_User_InfoModel.EOC_USER_NAME = payload["EOC_USER_NAME"] != null ? payload["EOC_USER_NAME"].ToString() : string.Empty;
            token_User_InfoModel.EOC_USER_TEL = payload["EOC_USER_TEL"] != null ? payload["EOC_USER_TEL"].ToString() : string.Empty;
            token_User_InfoModel.EOC_USER_EMAIL = payload["EOC_USER_EMAIL"] != null ? payload["EOC_USER_EMAIL"].ToString() : string.Empty;
            token_User_InfoModel.EOC_ID = payload["EOC_ID"] != null ? payload["EOC_ID"].ToString() : string.Empty;
            token_User_InfoModel.EOC_ID_ALL = payload["EOC_ID_ALL"] != null ? payload["EOC_ID_ALL"].ToString() : string.Empty;
            token_User_InfoModel.EOC_NAME = payload["EOC_NAME"] != null ? payload["EOC_NAME"].ToString() : string.Empty;
            token_User_InfoModel.EOC_LEVEL = payload["EOC_LEVEL"] != null ? payload["EOC_LEVEL"].ToString() : string.Empty;
            token_User_InfoModel.EOC_CITY_ID = payload["EOC_CITY_ID"] != null ? payload["EOC_CITY_ID"].ToString() : string.Empty;
            token_User_InfoModel.EOC_DEP_CODE = payload["EOC_DEP_CODE"] != null ? payload["EOC_DEP_CODE"].ToString() : string.Empty;
            token_User_InfoModel.PRJ_NO = payload["PRJ_NO"] != null ? payload["PRJ_NO"].ToString() : string.Empty;
            token_User_InfoModel.PRJ_NAME = payload["PRJ_NAME"] != null ? payload["PRJ_NAME"].ToString() : string.Empty;
            token_User_InfoModel.PRJ_DIS_DATA_ID = payload["PRJ_DIS_DATA_ID"] != null ? payload["PRJ_DIS_DATA_ID"].ToString() : string.Empty;
            token_User_InfoModel.PRJ_DIS_OPEN_LV = payload["PRJ_DIS_OPEN_LV"] != null ? payload["PRJ_DIS_OPEN_LV"].ToString() : string.Empty;
            token_User_InfoModel.ORG_OID = payload["ORG_OID"] != null ? payload["ORG_OID"].ToString() : string.Empty;
            token_User_InfoModel.ORG_OID_P = payload["ORG_OID_P"] != null ? payload["ORG_OID_P"].ToString() : string.Empty;
            token_User_InfoModel.ORG_OID_G = payload["ORG_OID_G"] != null ? payload["ORG_OID_G"].ToString() : string.Empty;
            token_User_InfoModel.ORG_ID = payload["ORG_ID"] != null ? payload["ORG_ID"].ToString() : string.Empty;
            token_User_InfoModel.ORG_ID_P = payload["ORG_ID_P"] != null ? payload["ORG_ID_P"].ToString() : string.Empty;
            token_User_InfoModel.ORG_ID_G = payload["ORG_ID_G"] != null ? payload["ORG_ID_G"].ToString() : string.Empty;

            return token_User_InfoModel;
        }

        /// <summary>
        /// 檢查access token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>error code : 0 is successful, otherwise is fault</returns>
        public int CheckToken(string token)
        {
            ReturnModel rtn = new ReturnModel();

            if (string.IsNullOrEmpty(token))
            {
                // token is null or empty
                return -99;
            }

            rtn = tokenHelper.CheckEx(token, true);
            if (rtn.Data.Equals(JwtTokenHelper.JwtStatus.None))
            {
                // token is ok
                return 0;
            }

            // token error
            return -1;
        }

        /// <summary>
        /// 移除Cache資料
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>release token</returns>
        public string RealeaseToken(string token)
        {
            string rtnToken = ResetToken(token, -1);
            return rtnToken;
        }

        /// <summary>
        /// The GeneratorToken
        /// </summary>
        /// <param name="tokenUserInfo">The userinfo<see cref="Token_User_InfoModel"/></param>
        /// <param name="time">expire time. if time = 0, set expire_time(declare in web.config) </param>
        /// <returns>token <see cref="TokenModel"/></returns>
        public string GenToken(Token_User_InfoModel tokenUserInfo, int time = 0)
        {
            string token = string.Empty;
            if (tokenUserInfo == null)
            {
                return string.Empty;
            }

            if (time == 0)
            {
                Dictionary<string, object> payload_token = new Dictionary<string, object>() {
                    { "exp", DateTime.Now.AddMinutes(expireTime).ToUnixExpires() },
                    { "UserInfo", tokenUserInfo }
                };

                AppendPayLoad(ref payload_token);

                token = tokenHelper.EnCode(payload_token);
            }
            else
            {
                Dictionary<string, object> payload_token = new Dictionary<string, object>() {
                    { "exp", DateTime.Now.AddMinutes(time).ToUnixExpires() },
                    { "UserInfo", tokenUserInfo }
                };

                AppendPayLoad(ref payload_token);

                token = tokenHelper.EnCode(payload_token);
            }

            return token;
        }

        /// <summary>
        /// 重新設定Token
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="time">default is 0</param>
        /// <returns>string</returns>
        public string ResetToken(string token, int time = 0)
        {
            if (string.IsNullOrEmpty(token))
            {
                // token is null or empty
                return string.Empty;
            }

            Token_User_InfoModel model = GetUserInfo(token);
            string newToken = GenToken(model, time);

            return newToken;
        }

        /// <summary>
        /// 重新設定Token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>string</returns>
        public string ReSetToken(Token_User_InfoModel model)
        {
            string newToken = GenToken(model);

            return newToken;
        }

        /// <summary>
        /// 替PayLoad新增驗證資料
        /// </summary>
        /// <param name="payload"></param>
        private void AppendPayLoad(ref Dictionary<string, object> payload)
        {
            payload.Add("iss", Issuer);
            payload.Add("aud", Audience);
            payload.Add("iat", DateTime.Now.ToUnixExpires());
        }
    }
}
