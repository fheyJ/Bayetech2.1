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
    
    public partial class T_WorkFlow_Assign
    {
        public string GUID { get; set; }
        public string SourceUserId { get; set; }
        public string SourceUserName { get; set; }
        public string SourceUserCode { get; set; }
        public string TargetUserName { get; set; }
        public string TargetUserId { get; set; }
        public string TargetUserCode { get; set; }
        public System.DateTime AssignDate { get; set; }
        public System.DateTime dRecordCreationDate { get; set; }
        public string sRecordCreator { get; set; }
        public string sRecordVersion { get; set; }
        public int type { get; set; }
        public string AssignKey { get; set; }
    }
}
