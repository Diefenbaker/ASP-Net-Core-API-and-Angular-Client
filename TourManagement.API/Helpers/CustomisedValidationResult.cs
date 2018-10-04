using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourManagement.API.Helpers
{
    public class CustomisedValidationResult : Dictionary<string, IEnumerable<CustomisedValidationError>>
    {

        public CustomisedValidationResult(): base(StringComparer.OrdinalIgnoreCase)
        {

        }

        public CustomisedValidationResult(ModelStateDictionary modelState): this()
        {
            if(modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            foreach(var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;

                if(errors !=null && errors.Count > 0)
                {
                    var errorsToAdd = new List<CustomisedValidationError>();
                    foreach(var error in errors)
                    {
                        //split the message to get the validator key
                        var keyAndMessage = error.ErrorMessage.Split('|');

                        //if there's no validator key just return the error message,
                        //otherwise add the validatorkey
                        if (keyAndMessage.Count() > 1)
                        {
                            errorsToAdd.Add(new CustomisedValidationError(
                                    keyAndMessage[1],
                                    keyAndMessage[0]
                                ));
                        }else
                        {
                            errorsToAdd.Add(new CustomisedValidationError(
                                    keyAndMessage[0]
                                ));
                        }
                        Add(key, errorsToAdd);
                    }
                }
            }
        }

    }
}
