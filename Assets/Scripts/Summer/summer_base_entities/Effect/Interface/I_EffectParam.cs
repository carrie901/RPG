
//=============================================================================
// Author : mashao
// CreateTime : 2018-1-11 19:46:20
// FileName : I_EffectParam.cs
//=============================================================================
namespace Summer
{
    public interface I_EffectParam
    {
        void ParseParam(EffCnf cnf);
        string GetValueText();
    }

    public interface I_EffectParamNew
    {
        void ParseParam(string text);
    }
}
