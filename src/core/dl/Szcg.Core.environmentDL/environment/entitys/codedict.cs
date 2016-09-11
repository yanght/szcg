namespace bacgDL.environment.entitys
{
    public class codedictcls
    {
        public int codetype;        //代码类型
        public int codeid;          //代码识别
        public string codename;     //代码名称
        public string inputcode;    //输入代码
        public string standardcode; //标准编码
        public int systemid;        //系统识别
        public string status;         //使用状态


        public int kind;            //查询类别
        public void setEmpty()
        {
            codetype = 0;
            codeid = 0;
            codename = "";
            inputcode = "";
            standardcode = "";
            systemid = 0;
            status = "0";
            kind = 0;

        }
    }
}

