using UnityEngine;

public static class AnimParamChecker
{
    public static bool ContainsParam(this Animator _Anim, string _ParamName)
    {
        foreach (var param in _Anim.parameters)
            if (param.name == _ParamName)
                return true;
        return false;
    }
}