
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
	public partial class Feedback
	{

		public Feedback ToPOCO(bool isPOCO = true){
			return new Feedback(){

				FeedbackID = this.FeedbackID,

				UserID = this.UserID,

				ContentMsg = this.ContentMsg,

				CreateTime = this.CreateTime,

			};
		}
	}

}
