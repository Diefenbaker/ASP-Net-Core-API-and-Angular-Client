﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourManagement.API.Helpers
{
    public class UnprocessableEntityObjectResult : ObjectResult
    {

        public UnprocessableEntityObjectResult(ModelStateDictionary modelState)
            : base(new CustomisedValidationResult(modelState))
        {
            if(modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }
            StatusCode = 422;
        }

    }
}
