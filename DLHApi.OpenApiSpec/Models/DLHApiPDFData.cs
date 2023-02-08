/*
 * IBM MOVES DLH API
 *
 * IBM solution for MOVES
 *
 * The version of the OpenAPI document: 0.0.1
 * Contact: support@ibm.com
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Org.OpenAPITools.Converters;

namespace Org.OpenAPITools.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class DLHApiPDFData : IEquatable<DLHApiPDFData>
    {
        /// <summary>
        /// Gets or Sets Success
        /// </summary>
        [DataMember(Name="Success", EmitDefaultValue=true)]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or Sets DlhFile
        /// </summary>
        [DataMember(Name="DlhFile", EmitDefaultValue=false)]
        public System.IO.Stream DlhFile { get; set; }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [DataMember(Name="Message", EmitDefaultValue=false)]
        public string Message { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DLHApiPDFData {\n");
            sb.Append("  Success: ").Append(Success).Append("\n");
            sb.Append("  DlhFile: ").Append(DlhFile).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DLHApiPDFData)obj);
        }

        /// <summary>
        /// Returns true if DLHApiPDFData instances are equal
        /// </summary>
        /// <param name="other">Instance of DLHApiPDFData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DLHApiPDFData other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Success == other.Success ||
                    
                    Success.Equals(other.Success)
                ) && 
                (
                    DlhFile == other.DlhFile ||
                    DlhFile != null &&
                    DlhFile.Equals(other.DlhFile)
                ) && 
                (
                    Message == other.Message ||
                    Message != null &&
                    Message.Equals(other.Message)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    
                    hashCode = hashCode * 59 + Success.GetHashCode();
                    if (DlhFile != null)
                    hashCode = hashCode * 59 + DlhFile.GetHashCode();
                    if (Message != null)
                    hashCode = hashCode * 59 + Message.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(DLHApiPDFData left, DLHApiPDFData right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DLHApiPDFData left, DLHApiPDFData right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}