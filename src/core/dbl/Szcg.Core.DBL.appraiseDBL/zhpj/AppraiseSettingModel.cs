using System;
using System.Collections.Generic;
using System.Text;

namespace DBbase.zhpj
{
    public class AppraiseSettingModel
    {
        private String id = "";
        private String modelname = "";
        private String parentmodel = "";
        private String remark = "";
        private String isbase = "";
        private String formTemplate = "";

        public String getId()
        {
            return id;
        }
        public void setId(String id)
        {
            this.id = id;
        }
        public String getModelName()
        {
            return modelname;
        }
        public void setModelName(String modelname)
        {
            this.modelname = modelname;
        }
        public String getParentModel()
        {
            return parentmodel;
        }
        public void setParentModel(String parentmodel)
        {
            this.parentmodel = parentmodel;
        }
        public String getRemark()
        {
            return remark;
        }
        public void setRemark(String remark)
        {
            this.remark = remark;
        }
        public String getIsbase()
        {
            return isbase;
        }
        public void setIsbase(String isbase)
        {
            this.isbase = isbase;
        }
        public String getFormTemplate()
        {
            return formTemplate;
        }
        public void setFormTemplate(String formTemplate)
        {
            this.formTemplate = formTemplate;
        }
    }

}
