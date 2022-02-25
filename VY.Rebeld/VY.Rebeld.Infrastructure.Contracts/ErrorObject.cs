using System;

namespace VY.Rebeld.Infrastructure.Contracts
{
    public class ErrorObject
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Exception ex { get; set; }

    }
}
