﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GuptaAgroDbContext : DbContext
    {
        public GuptaAgroDbContext()
            : base("name=GuptaAgroDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_beneficiary> tbl_beneficiary { get; set; }
        public virtual DbSet<tbl_employee> tbl_employee { get; set; }
        public virtual DbSet<tbl_OandM> tbl_OandM { get; set; }
        public virtual DbSet<tbl_roles> tbl_roles { get; set; }
    
        public virtual ObjectResult<Nullable<int>> usp_complaint(string workorder, Nullable<int> beneficiaryId, string assignedTo, string aadhar, Nullable<System.DateTime> reportedon, string problemtype, string createdBy)
        {
            var workorderParameter = workorder != null ?
                new ObjectParameter("workorder", workorder) :
                new ObjectParameter("workorder", typeof(string));
    
            var beneficiaryIdParameter = beneficiaryId.HasValue ?
                new ObjectParameter("beneficiaryId", beneficiaryId) :
                new ObjectParameter("beneficiaryId", typeof(int));
    
            var assignedToParameter = assignedTo != null ?
                new ObjectParameter("assignedTo", assignedTo) :
                new ObjectParameter("assignedTo", typeof(string));
    
            var aadharParameter = aadhar != null ?
                new ObjectParameter("aadhar", aadhar) :
                new ObjectParameter("aadhar", typeof(string));
    
            var reportedonParameter = reportedon.HasValue ?
                new ObjectParameter("reportedon", reportedon) :
                new ObjectParameter("reportedon", typeof(System.DateTime));
    
            var problemtypeParameter = problemtype != null ?
                new ObjectParameter("problemtype", problemtype) :
                new ObjectParameter("problemtype", typeof(string));
    
            var createdByParameter = createdBy != null ?
                new ObjectParameter("createdBy", createdBy) :
                new ObjectParameter("createdBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("usp_complaint", workorderParameter, beneficiaryIdParameter, assignedToParameter, aadharParameter, reportedonParameter, problemtypeParameter, createdByParameter);
        }
    
        public virtual ObjectResult<string> usp_generateUserID(string username, string contactno, string role)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var contactnoParameter = contactno != null ?
                new ObjectParameter("contactno", contactno) :
                new ObjectParameter("contactno", typeof(string));
    
            var roleParameter = role != null ?
                new ObjectParameter("role", role) :
                new ObjectParameter("role", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("usp_generateUserID", usernameParameter, contactnoParameter, roleParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> usp_RegisterUser(string employeeName, string userid, string role, string contactno, string password, Nullable<System.DateTime> createddate, string createdby)
        {
            var employeeNameParameter = employeeName != null ?
                new ObjectParameter("EmployeeName", employeeName) :
                new ObjectParameter("EmployeeName", typeof(string));
    
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            var roleParameter = role != null ?
                new ObjectParameter("role", role) :
                new ObjectParameter("role", typeof(string));
    
            var contactnoParameter = contactno != null ?
                new ObjectParameter("contactno", contactno) :
                new ObjectParameter("contactno", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var createddateParameter = createddate.HasValue ?
                new ObjectParameter("createddate", createddate) :
                new ObjectParameter("createddate", typeof(System.DateTime));
    
            var createdbyParameter = createdby != null ?
                new ObjectParameter("createdby", createdby) :
                new ObjectParameter("createdby", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("usp_RegisterUser", employeeNameParameter, useridParameter, roleParameter, contactnoParameter, passwordParameter, createddateParameter, createdbyParameter);
        }
    }
}
