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
        /// �õ�ģ��������Ϣ
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
        /// �õ��Զ����ֶ���Ϣ�б�
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
        /// Ϊ�Զ����ֶν�����������
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
        /// �õ��Զ����ֶ���Ϣ
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
                            values.setIsBase("�ǻ�������");
                        else
                            values.setIsBase("��������");
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
        /// �õ���ؽ�ɫ��Ϣ
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
        /// �õ����С��ģ����Ϣ
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
        /// ȡ���ģ����Զ���ָ������
        /// </summary>
        /// <param name="model">����ģ�����</param>
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
        /// ȡ��ش���ģ���С��ģ������
        /// </summary>
        /// <param name="model">����ģ�����</param>
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
        /// ��ʼ��ģ���С�������б�
        /// </summary>
        /// <param name="model">����ģ�����</param>
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
        /// ��ʼ������ģ�������б�
        /// </summary>
        /// <param name="model">����ģ�����</param>
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
        /// �õ�ģ��С����Ϣ
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
        public string Expresstion;//String���ͱ��ʽ 

        public Account(string Exp)
        {
            Expresstion = Exp;
        }

        // �ж��ַ����Ƿ�Ϊ��ֵ 
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

        // ����һĿ���� 
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

        // ��String���ͱ��ʽתΪ�ɲ��������������ɵ�ArrayList���ͱ��ʽ 
        public ArrayList Toexp_arraylist(string exp_str)
        {
            string exp_element = "", expchar;
            ArrayList exp_arraylist = new ArrayList();
            //�������ʽ 
            for (int i = 0; i < exp_str.Length; i++)
            {
                expchar = exp_str.Substring(i, 1);
                //������ַ�Ϊ����,С���ֻ��߸���(��������ļ��ţ� 
                if (char.IsNumber(exp_str, i) || expchar == "." || (expchar == "-" && (i == 0 || exp_str.Substring(i - 1, 1) == "(")))
                {
                    exp_element += expchar;//��Ϊ������ 
                }
                else//Ϊ����� 
                {
                    //����������ӵ�ArrayList���ͱ��ʽ 
                    if (exp_element != "")
                        exp_arraylist.Add(exp_element);
                    //���������ӵ�ArrayList���ͱ��ʽ 
                    exp_arraylist.Add(expchar);
                    exp_element = "";
                }
            }
            //������в�����δ��ӵ�ArrayList���ͱ��ʽ,��ִ����Ӳ��� 
            if (exp_element != "")
                exp_arraylist.Add(exp_element);
            return exp_arraylist;
        }

        //��������������ȼ� 
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

        //��ArrayList���͵���׺���ʽתΪArrayList���͵ĺ�׺���ʽ 
        public ArrayList Toexpback_arraylist(ArrayList exp)
        {
            ArrayList expback_arraylist = new ArrayList();
            Stack operators = new Stack();
            string op;
            //����ArrayList���͵���׺���ʽ 
            foreach (string s in exp)
            {
                //��Ϊ��������ӵ�ArrayList���͵ĺ�׺���ʽ 
                if (IsNumberExp(s))
                {
                    expback_arraylist.Add(s);
                }
                else
                {
                    switch (s)
                    {
                        //Ϊ����� 
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
                        //Ϊ������ 
                        case "(":
                            operators.Push(s);
                            break;
                        //Ϊ������ 
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
                            throw new Exception("����"); ;
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

        //����һ��ArrayList���͵ĺ�׺���ʽ��ֵ 
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

        //���ر���ı��ʽֵ 
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
