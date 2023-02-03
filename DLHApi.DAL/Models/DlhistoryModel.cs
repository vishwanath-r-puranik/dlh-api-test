﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.DAL.Models
{
    public class DlhistoryModel
    {
     //   public int Id { get; set; }
        public string? MVID { get; set; }
        public string? FullName { get; set; }
        //public string? FirstName { get; set; }
        //public string? MiddleName { get; set; }
        //public string? LastName { get; set; }
        public string? Dob { get; set; }
      //  public string? Address { get; set; }
        public string? LicenseClass { get; set; }
        public string? ServiceType { get; set; }
        public string? DateOfIssue { get; set; }
        public string? DateOfExpire { get; set; }
        public string? LicenseNumber { get; set; }
        public string? GDl { get; set; }
        public string? GDlExitDate { get; set; }
     //   public string? TransactionId { get; set; }
        public string? Conditions { get; set; }
        public IList<DlhHistoryDisplay>? historyInfo { get; set; }
    }
}
