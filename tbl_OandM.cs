//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CGSolar
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_OandM
    {
        public int OandMID { get; set; }
        public string WorkOrderID { get; set; }
        public int BeneficiaryID { get; set; }
        public string AssignedTo { get; set; }
        public string AadharNo { get; set; }
        public Nullable<System.DateTime> ProblemreportedOn { get; set; }
        public string ProblemType { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> DateOfCompletion { get; set; }
        public string Created_By { get; set; }
        public string ActualProblem { get; set; }
    
        public virtual tbl_beneficiary tbl_beneficiary { get; set; }
    }
}
