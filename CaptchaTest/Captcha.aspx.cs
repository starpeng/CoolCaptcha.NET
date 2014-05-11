#region CopyRight
/**************************************************************
   Copyright (c) 2014 StarPeng. All rights reserved.
   CLR版本        :    4.0.30319.34014
   命名空间名称   :    CaptchaTest
   文件名         :    Captcha
   创建时间       :    2014/5/11 17:14:27
   用户所在的域   :    XPC
   登录用户名     :    Star
   文件描述       :    
   版本           :    1.0.0
   历史           :    
   最后更新人     :   
   最后更新时间   :   
 **************************************************************/
#endregion CopyRight

using CoolCaptcha;
using System;
using System.Drawing.Imaging;

namespace CaptchaTest
{
    public partial class Captcha : System.Web.UI.Page
    {
        public const String CaptchaSessionName = "Captcha";
        protected override void OnPreInit(EventArgs e)
        {
            Response.Clear();
            var captcha = new SimpleCaptcha();
            captcha.SessionName = CaptchaSessionName;
            captcha.WordsFile = "~/App_Data/words-en.txt";
            using (var image = captcha.CreateImage())
            {
                Response.ContentType = "image/png";
                image.Save(Response.OutputStream, ImageFormat.Png);
                Response.End();
            }
        }
    }
}
