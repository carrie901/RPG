namespace Summer
{
    public class EntityPlayEffectAction : I_EntityAction
    {
        public void OnAction(BaseEntity entity, EventSetData param)
        {
            PlayEffectEventSkill data = param as PlayEffectEventSkill;
            if (data == null) return;

            PoolVfxObject vfx_go = TransformPool.Instance.Pop<PoolVfxObject>(data.effect_name);
            vfx_go.SetLifeTime(2f);
            vfx_go.BindGameobject(entity.EntityController.gameObject);
        }
    }
}
