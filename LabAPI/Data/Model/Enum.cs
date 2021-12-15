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
    }
   
}
