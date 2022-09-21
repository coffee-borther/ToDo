﻿namespace MyToDo.Api.Services
{
    public class ApiResponse
    {
        public ApiResponse(string message, bool status=false)
        {
            this.Message = message;
            this.Status = status;
        }

        public ApiResponse(bool status, object result)
        {
            this.Status = status;
            this.Result = result;
        }
        #region Prop

        public string Message { get; set; }

        public bool Status { get; set; }

        public object Result { get; set; }

        #endregion
    }
}