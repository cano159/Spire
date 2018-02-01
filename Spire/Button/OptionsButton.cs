using System;

namespace Spire.Button
{
    public abstract class OptionsButton : TowerFall.OptionsButton
    {
        public string Title { get; }

        protected OptionsButton(string title, OptionsButtonType type) : base(title)
        {
            Title = title.ToUpper();

            switch (type)
            {
                case OptionsButtonType.Boolean:
                    SetCallbacks(SetProperties, OnLeft, OnRight, OnConfirm);
                    break;
                case OptionsButtonType.Action:
                    SetCallbacks(SetProperties, null, null, OnConfirm);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public abstract void SetProperties();
        public abstract void OnLeft();
        public abstract void OnRight();
        public new abstract bool OnConfirm();
    }
}