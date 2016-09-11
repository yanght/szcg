using System;

namespace szcg.web.zhpj.funsion
{
    /// <summary>
    /// Summary description for Functions.
    /// </summary>
    public class Functions
    {
        //If you're using UTF-8 characters in your XML, you need to use this 
        //streamwriter object to output the XML.		
        private System.IO.StreamWriter writer = new System.IO.StreamWriter(System.Web.HttpContext.Current.Response.OutputStream, System.Text.Encoding.UTF8);

        //escapeXML function helps you escape special characters in XML
        public static string EscapeXML(string item, bool forDataURL)
        {
            //Convert ' to &apos; if dataURL
            if (forDataURL)
            {
                item = item.Replace("'", "&apos;");
            }
            else
            {
                //Else for dataXML 		
                //Convert % to %25
                item = item.Replace("%", "%25");
                //Convert ' to %26apos;
                item = item.Replace("'", "%26apos;");
                //Convert & to %26
                item = item.Replace("&", "%26");
            }
            //Common replacements
            item = item.Replace("<", "&lt;");
            item = item.Replace(">", "&gt;");
            //We've not considered any special characters here. 
            //You can add them as per your language and requirements.
            //Return
            return item;
        }

        //getPalette method returns a value between 1-5 depending on which
        //paletter the user wants to plot the chart with. 
        //Here, we just read from Session variable and show it
        //In your application, you could read this configuration from your 
        //User Configuration Manager, database, or global application settings
        public static string GetPalette()
        {
            string palette = String.Empty;
            if (System.Web.HttpContext.Current.Session["palette"] == null ||
                System.Web.HttpContext.Current.Session["palette"].ToString() == String.Empty)
                palette = "2";
            else
                palette = System.Web.HttpContext.Current.Session["palette"].ToString();

            return palette;
        }

        //getAnimationState returns 0 or 1, depending on whether we've to																								//animate chart. Here, we just read from Session variable and show it
        //In your application, you could read this configuration from your 
        //User Configuration Manager, database, or global application settings
        public static string GetAnimationState()
        {
            string animation = String.Empty;
            if (System.Web.HttpContext.Current.Session["animation"] != null &&
                System.Web.HttpContext.Current.Session["animation"].ToString() != "0")
                animation = "1";
            else
                animation = "0";
            return animation;
        }

        //getCaptionFontColor function returns a color code for caption. Basic
        //idea to use this is to demonstrate how to centralize your cosmetic 
        //attributes for the chart
        public static string getCaptionFontColor
        {
            get
            {
                //Return a hex color code without #
                return "666666";
                //FFC30C - Yellow Color
            }
        }

        public static string GetDecimal(decimal value)
        {
            string result = value.ToString("F");
            if (result.IndexOf(",") > -1)
                result = result.Replace(",", ".");
            return result;
        }

        public static string GetMonthName(int monthNumber)
        {
            string result = String.Empty;
            switch (monthNumber)
            {
                case 1:
                    result = "January";
                    break;
                case 2:
                    result = "February";
                    break;
                case 3:
                    result = "March";
                    break;
                case 4:
                    result = "April";
                    break;
                case 5:
                    result = "May";
                    break;
                case 6:
                    result = "June";
                    break;
                case 7:
                    result = "July";
                    break;
                case 8:
                    result = "August";
                    break;
                case 9:
                    result = "September";
                    break;
                case 10:
                    result = "October";
                    break;
                case 11:
                    result = "November";
                    break;
                case 12:
                    result = "December";
                    break;
                default:
                    break;
            }

            return result;
        }

        //If you've UTF-8 characters in your XML, you should use this
        //method to output the XML.
        public void Write(string stringForOutput)
        {
            writer.Write(stringForOutput);
            writer.Flush();
        }
    }
}
