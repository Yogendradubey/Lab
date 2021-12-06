using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LaboratoryAPI.Data.Model
{
    public class Enum
    {
        public enum TestType
        {
            GlucoseTests,
            CompleteBloodCount,
            LipidPanel,
            Urinalysis
        }

        public enum GenderType
        {
            Male,
            Female
        }

        public enum ResultType
        {
            Negative,
            Positive
        }

        public enum ResponseMessage
        {
            [Description("Saved Successfully")]
            SavedSuccessfully = 0,
            [Description("Record Not Found")]
            RecordNotFound = 1,
            [Description("Updated Successfully")]
            UpdatedSuccessfully = 2,
            [Description("Not a valid request")]
            NotAValidRequest = 3,
            [Description("Create Successfully")]
            CreatedSuccessfully = 4,
            [Description("Deleted Successfully")]
            DelectedSuccessfully = 5,
        }
    }
   
}
