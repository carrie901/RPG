﻿
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System;
using System.Collections.Generic;
using Summer;

namespace SummerEditor
{
    public class CsvPanel : EComponent
    {

        public ETextArea _des;
        public EButton _csv_to_cs_btn;
        public EButton _csv_to_byte_btn;
        public EButton _check_byte_btn;
        public EButton _check_csv_vaild;
        public CsvPanel(float width, float height) : base(width, height)
        {
            _init();
        }

        public void _init()
        {
            _des = new ETextArea(_size.x - 100, 100);
            _des.SetInfo(
                "1.csv生成Obj文件: 通过Csv生成对应的脚本,如果你修改了Table\\*.csv 中头四行的内容信息。那么你就需要重新生成cs代码\n"
                + "2.配置表有效性检测: 检测Csv表格中的数据的有效性\n");

            _csv_to_cs_btn = new EButton("Csv生成Cs代码", 120);
            _csv_to_cs_btn.OnClick += ClickCsvToCs;

            _csv_to_byte_btn = new EButton("生成二进制数据", 120);
            _csv_to_byte_btn.OnClick += ClickToByte;

            _check_byte_btn = new EButton("生成二进制数据", 120);
            _check_byte_btn.OnClick += ClickCheckByte;

            _check_csv_vaild = new EButton("配置表有效性检测", 120);
            _check_csv_vaild.OnClick += ClickCheckCsvVaild;

            _init_position();
        }

        public void _init_position()
        {
            float tmpX = 10;
            float tmpY = 10;
            //float tmp_height = 10;
            //float tmp_width = 10;
            AddComponent(_des, tmpX, tmpY);
            //tmpY += _des.Size.y + tmp_height;

            ERect anchor = _des;
            anchor = AddComponentDown(_csv_to_cs_btn, anchor);
            anchor = AddComponentRight(_csv_to_byte_btn, anchor);
            anchor = AddComponentRight(_check_byte_btn, anchor);
            anchor = AddComponentRight(_check_csv_vaild, anchor);

        }

        private void ClickCheckCsvVaild(EButton button)
        {
            TableManager.CheckVaild();
        }

        private void ClickCsvToCs(EButton button)
        {
            try
            {
                CodeGenerator.CreateCode();
                UnityEditor.EditorUtility.DisplayDialog("Csv生成cs文件成功", "", "Ok");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
                UnityEditor.EditorUtility.DisplayDialog("Csv生成cs文件失败", e.Message, "Ok");
            }
        }

        public void ClickToByte(EButton button)
        {
            StaticCnf.Clear();
            ConfigManager.ReadLocalConfig();
            ConfigManager.WriteByteConfig();
        }

        public void ClickCheckByte(EButton button)
        {
            CodeGenerator.ReadByte();
        }


    }
}