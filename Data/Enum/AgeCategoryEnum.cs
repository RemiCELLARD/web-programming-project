using System.ComponentModel.DataAnnotations;

namespace Web_Programming_Project.Data.Enum
{
    public enum AgeCategoryEnum
    {
        [Display(Name = "0-3 Years")]
        ZeroToThree = 0,

        [Display(Name = "4-7 Years")]
        FourToSeven = 1,

        [Display(Name = "7-12 Years")]
        SevenToTwelve = 2,

        [Display(Name = "12-18 Years")]
        TwelveToEighteen = 3,

        [Display(Name = "18+ Years")]
        MoreThanEighteen = 4
    }
}
