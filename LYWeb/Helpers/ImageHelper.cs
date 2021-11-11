using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LYWeb.Helpers
{
    public class ImageHelper
    {
        public static string ImgToBase64String(string Imagefilename)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Imagefilename);
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                ms.Dispose();
            }
        }
    }
}