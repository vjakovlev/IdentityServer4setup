using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiOne.Controllers
{
    [ApiController]
    public class SecretController : ControllerBase
    {
        [Route("/secret")]
        [Authorize]
        public string Index() 
        {
            var claims = User.Claims.ToList();

            return "Secret message from API one!";
        }
    }
}