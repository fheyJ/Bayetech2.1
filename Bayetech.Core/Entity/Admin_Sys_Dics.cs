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
    
    public partial class Admin_Sys_Dics
    {
        public int KeyId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public Nullable<int> Sortnum { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<bool> IsAllowModify { get; set; }
        public Nullable<bool> IsAllowDelete { get; set; }
    }
}
