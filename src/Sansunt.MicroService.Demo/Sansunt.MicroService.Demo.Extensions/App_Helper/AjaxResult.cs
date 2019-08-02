using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sansunt.Infra.Tools.Helpers;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Extensions.App_Helper
{
    /// <summary>
    /// ajax请求处理结果对象
    /// </summary>

    public class AjaxResult : ActionResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        private AjaxBackData Data { get; set; }
        /// <summary>
        /// 获取返回数据
        /// </summary>
        public AjaxBackData GetResponseData
        {
            get { return Data; }
        }
        #region 初始化函数
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="data"></param>
        public AjaxResult(dynamic data)
        {
            Data = new AjaxBackData(data);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMsg"></param>
        public AjaxResult(ErrorCodeEnum errorCode, dynamic errorMsg)
        {
            Data = new AjaxBackData(errorCode, errorMsg);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="ex"></param>
        public AjaxResult(Exception ex)
        {
            Data = new AjaxBackData(ex);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="data"></param>
        public AjaxResult(AjaxBackData data)
        {
            Data = data;
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ActionContext context)
        {
            HttpResponse response = context.HttpContext.Response;
            response.ContentType = "application/json";
            var result = Data.ToJson(false, true);
            response.WriteAsync(result);
        }
    }

    #region 数据

    public class AjaxBackData
    {
        #region 初始化函数
        public AjaxBackData() { }
        public AjaxBackData(dynamic data)
        {
            ErrorCode = ErrorCodeEnum.ok;
            ErrorMsg = ErrorCodeEnum.ok.ToString();
            Data = data;
        }
        public AjaxBackData(ErrorCodeEnum errorCode, dynamic errorMsg)
        {
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
            Data = new object();
        }
        public AjaxBackData(Exception ex)
        {
            ErrorCode = ErrorCodeEnum.SystemException;
            ErrorMsg = "系统异常(" + ex.Message + ")";
            Data = new object();
        }
        #endregion
        /// <summary>
        /// 错误代码 
        /// </summary>
        [JsonProperty("errorCode")]
        public ErrorCodeEnum ErrorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("errorMsg")]
        public dynamic ErrorMsg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty("data")]
        public dynamic Data { get; set; }
    }
    #endregion

}