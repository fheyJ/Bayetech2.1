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
    
    public partial class T_WorkFlow_AssignBiz
    {
        public string GUID { get; set; }
        public string SourceOwnerOrgId { get; set; }
        public string SourceOwnerOrgCode { get; set; }
        public string TargetOwnerOrgId { get; set; }
        public string TargetOwnerOrgCode { get; set; }
        public string SourceOwnerUserId { get; set; }
        public string SourceOwnerUserCode { get; set; }
        public string TargetOwnerUserId { get; set; }
        public string TargetOwnerUserCode { get; set; }
        public System.DateTime AssignDate { get; set; }
        public int Type { get; set; }
        public string AssignKey { get; set; }
        public int BizMoved { get; set; }
        public int SerialNo { get; set; }
        public System.DateTime dRecordCreationDate { get; set; }
        public string sRecordCreator { get; set; }
        public string sRecordVersion { get; set; }
    }
}
