
using System;
using System.Collections.Generic;
using Summer;

/// <summary>
/// 用于多层嵌套的数据类
/// 
/// 为了方便一些无法用excel表格来配置的数据，或者单一数据
/// 先留坑Msm 等后续需求是否需要
/// 
/// 1.解释性文件(每一个数据的类型int string float)
/// 2.cs代码
/// 3.数据文件(.md)
/// </summary>
public class ResMd
{
    public EdNode _root_node;

    public void ParseText(string text)
    {
        _root_node = new EdNode();
        _root_node._depth = -1;
        _root_node.Name = "RootTop";
        string[] lines = text.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        _parse_text(lines);
    }

    public void _parse_text(string[] lines)
    {
        EdNode last_node = _root_node;
        for (int i = 0; i < lines.Length; i++)
        {
            string content = lines[i].Trim();
            //TODO
            if (content.Length == 0 || content.StartsWith("//"))
                continue;

            if (content.StartsWith("</") && content.EndsWith(">"))
            {
                string text = content.Substring(2, content.Length - 3);

                if (last_node != null && text == last_node.Name)
                    last_node = last_node._parent;
                else
                    LogManager.Error("[EdNode] Error,</{0}>", text);
            }
            else if (content.StartsWith("<") && content.EndsWith(">"))
            {
                string text = content.Substring(1, content.Length - 2);
                if (last_node != null)
                    last_node = last_node.AddNode(text);
            }
            else if (content.Contains("="))
            {
                string[] texts = content.Split('=');
                if (last_node != null)
                    last_node.AddAttribute(texts[0], texts[1]);
            }
            else
            {
                LogManager.Error("不符合规范的数据:{0}", content);
            }
        }
    }
}



public class EdNode
{
    public string Name { get; set; }
    public List<EdAttribute> _datas;
    public List<EdNode> _nodes;
    public EdNode _parent;
    public int _depth;

    public EdAttribute AddAttribute(string key, string text)
    {
        EdAttribute attribute = new EdAttribute
        {
            Text = text,
            Key = key
        };
        attribute.SetParent(this);
        if (_datas == null)
            _datas = new List<EdAttribute>();
        _datas.Add(attribute);
        return attribute;
    }

    public EdNode AddNode(string name)
    {
        EdNode node = new EdNode { Name = name };
        node.SetParent(this);
        if (_nodes == null)
            _nodes = new List<EdNode>();
        _nodes.Add(node);
        return node;
    }
    public void SetParent(EdNode node)
    {
        if (_parent == null)
        {
            _parent = node;
            _depth = _parent._depth + 1;
        }

        else
            LogManager.Error("父节点已经存在");
    }
    public EdNode Parent { get { return _parent; } }
    public List<EdNode> Nodes { get { return _nodes; } }
    public List<EdAttribute> Datas { get { return _datas; } }
    public EdAttribute GetAttribute(string key)
    {
        if (_datas == null) return null;
        int length = _datas.Count;
        for (int i = 0; i < length; i++)
        {
            if (_datas[i].Key == key) return _datas[i];
        }
        return null;
    }
    public EdNode GetNode(string key)
    {
        if (_nodes == null) return null;
        int length = _nodes.Count;
        for (int i = 0; i < length; i++)
        {
            if (_nodes[i].Name == key) return _nodes[i];
        }
        return null;
    }
    public List<EdNode> GetNodes(string key)
    {
        List<EdNode> tmp_node = new List<EdNode>();
        if (_nodes == null) return tmp_node;

        int length = _nodes.Count;
        for (int i = 0; i < length; i++)
        {
            if (_nodes[i].Name == key)
                tmp_node.Add(_nodes[i]);
        }
        return tmp_node;
    }
    public string GetString()
    {
        string content = string.Empty;
        string tab = string.Empty;
        for (int i = 0; i < _depth; i++)
            tab += "    ";//'\t';
        content += string.Format(tab + "<{0}>\n\r", Name);
        if (_datas != null)
        {
            for (int i = 0; i < _datas.Count; i++)
            {
                content += _datas[i].ToText();
            }
        }

        if (_nodes != null)
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                content += _nodes[i].GetString();
            }
        }
        content += string.Format(tab + "</{0}>\n\r", Name);

        return content;
    }


}

public class EdAttribute
{
    public int _depth;
    public EdNode _parent;
    public string Text { get; set; }
    public string Key { get; set; }
    public void SetParent(EdNode node)
    {
        if (_parent == null)
        {
            _parent = node;
            _depth = _parent._depth + 1;
        }

        else
            LogManager.Error("父节点已经存在");
    }
    public EdNode Parent() { return _parent; }

    public string ToText()
    {
        string tab = string.Empty;
        for (int i = 0; i < _depth; i++)
            tab += "    "; //'\t';
        string content = string.Format(tab + "{0} = {1}\n\r", Key, Text);
        return content;
    }

    public int ToInt() { return int.Parse(Text); }

    public float ToFloat() { return float.Parse(Text); }

    public string ToStr() { return Text; }

    public int[] ToInts()
    {
        string[] contents = Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        int[] ints = new int[contents.Length];
        for (int i = 0; i < contents.Length; i++)
        {
            ints[i] = int.Parse(contents[i]);
        }
        return ints;
    }

    public float[] ToFloats()
    {
        string[] contents = Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        float[] fs = new float[contents.Length];
        for (int i = 0; i < contents.Length; i++)
        {
            fs[i] = int.Parse(contents[i]);
        }
        return fs;
    }

    public string[] ToStrs()
    {
        string[] contents = Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        return contents;
    }
}



