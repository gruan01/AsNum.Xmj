using System;

namespace AsNum.Xmj.Common.Interfaces {

    public interface ISettingItem {
        string Key { get; }
        //object Value { get; set; }
        //Type ValueType { get; }
        Type EditorType { get; }
        void Save();
    }

    public interface ISettingItem<T> : ISettingItem {
        T DefaultValue { get; }
        T Value { get; set; }
    }
}
