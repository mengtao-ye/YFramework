namespace YFramework
{
    public class  SheetClass
    {
        public string sheetName { get; set; }//Sheet名
        public string mainKey { get; set; }//主键
        public string properityName { get; set; }//变量名
        public string properityClassName { get; set; }//变量存储的类型
        public string properityTypeNamespace { get; set; }//变量存储的类型
        public VariableClass[] varList { get; set; }//类的属性值与Excel表的列名参数
        public VariableClass Find(string colName) {
            for (int i = 0; i < varList.Length; i++)
            {
                if (varList[i].excelCol == colName) return varList[i];
            }
            return null;
        }
    }
}
