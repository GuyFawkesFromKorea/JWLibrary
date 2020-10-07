﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWLibrary.ApiCore.Base;
using JWLibrary.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWLibrary.ApiCore.Controllers {
    public class AuthorizeController : JControllerBase<AuthorizeController> {
        public AuthorizeController(Microsoft.Extensions.Logging.ILogger<AuthorizeController> logger) : base(logger) {
        }

        [HttpGet]
        public string GetJWTToken(int accountId) {
            JWTTokenService jwtTokenService = new JWTTokenService();
            return jwtTokenService.GenerateJwtToken(accountId);
        }
    }
}
