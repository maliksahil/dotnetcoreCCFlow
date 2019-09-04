using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class TodoListController : Controller
    {
        static ConcurrentBag<TodoItem> todoStore = 
            new ConcurrentBag<TodoItem>() { 
                new TodoItem(){ Owner="Someone", Title="DoSomething" },
                new TodoItem(){ Owner="Someone Else", Title="DoSomething Else" },                
                 }; 

        // GET: api/values
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            // identity of the caller.
            System.Diagnostics.Debug.WriteLine(
                User.Claims.Where(claim => (claim.Type == "appid")).FirstOrDefault().Value);
            // to get groups, use the "groups" claim, and set "groupMembershipClaims": "All" in the manifest.xml of the app
            return todoStore;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]TodoItem Todo)
        {
            todoStore.Add(new TodoItem { Owner = Todo.Owner, Title = Todo.Title });
        }
    }
}
