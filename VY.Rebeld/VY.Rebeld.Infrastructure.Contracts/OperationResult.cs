using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VY.Rebeld.Infrastructure.Contracts
{
    public class OperationResult
    {
        public List<ErrorObject> _errors = new List<ErrorObject>();

        public void AddError(int code, string message, Exception exception = null)
        {
            _errors.Add(new ErrorObject() { Code = code, Message = message, ex = exception});
        }
        public void AddError(Exception exception)
        {
            _errors.Add(new ErrorObject() { ex = exception });
        }

        public void AddError(IEnumerable<ErrorObject> errors)
        {
            _errors.AddRange(errors);
        }

        public List<ErrorObject> GetAllErrors() => _errors.ToList();

        public bool HasErrors() => _errors.Count > 0;
    }

    public class OperationResult<T> : OperationResult
    { 
        public T Result { get; set; }
    }
}
