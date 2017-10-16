/// <summary>
/// //（base+add+_base_num*plus）
/// </summary>
public class PropertyIntParam
{
    public int _base_num;                //
    public int _real_num;                //实际
    public int _multiply_plus;           //+1%
    public int _plus;                    //+1

    public int Value
    {
        get { return _real_num; }
        set { _real_num = value; }
    }

    public void SetBase(int base_num)
    {
        _base_num = base_num;
        _refresh_data();
    }

    public void SetPlus(int plus_num)
    {
        _plus += plus_num;
        _refresh_data();
    }

    public void SetMultiplyPlus(int multiply_plus)
    {
        _multiply_plus += multiply_plus;
        _refresh_data();
    }

    public void _refresh_data()
    {
        _real_num = _base_num + _plus + (_base_num * _multiply_plus) / 100;
    }
}