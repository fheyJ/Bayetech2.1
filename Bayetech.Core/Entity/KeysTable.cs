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
    
    public partial class KeysTable
    {
        public System.Guid Id { get; set; }
        public long SurrogateKeyId { get; set; }
        public Nullable<long> SurrogateInstanceId { get; set; }
        public Nullable<byte> EncodingOption { get; set; }
        public byte[] Properties { get; set; }
        public Nullable<bool> IsAssociated { get; set; }
    }
}
