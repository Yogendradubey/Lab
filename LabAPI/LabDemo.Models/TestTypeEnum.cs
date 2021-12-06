using System.ComponentModel;

namespace LabDemo.Models
{
    public enum TestTypeEnum
    {        
        [Description("Glucose tests")]   
        Glocose,
        [Description("Complete blood count")]
        BloodCount,
        [Description("Lipid panel")]
        LipidPanel,
        [Description("Urinalysis")]
        Urinalysis
    }
}
