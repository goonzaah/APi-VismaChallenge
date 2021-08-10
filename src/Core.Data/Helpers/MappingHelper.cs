using Core.Data.Models;
using System.Collections.Generic;

namespace Core.Data.Helpers
{
    #region Public Enums
    public enum DiscountEnum
    {
        Quantity
    }
    #endregion

    public static class MappingHelper
    {
        #region Private Fields
        private static readonly Dictionary<DiscountEnum, Discount> _discountInfo = new Dictionary<DiscountEnum, Discount>()
        {
            { DiscountEnum.Quantity, new Discount(){ Description = "Descuento por cantidad", Percentage = 30 } }
        };
        #endregion

        #region Public Properties
        public static Dictionary<DiscountEnum, Discount> DiscountInfo
        {
            get { return _discountInfo; }
        }
       
        }
        #endregion
}

