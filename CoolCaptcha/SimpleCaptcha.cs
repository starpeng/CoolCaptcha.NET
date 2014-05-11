#region CopyRight
/**************************************************************
   Copyright (c) 2014 StarPeng. All rights reserved.
   CLR版本        :    4.0.30319.34014
   命名空间名称   :    CoolCaptcha
   文件名         :    SimpleCaptcha
   创建时间       :    2014/5/10 9:20:53
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace CoolCaptcha
{
    /// <summary>
    /// class simple captcha.
    /// </summary>
    public class SimpleCaptcha
    {
        #region privates

        /// <summary>
        /// The random
        /// </summary>
        private Random _random = new Random();
        private Single textFinalX = 0;
        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the width of the captcha image.
        /// <remarks>
        /// the default value is 200
        /// </remarks>
        /// </summary>
        /// <value>The width of the captcha image.</value>
        public Int32 Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the captcha image.
        /// <remarks>
        /// the default value is 70
        /// </remarks>
        /// </summary>
        /// <value>The height of the captcha image.</value>
        public Int32 Height { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the word (for non-dictionary _random text generation).
        /// <remarks>
        /// the default value is 5
        /// </remarks>
        /// </summary>
        /// <value>The minimum length of the word.</value>
        public Int32 MinWordLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the word (for non-dictionary _random text generation).
        /// </summary>
        /// <remarks>
        /// the default value is 5
        /// </remarks>
        /// <value>The maximum length of the word.</value>
        public Int32 MaxWordLength { get; set; }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the foreground.
        /// </summary>
        /// <value>The color of the foreground.</value>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the shadow.
        /// </summary>
        /// <value>The color of the shadow.</value>
        public Color ShadowColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show captcha text shadow.
        /// </summary>
        /// <value><c>true</c> if show captcha text shadow; otherwise, <c>false</c>.</value>
        public Boolean ShowShadow { get; set; }

        /// <summary>
        /// Gets or sets the width of the horizontal lineNum through the text.
        /// </summary>
        /// <value>The width of the horizontal lineNum through the text.</value>
        public Int32 LineWidth { get; set; }

        /// <summary>
        /// Gets or sets the internal image size factor (for better image quality).
        /// <para>1: low, 2: medium, 3: high</para>
        /// <remarks>
        /// the default value is 3
        /// </remarks>
        /// </summary>
        /// <value>The scale.</value>
        public Int32 Scale { get; set; }

        /// <summary>
        /// Gets or sets the maximum letter rotation clockwise.
        /// <remarks>
        /// the default value is 8
        /// </remarks>
        /// </summary>
        /// <value>The maximum letter rotation clockwise.</value>
        public Int32 MaxRotation { get; set; }

        /// <summary>
        /// Gets or sets the words file path.
        /// </summary>
        /// <value>The words file path.</value>
        public String WordsFile { get; set; }

        /// <summary>
        /// Gets or sets the font configuration.
        /// </summary>
        /// <value>The font configuration.</value>
        public FontConfig FontConfig { get; set; }

        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        /// <value>The name of the session.</value>
        public String SessionName { get; set; }

        #region wave configuracion

        /// <summary>
        /// Gets or sets the wave period of y axis.
        /// <remarks>
        /// the default value is 12
        /// </remarks>
        /// </summary>
        /// <value>The wave period of y axis.</value>
        public Int32 Yperiod { get; set; }

        /// <summary>
        /// Gets or sets the wave amplitude of y axis.
        /// <remarks>
        /// the default value is 14
        /// </remarks>
        /// </summary>
        /// <value>The wave amplitude of y axis.</value>
        public Int32 Yamplitude { get; set; }

        /// <summary>
        /// Gets or sets the wave period of x axis.
        /// <remarks>
        /// the default value is 11
        /// </remarks>
        /// </summary>
        /// <value>The wave period of x axis.</value>
        public Int32 Xperiod { get; set; }

        /// <summary>
        /// Gets or sets the wave amplitude of x axis.
        /// <remarks>
        /// the default value is 5
        /// </remarks>
        /// </summary>
        /// <value>The wave amplitude of x axis.</value>
        public Int32 Xamplitude { get; set; }

        #endregion

        /// <summary>
        /// Gets the font configurations.
        /// </summary>
        /// <value>The font configurations.</value>
        public static FontConfig[] Fonts { get; private set; }

        #endregion

        #region ctor.

        #region static ctor.

        /// <summary>
        /// init <see cref="SimpleCaptcha"/> class static members.
        /// </summary>
        static SimpleCaptcha()
        {
            var fonts = new List<FontConfig>();
            using (PrivateFontCollection privateFonts = new PrivateFontCollection())
            {
                #region load font from resource

                Action<Byte[]> loadFont = (Byte[] data) =>
                {
                    var tempFile = Path.GetTempFileName();
                    File.WriteAllBytes(tempFile, data);
                    privateFonts.AddFontFile(tempFile);
                    //var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    //var intptr = handle.AddrOfPinnedObject();
                    //privateFonts.AddMemoryFont(intptr, data.Length);
                };

                loadFont(Resources.AntykwaBold);
                loadFont(Resources.Candice);
                loadFont(Resources.DingDongDaddyO);
                loadFont(Resources.Duality);
                loadFont(Resources.Heineken);
                loadFont(Resources.Jura);
                loadFont(Resources.StayPuft);
                loadFont(Resources.TimesNewRomanBold);
                loadFont(Resources.VeraSansBold);

                fonts.Add(new FontConfig() { Spacing = -3F, MinSize = 27, MaxSize = 30, FontFamily = privateFonts.Families[0] });
                fonts.Add(new FontConfig() { Spacing = -3F, MinSize = 26, MaxSize = 28, FontFamily = privateFonts.Families[1] });
                fonts.Add(new FontConfig() { Spacing = -1.5F, MinSize = 32, MaxSize = 34, FontFamily = privateFonts.Families[2] });
                fonts.Add(new FontConfig() { Spacing = -2F, MinSize = 29, MaxSize = 32, FontFamily = privateFonts.Families[3] });
                fonts.Add(new FontConfig() { Spacing = -2F, MinSize = 30, MaxSize = 34, FontFamily = privateFonts.Families[4] });
                fonts.Add(new FontConfig() { Spacing = -2F, MinSize = 28, MaxSize = 32, FontFamily = privateFonts.Families[5] });
                fonts.Add(new FontConfig() { Spacing = -3F, MinSize = 28, MaxSize = 32, FontFamily = privateFonts.Families[6] });
                fonts.Add(new FontConfig() { Spacing = -3F, MinSize = 28, MaxSize = 34, FontFamily = privateFonts.Families[7] });
                fonts.Add(new FontConfig() { Spacing = -2F, MinSize = 28, MaxSize = 30, FontFamily = privateFonts.Families[8] });

                Fonts = fonts.ToArray();
                #endregion
            }
        }

        /*
        'Antykwa'  => array('spacing' => -3, 'minSize' => 27, 'maxSize' => 30, 'font' => 'AntykwaBold.ttf'),
        'Candice'  => array('spacing' =>-1.5,'minSize' => 28, 'maxSize' => 31, 'font' => 'Candice.ttf'),
        'DingDong' => array('spacing' => -2, 'minSize' => 24, 'maxSize' => 30, 'font' => 'Ding-DongDaddyO.ttf'),
        'Duality'  => array('spacing' => -2, 'minSize' => 30, 'maxSize' => 38, 'font' => 'Duality.ttf'),
        'Heineken' => array('spacing' => -2, 'minSize' => 24, 'maxSize' => 34, 'font' => 'Heineken.ttf'),
        'Jura'     => array('spacing' => -2, 'minSize' => 28, 'maxSize' => 32, 'font' => 'Jura.ttf'),
        'StayPuft' => array('spacing' =>-1.5,'minSize' => 28, 'maxSize' => 32, 'font' => 'StayPuft.ttf'),
        'Times'    => array('spacing' => -2, 'minSize' => 28, 'maxSize' => 34, 'font' => 'TimesNewRomanBold.ttf'),
        'VeraSans' => array('spacing' => -1, 'minSize' => 20, 'maxSize' => 28, 'font' => 'VeraSansBold.ttf'),
         
         */
        #endregion

        /// init <see cref="SimpleCaptcha"/> class.
        /// </summary>
        public SimpleCaptcha()
        {
            Width = 200;
            Height = 70;
            MinWordLength = 5;
            MaxWordLength = 8;
            BackColor = Color.FromArgb(255, 255, 255);
            ForeColor = Color.FromArgb(214, 36, 7);
            LineWidth = 0;
            Scale = 3;
            MaxRotation = 8;
            Yperiod = 12;
            Yamplitude = 14;
            Xperiod = 11;
            Xamplitude = 5;
        }

        #endregion

        #region methods

        #region create captcha image

        /// <summary>
        /// Creates the captcha image.
        /// </summary>
        /// <returns>Bitmap.</returns>
        public Bitmap CreateImage()
        {
            var text = GetCaptchaText();
            if (SessionName != null && SessionName.Length > 0)
            {
                if (HostingEnvironment.IsHosted)
                {
                    HttpContext.Current.Session[SessionName] = text;
                }
            }

            return CreateImage(text);
        }

        /// <summary>
        /// Creates the captcha image.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>Bitmap.</returns>
        public Bitmap CreateImage(String text)
        {
            if (text == null || text.Length == 0)
            {
                throw new ArgumentNullException("text");
            }

            var image = ImageAllocate();
            this.WriteText(image, text);
            this.WriteLine(image);
            this.WaveImage(image);
            image = new Bitmap(image, this.Width, this.Height);
            return image;
        }

        /// <summary>
        /// Allocate the image.
        /// </summary>
        /// <returns>Image.</returns>
        protected virtual Bitmap ImageAllocate()
        {
            var imageWidth = this.Width * this.Scale;
            var imageHeight = this.Height * this.Scale;
            var image = new Bitmap(imageWidth, imageHeight);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                using (SolidBrush sb = new SolidBrush(this.BackColor))
                {
                    graphics.FillRectangle(sb, 0, 0, imageWidth, imageHeight);
                }
            }

            return image;
        }

        /// <summary>
        /// Writes the text.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="text">The text.</param>
        protected virtual void WriteText(Bitmap image, String text)
        {
            var fontConfig = this.FontConfig;
            if (fontConfig == null)
            {
                var fontIndex = _random.Next(0, SimpleCaptcha.Fonts.Length - 1);
                fontConfig = SimpleCaptcha.Fonts[fontIndex];
            }

            var length = text.Length;
            var lettersMissing = this.MaxWordLength - length;
            var fontSizefactor = 1 + (lettersMissing * 0.09F);
            using (var graphics = Graphics.FromImage(image))
            using (var shadowBrush = new SolidBrush(this.ShadowColor))
            using (var foreBrush = new SolidBrush(this.ForeColor))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                var fontSize = fontConfig.MaxSize * this.Scale * fontSizefactor;
                var font = new Font(fontConfig.FontFamily, fontSize);
                var textSize = graphics.MeasureString(text, font);
                var x = (image.Width - textSize.Width - fontConfig.Spacing * text.Length) / 2;
                for (int i = 0; i < length; i++)
                {
                    fontSize = _random.Next(fontConfig.MinSize, fontConfig.MaxSize) * this.Scale * fontSizefactor;
                    font = new Font(fontConfig.FontFamily, fontSize);
                    var letter = text.Substring(i, 1);
                    var letterSize = graphics.MeasureString(letter, font);
                    var y = (image.Height - letterSize.Height) / 2;
                    var matrix = new Matrix();
                    var degree = _random.Next(-this.MaxRotation, this.MaxRotation);
                    matrix.RotateAt(degree, new PointF(x + letterSize.Width / 2, image.Height / 2), MatrixOrder.Append);
                    graphics.Transform = matrix;
                    graphics.DrawString(letter, font, foreBrush, x + Scale, y + Scale);
                    graphics.DrawString(letter, font, foreBrush, x, y);
                    x += Convert.ToInt32(letterSize.Width + (fontConfig.Spacing - 12) * this.Scale);
                    textFinalX = x;
                }
            }
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="image">The image.</param>
        protected virtual void WriteLine(Bitmap image)
        {
            if (this.LineWidth > 0)
            {
                var x1 = this.Width * this.Scale * 0.15F;
                var x2 = this.textFinalX;
                var y1 = _random.Next(this.Height * this.Scale * 40, this.Height * this.Scale * 65) / 100F;
                var y2 = _random.Next(this.Height * this.Scale * 40, this.Height * this.Scale * 65) / 100F;
                var width = this.LineWidth / 2 * this.Scale;
                using (var graphics = Graphics.FromImage(image))
                using (var linePen = new Pen(ForeColor, 1))
                {
                    for (var i = width * -1; i <= width; i++)
                    {
                        graphics.DrawLine(linePen, x1, y1 + i, x2, y2 + i);
                    }
                }
            }
        }

        /// <summary>
        /// Wave the image.
        /// </summary>
        /// <param name="image">The image.</param>
        protected virtual void WaveImage(Bitmap image)
        {
            var imageLength = image.Width * image.Height;
            var imageWidth = image.Width;
            var imageHeight = image.Height;
            var backColorArgb = this.BackColor.ToArgb();
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, imageWidth, imageHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            try
            {
                unsafe
                {
                    var imageDataPtr = (Int32*)imageData.Scan0;
                    {
                        //wave x
                        var xp = this.Scale * this.Xperiod * _random.Next(1, 3);
                        var k = _random.Next(0, 100);
                        for (var i = 1; i < imageWidth; i++)
                        {
                            var amplitude = unchecked(Convert.ToInt32(Math.Sin(k + i * 1.00 / xp) * this.Scale * this.Xamplitude));
                            for (int j = 0; j < imageHeight; j++)
                            {
                                if (j + amplitude >= 0 && j + amplitude < imageHeight)
                                {
                                    imageDataPtr[(j * imageWidth + i - 1)] = imageDataPtr[((j + amplitude) * imageWidth + i)];
                                }
                                else
                                {
                                    imageDataPtr[(j * imageWidth + i - 1)] = backColorArgb;
                                }
                            }
                        }
                    }
                    {
                        //wave y
                        var yp = this.Scale * this.Yperiod * _random.Next(1, 2);
                        var k = _random.Next(0, 100);
                        for (var i = 1; i < imageHeight; i++)
                        {
                            var amplitude = unchecked(Convert.ToInt32(Math.Sin(k + i * 1.00 / yp) * this.Scale * this.Yamplitude));
                            for (int j = 0; j < imageWidth; j++)
                            {
                                if (j + amplitude >= 0 && j + amplitude < imageWidth)
                                {
                                    imageDataPtr[((i - 1) * imageWidth + j)] = imageDataPtr[(i * imageWidth + j + amplitude)];
                                }
                                else
                                {
                                    imageDataPtr[((i - 1) * imageWidth + j)] = backColorArgb;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                image.UnlockBits(imageData);
            }
        }

        #endregion

        #region captcha text methods

        /// <summary>
        /// Gets the captcha text.
        /// </summary>
        /// <returns>captcha text.</returns>
        protected virtual String GetCaptchaText()
        {
            var text = this.GetDictionaryCaptchaText();
            if (text == null || text.Length == 0)
            {
                text = this.GetRandomCaptchaText();
            }

            return text;
        }

        /// <summary>
        /// Gets the dictionary captcha text.
        /// </summary>
        /// <param name="extended">The extended.</param>
        /// <returns>dictionary captcha text.</returns>
        protected virtual String GetDictionaryCaptchaText(Boolean extended = false)
        {
            if (WordsFile == null || WordsFile.Length == 0)
            {
                return null;
            }

            var filePath = SimpleCaptcha.MapPath(WordsFile);
            if (!File.Exists(filePath))
            {
                return null;
            }

            try
            {
                var file = new FileInfo(filePath);
                var fileSize = file.Length;
                using (var fs = File.OpenRead(filePath))
                using (var sr = new StreamReader(fs))
                {
                    var firstLine = sr.ReadLine();
                    var lineLength = firstLine.Length;
                    var lineCount = 1;
                    unchecked
                    {
                        lineCount = (Int32)(fileSize / firstLine.Length);
                    }

                    var lineNum = _random.Next(0, lineCount);
                    lineLength += Environment.NewLine.Length;
                    fs.Seek(lineLength * lineNum, SeekOrigin.Begin);
                    sr.DiscardBufferedData();
                    var text = sr.ReadLine().Trim();
                    if (extended)
                    {
                        var textChars = text.ToCharArray();
                        var vocals = new List<Char>() { 'a', 'e', 'i', 'o', 'u' };
                        for (int i = 0; i < textChars.Length; i++)
                        {
                            if (vocals.Contains(text[i]) && _random.Next(0, 1) == 1)
                            {
                                textChars[i] = vocals[_random.Next(0, 4)];
                            }
                        }

                        text = new String(textChars);
                    }

                    return text;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the random captcha text.
        /// </summary>
        /// <param name="length">The captcha text length.</param>
        /// <returns>random captcha text.</returns>
        protected virtual String GetRandomCaptchaText(Int32 length = 0)
        {
            if (length <= 0)
            {
                length = _random.Next(this.MinWordLength, this.MaxWordLength);
            }

            var words = "abcdefghijlmnopqrstvwyz";
            var vocals = "aeiou";
            var vocal = _random.Next(0, 1);
            var textChars = new Char[length];
            for (int i = 0; i < length; i++)
            {
                if (vocal == 1)
                {
                    textChars[i] = vocals[_random.Next(0, 4)];
                }
                else
                {
                    textChars[i] = words[_random.Next(0, 22)];
                }

                vocal = vocal ^ 1;
            }

            var text = new String(textChars);
            return text;
        }

        #endregion

        #region utility methods

        /// <summary>
        /// maps the path to the physical path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>physical path.</returns>
        private static String MapPath(String path)
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }
            else
            {
                String baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, path);
            }
        }

        #endregion

        #endregion
    }
}
