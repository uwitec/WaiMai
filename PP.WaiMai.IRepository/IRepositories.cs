﻿


 
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PP.WaiMai.Model;

namespace PP.WaiMai.IRepository
{ 

	public partial interface ICommentRepository : IBaseRepository<Comment>
    {
    }


	public partial interface IConfigRepository : IBaseRepository<Config>
    {
    }


	public partial interface IExpendLogRepository : IBaseRepository<ExpendLog>
    {
    }


	public partial interface IFeedbackRepository : IBaseRepository<Feedback>
    {
    }


	public partial interface IFoodMenuRepository : IBaseRepository<FoodMenu>
    {
    }


	public partial interface IFoodMenuCategoryRepository : IBaseRepository<FoodMenuCategory>
    {
    }


	public partial interface IOrderRepository : IBaseRepository<Order>
    {
    }


	public partial interface IRechargeRepository : IBaseRepository<Recharge>
    {
    }


	public partial interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
    }


	public partial interface ISarcasmRepository : IBaseRepository<Sarcasm>
    {
    }


	public partial interface IUserRepository : IBaseRepository<User>
    {
    }



}