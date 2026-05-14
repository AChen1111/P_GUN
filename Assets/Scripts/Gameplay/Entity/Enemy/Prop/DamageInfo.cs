using UnityEngine;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    public class DamageInfo {
        public int Damage { get; set; } = 1;

        /// <summary>
        /// 伤害来源方向，约定为攻击/子弹从来源飞向受击者的方向。
        /// </summary>
        public Vector2 SourceDirection { get; set; } = Vector2.zero;

        public DamageInfo() {
        }

        public DamageInfo(int damage, Vector2 sourceDirection) {
            Damage = damage;
            SourceDirection = sourceDirection;
        }
    }
}
