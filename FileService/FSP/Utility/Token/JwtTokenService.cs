///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  JwtTokenService.cs
//  程式名稱：
//  JWT公用方法
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  小柯             2019/03/19       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  JWT公用方法。
///////////////////////////////////////////////////////////////////////////////////////

namespace Utility.Token
{
    using System.Collections.Generic;
    using System.Web;
    using Utility.Cache;
    using Utility.Cookie;
    using Utility.Extentions;
    using Utility.Model;

    /// <summary>
    /// Defines the <see cref="JwtTokenService" />
    /// </summary>
    public class JwtTokenService
    {

        /// <summary>
        /// Defines the itoken
        /// </summary>
        internal ITokenHelper Itoken;

        /// <summary>
        /// Defines the icache
        /// </summary>
        internal ICache Icache;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenService"/> class.
        /// </summary>
        /// <param name="ihelper">The helper<see cref="ITokenHelper"/></param>
        /// <param name="icache">The cache<see cref="ICache"/></param>
        public JwtTokenService(ITokenHelper itoken, ICache icache)
        {
            Itoken = itoken;
            Icache = icache;
        }

        /// <summary>
        /// 解析JWT Token資料轉物件
        /// </summary>
        /// <returns>The <see cref="TOKEN_USER_INFO"/></returns>
        public Token_User_InfoModel GetTokenUserInfo(string tokenstr)
        {
            JwtTokenModel jwtTokenModel = Itoken.DeCode(tokenstr);

            Dictionary<string, object> payload = jwtTokenModel.Payload;

            return new Token_User_InfoModel
            {
                EMAIL = payload["EMAIL"].ToString(),
                EOC_ID = payload["EOC_CODE"].ToString(),
                PRJ_NO = payload["PRJ_NO"].ToString(),
                EOC_USER_EMAIL = payload["EOC_USER_EMAIL"].ToString(),
                EOC_USER_NAME = payload["EOC_USER_NAME"].ToString(),
                EOC_USER_TEL = payload["EOC_USER_TEL"].ToString(),
                GROUP_CODE = payload["GROUP_CODE"].ToString(),
                NAME = payload["NAME"].ToString(),
                ORG_ID = payload["ORG_ID"].ToString(),
                ORG_OID = payload["ORG_OID"].ToString(),
                ROLE_CODE = payload["ROLE_CODE"].ToString(),
                USERID = payload["USERID"].ToString(),
                USER_OID = payload["USER_OID"].ToString()
            };
        }

        /// <summary>
        /// 提供給SSO2站台登出
        /// </summary>
        /// <param name="accesstoken">accesstoken</param>
        public void LogoutSSO2()
        {
            string token = CookieService.GetValue("token");

            // 刪除對照表
            Icache.Remove(token);

            CookieService.Remove("USERID");
            CookieService.Remove("token");
            CookieService.Remove("backurl");
        }

        /// <summary>
        /// 產出Token，提供給第一次登入、Access Token的使用者更換
        /// </summary>
        /// <returns></returns>
        public TokenModel GeneratorToken()
        {
            // 產出Access Token ，10分鐘
            // UserInfoLess 用於確認該使用者是誰，盡量少
            Dictionary<string, object> payload = new Dictionary<string, object>()
            {
                { "iss", JwtEnCodeParamModel.Issuer },
                { "aud", JwtEnCodeParamModel.Audience },
                { "iat", JwtEnCodeParamModel.Issued.ToUnixExpires().ToString() },
                { "exp", JwtEnCodeParamModel.Exprie.ToUnixExpires().ToString() },
                { "userid", "123" }
            };

            string accesstoken = Itoken.EnCode(payload);

            // 產出Refresh Token (30分鐘)
            // 完整使用者資料，提供更換Token使用
            object userInfoObject = new { };
            Dictionary<string, object> payload_Refresh = new Dictionary<string, object>() {
                { "iss", JwtEnCodeParamModel.Issuer },
                { "aud", JwtEnCodeParamModel.Audience },
                { "iat", JwtEnCodeParamModel.Issued.ToUnixExpires().ToString() },
                { "exp", JwtEnCodeParamModel.Exprie_Refresh.ToUnixExpires().ToString() },
                { "UserInfoFull", userInfoObject }
            };

            string refreshtoken = Itoken.EnCode(payload);

            // 將Refresh Token寫入Session Server
            HttpContext.Current.Session["RefeshToken"] = refreshtoken;

            return new TokenModel { AccessToken = accesstoken, RefreshToken = refreshtoken };
        }

        //public TokenModel GeneratorRefreshToken(string accesstoken ,string refreshtoken)
        //{
        //    // Accesstoken必填
        //    if (!string.IsNullOrEmpty(accesstoken))
        //        return new TokenModel();

        //    // 檢查 Accesstoken正確性
        //    if (CheckAccessToken(accesstoken))
        //    {
        //        CheckRefreshToken(refreshtoken);
        //    }
        //    else
        //    {
        //        return new TokenModel();
        //    }
        //}

        /// <summary>
        /// 檢查 Access Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CheckAccessToken(string token)
        {
            ReturnModel rtn = new ReturnModel();
            rtn = Itoken.Check(token);
            return rtn.Status;
        }

        /// <summary>
        /// 檢查Refresh Token是否存活
        /// </summary>
        /// <param name="accesstoken">accesstoken</param>
        /// <param name="refreshtoken">refreshtoken</param>
        /// <returns></returns>
        public bool CheckRefreshToken(string accesstoken, string refreshtoken)
        {
            // 檢查Access Token 是否有在Redis之中，並且refreshtoken 必須相同。
            if (!string.IsNullOrEmpty(accesstoken))
            {
                string redis_refresh = Icache.Get(accesstoken);
                return redis_refresh == refreshtoken ? true : false;
            }

            return false;
        }

        ///// <summary>
        ///// 將Refresh
        ///// </summary>
        ///// <param name="key">The key<see cref="string"/></param>
        ///// <param name="value">The value<see cref="string"/></param>
        //private void SetRefreshToken2Cache(string key, string value)
        //{
        //    // Refresh Token寫入Reids
        //    Icache.Set(key, value);
        //}

        ///// <summary>
        ///// 刪除Refresh Token
        ///// </summary>
        ///// <param name="key"></param>
        //private void RemoveRefreshTokenfromCache(string key)
        //{
        //    Icache.Remove(key);
        //}


    }
}
