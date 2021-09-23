using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObject)
        {
            IsSuccessed = true;
            ResultObject = resultObject;
        }

        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}
