using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.zhpj
{
    public class AppraiseSetting
    {
        private String id = "";
        private String fieldName = "";
        private String viewName = "";
        private String codeName = "";
        private String express = "";
        private String express_ = "";
        private String isBase = "";
        private String isDisplay = "";
        private String model = "";
        private String modeldefined = "";
        private String remark = "";
        private int order_;
        private String tablename = "";
        private String procedurename = "";
        private String roleid = "";
        private String rolename = "";

        public String getExpress()
        {
            return express;
        }
        public void setExpress(String express)
        {
            this.express = express;
        }
        public String getExpress_()
        {
            return express_;
        }
        public void setExpress_(String express_)
        {
            this.express_ = express_;
        }
        public String getFieldName()
        {
            return fieldName;
        }
        public void setFieldName(String fieldName)
        {
            this.fieldName = fieldName;
        }
        public String getId()
        {
            return id;
        }
        public void setId(String id)
        {
            this.id = id;
        }
        public int getOrder_()
        {
            return order_;
        }
        public void setOrder_(int order_)
        {
            this.order_ = order_;
        }

        public String getIsBase()
        {
            return isBase;
        }
        public void setIsBase(String isBase)
        {
            this.isBase = isBase;
        }
        public String getIsDisplay()
        {
            return isDisplay;
        }
        public void setIsDisplay(String isDisplay)
        {
            this.isDisplay = isDisplay;
        }
        public String getViewName()
        {
            return viewName;
        }
        public void setViewName(String viewName)
        {
            this.viewName = viewName;
        }
        public String getModel()
        {
            return model;
        }
        public void setModel(String model)
        {
            this.model = model;
        }

        public String getModelDefined()
        {
            return modeldefined;
        }
        public void setModelDefined(String modeldefined)
        {
            this.modeldefined = modeldefined;
        }

        public String getRemark()
        {
            return remark;
        }
        public void setRemark(String remark)
        {
            this.remark = remark;
        }
        public String getCodeName()
        {
            return codeName;
        }
        public void setCodeName(String codeName)
        {
            this.codeName = codeName;
        }
        public String getTablename()
        {
            return tablename;
        }
        public void setTablename(String tablename)
        {
            this.tablename = tablename;
        }
        public String getProcedurename()
        {
            return procedurename;
        }
        public void setProcedurename(String procedurename)
        {
            this.procedurename = procedurename;
        }
        public String getRoleid()
        {
            return roleid;
        }
        public void setRoleid(String roleid)
        {
            this.roleid = roleid;
        }
        public String getRolename()
        {
            return rolename;
        }
        public void setRolename(String rolename)
        {
            this.rolename = rolename;
        }
    }
}
