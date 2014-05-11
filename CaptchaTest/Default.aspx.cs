#region CopyRight
/**************************************************************
   Copyright (c) 2014 StarPeng. All rights reserved.
   CLR版本        :    4.0.30319.34014
   命名空间名称   :    $rootnamespace$
   文件名         :    Default
   创建时间       :    2014/5/10 16:41:45
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
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CaptchaTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                var captcha = (String)Session[Captcha.CaptchaSessionName];
                if (String.Equals(captcha, Request.Form["captcha"], StringComparison.OrdinalIgnoreCase))
                {
                    Literal1.Text = "<p style=\"color:blue;\">Valid captcha</p>";
                }
                else
                {
                    Literal1.Text = "<p style=\"color:red;\">Invalid captcha</p>";
                }
            }
        }
    }
}
