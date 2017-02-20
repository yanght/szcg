using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Szcg.Web.Controllers
{
    public class MapController : Controller
    {
        //
        // GET: /Map/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Demo()
        {
            return View();
        }

        public ActionResult EncryptPassword()
        {
            bool rtn = false;

            ServiceReference1.CobSecurityServiceDelegateClient clicnt = new ServiceReference1.CobSecurityServiceDelegateClient();

            return Content(clicnt.EncryptPassword("11111111"));
        }

        public ActionResult Encrypt()
        {
            bool rtn = false;
            ServiceReference1.CobSecurityServiceDelegateClient clicnt = new ServiceReference1.CobSecurityServiceDelegateClient();

            try
            {
                string filePath = @"D:\pgp\file\yiguo20170215.txt";

                byte[] filebytes = clicnt.EncryptFile(filePath.Substring(filePath.LastIndexOf(@"\")), GetFileData(filePath));

                int lastIndex = filePath.LastIndexOf(".");

                rtn = WriteFile(filebytes, filePath + ".pgp");

            }
            catch (Exception ex)
            {
                return Content("fault:" + ex.ToString());
            }
            return Content(rtn.ToString());
        }

        public ActionResult Decrypt()
        {
            bool rtn = false;
            ServiceReference1.CobSecurityServiceDelegateClient clicnt = new ServiceReference1.CobSecurityServiceDelegateClient();

            try
            {
                string filePath = @"D:\pgp\file\test.pgp";

                byte[] filebytes = clicnt.DecryptFile(filePath.Substring(filePath.LastIndexOf(@"\")), GetFileData(filePath));

                int lastIndex = filePath.LastIndexOf(".");

                rtn = WriteFile(filebytes, filePath.Substring(0, lastIndex) + ".txt");

            }
            catch (Exception ex)
            {
                return Content("fault:" + ex.ToString());
            }
            return Content(rtn.ToString());
        }

        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="fileUrl">文件路径文件名称</param>
        /// <returns>byte[]</returns>
        protected byte[] GetFileData(string fileUrl)
        {
            FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception ex)
            {
                //MessageBoxHelper.ShowPrompt(ex.Message);
                return null;
            }
            finally
            {
                if (fs != null)
                {

                    //关闭资源
                    fs.Close();
                }
            }
        }


        //写byte[]到fileName  

        protected bool WriteFile(byte[] pReadByte, string fileName)
        {

            FileStream pFileStream = null;
            try
            {
                pFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                pFileStream.Write(pReadByte, 0, pReadByte.Length);
            }

            catch
            {
                return false;
            }

            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }
            return true;
        }
    }
}
