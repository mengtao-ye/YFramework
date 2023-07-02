namespace YFramework
{
    public class XmlClass
    {
        /// <summary>
        /// 类所在的命令空间
        /// </summary>
        public string classNamespace { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string className { get; set; }
        /// <summary>
        /// Xml名字，包含后缀
        /// </summary>
        public string xmlName { get; set; }
        /// <summary>
        /// Excel名字,包含后缀
        /// </summary>
        public string excelName { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public SheetClass[] sheetClasses { get; set; }
    }
}
