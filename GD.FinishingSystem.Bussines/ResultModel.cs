using System;
using System.Collections.Generic;
using System.Text;

namespace GD.FinishingSystem.Bussines
{
    public class ResultModel
    {
        public ResultModel()
        {
            IsSuccess = false;
            Message = "";
        }
        public bool IsSuccess { get; set; }
        public int resultType { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

}
