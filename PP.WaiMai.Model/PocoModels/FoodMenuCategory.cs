
//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;

namespace PP.WaiMai.Model
{
	public partial class FoodMenuCategory
	{

		public FoodMenuCategory ToPOCO(bool isPOCO = true){
			return new FoodMenuCategory(){

				FoodMenuCategoryID = this.FoodMenuCategoryID,

				RestaurantID = this.RestaurantID,

				CName = this.CName,

				CreateDate = this.CreateDate,

				Creator = this.Creator,

				EditDate = this.EditDate,

				Editor = this.Editor,

				IsSale = this.IsSale,

				IsDel = this.IsDel,

				Version = this.Version,

			};
		}
	}

}
