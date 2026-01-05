using System;
using System.Collections.Generic;

namespace EMIC2.Result
{
    public interface IResult
    {
        Guid ID
        {
            get;
        }

        bool Success
        {
            get;
            set;
        }

        string Message
        {
            get;
            set;
        }

        string ReturnValue
        {
            get;
            set;
        }

        Exception Exception
        {
            get;
            set;
        }

        List<IResult> InnerResults
        {
            get;
        }
    }
}