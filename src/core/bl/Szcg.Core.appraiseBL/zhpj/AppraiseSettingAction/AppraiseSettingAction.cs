using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using bacgDL.business;
using bacgDL.zhpj;
using DBbase.zhpj;

namespace bacgBL.zhpj.AppraiseSettingAction
{
    public class AppraiseSettingAction
    {

        /// <summary>
        /// 得到模块数据信息
        /// </summary>
        public DataSet getModelinfolist(DBbase.zhpj.AppraiseSettingModel prj, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (AppraiseSettingDAO dl = new AppraiseSettingDAO())
                {
                    return dl.getModelinfolist(prj, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 得到自定义字段信息列表
        /// </summary>
        public DataSet getFieldList(DBbase.zhpj.AppraiseSetting prj, PageInfo page, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (AppraiseSettingDAO dl = new AppraiseSettingDAO())
                {
                    return dl.getFieldList(prj, page);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
        }


        /// <summary>
        /// 为自定义字段进行重新排序
        /// </summary>
        public void orderField(string model, string fieldID, string status,out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (AppraiseSettingDAO dl = new AppraiseSettingDAO())
                {
                    dl.orderField(model, fieldID, status);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;;
            }
        }


        /// <summary>
        /// 得到自定义字段信息
        /// </summary>
        public DBbase.zhpj.AppraiseSetting getFieldInfo(int id, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = string.Format(@"select 
                  a.id, a.viewName, a.fieldName, a.codeName, a.express, a.express_, a.isbase, a.isdisplay, b.modelname,a.model_defined, 
                  a.remark, a.roleid, a.order_, a.procedurename, a.tablename
                  from a_appraise_field_detail  a left join a_appraise_model b on a.model=b.id
                                            where 1=1 and a.ID='{0}'", id);
            DBbase.zhpj.AppraiseSetting values = new DBbase.zhpj.AppraiseSetting();
            try
            {
                using (AppraiseSettingDAO dl = new AppraiseSettingDAO())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        values.setId(dr["ID"].ToString());
                        values.setViewName(dr["viewName"].ToString());
                        values.setFieldName(dr["fieldName"].ToString());
                        values.setCodeName(dr["codeName"].ToString());
                        values.setExpress(dr["express"].ToString());
                        values.setExpress_(dr["express_"].ToString());
                        if (dr["isbase"].ToString().Equals("1"))
                            values.setIsBase("非基础数据");
                        else
                            values.setIsBase("基础数据");
                        values.setIsDisplay(dr["isdisplay"].ToString());
                        values.setModel(dr["modelname"].ToString());
                        values.setModelDefined(dr["model_defined"].ToString());
                        values.setRemark(dr["remark"].ToString());
                        values.setRoleid(dr["roleid"].ToString());
                        values.setOrder_( Convert.ToInt32(dr["order_"].ToString()));
                        values.setProcedurename(dr["procedurename"].ToString());
                        values.setTablename(dr["tablename"].ToString());
                    }
                    dr.Close();
                }
                return values;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 得到相关角色信息
        /// </summary>
        public string getRoleInfo(string[] str, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = "select rolename from  dbo.p_role b where b.rolecode in (";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == "")
                    return "";
                if (i != str.Length - 1)
                    strSQL += str[i] + ",";
                else
                    strSQL += str[i];
            }
            strSQL += ")";
            string result = "";
            DBbase.zhpj.AppraiseSetting values = new DBbase.zhpj.AppraiseSetting();
            try
            {
                using (AppraiseSettingDAO dl = new AppraiseSettingDAO())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        result += dr["rolename"].ToString()+",";
                    }
                    dr.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 得到相关小类模块信息
        /// </summary>
        public string getModelInfo(string[] str, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = "select modelname from  a_appraise_model b where b.id in (";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == "")
                    return "";
                if (i != str.Length - 1)
                    strSQL += str[i] + ",";
                else
                    strSQL += str[i];
            }
            strSQL += ") and isnull(b.isdel,0)<>1";
            string result = "";
            DBbase.zhpj.AppraiseSetting values = new DBbase.zhpj.AppraiseSetting();
            try
            {
                using (AppraiseSettingDAO dl = new AppraiseSettingDAO())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        result += dr["modelname"].ToString() + ",";
                    }
                    dr.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 取相关模块的自定义指标数据
        /// </summary>
        /// <param name="model">数据模型类别</param>
        /// <returns></returns>
        public DataTable GetDiction(string model)
        {
            string sql = string.Format("select * from a_appraise_field_detail where model='{0}' and isbase <> '2'", model);
            try
            {
                using (AppraiseSettingDAO dl = new AppraiseSettingDAO())
                {
                    return dl.ExecuteDataset(sql).Tables[0];
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 取相关大类模块的小类模块数据
        /// </summary>
        /// <param name="model">数据模型类别</param>
        /// <returns></returns>
        public DataTable GetModelDiction(string parentmodel)
        {
            string sql = string.Format("select * from a_appraise_model where parentmodel='{0}' and isbase = '1' and isnull(isdel,0)<>1", parentmodel);
            try
            {
                using (bacgDL.zhpj.AppraiseSettingDAO dl = new bacgDL.zhpj.AppraiseSettingDAO()) 
                {
                    return dl.ExecuteDataset(sql).Tables[0];
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 初始化模块大小类下拉列表
        /// </summary>
        /// <param name="model">数据模型类别</param>
        /// <returns></returns>
        public ArrayList getModelDropList(string flag, string parentmodel,out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = "select * from a_appraise_model where 1=1 and isnull(isdel,0)<>1";
            if(flag=="0")
                strSQL = strSQL + " and isbase='0'";
            else
                strSQL = strSQL + " and isbase='1'and parentmodel='" + parentmodel + "'";
            ArrayList values = new ArrayList();
            try
            {
                using (AppraiseSettingDAO dl = new AppraiseSettingDAO())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        AppraiseSettingModel model = new AppraiseSettingModel();
                        model.setId(dr["ID"].ToString());
                        model.setIsbase(dr["ISBASE"].ToString());
                        model.setFormTemplate(dr["FormTemplate"].ToString());
                        model.setModelName(dr["MODELNAME"].ToString());
                        model.setParentModel(dr["PARENTMODEL"].ToString());
                        model.setRemark(dr["REMARK"].ToString());
                        values.Add(model);
                    }
                    dr.Close();
                }
                return values;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 初始化导出模班下拉列表
        /// </summary>
        /// <param name="model">数据模型类别</param>
        /// <returns></returns>
        public ArrayList getTempModelDropList(out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = "select * from a_appraise_TFile where 1=1";

            ArrayList values = new ArrayList();
            try
            {
                using (bacgDL.zhpj.AppraiseSettingDAO dl = new bacgDL.zhpj.AppraiseSettingDAO())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {
                        AppraiseSettingTFile model = new AppraiseSettingTFile();
                        model.setId(dr["ID"].ToString());
                        model.setViewName(dr["ViewName"].ToString());
                        model.setTempName(dr["TempName"].ToString());
                        values.Add(model);
                    }
                    dr.Close();
                }
                return values;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 得到模块小类信息
        /// </summary>
        public DBbase.zhpj.AppraiseSettingModel getModelInfo(int id, out string ErrMsg)
        {
            ErrMsg = "";
            string strSQL = string.Format(@"select * from a_appraise_model  a
                                            where 1=1 and a.ID='{0}' and isnull(isdel,0)<>1 ", id);
            DBbase.zhpj.AppraiseSettingModel values = new DBbase.zhpj.AppraiseSettingModel();
            try
            {
                using (AppraiseSettingDAO dl = new AppraiseSettingDAO())
                {
                    SqlDataReader dr = dl.ExecReaderSql(strSQL);
                    while (dr.Read())
                    {

                        values.setId(dr["ID"].ToString());
                        values.setIsbase(dr["ISBASE"].ToString());
                        values.setFormTemplate(dr["FormTemplate"].ToString());
                        values.setModelName(dr["MODELNAME"].ToString());
                        values.setParentModel(dr["PARENTMODEL"].ToString());
                        values.setRemark(dr["REMARK"].ToString());
                    }
                    dr.Close();
                }
                return values;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }
        }
    }



    public class Account
    {
        public string Expresstion;//String类型表达式 

        public Account(string Exp)
        {
            Expresstion = Exp;
        }

        // 判断字符串是否为数值 
        public bool IsNumberExp(string str)
        {
            bool isnumeric = false;
            byte c;
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            for (int i = 0; i < bytestr.Length; i++)
            {
                c = bytestr[i];
                if ((c >= 48 && c <= 57) || c == 46)
                {
                    isnumeric = true; 
                }
                else
                {
                    if (c == 45 && bytestr.Length > 1)
                    {
                        isnumeric = true;
                    }
                    else
                    {
                        isnumeric = false;
                        break;
                    }
                }
            }
            return isnumeric;
        }

        // 基本一目计算 
        public double account(double n1, double n2, string num_op)
        {
            double aresult = 0;
            switch (num_op)
            {
                case "+":
                    aresult = n1 + n2;
                    break;
                case "-":
                    aresult = n1 - n2;
                    break;
                case "*":
                    aresult = n1 * n2;
                    break;
                case "/":
                    aresult = n1 / n2;
                    break;
            }
            return aresult;
        }

        // 将String类型表达式转为由操作数和运算符组成的ArrayList类型表达式 
        public ArrayList Toexp_arraylist(string exp_str)
        {
            string exp_element = "", expchar;
            ArrayList exp_arraylist = new ArrayList();
            //遍历表达式 
            for (int i = 0; i < exp_str.Length; i++)
            {
                expchar = exp_str.Substring(i, 1);
                //如果该字符为数字,小数字或者负号(非运算符的减号） 
                if (char.IsNumber(exp_str, i) || expchar == "." || (expchar == "-" && (i == 0 || exp_str.Substring(i - 1, 1) == "(")))
                {
                    exp_element += expchar;//存为操作数 
                }
                else//为运算符 
                {
                    //将操作数添加到ArrayList类型表达式 
                    if (exp_element != "")
                        exp_arraylist.Add(exp_element);
                    //将运算符添加到ArrayList类型表达式 
                    exp_arraylist.Add(expchar);
                    exp_element = "";
                }
            }
            //如果还有操作数未添加到ArrayList类型表达式,则执行添加操作 
            if (exp_element != "")
                exp_arraylist.Add(exp_element);
            return exp_arraylist;
        }

        //返回运算符的优先级 
        private int Operatororder(string op)
        {
            switch (op)
            {
                case "*":
                    return 3;
                    break;
                case "/":
                    return 4;
                    break;
                case "+":
                    return 1;
                    break;
                case "-":
                    return 2;
                    break;
                default:
                    return 0;
                    break;
            }
        }

        private bool IsPop(string op, Stack operators)
        {
            if (operators.Count == 0)
            {
                return false;
            }
            else
            {
                if (operators.Peek().ToString() == "(" || Operatororder(op) > Operatororder(operators.Peek().ToString()))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        //将ArrayList类型的中缀表达式转为ArrayList类型的后缀表达式 
        public ArrayList Toexpback_arraylist(ArrayList exp)
        {
            ArrayList expback_arraylist = new ArrayList();
            Stack operators = new Stack();
            string op;
            //遍历ArrayList类型的中缀表达式 
            foreach (string s in exp)
            {
                //若为数字则添加到ArrayList类型的后缀表达式 
                if (IsNumberExp(s))
                {
                    expback_arraylist.Add(s);
                }
                else
                {
                    switch (s)
                    {
                        //为运算符 
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                            while (IsPop(s, operators))
                            {
                                expback_arraylist.Add(operators.Pop().ToString());
                            }
                            operators.Push(s);
                            break;
                        //为开括号 
                        case "(":
                            operators.Push(s);
                            break;
                        //为闭括号 
                        case ")":
                            while (operators.Count != 0)
                            {
                                op = operators.Pop().ToString();
                                if (op != "(")
                                {
                                    expback_arraylist.Add(op);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;
                        default:
                            throw new Exception("错误"); ;
                            break;


                    }

                }
            }
            while (operators.Count != 0)
            {
                expback_arraylist.Add(operators.Pop().ToString());
            }
            return expback_arraylist;
        }

        //计算一个ArrayList类型的后缀表达式的值 
        public double ExpValue(ArrayList expback)
        {
            double num1, num2, result = 0;
            Stack num = new Stack();
            foreach (string n in expback)
            {
                if (IsNumberExp(n))
                {
                    num.Push(n);
                }
                else
                {
                    num2 = Convert.ToDouble(num.Pop());
                    num1 = Convert.ToDouble(num.Pop());
                    result = account(num1, num2, n);
                    num.Push(result);
                }
            }
            return result;
        }

        //返回本类的表达式值 
        public object ExpValue()
        {
            try
            {
                ArrayList a1 = new ArrayList();
                ArrayList a2 = new ArrayList();
                a1 = Toexp_arraylist(Expresstion);
                a2 = Toexpback_arraylist(a1);
                return ExpValue(a2);
            }
            catch
            {
                return "err";
            }
        }
    }
}
