#region CopyRight
/**************************************************************
   Copyright (c) 2014 StarPeng. All rights reserved.
   CLR版本        :    4.0.30319.34014
   命名空间名称   :    CaptchaTool
   文件名         :    Arguments
   创建时间       :    2014/5/11 18:02:55
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
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace CaptchaTool
{
    /// <summary>
    /// Arguments class
    /// </summary>
    public class Arguments
    {
        private StringDictionary _parameters;
        private static Regex _regSpliter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex _regRemover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public Arguments(String[] args)
        {
            _parameters = new StringDictionary();
            String parameter = null;
            foreach (String arg in args)
            {
                var parts = _regSpliter.Split(arg, 3);
                switch (parts.Length)
                {
                    case 1:
                        if (parameter != null)
                        {
                            if (!_parameters.ContainsKey(parameter))
                            {
                                parts[0] = _regRemover.Replace(parts[0], "$1");
                                _parameters.Add(parameter, parts[0]);
                            }

                            parameter = null;
                        }
                        break;

                    case 2:
                        if (parameter != null)
                        {
                            if (!_parameters.ContainsKey(parameter))
                            {
                                _parameters.Add(parameter, "true");
                            }
                        }

                        parameter = parts[1];
                        break;
                    case 3:
                        if (parameter != null)
                        {
                            if (!_parameters.ContainsKey(parameter))
                            {
                                _parameters.Add(parameter, "true");
                            }
                        }

                        parameter = parts[1];
                        if (!_parameters.ContainsKey(parameter))
                        {
                            parts[2] = _regRemover.Replace(parts[2], "$1");
                            _parameters.Add(parameter, parts[2]);
                        }

                        parameter = null;
                        break;
                }
            }

            if (parameter != null)
            {
                if (!_parameters.ContainsKey(parameter))
                {
                    _parameters.Add(parameter, "true");
                }
            }
        }

        public String this[String Param]
        {
            get
            {
                return (_parameters[Param]);
            }
        }
    }
}
