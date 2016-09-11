using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using bacgDL.business;
using DBbase.zhpj;


namespace bacgDL.zhpj
{
    public class AppraiseSettingDAO : Teamax.Common.CommonDatabase
    {
        private int resutlCount = 0;

        public int getResutlCount()
        {
            return resutlCount;
        }

        public void setResutlCount(int resutlCount)
        {
            this.resutlCount = resutlCount;
        }

         /*
	     * 增加一个自定义字段
	     */
        public int insertAppraiseSetting(AppraiseSetting appraiseSetting, ref string strErr)
        {
            String isDisplay = appraiseSetting.getIsDisplay();
            String isBase = "1";
            String insertSql = "";
            // 构造插入的sql语句
            InsertSqlMaker insertSqlMaker = new InsertSqlMaker("a_appraise_field_detail");
            //insertSqlMaker.addData("ID", "'"+ appraiseSetting.getId()+ "'");
            insertSqlMaker.addData("FIELDNAME", "'" + appraiseSetting.getFieldName() + "'");
            insertSqlMaker.addData("VIEWNAME", "'" + appraiseSetting.getViewName() + "'");
            insertSqlMaker.addData("CODENAME", "'" + appraiseSetting.getCodeName() + "'");
            insertSqlMaker.addData("EXPRESS", "'" + appraiseSetting.getExpress() + "'");
            insertSqlMaker.addData("EXPRESS_", "'" + appraiseSetting.getExpress_() + "'");
            insertSqlMaker.addData("ISBASE", "'" + isBase + "'");
            insertSqlMaker.addData("ISDISPLAY", "'" + isDisplay + "'");
            insertSqlMaker.addData("MODEL", "'" + appraiseSetting.getModel() + "'");
            insertSqlMaker.addData("model_defined", "'" + appraiseSetting.getModelDefined() + "'");
            insertSqlMaker.addData("REMARK", "'" + appraiseSetting.getRemark() + "'");
            insertSqlMaker.addData("ROLEID", "'" + appraiseSetting.getRoleid() + "'");
            int order_ = this.getFieldOrderNum(appraiseSetting.getModel()) + 1;
            insertSqlMaker.addData("ORDER_", "" + order_ + "");
            //insertSqlMaker.addData("FORMTEMPLATE", "'" + appraiseSetting.getFormTemplate() + "'");

            insertSqlMaker.addData("TABLENAME", "'" + appraiseSetting.getTablename() + "'");
            insertSqlMaker.addData("PROCEDURENAME", "'" + appraiseSetting.getProcedurename() + "'");

            insertSql = insertSqlMaker.getSql();
            // 执行操作
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(insertSql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
	     * 删除一个自定义字段（只允许删除用户数据）
	     */
        public int deleteField(String id, ref string strErr)
        {
		    String sql = " delete from a_appraise_field_detail where id='"+id+"' and isbase='1'";
            // 执行操作
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(sql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
	    }

        /**
         * 获得字段的顺序号
         */
        public int getFieldOrderNum(string model)
        {
            int res = 0;
            String sql = " select isnull(max(order_),0) as ORDER_ from a_appraise_field_detail where model='" + model + "' ";
            // 执行操作
            SqlDataReader dr = (SqlDataReader)this.ExecuteReader(sql);
            while (dr.Read())
            {
                res = Convert.ToInt32(dr["ORDER_"].ToString());
            }
            dr.Close();
            return res;
        }

        /*
         * 对字段进行排序
         */
        //public void orderField(string model, string fieldID, string status, ref string strErr)
        //{
        //    //首先得到字段的位置
        //    int order_num = 0;
        //    int max_num = 0;
        //    int min_num = 0;
        //    String sql = " select order_ from a_appraise_field_detail where id='" + fieldID + "'";
        //    String sql2 = " select isnull(max(order_),0) as MAX_ORDER,isnull(min(order_),0) as MIN_ORDER from a_appraise_field_detail where model='" + model + "'";
        //    try
        //    {
        //        SqlDataReader vResult = (SqlDataReader)this.ExecuteReader(sql);
        //        Hashtable hTemp = null;
        //        while (vResult.Read())
        //        {
        //            if ("".Equals(vResult["ORDER_"].ToString()))
        //            {
        //                order_num = 0;
        //            }
        //            else
        //            {
        //                order_num = Convert.ToInt32(vResult["ORDER_"].ToString());
        //            }
        //        }
        //        vResult.Close();
        //        SqlDataReader vResult2 = (SqlDataReader)this.ExecuteReader(sql2);
        //        while (vResult2.Read())
        //        {
        //            if (vResult2["MAX_ORDER"].ToString() == null)
        //            {
        //                max_num = 0;
        //            }
        //            else
        //            {
        //                max_num = Convert.ToInt32(vResult2["MAX_ORDER"].ToString());
        //            }
        //            if (vResult2["MIN_ORDER"].ToString() == null)
        //            {
        //                min_num = 0;
        //            }
        //            else
        //            {
        //                min_num = Convert.ToInt32(vResult2["MIN_ORDER"].ToString());
        //            }
        //        }
        //        vResult2.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        strErr = ex.Message;
        //    }
        //    try
        //    {
        //        sql2 = "";
        //        if ("UP".Equals(status))
        //        {//向上移动
        //            if (min_num != order_num)
        //            {//当前位置不在第一位
        //                sql = " update a_appraise_field_detail set order_=order_+1 where model='" + model + "'and order_=" + (order_num - 1) + "";
        //                sql2 = " update a_appraise_field_detail set order_=order_-1 where id = '" + fieldID + "'";
        //                this.ExecuteReader(sql);
        //                this.ExecuteReader(sql2);
        //            }
        //        }
        //        else
        //        {//向下移动
        //            if (max_num != order_num)
        //            {//当前位置不在最后一位
        //                sql = " update a_appraise_field_detail set order_=order_-1 where model='" + model + "'and order_=" + (order_num + 1) + "";
        //                sql2 = " update a_appraise_field_detail set order_=order_+1 where id = '" + fieldID + "'";
        //                this.ExecuteReader(sql);
        //                this.ExecuteReader(sql2);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strErr = ex.Message;
        //    }
        //}


        /**
         * 更新一条记录
         * @param appraiseSetting
         * 封装要更新的信息
         */
        public int updateAppraiseSetting(AppraiseSetting appraiseSetting, ref string strErr)
        {
            if (appraiseSetting.getId() == null
                    || "".Equals(appraiseSetting.getId()))
            {
                strErr = "AppraiseSettingDAO.updateAppraiseSetting()|appraiseSetting.ID不能为空";
                return 0;
            }
            String updateSql = "";
            String isDisplay = appraiseSetting.getIsDisplay();
            // 构造更新的sql语句
            UpdateSqlMaker updateSqlMaker = new UpdateSqlMaker("a_appraise_field_detail");
            updateSqlMaker.addData("codeName", "'" + appraiseSetting.getCodeName() + "'");
            updateSqlMaker.addData("express", "'" + appraiseSetting.getExpress() + "'");
            updateSqlMaker.addData("express_", "'" + appraiseSetting.getExpress_() + "'");
            updateSqlMaker.addData("viewName", "'" + appraiseSetting.getViewName() + "'");
            updateSqlMaker.addData("FIELDNAME", "'" + appraiseSetting.getFieldName() + "'");

            updateSqlMaker.addData("isDisplay", "'" + isDisplay + "'");
            updateSqlMaker.addData("model", "'" + appraiseSetting.getModel() + "'");
            updateSqlMaker.addData("model_defined", "'" + appraiseSetting.getModelDefined() + "'");
            updateSqlMaker.addData("remark", "'" + appraiseSetting.getRemark() + "'");
            updateSqlMaker.addData("roleid", "'" + appraiseSetting.getRoleid() + "'");

            updateSqlMaker.addData("TABLENAME", "'" + appraiseSetting.getTablename() + "'");
            updateSqlMaker.addData("PROCEDURENAME", "'" + appraiseSetting.getProcedurename() + "'");

            updateSqlMaker.setWhere("id = '" + appraiseSetting.getId() + "'");
            updateSql = updateSqlMaker.getSql();
            // 执行操作
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(updateSql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
        * 增加一个自定义模块
        */
        public int insertAppraiseSettingModel(AppraiseSettingModel appraiseSettingModel, ref string strErr)
        {
            String insertSql = "";
            // 构造插入的sql语句
            InsertSqlMaker insertSqlMaker = new InsertSqlMaker("a_appraise_model");

            insertSqlMaker.addData("ID", "@id");
            insertSqlMaker.addData("MODELNAME", "'" + appraiseSettingModel.getModelName() + "'");
            insertSqlMaker.addData("PARENTMODEL", "'" + appraiseSettingModel.getParentModel() + "'");
            insertSqlMaker.addData("ISBASE", "'" + appraiseSettingModel.getIsbase() + "'");
            insertSqlMaker.addData("REMARK", "'" + appraiseSettingModel.getRemark() + "'");
            insertSqlMaker.addData("FORMTEMPLATE", "'" + appraiseSettingModel.getFormTemplate() + "'");

            insertSql = insertSqlMaker.getSql();
            // 执行操作
            try
            {
                int result = 0;
                insertSql = "declare @id int select @id=max(id)+1 from dbo.a_appraise_model " + insertSql;

                result = this.ExecuteNonQuery(insertSql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
	     * 删除一个自定义模块
	     */
        public int deleteAppraiseSettingModel(String id, ref string strErr)
        {
            String sql = " update a_appraise_model set IsDel = 1 where id='" + id + "' and isbase='1'";
            // 执行操作
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(sql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
         * 编辑一个自定义模块
         */
        public int updateAppraiseSettingModel(AppraiseSettingModel appraiseSettingModel, ref string strErr)
        {
            if (appraiseSettingModel.getId() == null
                    || "".Equals(appraiseSettingModel.getId()))
            {
                strErr = "AppraiseSettingDAO.updateAppraiseSetting()|appraiseSetting.ID不能为空";
                return 0;
            }
            String updateSql = "";
            // 构造更新的sql语句
            UpdateSqlMaker updateSqlMaker = new UpdateSqlMaker("a_appraise_model");

            updateSqlMaker.addData("MODELNAME", "'" + appraiseSettingModel.getModelName() + "'");
            updateSqlMaker.addData("PARENTMODEL", "'" + appraiseSettingModel.getParentModel() + "'");
            updateSqlMaker.addData("ISBASE", "'" + appraiseSettingModel.getIsbase() + "'");
            updateSqlMaker.addData("REMARK", "'" + appraiseSettingModel.getRemark() + "'");
            updateSqlMaker.addData("FORMTEMPLATE", "'" + appraiseSettingModel.getFormTemplate() + "'");

            updateSqlMaker.setWhere("id = '" + appraiseSettingModel.getId() + "'");
            updateSql = updateSqlMaker.getSql();
            // 执行操作
            try
            {
                int result = 0;
                result = this.ExecuteNonQuery(updateSql);
                return result;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return 0;
            }
        }

        /*
         * 校验同名模块
         */
        public int getSameNameNum(string name,string id)
        {
            int res = 0;
            String sql = " select isnull(count(modelname),0) as sum from a_appraise_model where isnull(isdel,0)<>1 and parentmodel = '" + id + "' and modelname='" + name + "' ";
            // 执行操作
            SqlDataReader dr = (SqlDataReader)this.ExecuteReader(sql);
            while (dr.Read())
            {
                res = Convert.ToInt32(dr["sum"].ToString());
            }
            dr.Close();
            return res;
        }

        /// <summary>
        /// 得到模块类型数据列表
        /// </summary>
        /// <returns></returns>
        public DataSet getModelinfolist(AppraiseSettingModel prj, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@ID",prj.getParentModel()),
                                new SqlParameter("@MODELNAME",prj.getModelName()),
                                new SqlParameter("@CurrentPage",page.CurrentPage),    
                                new SqlParameter("@RowCount",SqlDbType.Int),          //输出
                                new SqlParameter("@PageCount",SqlDbType.Int),         //输出
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[3].Direction = ParameterDirection.Output;
            arrSP[4].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_a_GetModelinfolist", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[3].Value.ToString();
            page.PageCount = arrSP[4].Value.ToString();

            return ds;
        }

        /// <summary>
        /// 得到自定义字段信息列表
        /// </summary>
        /// <returns></returns>
        public DataSet getFieldList(AppraiseSetting prj, PageInfo page)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@viewName",prj.getViewName()),
                                new SqlParameter("@codeName",prj.getCodeName()),
                                new SqlParameter("@model",prj.getModel()),  
                                new SqlParameter("@modeldefined",prj.getModelDefined()),          
                                new SqlParameter("@CurrentPage",page.CurrentPage),    
                                new SqlParameter("@RowCount",SqlDbType.Int),          //输出
                                new SqlParameter("@PageCount",SqlDbType.Int),         //输出
                                new SqlParameter("@PageSize",page.PageSize),
                                new SqlParameter("@Order",page.Order),
                                new SqlParameter("@Field",page.Field)};

            arrSP[5].Direction = ParameterDirection.Output;
            arrSP[6].Direction = ParameterDirection.Output;

            DataSet ds = this.ExecuteDataset("pr_a_GetAppraiseSetting", CommandType.StoredProcedure, arrSP);
            page.RowCount = arrSP[5].Value.ToString();
            page.PageCount = arrSP[6].Value.ToString();

            return ds;
        }

        /// <summary>
        /// 为自定义字段进行排序
        /// </summary>
        /// <returns></returns>
        public void orderField(string model, string id, string status)
        {
            SqlParameter[] arrSP = new SqlParameter[]{
                                new SqlParameter("@model",model),
                                new SqlParameter("@id",id),
                                new SqlParameter("@status",status)};
            this.ExecuteNonQuery("pr_a_orderField", CommandType.StoredProcedure, arrSP);
        }

        #region ExecReaderSql：执行sql语句,返回datareader类型的数据集
        /// <summary>
        /// 执行sql语句,返回datareader类型的数据集
        /// </summary>
        /// <param name="strSQL">要查询的sql语句</param>
        /// <returns></returns>
        public SqlDataReader ExecReaderSql(string strSQL)
        {
            return (SqlDataReader)this.ExecuteReader(strSQL);
        }
        #endregion

        /*
         * 得到自定义字段信息列表
         */
        //public ArrayList getFieldList(AppraiseSetting appraiseSetting)
        //{
        //    ArrayList list = new ArrayList(); //存放返回结果的对象
        //    String querySql = "select ID from a_appraise_field_detail where 1=1 ";
        //    if (!"".Equals(appraiseSetting.getViewName()))
        //    {
        //        querySql += " and viewName like '" + appraiseSetting.getViewName() + "%'";
        //    }
        //    if (!"".Equals(appraiseSetting.getCodeName()))
        //    {
        //        querySql += " and codeName like '" + appraiseSetting.getCodeName() + "%'";
        //    }
        //    if (!"".Equals(appraiseSetting.getModel()))
        //    {
        //        querySql += " and model = '" + appraiseSetting.getModel() + "'";
        //    }
        //    querySql += " order by ORDER_ asc ";
        //    // 执行操作
        //    DbOperator dbOperator = new DbOperator();
        //    Vector vResult = null;
        //    try
        //    {
        //        dbOperator.getConnection();
        //        vResult = dbOperator.openPageSql(querySql, startIndex, pageSize);
        //        this.resutlCount = dbOperator.getResultCount();
        //        Hashtable hTemp = null;
        //        for (int i = 0; i < vResult.size(); i++)
        //        {
        //            hTemp = (Hashtable)vResult.get(i);
        //            String id = (String)hTemp.get("ID");
        //            list.add(this.getFieldDetail(id));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.printStackTrace();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        dbOperator.freeConnection();
        //    }
        //    return list;
        //}

        /*
         * 判断指标代码是否存在
         */
        public Boolean isCodeNameExist(String codeName, ref string strErr)
        {
            Boolean res = false;
            String querySql = "select * from a_appraise_field_detail where codeName='" + codeName.Trim() + "'";

            // 执行操作
            try
            {

                SqlDataReader dr = (SqlDataReader)this.ExecuteReader(querySql);
                if (dr.Read())
                {
                    res = true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return res;
        }

        /*
         * 判断fieldname是否存在
         */
        public Boolean isFieldNameExist(String fieldName, ref string strErr)
        {
            Boolean res = false;
            String querySql = "select * from a_appraise_field_detail where fieldName='" + fieldName.Trim() + "'";

            // 执行操作
            try
            {
                SqlDataReader dr = (SqlDataReader)this.ExecuteReader(querySql);
                if (dr.Read())
                {
                    res = true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
            }
            return res;
        }

        /*
         * 得到自定义字段
         */
        //public ArrayList getFieldList(AppraiseSetting appraiseSetting, ref string strErr)
        //{
        //    ArrayList list = new ArrayList(); //存放返回结果的对象
        //    String querySql = "select ID from a_appraise_field_detail where 1=1 ";
        //    if (!"".Equals(appraiseSetting.getViewName()))
        //    {
        //        querySql += " and viewName like '" + appraiseSetting.getViewName() + "%'";
        //    }
        //    if (!"".Equals(appraiseSetting.getCodeName()))
        //    {
        //        querySql += " and codeName like '" + appraiseSetting.getCodeName() + "%'";
        //    }
        //    if (!"".Equals(appraiseSetting.getModel()))
        //    {
        //        querySql += " and model = '" + appraiseSetting.getModel() + "'";
        //    }
        //    querySql += " order by ID desc ";


        //     执行操作
        //    try
        //    {
        //        DataSet ds = this.ExecuteDataset(querySql);
        //        this.resutlCount = ds.Tables[0].Rows.Count;
        //        for (int i = 0; i < resutlCount; i++)
        //        {
        //            String id = ds.Tables[0].Rows[0].ToString();
        //            list.Add(id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strErr = ex.Message;
        //    }
        //    return list;
        //}

    }

    public class InsertSqlMaker
    {

        public InsertSqlMaker(String tableName)
        {
            if (tableName.Length == 0)
            {
                //throw new Exception("数据库表名不能为空！");
            }
            this.tableName = tableName;
        }

        /** 表名*/
        private String tableName;

        /** 用于保存插入的数据*/
        ArrayList arrayList_Data = new ArrayList();

        /** 保存一项数据的bean*/
        OptionBean optionBean = null;

        /**
         * 添加要插入的数据
         * @param fieldName 字段名
         * @param data 字段数据，如果如果是字符串，带''等
         */
        public void addData(String fieldName, String data)
        {
            if (!"".Equals(data))
            {
                optionBean = new OptionBean(fieldName, data);
                arrayList_Data.Add(optionBean);
            }
        }

        /** 得到构造的sql*/
        public String getSql()
        {
            if (this.arrayList_Data.Count == 0)
            {
                //throw new Exception("没有插入数据！");
            }

            String mySql = "";//sql语句
            String mySql_field = "";//sql的field部分
            String mySql_data = "";//sql的data部分

            OptionBean optionBean;
            //构造field, data
            for (int i = 0; i < this.arrayList_Data.Count; i++)
            {
                optionBean = (OptionBean)this.arrayList_Data[i];
                mySql_field += optionBean.getLabel() + ",";
                mySql_data += optionBean.getValue() + ",";
            }
            //去掉最后一个","
            mySql_field = mySql_field.Substring(0, mySql_field.Length - 1);
            mySql_data = mySql_data.Substring(0, mySql_data.Length - 1);

            mySql = "INSERT INTO " + this.tableName;
            mySql += " (" + mySql_field + ") ";
            mySql += "VALUES(" + mySql_data + ")";
            return mySql;
        }
        public String getTableName()
        {
            return tableName;
        }
        public void setTableName(String tableName)
        {
            if (tableName.Length == 0)
            {
                //throw new Exception("数据库表名不能为空！");
            }

            this.tableName = tableName;
        }
    }

    public class UpdateSqlMaker
    {

        public UpdateSqlMaker(String tableName)
        {
            if (tableName.Length == 0)
            {
                //throw new Exception("数据库表名不能为空！");
            }
            this.tableName = tableName;
        }

        /** 表名*/
        private String tableName = "";

        /**更新的条件*/
        private String where = "";

        /** 用于保存插入的数据*/
        ArrayList arrayList_Data = new ArrayList();

        /** 保存一项数据的bean*/
        OptionBean optionBean = null;

        /**
         * 添加要插入的数据
         * @param fieldName 字段名
         * @param data 字段数据，如果如果是字符串，带''等
         */
        public void addData(String fieldName, String data)
        {
            if (!"".Equals(data))
            {
                optionBean = new OptionBean(fieldName, data);
                arrayList_Data.Add(optionBean);
            }
        }

        /** 得到构造的sql*/
        public String getSql()
        {
            if (this.arrayList_Data.Count == 0)
            {
            }
            if (where.Length <= 0)
            {
            }

            String mySql = "";//sql语句
            mySql = "UPDATE " + this.tableName + " SET ";

            OptionBean optionBean;
            //构造field, data
            for (int i = 0; i < this.arrayList_Data.Count; i++)
            {
                optionBean = (OptionBean)this.arrayList_Data[i];
                mySql += optionBean.getLabel() + "=" + optionBean.getValue() + ",";
            }
            //去掉最后一个","
            mySql = mySql.Substring(0, mySql.Length - 1);

            mySql += " WHERE " + this.where;
            return mySql;
        }
        public String getTableName()
        {
            return tableName;
        }
        public void setTableName(String tableName)
        {
            if (tableName.Length == 0)
            {
            }

            this.tableName = tableName;
        }
        public void setWhere(String where)
        {
            this.where = where;
        }
    }

    public class OptionBean
    {


        // ----------------------------------------------------------- Constructors


        /**
         * Construct an instance with the supplied property values.
         */
        public OptionBean(String label, String value)
        {
            this.label = label;
            this.value = value;
        }


        // ------------------------------------------------------------- Properties


        /**
         * The property which supplies the option label visible to the end user.
         */
        private String label;

        public String getLabel()
        {
            return this.label;
        }

        public void setLabel(String label)
        {
            this.label = label;
        }


        /**
         * The property which supplies the value returned to the server.
         */
        private String value;

        public String getValue()
        {
            return this.value;
        }

        public void setValue(String value)
        {
            this.value = value;
        }


    }
}
