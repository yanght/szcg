using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ywdxDL;
namespace ywdxBL
{
    public class ywdxManager 
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="probsource"></param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public DataSet GetDXlist(string starttime ,string endtime,string probsource,out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                ywdxManagers f = new ywdxManagers();
                
                   return f.GetDXlist(starttime, endtime, probsource);
                    
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
        public DataSet GetCustomer(string type,out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                ywdxManagers f = new ywdxManagers();

                return f.getCustomer(type);

            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }
    }
}
