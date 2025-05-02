using System.Collections;
using UnityEngine;
//用途：經驗寶石，可以收集
namespace Vampire
{
    public enum GemType 
    {
        White1 = 1,
        Blue2 = 2,
        Green10 = 10,
        Red50 = 50
    }

    public class ExpGem : Collectable
    {
        [Header("Gem Dependencies")]
        [SerializeField] protected ExpGemBlueprint gemBlueprint;
        protected GemType gemType;
        protected TrailRenderer trailRenderer;
        protected SpriteRenderer spriteRenderer;

        public TrailRenderer TrailRenderer { get => trailRenderer; }

        protected override void Awake()
        {
            //下面這行是繼承Collectable的Awake方法
            base.Awake();
            //下面這行是獲取TrailRenderer組件
            trailRenderer = GetComponent<TrailRenderer>();
            //下面這行是獲取SpriteRenderer組件
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Setup(Vector2 position, GemType gemType = GemType.White1, bool spawnAnimation = true)
        {
            trailRenderer.emitting = false;
            transform.position = position;
            trailRenderer.emitting = true;
            this.gemType = gemType;
            (Sprite sprite, Color color) = gemBlueprint.gemSpritesAndColors[gemType];
            spriteRenderer.sprite = sprite;
            trailRenderer.startColor = color;
            trailRenderer.endColor = color;
            base.Setup(spawnAnimation);
        }

        protected override void OnCollected()
        {
            spriteRenderer.enabled = false;
            playerCharacter.GainExp((float)gemType);
            entityManager.DespawnGem(this);
            spriteRenderer.enabled = true;
        }
    }
}
