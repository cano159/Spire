using System;

namespace OnlineMultiplayerMod.Model
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class OverrideBase : Attribute
    {
        public string MethodToCall {get; private set;}

        public OverrideBase()
        {
            MethodToCall = "";
        }

        public OverrideBase(string methodToCall)
        {
            MethodToCall = methodToCall;
        }
    }
}