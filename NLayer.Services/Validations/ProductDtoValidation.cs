
using FluentValidation;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Services.Validations
{
    public class ProductDtoValidation :AbstractValidator<ProductDto>
    {
        //https://docs.fluentvalidation.net/en/latest/aspnet.html
        //https://docs.fluentvalidation.net/en/latest/built-in-validators.html

        public ProductDtoValidation()
        {
            //propertyName özel bir keyword. Fluent Validation bunu otomatik olarak name yazar ekrana
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 1'den büyük olmalıdır.");
            


        }
    }
}
