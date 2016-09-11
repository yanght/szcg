using System;
using System.Collections.Generic;
using System.Text;

namespace Teamax.Common
{

    internal class JavaScriptWriter
    {
        public JavaScriptWriter() { }

        private StringBuilder sb = new StringBuilder();
        private int currIndent = 0;
        private int openBlocks = 0;
        private bool format = false;

        public JavaScriptWriter(bool Formatted)
        {
            format = Formatted;
        }
        /// <summary>
        /// ��ǰ�������
        /// </summary>
        public int Indent
        {
            get { return currIndent; }
            set { currIndent = value; }
        }

        public void AddLine(params string[] parts)
        {
            if (format)
            {
                for (int i = 0; i < currIndent; i++)
                {
                    sb.Append("\t");
                }
                foreach (string part in parts)
                {
                    sb.Append(part);
                }
            }
            if (format)
            {
                sb.Append(Environment.NewLine);
            }
            else if (parts.Length > 0)
            {
                sb.Append(" ");
            }
        }

        /// <summary>
        /// ���롰{����ʹ�������һ��
        /// </summary>
        public void OpenBlock()
        {
            AddLine("{");
            currIndent++;
            openBlocks++;

        }
        /// <summary>
        /// ���롰}������ʹ�����չһ��
        /// </summary>
        public void CloseBlock()
        {
            if (openBlocks < 1)
                throw new InvalidOperationException("�ڵ���JavaScriptWriter.CloseBlock()ʱû����ǰ��JavaScriptWriter.OpenBlock()����");
            currIndent--;
            openBlocks--;
            AddLine("}");
        }
        /// <summary>
        /// ΪJavascript����ע��
        /// </summary>
        /// <param name="CommentText">ע����ַ�������</param>
        public void AddConnentLine(params string[] CommentText)
        {
            if (format)
            {
                for (int i = 0; i < currIndent; i++)
                {
                    sb.Append("\t");
                }
                sb.Append("//");
                foreach (string part in CommentText)
                {
                    sb.Append(part);
                }
                sb.Append(Environment.NewLine);
            }
        }
        public string ReadSaveJavascript(string scriptPath)
        {
            System.IO.FileStream fs = new System.IO.FileStream(scriptPath, System.IO.FileMode.Open);
            System.IO.StreamReader reader = new System.IO.StreamReader(fs);

            reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            JavaScriptWriter script = new JavaScriptWriter(true);

            while (!reader.EndOfStream)
            {
                string strLine = reader.ReadLine();
                if (strLine.Length > 0)
                    strLine = strLine.Replace("\"", "\\" + "\"");
                script.AddLine(strLine);
            }
            fs.Close();
            return script.ToString();
        }
        public override string ToString()
        {
            if (openBlocks > 0)
                throw new InvalidOperationException("JavascriptWriter��û����Ӧ�Ĺرձ�ʶ");
            return string.Format("<script type=\"text/javascript\">{0}{1}</script>", Environment.NewLine, sb);
        }

    }
}
