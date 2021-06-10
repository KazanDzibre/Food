using Food.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
	private readonly string _type;

	public AuthorizeAttribute(string type)
	{
		_type = type;
	}
	public void OnAuthorization(AuthorizationFilterContext context)
	{
		var user = (User)context.HttpContext.Items["User"];
		if (user == null || user.Type != _type)
		{
			//not logged in
			context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
		}
	}
}
