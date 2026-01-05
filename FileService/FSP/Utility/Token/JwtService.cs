///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  JwtService.cs
//  程式名稱：
//  提供給每個系統使用token的模組
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  小柯             2019/04/18       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供給每個系統使用token的模組。
///////////////////////////////////////////////////////////////////////////////////////

namespace Utility.Token
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using Utility.Cache;
    using Utility.Cookie;
    using Utility.EMIC2;
    using Utility.Extentions;
    using Utility.Model;

    /// <summary>
    /// Defines the <see cref="IJwtService" />
    /// </summary>
    public interface IJwtService
    {
        string Token { get; set; }

        /// <summary>
        /// 透過cookie的access取得使用者資料
        /// </summary>
        /// <returns>The <see cref="Token_User_InfoModel"/></returns>
        Token_User_InfoModel GetUserInfo();

        /// <summary>
        /// 檢查access & refresh token
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        ReturnModel CheckToken();

        /// <summary>
        /// 將Token寫入Cache
        /// </summary>
        /// <param name="accresstoken">The accresstoken<see cref="string"/></param>
        /// <param name="refreshtoken">The refreshtoken<see cref="string"/></param>
        bool SetRefreshToken(string accresstoken, string refreshtoken);
        bool SetRefreshToken(string accresstoken, Token_User_InfoModel info);

        /// <summary>
        /// 移除Cache資料
        /// </summary>
        void RemoveToken();

        /// <summary>
        /// The GeneratorToken
        /// </summary>
        /// <param name="userinfo">The userinfo<see cref="Token_User_InfoModel"/></param>
        /// <returns>The <see cref="TokenModel"/></returns>
        //TokenModel GeneratorToken(Token_User_InfoModel userinfo);

        TokenModel GenToken2Cache(Token_User_InfoModel tokenUserInfo);

        /// <summary>
        /// 重新設定Token
        /// </summary>
        /// <param name="tokenUserInfo"></param>
        /// <returns></returns>
        // TokenModel ReSetToken2Cache(Token_User_InfoModel tokenUserInfo);

        TokenModel GetTokenModel();
    }

    /// <summary>
    /// Defines the <see cref="JwtService" />
    /// </summary>
    public class JwtService : IJwtService
    {
        public enum CheckStatus
        {
            None,
            Ok,
            AccessTokenError,
            RefreshTokenError
        }

        public string Token { get; set; }
        //add by david
        private int ExpiresAtMinutes { get; set; } = int.Parse(ConfigurationManager.AppSettings["Exprie_Refresh"]);
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
        /// Defines the Cache
        /// </summary>
        private ICache cache;

        private ITokenHelper tokenHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService"/> class.
        /// </summary>
        /// <param name="cache">The cache<see cref="ICache"/></param>
        /// <param name="token"></param>
        public JwtService(ICache cache,ITokenHelper tokenHelper)
        {
            this.cache = cache;
            this.tokenHelper = tokenHelper;
        }

        public TokenModel GetTokenModel()
        {
            TokenModel model = new TokenModel();
            model.AccessToken = Token;
            model.RefreshToken = cache.Get(Token);
            return model;
        }

        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        /// <param name="token">The accesstoken<see cref="string"/></param>
        /// <returns></returns>
        public Token_User_InfoModel GetUserInfo()
        {
            // 透過accesstoken取得Refreashtoken
            if (string.IsNullOrEmpty(Token))
                return null;

            string refreshtoken = cache.Get(Token);
            if (string.IsNullOrEmpty(refreshtoken))
            {
                return null;
            }

            JwtTokenModel jwtToken = this.tokenHelper.DeCode(refreshtoken);
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
        /// 檢查access token 、refresh token是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ReturnModel CheckToken()
        {
            ReturnModel rtn = new ReturnModel();
            ReturnModel rtn_r = new ReturnModel();

            if (string.IsNullOrEmpty(Token))
            {
                rtn.Data = CheckStatus.AccessTokenError;
                return rtn;
            }

            rtn = CheckAccessToken();

            // Accesstoken驗證未過
            if (!rtn.Status)
            {
                rtn.Data = CheckStatus.AccessTokenError;
                return rtn;
            }

            //rtn_r = CheckRefreshToken();

            //// RefeshToken驗證未過
            //if (!rtn_r.Status)
            //{
            //    rtn_r.Data = CheckStatus.RefreshTokenError;
            //    return rtn_r;
            //}

            return rtn;
        }

        /// <summary>
        /// 將Token寫入Cache
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="refreshtoken">refreshtoken</param>
        public bool SetRefreshToken(string token, string refreshtoken)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshtoken))
                return false;
            return cache.Set(token, refreshtoken);
        }

        /// <summary>
        /// 將Token寫入Cache
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="userinfo">userinfo</param>
        public bool SetRefreshToken(string token, Token_User_InfoModel userinfo)
        {
            if (string.IsNullOrEmpty(token))
                return false;
            if (userinfo == null || string.IsNullOrEmpty(userinfo.USERID))
                return false;
            return cache.Set(token, GeneratorRefreshToken(userinfo));
        }

        /// <summary>
        /// The RemoveToken
        /// </summary>
        public void RemoveToken()
        {
            // 刪除對照表
            if (string.IsNullOrEmpty(Token) == false)
            {
                cache.Remove(Token);
            }

            Log.Debug("Remove Token [" + Token + "]");
            CookieService.Remove("expire_time");
            CookieService.Remove("token");
        }

        /// <summary>
        /// 產生 TokenModel
        /// </summary>
        /// <param name="tokenUserInfo">tokenUserInfo</param>
        /// <returns>TokenModel</returns>
        public TokenModel GenToken2Cache(Token_User_InfoModel tokenUserInfo)
        {
            // 產生token
            TokenModel token = GeneratorToken(tokenUserInfo);

            try
            {
                // 將token寫入快取
                if (SetRefreshToken(token.AccessToken, token.RefreshToken) == false)
                    return null;

                if (string.IsNullOrEmpty(cache.Get(token.AccessToken)) == false)
                {
                    // 將Access Token 寫入 Cookie
                    // CookieService.Set("token", token.AccessToken, ExpiresAtMinutes);
                    CookieService.Set("token", token.AccessToken);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return token;
        }

        /// <summary>
        /// 刪除現有token並重新產生token
        /// </summary>
        /// <param name="tokenUserInfo">tokenUserInfo</param>
        /// <returns>TokenModel</returns>
        //public TokenModel ReSetToken2Cache(Token_User_InfoModel tokenUserInfo)
        //{
        //    if (tokenUserInfo == null || string.IsNullOrEmpty(tokenUserInfo.USERID))
        //        return null;
        //    // 刪除原有的token資料
        //    CookieService.Remove("token");
        //    RemoveToken();

        //    // 產生token
        //    TokenModel token = GenToken2Cache(tokenUserInfo);

        //    return token;
        //}

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

        /// <summary>
        /// 檢查 Access Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private ReturnModel CheckAccessToken()
        {
            ReturnModel rtn = new ReturnModel();

            if (string.IsNullOrEmpty(Token))
                return rtn;

            return this.tokenHelper.Check(Token, false);
        }

        /// <summary>
        /// 檢查Refresh Token是否存活
        /// </summary>
        /// <param name="token">accesstoken</param>
        /// <returns></returns>
        private ReturnModel CheckRefreshToken()
        {
            ReturnModel rtn = new ReturnModel();

            string refreshtoken = cache.Get(Token);
            rtn = this.tokenHelper.Check(refreshtoken, false);


            return rtn;
        }

        /// <summary>
        /// 產出Token，提供給第一次登入、Access Token的使用者更換
        /// </summary>
        /// <param name="userinfo">使用者資訊</param>
        /// <returns></returns>
        private TokenModel GeneratorToken(Token_User_InfoModel userinfo)
        {
            if (userinfo == null || string.IsNullOrEmpty(userinfo.USERID)) return null;
            // 產出Access Token ，10分鐘
            // UserInfoLess 用於確認該使用者是誰，盡量少
            Dictionary<string, object> payload = new Dictionary<string, object>()
            {
                //{ "exp",  DateTime.Now.AddMinutes(int.Parse(ConfigurationManager.AppSettings["Exprie"])).ToUnixExpires() },
                { "exp",  DateTime.Now.AddMinutes(int.Parse(ConfigurationManager.AppSettings["Exprie_Refresh"])).ToUnixExpires() },
                { "exp_timer", DateTime.Now.AddMinutes(int.Parse(ConfigurationManager.AppSettings["Exprie_Refresh"])).ToUnixExpires() },
                { "GUID", Guid.NewGuid() },
                { "USETIME", DateTime.Now.ToString("yyyyMMddHHmmss.fffffffK") },
                //{ "USERID", userinfo.USERID },
                //{ "NAME", userinfo.NAME },
                //{ "COMPANY", userinfo.COMPANY },
                //{ "USER_OID", userinfo.USER_OID },
                //{ "DEPARTMENT", userinfo.DEPARTMENT },
                //{ "TITLE", userinfo.TITLE },
            };

            AppendPayLoad(ref payload);

            string accesstoken = this.tokenHelper.EnCode(payload);

            return new TokenModel { AccessToken = accesstoken, RefreshToken = GeneratorRefreshToken(userinfo) };
        }

        private string GeneratorRefreshToken(Token_User_InfoModel userinfo)
        {
            // 產出Refresh Token (30分鐘)
            // 完整使用者資料，提供更換Token使用

            Dictionary<string, object> payload_Refresh = new Dictionary<string, object>() {
                { "exp", DateTime.Now.AddMinutes(int.Parse(ConfigurationManager.AppSettings["Exprie_Refresh"]) + 5).ToUnixExpires() },
                { "UserInfo", userinfo }
            };

            AppendPayLoad(ref payload_Refresh);

            string refreshtoken = this.tokenHelper.EnCode(payload_Refresh);
            return refreshtoken;
        }


    }
}
