using System;
using DLHAPI.Models;

namespace DLHAPI.Data
{
	public class DLHDataStorage
	{
        public static List<DlhistoryModel> GetAllDlhistory() =>
            new List<DlhistoryModel>
            {
                new DlhistoryModel { Id = 1, MVID = "123", FirstName="Mike", MiddleName= "Jones", LastName="Turner", Dob= DateOnly.Parse("1986/12/12"), Address="101 Loons Cresent Oakville Alberta K3E 6F2", LicenseClass = "56 – Passenger Vehicles, Motorcycle", ServiceType = "Renewal", DateOfIssue= DateTime.Parse("2020/1/1"), DateOfExpire= DateTime.Parse("2030/1/1"), LicenseNumber= "123456G", GDl= true, GDlExitDate = DateTime.Parse("2023/2/2"), TransactionId ="123", historyInfo = new List<DlhistoryInfo>{
                        new DlhistoryInfo{Id = 1, ServiceDate = DateTime.Parse("2018/1/1"), ServiceType = "O-Renew", LicenseClass = "56" },
                        new DlhistoryInfo{Id = 2, ServiceDate = DateTime.Parse("2020/1/1"), ServiceType = "First", LicenseClass = "5" },
                        new DlhistoryInfo{Id = 3, ServiceDate = DateTime.Parse("2022/1/1"), ServiceType = "Second", LicenseClass = "6" }
                    }
                },
                new DlhistoryModel { Id = 2, MVID = "234", FirstName="Nike", MiddleName= "Jones", LastName="Nova", Dob= DateOnly.Parse("1980/9/9"), Address="102 Loons Avenue Oakville Alberta K3E 6F2", LicenseClass = "5 – Motorcycle", ServiceType = "First", DateOfIssue= DateTime.Parse("2020/2/2"), DateOfExpire= DateTime.Parse("2030/2/2"), LicenseNumber= "234567G", GDl= false, GDlExitDate = null, TransactionId ="234", historyInfo = new List<DlhistoryInfo>{
                        new DlhistoryInfo{Id = 1, ServiceDate = DateTime.Parse("2017/1/1"), ServiceType = "O-Renew", LicenseClass = "56" },
                        new DlhistoryInfo{Id = 2, ServiceDate = DateTime.Parse("2018/1/1"), ServiceType = "Second", LicenseClass = "7" },
                        new DlhistoryInfo{Id = 3, ServiceDate = DateTime.Parse("2020/1/1"), ServiceType = "Fourth", LicenseClass = "3" }
                    }
                },
                new DlhistoryModel { Id = 3, MVID = "345", FirstName="Naina", MiddleName= "Haider", LastName="Messi", Dob= DateOnly.Parse("1990/8/8"), Address="103 Mountain Cresent Oakville Alberta K3E 6F2", LicenseClass = "6 – 4 wheel Vehicles", ServiceType = "Second", DateOfIssue= DateTime.Parse("2020/3/3"), DateOfExpire= DateTime.Parse("2030/3/3"), LicenseNumber= "345678G", GDl= true, GDlExitDate = DateTime.Parse("2023/6/7"), TransactionId ="345", historyInfo = new List<DlhistoryInfo>{
                        new DlhistoryInfo{Id = 1, ServiceDate = DateTime.Parse("2012/1/1"), ServiceType = "O-Renew", LicenseClass = "5" },
                        new DlhistoryInfo{Id = 2, ServiceDate = DateTime.Parse("2014/1/1"), ServiceType = "First", LicenseClass = "4" },
                        new DlhistoryInfo{Id = 3, ServiceDate = DateTime.Parse("2016/1/1"), ServiceType = "Third", LicenseClass = "1" }
                    }
                },
                new DlhistoryModel { Id = 4, MVID = "456", FirstName="Victor", MiddleName= "Ravi", LastName="Shifa", Dob= DateOnly.Parse("1988/7/7"), Address="104 Berry Cresent Oakville Alberta K3E 6F2", LicenseClass = "7 – heavy duty", ServiceType = "Third", DateOfIssue= DateTime.Parse("2020/4/4"), DateOfExpire= DateTime.Parse("2030/4/4"), LicenseNumber= "456789G", GDl= false, GDlExitDate = null, TransactionId ="456", historyInfo = null
                },
                new DlhistoryModel { Id = 5, MVID = "567", FirstName="Tom", MiddleName= "Kelly", LastName="Kanta", Dob= DateOnly.Parse("1993/5/5"), Address="105 Cannes Cresent Oakville Alberta K3E 6F2", LicenseClass = "1 - learner's", ServiceType = "Fourth", DateOfIssue= DateTime.Parse("2020/5/5"), DateOfExpire= DateTime.Parse("2030/5/5"), LicenseNumber= "567890G", GDl= false, GDlExitDate = null, TransactionId ="567", historyInfo = null
                }
            };
    }
}

