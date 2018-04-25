using UnityEngine;

namespace Summer
{
    public interface I_Transform
    {
        Vector3 pos();
        Vector3 dir();
        Vector3 localScale();

        void setoffset(Vector3 offset);

        bool setLocalScale(Vector3 localScale);
        bool attachChild(Transform tran, float scale = 1);
        Quaternion rotation();
        Transform getTransform();
        I_Transform next();
    }

    public enum E_TRANSFORM_TYPE
    {
        None = 0,
        Self, //自身的transform
        Header, //头节点
        Center, //自身的中心点
        Node, //某个节点的transform
        Node_Face,// 某个节点的transform+facedir
        Root,   //	Character配置表格中指定的root节点
        Name,	/// 用来显示血条和名字的节点
        Blade,
        WeaponBack, //武器的背部节点
    }
}
