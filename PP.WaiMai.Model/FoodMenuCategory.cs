
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace PP.WaiMai.Model
{

using System;
    using System.Collections.Generic;
    
public partial class FoodMenuCategory
{

    public FoodMenuCategory()
    {

        this.FoodMenu = new HashSet<FoodMenu>();

    }


    public int FoodMenuCategoryID { get; set; }

    public int RestaurantID { get; set; }

    public string CName { get; set; }

    public System.DateTime CreateDate { get; set; }

    public string Creator { get; set; }

    public Nullable<System.DateTime> EditDate { get; set; }

    public string Editor { get; set; }

    public bool IsSale { get; set; }

    public bool IsDel { get; set; }

    public int Version { get; set; }



    public virtual ICollection<FoodMenu> FoodMenu { get; set; }

    public virtual Restaurant Restaurant { get; set; }

}

}
