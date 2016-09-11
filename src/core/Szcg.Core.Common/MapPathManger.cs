/* ****************************************************************************************
 * 版权所有：嘉兴康思网络科技有限公司 
 * 用    途：进行地图管理的类
 * 结构组成：
 * 作    者：yaoch
 * 创建日期：2012-10-17
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/

using System;



namespace Teamax.Common
{
    public class MapPathManger
    {


        public MapPathManger()
        {
        }

        #region 注释老的地图加载方法  2016.05.31

        //根据角色、系统和地区来判断系统加载的地图路径
        //public static String GetCurMapPath(String role,String step,String area)
        //{
        //    string str = "";
        //    string str3 = "MapBrowser_zx/index1.html";
        //    string str4 = "MapBrowser_cf/index.html";
        //    string str5 = "MapBrowser_cf/index1.html";
        //    string str6 = "MapBrowser_wz/index.html";
        //    string str7 = "MapBrowser_wz/index1.html";
        //    string str8 = "MapBrowser/index.html";
        //   // string str9 = "MapBrowser/index1.html";
        //    if (step == "1")
        //    {
        //        if (role == "1")
        //        {
        //            if (area == "330483107")
        //            {
        //                return str4;
        //            }
        //            if (area == "330483100")
        //            {
        //                return str6;
        //            }
        //            return str8;
        //        }
        //        if (role == "33")
        //        {
        //            if (area == "330483107")
        //            {
        //                return str4;
        //            }
        //            if (area == "330483100")
        //            {
        //                return str = str6;
        //            }
                     
        //        }
        //        return str8;
        //    }
        //    if (step == "2")
        //    {
        //        if (area == "330483107")
        //        {
        //            return str4;
        //        }
        //        if (area == "330483100")
        //        {
        //            return str6;
        //        }
        //        return str8;
        //    }
        //    if (step != "2")
        //    {
        //        if (step == "3")
        //        {
        //            if (area == "330483107")
        //            {
        //                return str4;
        //            }
        //            if (area == "330483100")
        //            {
        //                return str = str6;
        //            }
        //        }
        //        if (step == "4")
        //        {
        //            if ((role == "15") || (role == "34"))
        //            {
        //                return str3;
        //            }
        //            if (role == "35")
        //            {
        //                if (area == "330483107")
        //                {
        //                    return str5;
        //                }
        //                if (area == "330483100")
        //                {
        //                    str = str7;
        //                }
        //                return str;
        //            }
        //            return str8;
        //        }
        //    }
        //    return str8;

        //}

        #endregion

        public static String GetCurMapPath(String role, String step, String area)
        {
            string str = "";
            string str3 = "maps/index.html";
            string str4 = "maps/index.html";
            string str5 = "maps/index.html";
            string str6 = "maps/index.html";
            string str7 = "maps/index.html";
            string str8 = "maps/index.html";
            // string str9 = "MapBrowser/index1.html";
            if (step == "1")
            {
                if (role == "1")
                {
                    if (area == "330483107")
                    {
                        return str4;
                    }
                    if (area == "330483100")
                    {
                        return str6;
                    }
                    return str8;
                }
                if (role == "33")
                {
                    if (area == "330483107")
                    {
                        return str4;
                    }
                    if (area == "330483100")
                    {
                        return str = str6;
                    }

                }
                return str8;
            }
            if (step == "2")
            {
                if (area == "330483107")
                {
                    return str4;
                }
                if (area == "330483100")
                {
                    return str6;
                }
                return str8;
            }
            if (step != "2")
            {
                if (step == "3")
                {
                    if (area == "330483107")
                    {
                        return str4;
                    }
                    if (area == "330483100")
                    {
                        return str = str6;
                    }
                }
                if (step == "4")
                {
                    if ((role == "15") || (role == "34"))
                    {
                        return str3;
                    }
                    if (role == "35")
                    {
                        if (area == "330483107")
                        {
                            return str5;
                        }
                        if (area == "330483100")
                        {
                            str = str7;
                        }
                        return str;
                    }
                    return str8;
                }
            }
            return str8;

        }
    }
}
