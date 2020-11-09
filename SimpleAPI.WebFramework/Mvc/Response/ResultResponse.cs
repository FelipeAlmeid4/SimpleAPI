using System;

namespace SimpleAPI.WebFramework.Mvc.Response
{
    [Serializable]
    public class ResultResponse<TResult> : IResponse
    {
        public TResult Result { get; set; }

        public ResultResponse()
        { }

        public ResultResponse(TResult result)
        {
            Result = result;
        }
    }

    [Serializable]
    public class ResultResponse : ResultResponse<object>
    {
        public ResultResponse()
        { }

        public ResultResponse(object result)
            : base(result)
        { }
    }
}
