///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  TokenFactory.cs
//  程式名稱：
//  TokenService 工廠
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  小柯             2019/04/22       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  TokenFactory。
///////////////////////////////////////////////////////////////////////////////////////
namespace Utility.Token
{
    using Utility.Cache;
    using Utility.Cookie;

    public class TokenFactory
    {
        private IJwtService jwtService;

        /// <summary>
        /// 取得TokenService物件
        /// </summary>
        /// <returns></returns>
        public IJwtService GetService()
        {

            // 設置初始化的被裝飾者 Redis 並且設定第一個資料庫
            DecorateFacotry factroy = new DecorateFacotry(new Redis(1));

            // 附加MSSQL寫入功能
            // 說明：如果SA之後要再加入功能，只需要新建模組實作ICache與繼承BaseCache，再從這邊SetCache即可
            factroy.SetCache(new MSSQL());

            ICache cache = factroy.GetCache();

            ITokenHelper tokenHelper = new JwtTokenHelper(JwtService.Issuer, JwtService.Audience, JwtService.KEY);

            jwtService = new JwtService(cache, tokenHelper);

            jwtService.Token = CookieService.GetValue("token");

            return jwtService;
        }
    }
}