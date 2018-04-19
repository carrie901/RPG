using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public interface I_EntityAction
    {
        void OnAction(BaseEntity entity, EventSetData param);
    }


    public interface I_EntityCommnad
    {
        void OnCommand();
    }

    public class EntityMoveCommand : I_EntityCommnad
    {
        public Vector2 direction;
        public void OnCommand()
        {

        }
    }

    public class EntityAttackCommand : I_EntityCommnad
    {
        public void OnCommand()
        {

        }
    }

    public class EntitySkillCommand : I_EntityCommnad
    {
        public int skill_id;
        public void OnCommand()
        {

        }
    }
}
