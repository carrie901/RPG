using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SummerEditor
{
    public class EOpenList : EComponent
    {
        public static float asset_path_w = 5 * FONT_W;
        public static float ref_count_w = 5 * FONT_W;
        public static float be_ref_count_w = 5 * FONT_W;
        public static float men_size_w = 5 * FONT_W;
        public static float fil_size_w = 5 * FONT_W;
        public static float ref_texture_w = 5 * FONT_W;
        public EButton _asset_path_btn;
        public EButton _ref_btn;
        public EButton _be_ref_btn;
        public EButton _mem_size_btn;
        public EButton _file_size_btn;
        public EButton _ref_texture_btn;

        public const float FONT_W = 20;
        public EOpenList(float width, float height) : base(width, height)
        {
            _asset_path_btn = new EButton(500, "资源名称");
            _ref_btn = new EButton(ref_count_w, "资源引用数");
            _be_ref_btn = new EButton(be_ref_count_w, "资源被引用数");
            _mem_size_btn = new EButton(men_size_w, "资源加载内存评估");
            _file_size_btn = new EButton(fil_size_w,"资源本身大小");
            _ref_texture_btn = new EButton(ref_texture_w, "资源引用贴图数量");
            InitComponent();
        }

        public void InitComponent()
        {
            show_bg = false;
            float left = 0;

            AddComponent(_asset_path_btn, left, 0);
            left += _asset_path_btn.Size.x;

            AddComponent(_ref_btn, left, 0);
            left += _ref_btn.Size.x;

            AddComponent(_be_ref_btn, left, 0);
            left += _be_ref_btn.Size.x;

            AddComponent(_mem_size_btn, left, 0);
            left += _mem_size_btn.Size.x;

            AddComponent(_file_size_btn, left, 0);
            left += _file_size_btn.Size.x;

            AddComponent(_ref_texture_btn, left, 0);
            left += _ref_texture_btn.Size.x;
        }
    }
}

