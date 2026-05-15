using CSObjectWrapEditor;
using UnityEngine;

namespace XLua.Editor
{
    /// <summary>
    /// XLua 生成代码路径配置.
    /// </summary>
    public static class XLuaGenPathConfig
    {
        /// <summary>
        /// 生成代码需要和 XLua.Runtime 在同一程序集内, 否则 partial 类型无法合并.
        /// </summary>
        [GenPath]
        public static readonly string GenPath = Application.dataPath + "/XLua/Src/Gen/";
    }
}
