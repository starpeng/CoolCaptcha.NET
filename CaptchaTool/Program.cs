#region CopyRight
/**************************************************************
   Copyright (c) 2014 StarPeng. All rights reserved.
   CLR版本        :    4.0.30319.34014
   命名空间名称   :    $rootnamespace$
   文件名         :    Program
   创建时间       :    2014/5/10 11:28:06
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CaptchaTool
{
    class Program
    {
        private static Regex regCharactor = new Regex(@"^[a-z]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        static void Main(String[] args)
        {
            var minLength = 5;
            var maxLength = 8;
            var arguments = new Arguments(args);
            var infile = arguments["in"];
            var outfile = arguments["out"];
            if (infile == null || infile.Length == 0)
            {
                WriteUsage();
                return;
            }

            if (outfile == null || outfile.Length == 0)
            {
                WriteUsage();
                return;
            }

            if (arguments["min"] != null)
            {
                var value = arguments["min"];
                if (!Int32.TryParse(value, out minLength))
                {
                    WriteError("Invalid minimize length.");
                    return;
                }
            }

            if (arguments["max"] != null)
            {
                var value = arguments["max"];
                if (!Int32.TryParse(value, out maxLength))
                {
                    WriteError("Invalid maximize length.");
                    return;
                }
            }

            if (minLength > maxLength)
            {
                WriteError("maximize length must greater than minimize length.");
                return;
            }

            if (!File.Exists(infile))
            {
                WriteError("input file not exists.");
                return;
            }

            using (var fsIn = new FileStream(infile, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fsIn))
            using (var fsOut = new FileStream(outfile, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(fsOut))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    line = line.ToLowerInvariant().Trim();
                    var lineLength = line.Length;
                    if (lineLength >= minLength && lineLength <= maxLength && regCharactor.IsMatch(line))
                    {
                        sw.WriteLine(line.PadRight(maxLength));
                    }

                    line = sr.ReadLine();
                }
            }
        }

        static void WriteUsage()
        {
            Console.WriteLine("Captcha tool usages:");
            Console.WriteLine("/in   input file path,the source dictionary file.");
            Console.WriteLine("/out  output file path.the target captcha dictionary file.");
            Console.WriteLine("/min  minimize captcha text length,the default is 5.");
            Console.WriteLine("/max  maximize captcha text length,the default is 8.");
        }

        static void WriteError(String format, params Object[] args)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(format, args);
            Console.ForegroundColor = old;
        }

    }
}
