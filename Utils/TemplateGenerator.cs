using System;
using System.Text;
using DLHAPI.Models;

namespace DLHAPI.Utils
{
    public class TemplateGenerator
    {
        public static string GetHTMLString(DlhistoryModel dLHistoryModel)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                        <head>
                        <style>
                            .movesheaderclass{
                            font-family:Arial;
                            font-size:14px;
                            font-weight: bold;
                            }
                            .generaltextclass{
                            font-family:Arial;
                            font-size:14px;
                            }
                            .tableborder{
                              border: 1px solid black;
                              border-collapse: collapse;
                            }
                            .tableborderbottom{
                              border-bottom: 1px solid black;
                              border-collapse: collapse;
                            }
                            .tablebordertop{
                                    border-top: 1px solid black;
                                    border-collapse: collapse;
                                  }
                            .tableborderright{
                              border-right: 1px solid black;
                              border-collapse: collapse;
                            }
                            .driverlicenseinfotableclass{
                              text-align: left;
                            }
                            .driverlicensehistorytableclass{
                              text-align: left;
                            }
                        </style>
                        </head>
                        <body>
                        <p class=""generaltextclass"">Alberta Registrar of Motor Vehicle Services</p>
                        <p class=""movesheaderclass"">Driver’s Licence History Report </p>
                        <p class=""generaltextclass"">Report Date: YYYY/MM/DD </p>");
            sb.AppendFormat(@"<p class=""generaltextclass"">MVID:");
                            if (dLHistoryModel != null)
                            {
                                sb.AppendFormat(@"{0}</p>", dLHistoryModel.MVID);
                            }
            sb.Append(@"<p class=""generaltextclass"">This document provides driver’s licence history as far back as available through Alberta’s electronic Motor Vehicle System (MOVES) and is not a driver record.</p>
                        <p class=""movesheaderclass"">Driver’s Licence Information</p>
                        <table style=""width:100%"" class=""generaltextclass driverlicenseinfotableclass tableborder"">
                          <tr>
                            <td class=""tableborderright"">Driver’s Licence No.</td>
                            <td class=""tableborderright"" colspan=4>Name</td> 
                            <td>Date of Birth </td>
                          </tr>");
            if (dLHistoryModel != null)
            {
                sb.AppendFormat(@"<tr class=""tableborderbottom"">
                            <td class=""tableborderright"">{0}</td>
                            <td  class=""tableborderright"" colspan=4>{1} {2} {3}</td> 
                            <td>{4}</td>
                          </tr>", dLHistoryModel.LicenseNumber, dLHistoryModel.LastName, dLHistoryModel.FirstName, dLHistoryModel.MiddleName, dLHistoryModel.Dob);
            }
            sb.Append(@"
                          <tr>
                            <td class=""tableborderright tablebordertop"">Issue Date</td>
                            <td class=""tableborderright tablebordertop"">Expiry Date</td>
                            <td class=""tableborderright tablebordertop"" colspan=4>Service Type</td>
                          </tr>");
            if (dLHistoryModel != null)
            {
                sb.AppendFormat(@"
                          <tr class=""tableborderbottom"">
                            <td class=""tableborderright"">{0}</td>
                            <td class=""tableborderright"">{1}</td>
                            <td colspan=4>{2}</td>
                          </tr>", dLHistoryModel.DateOfIssue, dLHistoryModel.DateOfExpire, dLHistoryModel.ServiceType);
            }
            sb.Append(@"
                          <tr>
                            <td class=""tableborderright tablebordertop"" colspan=4>Licence Class</td>
                            <td class=""tableborderright tablebordertop"">GDL</td>
                            <td class=""tableborderright tablebordertop"">GDL Exit Date</td>
                          </tr>");
            if (dLHistoryModel != null)
            {
                sb.AppendFormat(@"
                          <tr class=""tableborderbottom"">
                            <td  class=""tableborderright"" colspan=4>{0}</td>
                            <td class=""tableborderright"">{1}</td>
                            <td>{2}</td>
                          </tr>", dLHistoryModel.LicenseClass, dLHistoryModel.GDl, dLHistoryModel?.GDlExitDate);
            }
            sb.Append(@"
                          <tr>
                            <td class=""tableborderright tablebordertop"" colspan=6>Condition, Restrictions, Endorsements </td>
                          </tr>");
            if (dLHistoryModel != null)
            {
                sb.AppendFormat(@"
                          <tr>
                            <td colspan=6>A - Corrective Lenses </td>
                          </tr>
                          <tr>
                            <td colspan=6>C – Periodic Medical Report </td>
                          </tr>");
            }
            sb.Append(@"
                        </table>
                        <p class=""movesheaderclass"">Driver’s Licence History Information</p>");
            if (dLHistoryModel != null && dLHistoryModel?.historyInfo != null && dLHistoryModel?.historyInfo.Count > 0)
            {
                sb.Append(@"
                        <table style=""width:80%"" class=""generaltextclass driverlicensehistorytableclass tableborder"">
                          <tr class=""tableborder"">
                            <th class=""tableborder"">Date</th>
                            <th class=""tableborder"">Service Type</th> 
                            <th class=""tableborder"">Driver’s Licence Class</th>
                          </tr>");
                foreach (var item in dLHistoryModel?.historyInfo)
                {
                    sb.AppendFormat(@"<tr class=""tableborder"">
                                    <th class=""tableborder"">{0}</th>
                                    <th class=""tableborder"">{1}</th>
                                    <th class=""tableborder"">{2}</th>
                                  </tr>", item.ServiceDate, item.ServiceType, item.LicenseClass);
                }
                sb.Append(@"
                                </table>");
            }
            sb.Append(@"
                                <p class=""generaltextclass"">End of Alberta licensing history </p>
                                <p class=""generaltextclass"">Disclaimer Text</p>
                                </body>
                                </html>");
            return sb.ToString();
        }
    }
}
