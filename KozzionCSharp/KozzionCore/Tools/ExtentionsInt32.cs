namespace System
{
    public static class ExtentionsInt32
    {
        public static int MostSignificantValueBitPosition(this Int32 value)
        {
            if (value == 0)
            {
                return 0;
            }

            if (0 < value)
            {
                int position = 0;
                while (value > 0)
                {
                    value = value >> 1;
                    position++;
                }
                return position;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /*[TestMethod]
        *public void test_bit()
        *{
        *    Assert.AreEqual(1, 1.MostSignificantValueBitPosition());
        *    Assert.AreEqual(2, 2.MostSignificantValueBitPosition());
        *    Assert.AreEqual(2, 3.MostSignificantValueBitPosition());
        *    Assert.AreEqual(3, 4.MostSignificantValueBitPosition());
        *}
        */
    }
        

}
