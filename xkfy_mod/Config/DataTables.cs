using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace xkfy_mod.Config
{
    public class DataTables : ConfigurationSection  // 所有配置节点都要选择这个基类
    {
        private static readonly ConfigurationProperty SProperty
            = new ConfigurationProperty(string.Empty, typeof(MyKeyValueCollection), null,
                                            ConfigurationPropertyOptions.IsDefaultCollection);

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public MyKeyValueCollection KeyValues
        {
            get
            {
                return (MyKeyValueCollection)base[SProperty];
            }
        }
    }


    [ConfigurationCollection(typeof(MyKeyValueSetting))]
    public class MyKeyValueCollection : ConfigurationElementCollection      // 自定义一个集合
    {
        // 基本上，所有的方法都只要简单地调用基类的实现就可以了。

        public MyKeyValueCollection() : base(StringComparer.OrdinalIgnoreCase)  // 忽略大小写
        {
        }

        // 其实关键就是这个索引器。但它也是调用基类的实现，只是做下类型转就行了。
        new public MyKeyValueSetting this[string name]
        {
            get
            {
                return (MyKeyValueSetting)base.BaseGet(name);
            }
        }

        // 下面二个方法中抽象类中必须要实现的。
        protected override ConfigurationElement CreateNewElement()
        {
            return new MyKeyValueSetting();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MyKeyValueSetting)element).MainDtName;
        }

        // 说明：如果不需要在代码中修改集合，可以不实现Add, Clear, Remove
        public void Add(MyKeyValueSetting setting)
        {
            this.BaseAdd(setting);
        }
        public void Clear()
        {
            base.BaseClear();
        }
        public void Remove(string name)
        {
            base.BaseRemove(name);
        }
    }

    public class MyKeyValueSetting : ConfigurationElement   // 集合中的每个元素
    {
        [ConfigurationProperty("MainDtName", IsRequired = true)]
        public string MainDtName
        {
            get { return this["MainDtName"].ToString(); }
            set { this["MainDtName"] = value; }
        }

        [ConfigurationProperty("DetailDtName", IsRequired = true)]
        public string DetailDtName
        {
            get { return this["DetailDtName"].ToString(); }
            set { this["DetailDtName"] = value; }
        }

        [ConfigurationProperty("DtType", IsRequired = true)]
        public string DtType
        {
            get { return this["DtType"].ToString(); }
            set { this["DtType"] = value; }
        }

        [ConfigurationProperty("txtName", IsRequired = true)]
        public string TxtName
        {
            get { return this["txtName"].ToString(); }
            set { this["txtName"] = value; }
        }

        [ConfigurationProperty("classify", IsRequired = true)]
        public string Classify
        {
            get { return this["classify"].ToString(); }
            set { this["classify"] = value; }
        }

        [ConfigurationProperty("Notes", IsRequired = true)]
        public string Notes
        {
            get { return this["Notes"].ToString(); }
            set { this["Notes"] = value; }
        }

        [ConfigurationProperty("basicCritical", IsRequired = true)]
        public int BasicCritical
        {
            get { return Convert.ToInt32(this["basicCritical"]); }
            set { this["basicCritical"] = value; }
        }

        [ConfigurationProperty("effectCritical", IsRequired = true)]
        public int EffectCritical
        {
            get { return Convert.ToInt32(this["effectCritical"]); }
            set { this["effectCritical"] = value; }
        }

    }
}
