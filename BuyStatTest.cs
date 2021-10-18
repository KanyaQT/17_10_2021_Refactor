using System;
using Xunit;

namespace test3.test
{    
    public class BuyStatTest
    {
        [Fact]
        public void BuyStat_10_20_ref_points10()
        {
            int value = 10;
            int points = 20;
            int expected = 10;

            CharacterStat cs = new CharacterStat(0, "test");
            cs.BuyStat(value, ref points);

            Assert.Equal(expected, points);
        }
    }
}
