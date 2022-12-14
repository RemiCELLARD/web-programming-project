using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Web_Programming_Project.Data.Enum
{
    public enum BrickSizeEnum
    {
        [Display(Name = "1 x 1 x 1")]
        OneOneOne = 1,
        [Display(Name = "1 x 2 x 1")]
        OneTwoOne = 2,
        [Display(Name = "1 x 3 x 1")]
        OneThreeOne = 3,
        [Display(Name = "1 x 4 x 1")]
        OneFourOne = 4,
        [Display(Name = "1 x 5 x 1")]
        OneFiveOne = 5,
        [Display(Name = "1 x 6 x 1")]
        OneSixOne = 6,
        [Display(Name = "1 x 7 x 1")]
        OneSevenOne = 7,
        [Display(Name = "1 x 8 x 1")]
        OneEightOne = 8,
        [Display(Name = "1 x 9 x 1")]
        OneNineOne = 9,
        [Display(Name = "1 x 10 x 1")]
        OneTenOne = 10,
        [Display(Name = "1 x 11 x 1")]
        OneElevenOne = 11,
        [Display(Name = "1 x 12 x 1")]
        OneTwelveOne = 12,
        [Display(Name = "2 x 1 x 1")]
        TwoOneOne = 13,
        [Display(Name = "2 x 2 x 1")]
        TwoTwoOne = 14,
        [Display(Name = "2 x 3 x 1")]
        TwoThreeOne = 15,
        [Display(Name = "2 x 4 x 1")]
        TwoFourOne = 16,
        [Display(Name = "2 x 5 x 1")]
        TwoFiveOne = 17,
        [Display(Name = "2 x 6 x 1")]
        TwoSixOne = 18,
        [Display(Name = "2 x 7 x 1")]
        TwoSevenOne = 19,
        [Display(Name = "2 x 8 x 1")]
        TwoEightOne = 20,
        [Display(Name = "2 x 9 x 1")]
        TwoNineOne = 21,
        [Display(Name = "2 x 10 x 1")]
        TwoTenOne = 22,
        [Display(Name = "2 x 11 x 1")]
        TwoElevenOne = 23,
        [Display(Name = "2 x 12 x 1")]
        TwoTwelveOne = 24,
        [Display(Name = "3 x 1 x 1")]
        ThreeOneOne = 25,
        [Display(Name = "3 x 2 x 1")]
        ThreeTwoOne = 26,
        [Display(Name = "3 x 3 x 1")]
        ThreeThreeOne = 27,
        [Display(Name = "3 x 4 x 1")]
        ThreeFourOne = 28,
        [Display(Name = "3 x 5 x 1")]
        ThreeFiveOne = 29,
        [Display(Name = "3 x 6 x 1")]
        ThreeSixOne = 30,
        [Display(Name = "3 x 7 x 1")]
        ThreeSevenOne = 31,
        [Display(Name = "3 x 8 x 1")]
        ThreeEightOne = 32,
        [Display(Name = "3 x 9 x 1")]
        ThreeNineOne = 33,
        [Display(Name = "3 x 10 x 1")]
        ThreeTenOne = 34,
        [Display(Name = "3 x 11 x 1")]
        ThreeElevenOne = 35,
        [Display(Name = "3 x 12 x 1")]
        ThreeTwelveOne = 36,
        [Display(Name = "4 x 1 x 1")]
        FourOneOne = 37,
        [Display(Name = "4 x 2 x 1")]
        FourTwoOne = 38,
        [Display(Name = "4 x 3 x 1")]
        FourThreeOne = 39,
        [Display(Name = "4 x 4 x 1")]
        FourFourOne = 40,
        [Display(Name = "4 x 5 x 1")]
        FourFiveOne = 41,
        [Display(Name = "4 x 6 x 1")]
        FourSixOne = 42,
        [Display(Name = "4 x 7 x 1")]
        FourSevenOne = 43,
        [Display(Name = "4 x 8 x 1")]
        FourEightOne = 44,
        [Display(Name = "4 x 9 x 1")]
        FourNineOne = 45,
        [Display(Name = "4 x 10 x 1")]
        FourTenOne = 46,
        [Display(Name = "4 x 11 x 1")]
        FourElevenOne = 47,
        [Display(Name = "4 x 12 x 1")]
        FourTwelveOne = 48,
        [Display(Name = "1 x 1 x 0.33")]
        OneOneOneThird = 49,
        [Display(Name = "1 x 2 x 0.33")]
        OneTwoOneThird = 50,
        [Display(Name = "1 x 3 x 0.33")]
        OneThreeOneThird = 51,
        [Display(Name = "1 x 4 x 0.33")]
        OneFourOneThird = 52,
        [Display(Name = "1 x 5 x 0.33")]
        OneFiveOneThird = 53,
        [Display(Name = "1 x 6 x 0.33")]
        OneSixOneThird = 54,
        [Display(Name = "1 x 7 x 0.33")]
        OneSevenOneThird = 55,
        [Display(Name = "1 x 8 x 0.33")]
        OneEightOneThird = 56,
        [Display(Name = "1 x 9 x 0.33")]
        OneNineOneThird = 57,
        [Display(Name = "1 x 10 x 0.33")]
        OneTenOneThird = 58,
        [Display(Name = "1 x 11 x 0.33")]
        OneElevenOneThird = 59,
        [Display(Name = "1 x 12 x 0.33")]
        OneTwelveOneThird = 60,
        [Display(Name = "2 x 1 x 0.33")]
        TwoOneOneThird = 61,
        [Display(Name = "2 x 2 x 0.33")]
        TwoTwoOneThird = 62,
        [Display(Name = "2 x 3 x 0.33")]
        TwoThreeOneThird = 63,
        [Display(Name = "2 x 4 x 0.33")]
        TwoFourOneThird = 64,
        [Display(Name = "2 x 5 x 0.33")]
        TwoFiveOneThird = 65,
        [Display(Name = "2 x 6 x 0.33")]
        TwoSixOneThird = 66,
        [Display(Name = "2 x 7 x 0.33")]
        TwoSevenOneThird = 67,
        [Display(Name = "2 x 8 x 0.33")]
        TwoEightOneThird = 68,
        [Display(Name = "2 x 9 x 0.33")]
        TwoNineOneThird = 69,
        [Display(Name = "2 x 10 x 0.33")]
        TwoTenOneThird = 70,
        [Display(Name = "2 x 11 x 0.33")]
        TwoElevenOneThird = 71,
        [Display(Name = "2 x 12 x 0.33")]
        TwoTwelveOneThird = 72,
        [Display(Name = "3 x 1 x 0.33")]
        ThreeOneOneThird = 73,
        [Display(Name = "3 x 2 x 0.33")]
        ThreeTwoOneThird = 74,
        [Display(Name = "3 x 3 x 0.33")]
        ThreeThreeOneThird = 75,
        [Display(Name = "3 x 4 x 0.33")]
        ThreeFourOneThird = 76,
        [Display(Name = "3 x 5 x 0.33")]
        ThreeFiveOneThird = 77,
        [Display(Name = "3 x 6 x 0.33")]
        ThreeSixOneThird = 78,
        [Display(Name = "3 x 7 x 0.33")]
        ThreeSevenOneThird = 79,
        [Display(Name = "3 x 8 x 0.33")]
        ThreeEightOneThird = 80,
        [Display(Name = "3 x 9 x 0.33")]
        ThreeNineOneThird = 81,
        [Display(Name = "3 x 10 x 0.33")]
        ThreeTenOneThird = 82,
        [Display(Name = "3 x 11 x 0.33")]
        ThreeElevenOneThird = 83,
        [Display(Name = "3 x 12 x 0.33")]
        ThreeTwelveOneThird = 84,
        [Display(Name = "4 x 1 x 0.33")]
        FourOneOneThird = 85,
        [Display(Name = "4 x 2 x 0.33")]
        FourTwoOneThird = 86,
        [Display(Name = "4 x 3 x 0.33")]
        FourThreeOneThird = 87,
        [Display(Name = "4 x 4 x 0.33")]
        FourFourOneThird = 88,
        [Display(Name = "4 x 5 x 0.33")]
        FourFiveOneThird = 89,
        [Display(Name = "4 x 6 x 0.33")]
        FourSixOneThird = 90,
        [Display(Name = "4 x 7 x 0.33")]
        FourSevenOneThird = 91,
        [Display(Name = "4 x 8 x 0.33")]
        FourEightOneThird = 92,
        [Display(Name = "4 x 9 x 0.33")]
        FourNineOneThird = 93,
        [Display(Name = "4 x 10 x 0.33")]
        FourTenOneThird = 94,
        [Display(Name = "4 x 11 x 0.33")]
        FourElevenOneThird = 95,
        [Display(Name = "4 x 12 x 0.33")]
        FourTwelveOneThird = 96
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this BrickSizeEnum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
