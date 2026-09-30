using System;
using Game.Core;
using UnityEngine;

/// <summary>
/// 表格资源列写入短名并立即检查 Catalog, 保留原列名和导出格式.
/// </summary>
public static class AddressableImportKeys
{
    public static void Map<T>(Excel2SoMapping map, string column, string property, bool list = false) where T : UnityEngine.Object
    {
        if (list) map.Column(column).To(property).AsStringList(";");
        else map.Column(column).To(property).AsString();
        map.Column(column).Custom((row, _) =>
        {
            if (!row.TryGet(column, out var value) || string.IsNullOrWhiteSpace(value)) return;
            var keys = list ? value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries) : new[] { value };
            foreach (var key in keys)
            {
                // 只检查已注册资源, 导入器不自动推测路径或补注册条目.
                AddressableAssetAccess.Get<T>(list ? key.Trim() : key);
            }
        });
    }
}
