using Demo_Application.Controllers;
using Demo_Application.Enum;
using Demo_Application.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Application
{
    public class TestAttribute : TypeFilterAttribute
    {
        public TestAttribute(PermissionEnum.Rights[] item) : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { item };
        }

        public class AuthorizeActionFilter : IAuthorizationFilter
        {
            private readonly PermissionEnum.Rights[] _item;

            public AuthorizeActionFilter(PermissionEnum.Rights[] item)
            {
                _item = item;
            }
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                string userId = context.HttpContext.Request?.Headers["UserId"].ToString();
                
                var userList = UserList();  // Need to get this list from DB as per user
                var _right = _item[0].ToString();
                bool isUserPermission = userList.Where(w => w.Id == Convert.ToInt32(userId) && w.Right == _right).Any();
                if (!isUserPermission)
                {
                    var _res = new { status = 401, Message = "Unauthorized Access", Data = "Unauthorized Access" };
                    context.Result = new JsonResult(_res);
                    return;
                }
                
            }

            public List<Users> UserList()
            {
                List<Users> users = new List<Users>
                {
                    new Users { Id = 1, Name = "Mahesh Chand", Right ="ADD"},
                    new Users { Id = 2, Name = "Neel Beniwal" , Right ="EDIT"},
                    new Users { Id = 10, Name = "Chris Love", Right ="VIEW"},
                    new Users { Id=22, Name = "Rakesh Chand" , Right ="ADD"},
                    new Users { Id=15, Name = "Test Test", Right ="DELETE" }
                };
                return users;
            }
        }
    }



}

