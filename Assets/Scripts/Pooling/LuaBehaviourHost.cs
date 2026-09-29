using UnityEngine;

namespace Game.Pooling
{
    /// <summary>
    /// 提供不依赖 xLua 的组件契约, 避免玩法程序集与 xLua 生成代码形成循环依赖.
    /// </summary>
    public abstract class LuaBehaviourHost : MonoBehaviour, IPoolable
    {
        public abstract string TypeName { get; }

        public abstract void CallLuaFunction(string functionName);
        public abstract void CallLuaFunction(string functionName, Vector2 value);
        public abstract void CallLuaFunction(string functionName, float value);
        public abstract void CallLuaFunction(string functionName, GameObject value);
        public abstract void CallLuaFunction(string functionName, Vector2 direction, float deltaTime);
        public abstract bool CallLuaFunctionBool(string functionName);
        public abstract bool HasLuaFunction(string functionName);
        public abstract void SetLuaField(string fieldName, object value);
        public abstract void OnSpawnFromPool();
        public abstract void OnRecycleToPool();
    }
}
