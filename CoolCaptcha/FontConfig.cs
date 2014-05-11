#region CopyRight
/**************************************************************
   Copyright (c) 2014 StarPeng. All rights reserved.
   CLR版本        :    4.0.30319.34014
   命名空间名称   :    CoolCaptcha
   文件名         :    FontConfig
   创建时间       :    2014/5/10 9:43:55
   用户所在的域   :    XPC
   登录用户名     :    Star
   文件描述       :    
   版本           :    1.0.0
   历史           :    
   最后更新人     :   
   最后更新时间   :   
 **************************************************************/
#endregion CopyRight

using System;
using System.Drawing;

namespace CoolCaptcha
{
    /// <summary>
    /// class font configuration.
    /// </summary>
    public class FontConfig
    {
        #region properties

        /// <summary>
        /// Gets or sets the relative pixel space between character.
        /// </summary>
        /// <value>The relative pixel space between character.</value>
        public Single Spacing { get; set; }

        /// <summary>
        /// Gets or sets the minimum font size.
        /// </summary>
        /// <value>The minimum font size.</value>
        public Int32 MinSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum font size.
        /// </summary>
        /// <value>The maximum font size.</value>
        public Int32 MaxSize { get; set; }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        /// <value>font family.</value>
        public FontFamily FontFamily { get; set; }


        #endregion
    }
}
