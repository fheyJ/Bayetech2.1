//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bayetech.Core.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Flow_Trust
    {
        public int Trust_ID { get; set; }
        public decimal FLOW_ID { get; set; }
        public string Consigner { get; set; }
        public string Operator { get; set; }
        public System.DateTime Trust_Time { get; set; }
        public Nullable<int> isHalfConsign { get; set; }
        public int DisplayType { get; set; }
        public string Trust_Relation { get; set; }
    }
}
