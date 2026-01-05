///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  CommonSSO2Dao.cs
//  程式名稱：
//  搜尋
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期              版本              備註
//  Enosh           2019-09-03       1.0.0.0           初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  待辦事項使用
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.COMMON
{
    using System.Collections.Generic;
    using System.Linq;

    using EMIC2.Models.Interface;
    using EMIC2.Models.Interface.COMMON;

    public class CommonSSO2Dao : ICommonSSO2Dao
    {
        private readonly IRepository<SSO2_FUNCTION> _SSO2FunctionRepository;

        public CommonSSO2Dao(IRepository<SSO2_FUNCTION> sso2FunctionRepository)
        {
            this._SSO2FunctionRepository = sso2FunctionRepository;
        }

        public IEnumerable<SSO2_FUNCTION> Search(string[] functionCodes)
        {
            return (from f in _SSO2FunctionRepository.GetAll()
                    where functionCodes.Contains(f.FUNCTION_CODE)
                    select f).ToList();
        }
    }
}
