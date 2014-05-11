#region CopyRight
/**************************************************************
   Copyright (c) 2014 StarPeng. All rights reserved.
   CLR版本        :    4.0.30319.34014
   命名空间名称   :    $rootnamespace$
   文件名         :    Program
   创建时间       :    2014/5/11 10:25:55
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
using System.Text;
using System.IO;
using System.Threading;

namespace CaptchaConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateAll();
        }

        public static void GenerateAll()
        {
            var foldeName = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            var captcha = new SimpleCaptcha();
            captcha.MinWordLength = 6;
            captcha.MaxWordLength = 6;
            captcha.Scale = 6;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    captcha.FontConfig = SimpleCaptcha.Fonts[i];
                    using (var image = captcha.CreateImage())
                    {
                        var path = String.Format("{0}{1}\\{2}-{3}.png", AppDomain.CurrentDomain.BaseDirectory, foldeName, i, j);
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                        image.Save(path, ImageFormat.Png);
                    }
                }
            }
        }
    }
}
