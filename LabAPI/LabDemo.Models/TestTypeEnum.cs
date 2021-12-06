using System.ComponentModel;

namespace LabDemo.Models
{
    public enum TestTypeEnum
    {        
        [Description("Glucose tests")]   
        Glocose=1,
        [Description("Complete blood count")]
        BloodCount,
        [Description("Lipid panel")]
        LipidPanel,
        [Description("Urinalysis")]
        Urinalysis
    }
}
